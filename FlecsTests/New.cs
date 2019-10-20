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
		void New_component()
		{
			ecs.ECS_COMPONENT(_world, Position);

			var e = ecs.@new(world, Position);
			//test_assert(e != 0);
			//test_assert(ecs_has(world, e, Position));
		}
	}
}
