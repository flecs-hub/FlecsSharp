using System;
using System.Collections.Generic;
using static Flecs.Macros;


namespace Flecs.Examples
{
	public unsafe class GetChildren
	{
		static List<EntityId> EntityList = new List<EntityId>();

		static void GetChildrenSystem(ref Rows rows)
		{
			for (var i = 0; i < rows.count; i++)
				EntityList.Add(rows.Entities[i]);
		}

		static void PrintChildren(World world, string parentId)
		{
			for (var i = 0; i < EntityList.Count; i++)
				Console.WriteLine("Child found: '{0}.{1}'", parentId, ecs.get_id(world, EntityList[i]));
		}

		public static void Run(World world)
		{
			var fooType = Caches.AddComponentTypedef<int>(world, "Foo");
			var barType = Caches.AddComponentTypedef<int>(world, "Bar");

			var systemEntity = ECS_SYSTEM(world, GetChildrenSystem, SystemKind.Manual, "EcsId");

			/* Create two parents */
			var parent_1 = ecs.new_entity(world);
			var parent_2 = ecs.new_entity(world);

			/* Get type variables for parents so they can be used as filter */
			var parent_1Type = ecs.type_from_entity(world, parent_1);
			var parent_2Type = ecs.type_from_entity(world, parent_2);

			/* Create two children for each parent */
			var child_1_1 = ecs.new_child(world, parent_1, fooType);
			var child_1_2 = ecs.new_child(world, parent_1, barType);

			var child_2_1 = ecs.new_child(world, parent_2, fooType);
			var child_2_2 = ecs.new_child(world, parent_2, barType);

			/* Set ids so it's easier to see which children were resolved */
			ecs.set_id(world, child_1_1, "child_1_1");
			ecs.set_id(world, child_1_2, "child_1_2");
			ecs.set_id(world, child_2_1, "child_2_1");
			ecs.set_id(world, child_2_2, "child_2_2");

			/* Collect children for parent_1 */
			ecs.run_w_filter(world, systemEntity, 0, 0, 0, parent_1Type, IntPtr.Zero);

			PrintChildren(world, "parent_1");
			Console.WriteLine("---\n");

			EntityList.Clear();

			/* Collect children for parent_2 */
			ecs.run_w_filter(world, systemEntity, 0, 0, 0, parent_2Type, IntPtr.Zero);

			PrintChildren(world, "parent_2");

			/* Cleanup */
			EntityList.Clear();
		}
	}
}