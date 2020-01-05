using System;
using static Flecs.Macros;


namespace Flecs.Examples
{
	public class MoveSystem
	{
		public static void Run(World world)
		{
			/* Define components */
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);

			/* Define a system called Move that is executed every frame, and subscribes
			 * for the 'Position' and 'Velocity' components */
			ECS_SYSTEM(world, Move, SystemKind.OnUpdate, "Position, Velocity");

			/* Create an entity with Position and Velocity. Creating an entity with the
			     * ECS_ENTITY macro is an easy way to create entities with multiple
			     * components. Additionally, entities created with this macro can be looked
			     * up by their name ('MyEntity'). */
			var e = ECS_ENTITY(world, "MyEntity", "Position, Velocity");

			/* Initialize values for the entity */
			ecs.set(world, e, new Position {X = 0, Y = 0});
			ecs.set(world, e, new Velocity {X = 1, Y = 1});

			/* Run systems */
			ecs.progress(world, 0);
			ecs.progress(world, 0);
			ecs.progress(world, 0);
		}

		static void Move(ref Rows rows)
		{
			/* Get the two columns from the system signature */
			ECS_COLUMN<Position>(ref rows, out var p, 1);
			ECS_COLUMN<Velocity>(ref rows, out var v, 2);

			/* Iterate all the entities */
			for (var i = 0; i < rows.count; i++)
			{
				p[i].X += v[i].X;
				p[i].Y += v[i].Y;

				Console.WriteLine("{0} moved to (.x = {1}, .y = {2})",
					ecs.get_id(rows.world, rows[i]), p[i].X, p[i].Y);
			}
		}
	}
}