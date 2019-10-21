using System;
using NUnit.Framework;

namespace Flecs.Tests
{
	public class Container : AbstractTest
	{
		[Test]
		public void Container_child()
		{
			var parent = ecs.ecs_new(world, (TypeId)0);
			var child = ecs.ecs_new_child(world, parent, (TypeId)0);

			Assert.IsTrue(ecs.contains(world, parent, child));
		}

		[Test]
		public void Container_child_w_component()
		{
			var typeId = ecs.ECS_COMPONENT<Position>(world);

			var parent = ecs.ecs_new(world, (TypeId)0);
			var child = ecs.ecs_new_child(world, parent, typeId);

			Assert.IsTrue(ecs.contains(world, parent, child));
			Assert.IsTrue(ecs.ecs_has<Position>(world, child));
		}

		[Test]
		public void Container_child_w_type()
		{
			ecs.ECS_COMPONENT<Position>(world);
			var typeId = ecs.ECS_TYPE(world, "Type", "Position");

			var parent = ecs.ecs_new(world, (TypeId)0);
			var child = ecs.ecs_new_child(world, parent, typeId);

			Assert.IsTrue(ecs.contains(world, parent, child));
			Assert.IsTrue(ecs.ecs_has(world, child, typeId));
			Assert.IsTrue(ecs.ecs_has<Position>(world, child));
		}

		[Test]
		public unsafe void Container_child_w_type_w_childof()
		{
			var posTypeId = ecs.ECS_COMPONENT<Position>(world);
			var parent = ecs.ECS_ENTITY(world, "parent", "");
			var typeId = ecs.ECS_TYPE(world, "Type", "Position, CHILDOF | parent");

			var child = ecs.ecs_new_child(world, parent, typeId);
			Assert.NotZero((UInt64)child);

			var childType = ecs.get_type(world, child);
			Assert.AreNotEqual(childType, default(TypeId));
			Assert.IsTrue(ecs.vector_count(childType) == 2);

			var array = ecs.vector_first(childType);
			Assert.IsFalse(array == IntPtr.Zero);

			EntityId* eArr = (EntityId*)array.ToPointer();
			var sssssecond = eArr[1];
			Assert.IsTrue(eArr[0].Equals(ecs.type_to_entity(world, posTypeId)));

			//Assert.IsTrue(eArr[1].Equals(parent.Value | (1u << 63)));
			//test_int(array[1], parent | ECS_CHILDOF);
		}
	}
}
