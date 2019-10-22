using System;
using NUnit.Framework;


namespace Flecs.Tests
{
	[TestFixture]
	public unsafe class SystemOnFrame : AbstractTest
	{
		static void Iter(ref Rows rows)
		{
			ecs.ECS_COLUMN<Position>(ref rows, out var p, 1);

			Span<Velocity> v = null;
			Span<Mass> m = null;

			if (rows.columnCount >= 2)
				v = new Span<Velocity>(ecs.ecs_column<Velocity>(ref rows, 2), (int)rows.count);

			if (rows.columnCount >= 3)
				m = new Span<Mass>(ecs.ecs_column<Mass>(ref rows, 3), (int)rows.count);

			TestData.ProbeSystem(ref rows);

			for (var i = 0; i < rows.count; i ++)
			{
				p[i].x = 10;
				p[i].y = 20;

				if (v != null)
				{
					v[i].x = 30;
					v[i].y = 40;
				}

				if (m != null)
					m[i].mass = 50;
			}
		}

		[Test]
		public void SystemOnFrame_1_type_1_component()
		{
			var positionTypeId = ecs.ECS_COMPONENT<Position>(world);

			var e_1 = ecs.ECS_ENTITY(world, "e_1", "Position");
			var e_2 = ecs.ECS_ENTITY(world, "e_2", "Position");
			var e_3 = ecs.ECS_ENTITY(world, "e_3", "Position");

			var systemEntityId = ecs.ECS_SYSTEM(world, Iter, SystemKind.OnUpdate, "Position");

			var ctx = Heap.Alloc<SysTestData>();
			ecs.set_context(world, (IntPtr)ctx);

			ecs.progress(world, 1);

			Assert.IsTrue(ctx->count == 3);
			Assert.IsTrue(ctx->invoked == 1);
			Assert.IsTrue(ctx->system.Value == systemEntityId.Value);
			Assert.IsTrue(ctx->column_count == 1);

			Assert.IsTrue(ctx->e[0] == e_1.Value);
			Assert.IsTrue(ctx->e[1] == e_2.Value);
			Assert.IsTrue(ctx->e[2] == e_3.Value);

			var positionEntityId = ecs.type_to_entity(world, positionTypeId);
			Assert.IsTrue(positionEntityId.Value == ctx->c[0]);
		}
	}
}