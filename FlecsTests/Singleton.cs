using System;
using NUnit.Framework;
using static Flecs.Macros;


namespace Flecs.Tests
{
	[TestFixture]
	public unsafe class Singleton : AbstractTest
	{
		[Test]
		public void Singleton_set()
		{
			ECS_COMPONENT<Position>(world);

			ecs.set_singleton(world, new Position {x = 10, y = 20});

			var p = ecs.get_singleton_ptr<Position>(world);
			Assert.IsTrue(p != null);
			Assert.IsTrue(p->x == 10);
			Assert.IsTrue(p->y == 20);
		}

		[Test]
		public void Singleton_set_ptr()
		{
			ECS_COMPONENT<Position>(world);

			var p_value = new Position {x = 10, y = 20};
			ecs.set_singleton_ptr(world, ref p_value);

			var p = ecs.get_singleton_ptr<Position>(world);
			Assert.IsTrue(p != null);
			Assert.IsTrue(p->x == 10);
			Assert.IsTrue(p->y == 20);
		}

		void Iter_w_singleton(ref Rows rows)
		{
			ECS_COLUMN<Position>(ref rows, out var p, 1);
			ECS_COLUMN<Velocity>(ref rows, out var v, 2);
			Assert.IsTrue(v.Count == 0 || ecs.is_shared(ref rows, 2));

			TestData.ProbeSystem(ref rows);

			if (v.Count != 0)
			{
				for (var i = 0; i < rows.count; i++)
				{
					p[i].x += v[0].x;
					p[i].y += v[0].y;
				}
			}
		}

		[Test]
		public void Singleton_system_w_singleton()
		{
			var posType = ECS_COMPONENT<Position>(world);
			var velType = ECS_COMPONENT<Velocity>(world);
			ECS_SYSTEM(world, Iter_w_singleton, SystemKind.OnUpdate, "Position, $.Velocity");

			ecs.set_singleton(world, new Velocity {x = 1, y = 2});
			Assert.IsTrue(ecs.has(world, ecs.ECS_SINGLETON, velType));

			var pos = new Position {x = 10, y = 20};
			var e = ecs.set(world, (EntityId)0, pos);
			Assert.IsTrue(!ecs.has(world, ecs.ECS_SINGLETON, posType));

			var ctx = Heap.Alloc<SysTestData>();
			ecs.set_context(world, (IntPtr)ctx);

			ecs.progress(world, 1);

			Assert.IsTrue(ctx->count == 1);

			var p = ecs.get_ptr<Position>(world, e);
			Assert.IsTrue(p != null);
			Assert.IsTrue(p->x == 11);
			Assert.IsTrue(p->y == 22);
		}
	}
}