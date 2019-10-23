using System;
using NUnit.Framework;
using static Flecs.Macros;

namespace Flecs.Tests
{
	[TestFixture]
	public class Prefab : AbstractTest
	{
		[Test]
		public void Prefab_new_w_prefab()
		{
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);
			var (prefabEntity, prefabType) = ECS_PREFAB(world, "Prefab", "Position");

			ecs_set(world, prefabEntity, new Position {x = 10, y = 20});

			var e_1 = ecs_new_instance(world, prefabEntity, TypeId.Zero);
			Assert.NotZero((UInt64)e_1);
			Assert.IsTrue(ecs_has<Position>(world, e_1));
			Assert.IsTrue(ecs_has(world, e_1, prefabType));

			var hasPrefabType = ecs_has(world, e_1, ecs.TEcsPrefab);
			var hasId = ecs_has(world, e_1, ecs.TEcsId);
			var prefabPtr = ecs_get_ptr(world, e_1, prefabType);
			var idPtr = ecs_get_ptr(world, e_1, ecs.TEcsId);

			// These components should never be inherited from prefabs
			Assert.IsTrue(!ecs_has(world, e_1, ecs.TEcsPrefab));
			Assert.IsTrue(!ecs_has(world, e_1, ecs.TEcsId));
			Assert.IsTrue(ecs_get_ptr(world, e_1, ecs.TEcsPrefab) == IntPtr.Zero);
			Assert.IsTrue(ecs_get_ptr(world, e_1, ecs.TEcsId) == IntPtr.Zero);
		}
	}
}