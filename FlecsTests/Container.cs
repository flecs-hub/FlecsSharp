using System;
using NUnit.Framework;
using static Flecs.Macros;

namespace Flecs.Tests
{
	public class Container : AbstractTest
	{
		[Test]
		public void Container_child()
		{
			var parent = ecs.new_entity(world, TypeId.Zero);
			var child = ecs.new_child(world, parent, TypeId.Zero);

			Assert.IsTrue(ecs.contains(world, parent, child));
		}

		[Test]
		public void Container_child_w_component()
		{
			var typeId = ECS_COMPONENT<Position>(world);

			var parent = ecs.new_entity(world, TypeId.Zero);
			var child = ecs.new_child(world, parent, typeId);

			Assert.IsTrue(ecs.contains(world, parent, child));
			Assert.IsTrue(ecs.has<Position>(world, child));
		}

		[Test]
		public void Container_child_w_type()
		{
			ECS_COMPONENT<Position>(world);
			var (_, typeId) = ECS_TYPE(world, "Type", "Position");

			var parent = ecs.new_entity(world, TypeId.Zero);
			var child = ecs.new_child(world, parent, typeId);

			Assert.IsTrue(ecs.contains(world, parent, child));
			Assert.IsTrue(ecs.has(world, child, typeId));
			Assert.IsTrue(ecs.has<Position>(world, child));
		}

		[Test]
		public unsafe void Container_child_w_type_w_childof()
		{
			var posTypeId = ECS_COMPONENT<Position>(world);
			var parent = ECS_ENTITY(world, "parent", "");
			var (_, typeId) = ECS_TYPE(world, "Type", "Position, CHILDOF | parent");

			var child = ecs.new_child(world, parent, typeId);
			Assert.NotZero((UInt64)child);

			var childType = ecs.get_type(world, child);
			Assert.AreNotEqual(childType, default(TypeId));
			Assert.IsTrue(ecs.vector_count(childType.AsVector) == 2);

			var array = ecs.vector_first(childType.AsVector);
			Assert.IsFalse(array == IntPtr.Zero);

			EntityId* eArr = (EntityId*)array.ToPointer();
			Assert.IsTrue(eArr[0].Equals(ecs.type_to_entity(world, posTypeId)));
			Assert.AreEqual(eArr[1].Value,parent.Value | ecs.ECS_CHILDOF.Value);
		}
	}
}
