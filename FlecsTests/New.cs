using System;
using NUnit.Framework;

namespace Flecs.Tests
{
	[TestFixture]
	public class New : AbstractTest
	{
		public struct Position
		{
			float x, y;
		}

		[Test]
		public void New_empty()
		{
			var e = _ecs.@new(_world, (TypeId)0);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.is_empty(_world, e));
		}

		[Test]
		public void New_component()
		{
			ecs.ECS_COMPONENT(_world, typeof(Position));

			var e = ecs.ecs_new(_world, typeof(Position));
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs.ecs_has(_world, e, typeof(Position)));
		}
	}
}
