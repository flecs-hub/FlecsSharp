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
	}
}
