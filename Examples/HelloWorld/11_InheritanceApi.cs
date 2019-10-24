using System;
using Flecs;
using static Flecs.Macros;


namespace Samples
{
	public unsafe class InheritanceApi
	{
		public static void Run(World world)
		{
			/* Define components */
			ECS_COMPONENT<Position>(world);
			var forceType = Caches.AddTypedef<Position>(world, "Force");
			var massType = Caches.AddTypedef<int>(world, "Mass");

			/* Define a system called Move that is executed every frame, and subscribes
			     * for the 'Position', 'Force' and 'Mass' components. The Mass component
			     * will be either shared or owned. */
			ECS_SYSTEM(world, Move, SystemKind.OnUpdate, "Position, Force, Mass");

			/* Create two base entities */
			var heavyEntity = ecs.new_entity(world);
			ecs.set_typedef(world, heavyEntity, massType, 100);

			var lightEntity = ecs.new_entity(world);
			ecs.set_typedef(world, lightEntity, massType, 10);

			/* Create regular entity with Position, Force and Mass */
			var instance0 = ecs.new_entity(world, "Instance0");
			ecs.set(world, instance0, new Position {X = 0, Y = 0});
			ecs.set_typedef(world, instance0, forceType, new Position {X = 10, Y = 10});
			ecs.set_typedef(world, instance0, massType, 2);

			/* Create instances which share the Mass component from a base */
			var instance1 = ecs.new_instance(world, lightEntity);
			ecs.set_id(world, instance1, "Instance1");
			ecs.set(world, instance1, new Position {X = 0, Y = 0});
			ecs.set_typedef(world, instance1, forceType, new Position {X = 10, Y = 10});

			var instance2 = ecs.new_instance(world, lightEntity);
			ecs.set_id(world, instance2, "Instance2");
			ecs.set(world, instance2, new Position {X = 0, Y = 0});
			ecs.set_typedef(world, instance2, forceType, new Position {X = 10, Y = 10});

			var instance3 = ecs.new_instance(world, heavyEntity);
			ecs.set_id(world, instance3, "Instance3");
			ecs.set(world, instance3, new Position {X = 0, Y = 0});
			ecs.set_typedef(world, instance3, forceType, new Position {X = 10, Y = 10});

			var instance4 = ecs.new_instance(world, heavyEntity);
			ecs.set_id(world, instance4, "Instance4");
			ecs.set(world, instance4, new Position {X = 0, Y = 0});
			ecs.set_typedef(world, instance4, forceType, new Position {X = 10, Y = 10});

			/* Run systems */
			ecs.progress(world, 0);
			ecs.progress(world, 0);
			ecs.progress(world, 0);
		}

		static void Move(ref Rows rows)
		{
			/* Get the two columns from the system signature */
			ECS_COLUMN<Position>(ref rows, out var p, 1);
			ECS_COLUMN<Position>(ref rows, out var v, 2);
			var m = ecs.column<int>(ref rows, 3);

			/* Iterate all the entities */
			for (var i = 0; i < rows.count; i++)
			{
				if (ecs.is_shared(ref rows, 3))
				{
					p[i].X += v[i].X / m[0];
					p[i].Y += v[i].Y / m[0];
				}
				else
				{
					p[i].X += v[i].X / m[i];
					p[i].Y += v[i].Y / m[i];
				}

				Console.WriteLine("{0} moved to (.x = {1}, .y = {2})",
					ecs.get_id(rows.world, rows[i]), p[i].X, p[i].Y);
			}
		}
	}
}