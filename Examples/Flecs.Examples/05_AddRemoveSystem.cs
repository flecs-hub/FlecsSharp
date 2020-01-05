using System;
using static Flecs.Macros;

namespace Flecs.Examples
{
	public class AddRemoveSystem
	{
		public static void Run(World world)
		{
			ECS_COMPONENT<Position>(world);

			/* Register two systems that are executed when Position is added or removed to entities. */
			ECS_SYSTEM(world, AddPosition, SystemKind.OnAdd, "Position");
			ECS_SYSTEM(world, RemovePosition, SystemKind.OnRemove, "Position");

			/* Create new entity with Position. This triggers the OnAdd system. */
			var e = ecs.new_entity<Position>(world);

			/* Remove Position. This will trigger the OnRemove system. */
			ecs.remove<Position>(world, e);

			/* Add Position again. This will retrigger the OnAdd system */
			ecs.add<Position>(world, e);

			/* Add Position again. This does not retrigger the OnAdd system since the
			 * entity already has Position */
			ecs.add<Position>(world, e);
		}

		/* This system will be called when Position is added */
		static void AddPosition(ref Rows rows)
		{
			ECS_COLUMN<Position>(ref rows, out var p, 1);

			for (var i = 0; i < rows.count; i++)
			{
				p[i].X = 10;
				p[i].Y = 20;

				Console.WriteLine("Position added");
			}
		}

		/* This system will be called when Position is removed */
		static void RemovePosition(ref Rows rows)
		{
			ECS_COLUMN<Position>(ref rows, out var p, 1);

			for (var i = 0; i < rows.count; i++)
				Console.WriteLine($"Position removed: {p[i].X}, {p[i].Y}");
		}
	}
}
