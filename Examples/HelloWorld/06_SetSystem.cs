using System;
using Flecs;
using static Flecs.Macros;

namespace Samples
{
	public class SetSystem
	{
		public static void Run(World world)
		{
			ECS_COMPONENT<Position>(world);

			/* Register system that is invoked when Position is added */
			ECS_SYSTEM(world, AddPosition, SystemKind.OnAdd, "Position");

			/* Register system that is invoked when a value is assigned to Position.
				 * There are different conditions under which an OnSet system is triggerd.
				 * This example demonstrates how OnSet is called after an OnAdd system, and
				 * after calling ecs_set. */
			ECS_SYSTEM(world, SetPosition, SystemKind.OnSet, "Position");

			/* Create new entity with Position. This triggers the OnAdd system. */
			var e = ecs.new_entity<Position>(world);

			/* Set Position to a new value (invokes OnSet system) */
			ecs.set(world, e, new Position { X = 20, Y = 30 });
			ecs.set(world, e, new Position { X = 30, Y = 40 });
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
		static void SetPosition(ref Rows rows)
		{
			ECS_COLUMN<Position>(ref rows, out var p, 1);

			for (var i = 0; i < rows.count; i++)
				Console.WriteLine($"Position set: {p[i].X}, {p[i].Y}");
		}
	}
}
