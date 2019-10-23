using System;
using NUnit.Framework;
using static Flecs.Macros;

namespace Flecs.Tests
{
	[TestFixture]
	public unsafe class Prefab : AbstractTest
	{
		[Test]
		public void Prefab_new_w_prefab()
		{
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);
			var (prefabEntity, prefabType) = ECS_PREFAB(world, "Prefab", "Position");

			ecs.set(world, prefabEntity, new Position {x = 10, y = 20});

			var e_1 = ecs.new_instance(world, prefabEntity);
			Assert.NotZero((UInt64)e_1);
			Assert.IsTrue(ecs.has<Position>(world, e_1));
			Assert.IsTrue(ecs.has(world, e_1, prefabType));

			// These components should never be inherited from prefabs
			Assert.IsTrue(!ecs.has(world, e_1, ecs.TEcsPrefab));
			Assert.IsTrue(!ecs.has(world, e_1, ecs.TEcsId));
			Assert.IsTrue(ecs.get_ptr(world, e_1, ecs.TEcsPrefab) == IntPtr.Zero);
			Assert.IsTrue(ecs.get_ptr(world, e_1, ecs.TEcsId) == IntPtr.Zero);

			ecs.add<Velocity>(world, e_1);
			Assert.IsTrue(ecs.has<Position>(world, e_1));
			Assert.IsTrue(ecs.has<Velocity>(world, e_1));
			Assert.IsTrue(ecs.has(world, e_1, prefabType));
			Assert.IsTrue(!ecs.has(world, e_1, ecs.TEcsPrefab));
			Assert.IsTrue(!ecs.has(world, e_1, ecs.TEcsId));
			Assert.IsTrue(ecs.get_ptr(world, e_1, ecs.TEcsPrefab) == IntPtr.Zero);
			Assert.IsTrue(ecs.get_ptr(world, e_1, ecs.TEcsId) == IntPtr.Zero);

			var e_2 = ecs.new_instance(world, prefabEntity);
			Assert.IsTrue(ecs.has<Position>(world, e_2));
			Assert.IsTrue(ecs.has(world, e_2, prefabType));
			Assert.IsTrue(!ecs.has(world, e_2, ecs.TEcsPrefab));
			Assert.IsTrue(!ecs.has(world, e_2, ecs.TEcsId));
			Assert.IsTrue(ecs.get_ptr(world, e_2, ecs.TEcsPrefab) == IntPtr.Zero);
			Assert.IsTrue(ecs.get_ptr(world, e_2, ecs.TEcsId) == IntPtr.Zero);

			ecs.add<Velocity>(world, e_2);
			Assert.IsTrue(ecs.has<Position>(world, e_2));
			Assert.IsTrue(ecs.has<Velocity>(world, e_2));
			Assert.IsTrue(ecs.has(world, e_2, prefabType));
			Assert.IsTrue(!ecs.has(world, e_2, ecs.TEcsPrefab));
			Assert.IsTrue(!ecs.has(world, e_2, ecs.TEcsId));
			Assert.IsTrue(ecs.get_ptr(world, e_2, ecs.TEcsPrefab) == IntPtr.Zero);
			Assert.IsTrue(ecs.get_ptr(world, e_2, ecs.TEcsId) == IntPtr.Zero);

			var p_1 = ecs.get_ptr<Position>(world, e_1);
			var p_2 = ecs.get_ptr<Position>(world, e_2);
			var p_prefab = ecs.get_ptr<Position>(world, prefabEntity);

			Assert.IsTrue(p_1 != default);
			Assert.IsTrue(p_2 != default);
			Assert.IsTrue(p_prefab != default);
			Assert.IsTrue(p_1 == p_2);
			Assert.IsTrue(p_1 == p_prefab);

			Assert.IsTrue(p_1->x == 10);
			Assert.IsTrue(p_1->y == 20);

			var v_1 = ecs.get_ptr<Velocity>(world, e_1);
			var v_2 = ecs.get_ptr<Velocity>(world, e_2);

			Assert.IsTrue(v_1 != default);
			Assert.IsTrue(v_2 != default);
			Assert.IsTrue(v_1 != v_2);
		}

		void Prefab_w_field(ref Rows rows)
		{
			TestData.ProbeSystem(ref rows);

			for (int i = 0; i < (int)rows.count; i++)
			{
				var p = ecs.field<Position>(ref rows, 1, (uint)i);
				var v = ecs.field<Velocity>(ref rows, 2, (uint)i);
				p->x += v->x;
				p->y += v->y;
			}
		}

		[Test]
		public void Prefab_iterate_w_prefab_field()
		{
			var posTypeId = ECS_COMPONENT<Position>(world);
			var velTypeId = ECS_COMPONENT<Velocity>(world);
			var (prefabEntity, prefabType) = ECS_PREFAB(world, "Prefab", "Velocity");
			var typeTypeId = ECS_TYPE(world, "Type", "INSTANCEOF | Prefab, Position");
			var systemEntity = ECS_SYSTEM(world, Prefab_w_field, SystemKind.OnUpdate, "Position, Velocity");

			ecs.set(world, prefabEntity, new Velocity { x = 1, y = 2 });

			var e_1 = ecs.new_entity(world, typeTypeId);
			Assert.NotZero((UInt64)e_1);
			ecs.set(world, e_1, new Position());

			var ctx = Heap.Alloc<SysTestData>();
			ecs.set_context(world, (IntPtr)ctx);

			ecs.progress(world, 1);

			Assert.IsTrue(ctx->count == 1);
			Assert.IsTrue(ctx->invoked == 1);
			Assert.IsTrue(ctx->system.Value == systemEntity.Value);
			Assert.IsTrue(ctx->column_count == 2);

			Assert.IsTrue(ctx->e[0] == e_1.Value);
			Assert.IsTrue(ctx->GetC(0, 0) == ecs.type_to_entity(world, posTypeId).Value);
			Assert.IsTrue(ctx->GetS(0, 0) == 0);
			Assert.IsTrue(ctx->GetC(0, 1) == ecs.type_to_entity(world, velTypeId).Value);
			Assert.IsTrue(ctx->GetS(0, 1) == prefabEntity.Value);

			var p = ecs.get_ptr<Position>(world, e_1);
			Assert.IsTrue(p != null);
			Assert.IsTrue(p->x == 1);
			Assert.IsTrue(p->y == 2);
		}
	}
}