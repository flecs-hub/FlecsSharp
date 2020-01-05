using System;
using NUnit.Framework;
using static Flecs.Macros;

namespace Flecs.Tests
{
	[TestFixture]
	public class New : AbstractTest
	{
		[Test]
		public void New_empty()
		{
			var e = ecs.new_entity(world, TypeId.Zero);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.is_empty(world, e));
		}

		[Test]
		public void New_component()
		{
			ECS_COMPONENT(world, typeof(Position));

			var e = ecs.new_entity(world, typeof(Position));
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.has(world, e, typeof(Position)));
		}

		[Test]
		public void New_type()
		{
			ECS_COMPONENT(world, typeof(Position));
			var (_, typeId) = ECS_TYPE(world, "Type", "Position");

			var e = ecs.new_entity(world, typeId);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.has(world, e, typeof(Position)));
		}

		[Test]
		public void New_type_of_2()
		{
			ECS_COMPONENT(world, typeof(Position));
			ECS_COMPONENT(world, typeof(Velocity));
			var (_, typeId) = ECS_TYPE(world, "Type", "Position, Velocity");

			var e = ecs.new_entity(world, typeId);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.has(world, e, typeof(Position)));
			Assert.IsTrue(ecs.has(world, e, typeof(Velocity)));
		}

		[Test]
		public void New_type_w_type()
		{
			ECS_COMPONENT(world, typeof(Position));
			ECS_TYPE(world, "Type_1", "Position");
			var (_, typeId2) = ECS_TYPE(world, "Type_2", "Type_1");

			var e = ecs.new_entity(world, typeId2);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.has<Position>(world, e));
		}

		[Test]
		public void New_type_w_2_types()
		{
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);

			ECS_TYPE(world, "Type_1", "Position");
			ECS_TYPE(world, "Type_2", "Velocity");
			var (_, typeId3) = ECS_TYPE(world, "Type_3", "Type_1, Type_2");

			var e = ecs.new_entity(world, typeId3);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.has<Position>(world, e));
			Assert.IsTrue(ecs.has<Velocity>(world, e));
		}

		[Test]
		public void New_type_mixed()
		{
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);

			ECS_TYPE(world, "Type_1", "Position");
			var (_, typeId2) = ECS_TYPE(world, "Type_2", "Type_1, Velocity");

			var e = ecs.new_entity(world, typeId2);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.has<Position>(world, e));
			Assert.IsTrue(ecs.has<Velocity>(world, e));
		}

		[Test]
		public void New_tag()
		{
			var typeId = ECS_TAG(world, "Tag");

			var e = ecs.new_entity(world, typeId);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.has(world, e, typeId));
		}

		[Test]
		public void New_type_w_tag()
		{
			var tagTypeId = ECS_TAG(world, "Tag");
			var (_, typeId) = ECS_TYPE(world, "Type", "Tag");

			var e = ecs.new_entity(world, typeId);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.has(world, e, tagTypeId));
		}

		[Test]
		public void New_type_w_2_tags()
		{
			var tag1Type = ECS_TAG(world, "Tag_1");
			var tag2Type = ECS_TAG(world, "Tag_2");

			var (_, typeId) = ECS_TYPE(world, "Type", "Tag_1, Tag_2");

			var e = ecs.new_entity(world, typeId);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.has(world, e, tag1Type));
			Assert.IsTrue(ecs.has(world, e, tag2Type));
		}

		[Test]
		public void New_type_w_tag_mixed()
		{
			var tagTypeId = ECS_TAG(world, "Tag");
			var posTypeId = ECS_COMPONENT<Position>(world);

			var (_, typeId) = ECS_TYPE(world, "Type", "Position, Tag");

			var e = ecs.new_entity(world, typeId);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.has(world, e, tagTypeId));
			Assert.IsTrue(ecs.has(world, e, posTypeId));
		}

		[Test]
		public void New_redefine_component()
		{
			var t = ECS_COMPONENT<Position>(world);
			var t2 = ECS_COMPONENT<Position>(world);
			Assert.IsTrue(t.ptr == t2.ptr);
		}
	}
}
