using System;
using System.Collections.Generic;
using static Flecs.Macros;


namespace Flecs.Examples
{
	public unsafe class OnDemandSystem
	{
		static void Move(ref Rows rows)
		{
			ECS_COLUMN<Position>(ref rows, out var p, 1);
			ECS_COLUMN<Velocity>(ref rows, out var v, 2);

			for (var i = 0; i < rows.count; i++)
			{
				p[i].X += v[i].X;
				p[i].Y += v[i].Y;

				Console.WriteLine("Move ({0}, {1})", p[i].X, p[i].Y);
			}
		}

		static void PrintPosition(ref Rows rows)
		{
			ECS_COLUMN<Position>(ref rows, out var p, 1);

			for (int i = 0; i < rows.count; i++)
				Console.WriteLine("Print ({0}, {1})", p[i].X, p[i].Y);
		}

		public static void Run(World world)
		{
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);

			/* The 'Move' system has the 'EcsOnDemand' tag which means Flecs will only
			 * run this system if there is interest in any of its [out] columns. In this
			 * case the system will only be ran if there is interest in Position. */
			ECS_SYSTEM(world, Move, SystemKind.OnUpdate, "[out] Position, Velocity, SYSTEM.EcsOnDemand");

			/* The 'PrintPosition' is a regular system with an [in] column. This signals
			 * that the system will not write Position, and relies on another system to
			 * provide a value for it. If there are any OnDemand systems that provide
			 * 'Position' as an output, they will be enabled. */
			var printSystemEntity = ECS_SYSTEM(world, PrintPosition, SystemKind.OnUpdate, "[in] Position");

			/* Create entity, set components */
			var e = ecs.set(world, EntityId.Zero, new Position { X = 0, Y = 0 });
			ecs.set(world, e, new Velocity { X = 1, Y = 2 });

			/* If this line is uncommented, the PrintPosition system will be disabled.
			 * As a result there will be no more enabled systems with interest in the 
			 * 'Position' component, and therefore the 'Move' system will be disabled
			 * as a result as well. */

			//ecs.enable(world, printSystemEntity, false);

			ecs.progress(world, 0);
			ecs.progress(world, 0);
			ecs.progress(world, 0);
		}
	}
}