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
			var components = new EntityId[] { ecs.type_to_entity(world, posTypeId) };
			var positions = new Position[] {new Position {x = 10, y = 20}, new Position {x = 11, y = 21}, new Position {x = 12, y = 22}};

			fixed (void* positionsPtr = positions)
			{
				var columns = new[] { (IntPtr)positionsPtr };
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

			for (var i = 0; i < 3; i ++)
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

			var components = new EntityId[] { ecs.type_to_entity(world, posTypeId) };
			var positions = new Position[] {new Position {x = 10, y = 20}, new Position {x = 11, y = 21}, new Position {x = 12, y = 22}};

			var e = ecs.set_w_data<Position>(world, 1, 3, null, positions);

			Assert.IsTrue((UInt64)e != 0);
			Assert.IsTrue(ecs.count(world, posTypeId) == 3);

			for (var i = 0; i < 3; i ++)
			{
				var entity = (EntityId)(e.Value + (UInt64)i);
				Assert.IsTrue(ecs.has(world, entity, posTypeId));

				var p = ecs.get_ptr<Position>(world, entity);
				Assert.IsTrue(p != null);
				Assert.IsTrue(p->x == 10 + i);
				Assert.IsTrue(p->y == 20 + i);
			}
		}
	}
}