using System;
using NUnit.Framework;
using static Flecs.Macros;


namespace Flecs.Tests
{
	[TestFixture]
	public unsafe class Set_w_data : AbstractTest
	{
		[Test]
		public void Set_w_data_1_column_3_rows()
		{
			var posTypeId = ECS_COMPONENT<Position>(world);

			EntityId e;
			var components = new[] {ecs.type_to_entity(world, posTypeId)};
			var positions = new[] {new Position {x = 10, y = 20}, new Position {x = 11, y = 21}, new Position {x = 12, y = 22}};

			fixed (void* positionsPtr = positions)
			{
				var columns = new[] {(IntPtr)positionsPtr};
				fixed (void* columnsPtr = columns)
				{
					fixed (EntityId* componentsPtr = components)
					{
						var tableData = new TableData
						{
							columnCount = 1,
							rowCount = 3,
							entities = null,
							components = componentsPtr,
							columns = columnsPtr
						};

						e = ecs.set_w_data(world, ref tableData);
					}
				}
			}

			Assert.IsTrue((UInt64)e != 0);
			Assert.IsTrue(ecs.count(world, posTypeId) == 3);

			for (var i = 0; i < 3; i++)
			{
				var entity = (EntityId)(e.Value + (UInt64)i);
				Assert.IsTrue(ecs.has(world, entity, posTypeId));

				var p = ecs.get_ptr<Position>(world, entity);
				Assert.IsTrue(p != null);
				Assert.IsTrue(p->x == 10 + i);
				Assert.IsTrue(p->y == 20 + i);
			}
		}

		[Test]
		public void Set_w_data_1_column_3_rows_stackalloc()
		{
			var posTypeId = ECS_COMPONENT<Position>(world);

			EntityId* components = stackalloc[] {ecs.type_to_entity(world, posTypeId)};
			void* positions = stackalloc[] {new Position {x = 10, y = 20}, new Position {x = 11, y = 21}, new Position {x = 12, y = 22}};
			var columns = stackalloc[] {(IntPtr)positions};

			var tableData = new TableData
			{
				columnCount = 1,
				rowCount = 3,
				entities = null,
				components = components,
				columns = columns
			};

			var e = ecs.set_w_data(world, ref tableData);

			Assert.IsTrue((UInt64)e != 0);
			Assert.IsTrue(ecs.count(world, posTypeId) == 3);

			for (var i = 0; i < 3; i++)
			{
				var entity = (EntityId)(e.Value + (UInt64)i);
				Assert.IsTrue(ecs.has(world, entity, posTypeId));

				var p = ecs.get_ptr<Position>(world, entity);
				Assert.IsTrue(p != null);
				Assert.IsTrue(p->x == 10 + i);
				Assert.IsTrue(p->y == 20 + i);
			}
		}

		[Test]
		public void Set_w_data_1_column_3_rows_macro()
		{
			var posTypeId = ECS_COMPONENT<Position>(world);

			var positions = new[] {new Position {x = 10, y = 20}, new Position {x = 11, y = 21}, new Position {x = 12, y = 22}};

			var e = ecs.set_w_data(world, 3, null, positions);

			Assert.IsTrue((UInt64)e != 0);
			Assert.IsTrue(ecs.count(world, posTypeId) == 3);

			for (var i = 0; i < 3; i++)
			{
				var entity = (EntityId)(e.Value + (UInt64)i);
				Assert.IsTrue(ecs.has(world, entity, posTypeId));

				var p = ecs.get_ptr<Position>(world, entity);
				Assert.IsTrue(p != null);
				Assert.IsTrue(p->x == 10 + i);
				Assert.IsTrue(p->y == 20 + i);
			}
		}

		[Test]
		public void Set_w_data_2_columns_3_rows()
		{
			var posTypeId = ECS_COMPONENT<Position>(world);
			var velTypeId = ECS_COMPONENT<Velocity>(world);
			var (_, typeId) = ECS_TYPE(world, "Type", "Position, Velocity");

			var positions = new[] {new Position {x = 10, y = 20}, new Position {x = 11, y = 21}, new Position {x = 12, y = 22}};
			var velocities = new[] {new Velocity {x = 30, y = 40}, new Velocity {x = 31, y = 41}, new Velocity {x = 32, y = 42}};

			var e = ecs.set_w_data(world, 3, null, positions, velocities);

			Assert.IsTrue((UInt64)e != 0);
			Assert.IsTrue(ecs.count(world, typeId) == 3);

			for (var i = 0; i < 3; i++)
			{
				var entity = (EntityId)(e.Value + (UInt64)i);
				Assert.IsTrue(ecs.has(world, entity, posTypeId));
				Assert.IsTrue(ecs.has(world, entity, velTypeId));

				var p = ecs.get_ptr<Position>(world, entity);
				Assert.IsTrue(p != null);
				Assert.IsTrue(p->x == 10 + i);
				Assert.IsTrue(p->y == 20 + i);

				var v = ecs.get_ptr<Velocity>(world, entity);
				Assert.IsTrue(v != null);
				Assert.IsTrue(v->x == 30 + i);
				Assert.IsTrue(v->y == 40 + i);
			}
		}

		[Test]
		public void Set_w_data_1_column_3_rows_w_entities()
		{
			var posTypeId = ECS_COMPONENT<Position>(world);

			var entities = new[] {(EntityId)5000, (EntityId)5001, (EntityId)5002};
			var positions = new[] {new Position {x = 10, y = 20}, new Position {x = 11, y = 21}, new Position {x = 12, y = 22}};

			var e = ecs.set_w_data(world, 3, entities, positions);

			Assert.IsTrue((UInt64)e != 0);
			Assert.IsTrue(ecs.count(world, posTypeId) == 3);

			for (var i = 0; i < 3; i++)
			{
				var entity = (EntityId)(5000 + (UInt64)i);
				Assert.IsTrue(ecs.has(world, entity, posTypeId));

				var p = ecs.get_ptr<Position>(world, entity);
				Assert.IsTrue(p != null);
				Assert.IsTrue(p->x == 10 + i);
				Assert.IsTrue(p->y == 20 + i);
			}
		}

		[Test]
		public void Set_w_data_2_columns_3_rows_no_data()
		{
			var posTypeId = ECS_COMPONENT<Position>(world);
			var velTypeId = ECS_COMPONENT<Velocity>(world);
			var (_, typeId) = ECS_TYPE(world, "Type", "Position, Velocity");
			var entities = new[] {(EntityId)5000, (EntityId)5001, (EntityId)5002};

			var e = ecs.set_w_data<Position, Velocity>(world, 3, entities, null, null);

			Assert.IsTrue((UInt64)e != 0);
			Assert.IsTrue(ecs.count(world, typeId) == 3);

			for (var i = 0; i < 3; i ++)
			{
				var entity = (EntityId)(5000 + (UInt64)i);
				Assert.IsTrue(ecs.has(world, entity, posTypeId));
				Assert.IsTrue(ecs.has(world, entity, velTypeId));
			}
		}
	}
}