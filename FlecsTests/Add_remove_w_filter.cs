using NUnit.Framework;
using static Flecs.Macros;


namespace Flecs.Tests
{
	[TestFixture]
	public class Add_remove_w_filter : AbstractTest
	{
		[Test]
		public void Add_remove_w_filter_remove_1_no_filter()
		{
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);
			var massType = ECS_COMPONENT<Mass>(world);

			var type1 = ECS_TYPE(world, "Type_1", "Position, Velocity");
			var type2 = ECS_TYPE(world, "Type_2", "Position, Velocity, Mass");

			var e_1 = ecs.new_w_count(world, type1, 3);
			var e_2 = ecs.new_w_count(world, type2, 3);

			// Type_1 is superset of Type_2
			Assert.IsTrue(ecs.count(world, type1) == 6);
			Assert.IsTrue(ecs.count(world, type2) == 3);

			// Remove component Mass
			var filter = new TypeFilter();
			ecs.add_remove_w_filter(world, TypeId.Zero, massType, ref filter);

			// ecs_count tests if the number of entities in the tables is correct
			Assert.IsTrue(ecs.count(world, type1) == 6);
			Assert.IsTrue(ecs.count(world, type2) == 0);

			// ecs_get_type tests if the entity index is properly updated
			Assert.IsTrue(ecs.get_type(world, e_1) == type1);
			Assert.IsTrue(ecs.get_type(world, (EntityId)(e_1.Value + 1)) == type1);
			Assert.IsTrue(ecs.get_type(world, (EntityId)(e_1.Value + 2)) == type1);

			Assert.IsTrue(ecs.get_type(world, e_2) == type1);
			Assert.IsTrue(ecs.get_type(world, (EntityId)(e_2.Value + 1)) == type1);
			Assert.IsTrue(ecs.get_type(world, (EntityId)(e_2.Value + 2)) == type1);
		}

		[Test]
		public void Add_remove_w_filter_remove_1_include_1()
		{
			var posType = ECS_COMPONENT<Position>(world);
			var velType = ECS_COMPONENT<Velocity>(world);
			var massType = ECS_COMPONENT<Mass>(world);

			var type1 = ECS_TYPE(world, "Type_1", "Position, Velocity");
			var type2 = ECS_TYPE(world, "Type_2", "Position, Velocity, Mass");
			var type3 = ECS_TYPE(world, "Type_3", "Position, Mass");

			var e_1 = ecs.new_w_count(world, type1, 3);
			var e_2 = ecs.new_w_count(world, type2, 3);
			var e_3 = ecs.new_w_count(world, type3, 3);

			// Type_1 is superset of Type_2
			Assert.IsTrue(ecs.count(world, type1) == 6);
			Assert.IsTrue(ecs.count(world, type2) == 3);
			Assert.IsTrue(ecs.count(world, type3) == 6);

			// Remove component Mass
			var filter = new TypeFilter
			{
				include = velType
			};
			ecs.add_remove_w_filter(world, TypeId.Zero, massType, ref filter);

			// ecs_count tests if the number of entities in the tables is correct
			Assert.IsTrue(ecs.count(world, type1) == 6);
			Assert.IsTrue(ecs.count(world, type2) == 0);
			Assert.IsTrue(ecs.count(world, type3) == 3);
			Assert.IsTrue(ecs.count(world, posType) == 9);

			// ecs_get_type tests if the entity index is properly updated
			Assert.IsTrue(ecs.get_type(world, e_1) == type1);
			Assert.IsTrue(ecs.get_type(world, (EntityId)(e_1.Value + 1)) == type1);
			Assert.IsTrue(ecs.get_type(world, (EntityId)(e_1.Value + 2)) == type1);

			Assert.IsTrue(ecs.get_type(world, e_2) == type1);
			Assert.IsTrue(ecs.get_type(world, (EntityId)(e_2.Value + 1)) == type1);
			Assert.IsTrue(ecs.get_type(world, (EntityId)(e_2.Value + 2)) == type1);

			Assert.IsTrue(ecs.get_type(world, e_3) == type3);
			Assert.IsTrue(ecs.get_type(world, (EntityId)(e_3.Value + 1)) == type3);
			Assert.IsTrue(ecs.get_type(world, (EntityId)(e_3.Value + 2)) == type3);
		}
	}
}