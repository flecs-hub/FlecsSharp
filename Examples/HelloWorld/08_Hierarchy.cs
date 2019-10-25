using System;
using Flecs;
using static Flecs.Macros;


namespace Samples
{
	public unsafe class Hierarchy
	{
		public static void Run(World world)
		{
			/* Define components */
			Caches.AddComponentTypedef<Position>(world, "WorldPosition");
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);

			/* Move entities with Position and Velocity */
			ECS_SYSTEM(world, Move, SystemKind.OnUpdate, "Position, Velocity");

			/* Transform local coordinates to world coordinates. A CASCADE column
			     * guarantees that entities are evaluated breadth-first, according to the
			     * hierarchy. This system will depth-sort based on parents that have the
			     * WorldPosition component. */
			ECS_SYSTEM(world, Transform, SystemKind.OnUpdate, "CASCADE.WorldPosition, WorldPosition, Position");

			/* Create root of the hierarchy which moves around */
			var e = ECS_ENTITY(world, "Root", "WorldPosition, Position, Velocity");
			ecs.set(world, e, new Position {X = 0, Y = 0});
			ecs.set(world, e, new Velocity {X = 1, Y = 2});

			/* Create children that don't move and are relative to the parent */
			var child1 = ECS_ENTITY(world, "Child1", "WorldPosition, Position, CHILDOF | Root");
			ecs.set(world, child1, new Position {X = 100, Y = 100});
			{
				var gChild1 = ECS_ENTITY(world, "GChild1", "WorldPosition, Position, CHILDOF | Child1");
				ecs.set(world, gChild1, new Position {X = 1000, Y = 1000});
			}

			var child2 = ECS_ENTITY(world, "Child2", "WorldPosition, Position, CHILDOF | Root");
			ecs.set(world, child2, new Position {X = 200, Y = 200});
			{
				var gChild2 = ECS_ENTITY(world, "GChild2", "WorldPosition, Position, CHILDOF | Child2");
				ecs.set(world, gChild2, new Position {X = 2000, Y = 2000});
			}


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

		/* Implement a system that transforms world coordinates hierarchically. If the
		 * CASCADE column is set, it points to the world coordinate of the parent. This
		 * will be used to then transform Position to WorldPosition of the child.
		 * If the CASCADE column is not set, the system matched a root. In that case,
		 * just assign the Position to the WorldPosition. */
		static void Transform(ref Rows rows)
		{
			/* Get the three columns from the system signature. notice that we use ecs.column directly here and work
			 * with the raw pointer. This lets us null check it to test for the column presence. If we used ECS_COLUMN,
			 * we would always get back a Set<T> and not know if it was just empty (count == 0) or not present at all
			 * without checking rows.count against span.length. */
			var parent_wp = ecs.column<Position>(ref rows, 1);
			ECS_COLUMN<Position>(ref rows, out var wp, 2);
			ECS_COLUMN<Position>(ref rows, out var p, 3);

			if (parent_wp == null)
			{
				for (var i = 0; i < rows.count; i++)
				{
					wp[i].X = p[i].X;
					wp[i].Y = p[i].Y;

					/* Print something to the console so we can see the system is being invoked */
					Console.WriteLine("{0} transformed to (.x = {1}, .y = {2}) <<root>>",
						ecs.get_id(rows.world, rows[i]), wp[i].X, wp[i].Y);
				}
			}
			else
			{
				for (var i = 0; i < rows.count; i++)
				{
					wp[i].X = parent_wp->X + p[i].X;
					wp[i].Y = parent_wp->Y + p[i].Y;

					/* Print something to the console so we can see the system is being invoked */
					Console.WriteLine("{0} transformed to (.x = {1}, .y = {2}) <<child>>",
						ecs.get_id(rows.world, rows[i]), wp[i].X, wp[i].Y);
				}
			}
		}
	}
}