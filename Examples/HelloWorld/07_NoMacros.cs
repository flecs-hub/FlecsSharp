using System;
using Flecs;
using static Flecs.Macros;

namespace Samples
{
	public unsafe class NoMacros
	{
		public static void Run(World world)
		{
			/* Define components */
			var posEntity = ecs.new_component(world, Caches.AddUnmanagedString("Position"), Heap.SizeOf<Position>());
			var velEntity = ecs.new_component(world, Caches.AddUnmanagedString("Velocity"), Heap.SizeOf<Velocity>());

			/* Obtain variables to component types */
			var posType = ecs.type_from_entity(world, posEntity);
			var velType = ecs.type_from_entity(world, velEntity);

			/* Register system */
			ecs.new_system(world, Caches.AddUnmanagedString("Move"), SystemKind.OnUpdate, "Position, Velocity", Move);
			ECS_SYSTEM(world, Move, SystemKind.OnUpdate, "Position, Velocity");

			/* Create an entity */
			var e = ecs.new_entity(world);

			/* Set entity identifier using builtin component */
			ecs.set_ptr(world, e, ecs.TEcsId, (void*)Caches.AddUnmanagedString("MyEntity").Ptr());

			/* Set values for entity. */
			ecs.set(world, e, new Position { X = 0, Y = 0 });
			ecs.set(world, e, new Velocity { X = 1, Y = 1 });

			/* Run systems */
			ecs.progress(world, 0);
			ecs.progress(world, 0);
			ecs.progress(world, 0);
		}

		static void Move(ref Rows rows)
		{
			/* Get the two columns from the system signature */
			var p = ecs.column<Position>(ref rows, 1);
			var v = ecs.column<Velocity>(ref rows, 2);

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
