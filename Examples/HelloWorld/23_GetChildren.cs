using System;
using Flecs;
using static Flecs.Macros;


namespace Samples
{
	public unsafe class GetChildren
	{
		static VectorParams entity_params = new VectorParams {elementSize = (uint)Heap.SizeOf<EntityId>()};

		static void GetChildrenSystem(ref Rows rows)
		{
			var children = new Vector(rows.param);

			for (var i = 0; i < rows.count; i++)
			{
				var elem = ecs.vector_add(ref children, ref entity_params);
				UInt64* elemPtr = (UInt64*)elem.ToPointer();
				*elemPtr = rows.Entities[i].Value;
			}
		}

		static void PrintChildren(World world, string parentId, ref Vector children)
		{
			var entities = children.ToSpan<EntityId>();

			for (var i = 0; i < entities.Length; i++)
				Console.WriteLine("Child found: '{0}.{1}'", parentId, ecs.get_id(world, entities[i]));
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

			/* Create vector to store child entities */
			// TODO: we have to allocate all the vector elements we need because we lose the pointer when it realloced in Flecs
			var children = ecs.vector_new(ref entity_params, 10);

			/* Collect children for parent_1 */
			ecs.run_w_filter(world, systemEntity, 0, 0, 0, parent_1Type, children.ptr);

			PrintChildren(world, "parent_1", ref children);
			Console.WriteLine("---\n");

			ecs.vector_clear(children);

			var cnt = ecs.vector_count(children);

			/* Collect children for parent_2 */
			ecs.run_w_filter(world, systemEntity, 0, 0, 0, parent_2Type, children.ptr);

			PrintChildren(world, "parent_2", ref children);

			/* Cleanup */
			ecs.vector_free(children);
		}
	}
}