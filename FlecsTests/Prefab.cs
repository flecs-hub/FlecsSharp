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
	}
}