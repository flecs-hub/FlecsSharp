using NUnit.Framework;
using static Flecs.Macros;

namespace Flecs.Tests
{
	[TestFixture]
	public class Clone : AbstractTest
	{
		[Test]
		public void Clone_empty_w_value()
		{
			var e_1 = ecs.new_entity(world);
			Assert.IsTrue(e_1.Value != 0);

			var e_2 = ecs.clone(world, e_1, true);
			Assert.IsTrue(e_2.Value != 0);
			Assert.IsTrue(e_1 != e_2);

			Assert.IsTrue(ecs.is_empty(world, e_1));
			Assert.IsTrue(ecs.is_empty(world, e_2));
		}

		[Test]
		public void Clone_1_component()
		{
			var posType = ECS_COMPONENT<Position>(world);

			var e_1 = ecs.new_entity(world, posType);
			Assert.IsTrue(e_1.Value != 0);

			var e_2 = ecs.clone(world, e_1, false);
			Assert.IsTrue(e_2.Value != 0);
			Assert.IsTrue(e_1 != e_2);

			Assert.IsTrue(ecs.has(world, e_1, posType));
			Assert.IsTrue(ecs.has(world, e_2, posType));
			Assert.IsTrue(ecs.get_ptr(world, e_1, posType) != ecs.get_ptr(world, e_2, posType));
		}
	}
}