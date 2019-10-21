using System;
using NUnit.Framework;

namespace Flecs.Tests
{
	[TestFixture]
	public class New : AbstractTest
	{
		[Test]
		public void New_empty()
		{
			var e = ecs.ecs_new(world, (TypeId)0);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.is_empty(world, e));
		}

		[Test]
		public void New_component()
		{
			ecs.ECS_COMPONENT(world, typeof(Position));

			var e = ecs.ecs_new(world, typeof(Position));
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.ecs_has(world, e, typeof(Position)));
		}

		[Test]
		public void New_type()
		{
			ecs.ECS_COMPONENT(world, typeof(Position));
			var typeId = ecs.ECS_TYPE(world, "Type", "Position");

			var e = ecs.ecs_new(world, typeId);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.ecs_has(world, e, typeof(Position)));
		}

		[Test]
		public void New_type_of_2()
		{
			ecs.ECS_COMPONENT(world, typeof(Position));
			ecs.ECS_COMPONENT(world, typeof(Velocity));
			var typeId = ecs.ECS_TYPE(world, "Type", "Position, Velocity");

			var e = ecs.ecs_new(world, typeId);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.ecs_has(world, e, typeof(Position)));
			Assert.IsTrue(ecs.ecs_has(world, e, typeof(Velocity)));
		}

		[Test]
		public void New_type_w_type()
		{
			ecs.ECS_COMPONENT(world, typeof(Position));
			ecs.ECS_TYPE(world, "Type_1", "Position");
			var typeId2 = ecs.ECS_TYPE(world, "Type_2", "Type_1");

			var e = ecs.ecs_new(world, typeId2);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.ecs_has<Position>(world, e));
		}

		[Test]
		public void New_type_w_2_types()
		{
			ecs.ECS_COMPONENT<Position>(world);
			ecs.ECS_COMPONENT<Velocity>(world);

			ecs.ECS_TYPE(world, "Type_1", "Position");
			ecs.ECS_TYPE(world, "Type_2", "Velocity");
			var typeId3 = ecs.ECS_TYPE(world, "Type_3", "Type_1, Type_2");

			var e = ecs.ecs_new(world, typeId3);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.ecs_has<Position>(world, e));
			Assert.IsTrue(ecs.ecs_has<Velocity>(world, e));
		}

		[Test]
		public void New_type_mixed()
		{
			ecs.ECS_COMPONENT<Position>(world);
			ecs.ECS_COMPONENT<Velocity>(world);

			ecs.ECS_TYPE(world, "Type_1", "Position");
			var typeId2 = ecs.ECS_TYPE(world, "Type_2", "Type_1, Velocity");

			var e = ecs.ecs_new(world, typeId2);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.ecs_has<Position>(world, e));
			Assert.IsTrue(ecs.ecs_has<Velocity>(world, e));
		}

		[Test]
		public void New_tag()
		{
			var typeId = ecs.ECS_TAG(world, "Tag");

			var e = ecs.ecs_new(world, typeId);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.ecs_has(world, e, typeId));
		}

		[Test]
		public void New_type_w_tag()
		{
			var tagTypeId = ecs.ECS_TAG(world, "Tag");
			var typeId = ecs.ECS_TYPE(world, "Type", "Tag");

			var e = ecs.ecs_new(world, typeId);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.ecs_has(world, e, tagTypeId));
		}

		[Test]
		public void New_type_w_2_tags()
		{
			var tag1Type = ecs.ECS_TAG(world, "Tag_1");
			var tag2Type = ecs.ECS_TAG(world, "Tag_2");

			var typeId = ecs.ECS_TYPE(world, "Type", "Tag_1, Tag_2");

			var e = ecs.ecs_new(world, typeId);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.ecs_has(world, e, tag1Type));
			Assert.IsTrue(ecs.ecs_has(world, e, tag2Type));
		}

		[Test]
		public void New_type_w_tag_mixed()
		{
			var tagTypeId = ecs.ECS_TAG(world, "Tag");
			var posTypeId = ecs.ECS_COMPONENT<Position>(world);

			var typeId = ecs.ECS_TYPE(world, "Type", "Position, Tag");

			var e = ecs.ecs_new(world, typeId);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.ecs_has(world, e, tagTypeId));
			Assert.IsTrue(ecs.ecs_has(world, e, posTypeId));
		}

		[Test]
		public void New_redefine_component()
		{
			var t = ecs.ECS_COMPONENT<Position>(world);
			var t2 = ecs.ECS_COMPONENT<Position>(world);
			Assert.IsTrue(t.ptr == t2.ptr);
		}
	}
}
