using System;
using System.Diagnostics;
using static Flecs.Macros;

namespace Flecs.Examples
{
	public unsafe class Dump
	{
		public static void Run(World world)
		{
			var moveSys = ECS_SYSTEM<Position, Speed>(world, MoveSystem, SystemKind.Manual);
			ECS_SYSTEM<Position>(world, PositionSystem, SystemKind.OnUpdate);
			var moveRawSys = ECS_SYSTEM(world, MoveSystemRaw, SystemKind.Manual, "Position, Speed");
			var moveSpanSys = ECS_SYSTEM(world, MoveSystemSpan, SystemKind.Manual, "Position, Speed");

			ecs.new_entity(world, new Position { X = 1, Y = 2 }, new Speed { SpeedValue = 5 });
			ecs.new_entity(world, new Position { X = 1, Y = 2 }, new Speed { SpeedValue = 5 });
			ecs.new_entity(world, new Position { X = 1, Y = 2 }, new Speed { SpeedValue = 5 });

			var e = ecs.new_entity(world);
			ecs.set(world, e, new Position { X = 14, Y = 2 });
			e = ecs.new_entity(world);
			ecs.set(world, e, new Position { X = 13, Y = 2 });

			ecs.new_entity<Position, Speed>(world);
			ecs.new_entity<Position, Speed>(world);

			ecs.new_entity(world, new Position { X = 75, Y = 23 }, new Speed { SpeedValue = 66 });

			var watch = System.Diagnostics.Stopwatch.StartNew();
			for (var j = 0; j < 100000; j++)
				ecs.new_entity(world, new Position { X = 75, Y = 23 }, new Speed { SpeedValue = 66 });
			Console.WriteLine($"-- add one-by-one: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");

			// bulk add with a template
			watch.Restart();
			BulkAddWithTemplate(world, 100000);
			Console.WriteLine($"-- bulk add w template: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");

			// bulk add with an init system
			watch.Restart();
			BulkAddWithInitSystem(world, 100000);
			Console.WriteLine($"-- add w init system: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");

			ProfileProgressTicks(world, 3);


			for (var q = 0; q < 10; q++)
			{
				Console.WriteLine("-------- ++++++++ ---------");
				watch.Restart();
				for (var z = 0; z < 1; z++)
					ecs.run(world, moveSys, 0.016f, IntPtr.Zero);
				Console.WriteLine($"------- --- Move system: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");

				watch.Restart();
				for (var z = 0; z < 1; z++)
					ecs.run(world, moveRawSys, 0.016f, IntPtr.Zero);
				Console.WriteLine($"------- --- Move Raw system: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");

				watch.Restart();
				for (var z = 0; z < 1; z++)
					ecs.run(world, moveSpanSys, 0.016f, IntPtr.Zero);
				Console.WriteLine($"------- --- Move Span system: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");
			}



			ecs.set_target_fps(world, 60);
			var i = 0;
			while (ecs.progress(world, 0))
			{
				if (i++ > 3)
					break;
			}
		}

		static void PositionSystem(ref Rows rows, Span<Position> position)
		{
			// Console.WriteLine($"PositionSystem: {rows.count}");
			for (int i = 0; i < (int)rows.count; i++)
			{
				var entityId = rows[i];
				ref var pos = ref position[i];
				pos.X += 1;
				pos.Y += 1;
			}
		}

		static void OnAddMoveSystem(ref Rows rows, Span<Position> position, Span<Speed> speed)
		{
			Console.WriteLine($"OnAddMoveSystem: {rows.count}");
			for (int i = 0; i < (int)rows.count; i++)
			{
				var entityId = rows[i];
				ref var pos = ref position[i];
				pos.X = i;
				pos.Y = i;
			}
		}

		static void MoveSystem(ref Rows rows, Span<Position> position, Span<Speed> speed)
		{
			// Console.WriteLine($"MoveSystem: {rows.count}");
			for (var i = 0; i < (int)rows.count; i++)
			{
				var entityId = rows[i];
				position[i].X += speed[i].SpeedValue * rows.deltaTime;
				position[i].Y += speed[i].SpeedValue * rows.deltaTime;
			}
		}

		static void MoveSystemRaw(ref Rows rows)
		{
			unchecked
			{
				var position = ecs.column<Position>(ref rows, 1);
				var speed = ecs.column<Speed>(ref rows, 2);

				// Console.WriteLine($"MoveSystemRaw: {rows.count}");
				for (var i = 0; i < (int)rows.count; i++)
				{
					var entityId = rows[i];
					position[i].X += speed[i].SpeedValue * rows.deltaTime;
					position[i].Y += speed[i].SpeedValue * rows.deltaTime;
				}
			}
		}

		static void MoveSystemSpan(ref Rows rows)
		{
			var position = new Span<Position>(ecs.column<Position>(ref rows, 1), (int)rows.count);
			var speed = new Span<Speed>(ecs.column<Speed>(ref rows, 2), (int)rows.count);

			// Console.WriteLine($"MoveSystemRaw: {rows.count}");
			for (var i = 0; i < (int)rows.count; i++)
			{
				var entityId = rows[i];
				ref var pos = ref position[i];
				pos.X += speed[i].SpeedValue * rows.deltaTime;
				pos.Y += speed[i].SpeedValue * rows.deltaTime;
			}
		}

		static void ProfileProgressTicks(World world, int iterations = 3)
		{
			var watch = new Stopwatch();
			ecs.measure_frame_time(world, true);
			ecs.measure_system_time(world, true);
			for (var j = 0; j < iterations; j++)
			{
				watch.Restart();
				ecs.progress(world, 10);
				Console.WriteLine($"---- progress: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");

				ecs.get_stats(world, out var stats);
				Console.WriteLine($"---- frame time: {stats.frameTime} ms, system time: {stats.systemTime}, systems: {stats.systemCount}, entities: {stats.entityCount}");
				ecs.free_stats(ref stats);
			}
		}

		static void BulkAddWithTemplate(World world, uint count)
		{
			var templateEntity = ECS_ENTITY(world, "MyTemplate", "Position, Speed");
			ecs.set(world, templateEntity, new Position { X = 999, Y = 999 });
			ecs.set(world, templateEntity, new Speed { SpeedValue = 999 });

			var (_, templateType) = ECS_TYPE(world, "MyType", "INSTANCEOF | MyTemplate, Position, Speed");
			ecs.new_w_count(world, templateType, count);
		}

		static void BulkAddWithInitSystem(World world, uint count)
		{
			ECS_SYSTEM<Position, Speed>(world, OnAddMoveSystem, SystemKind.OnAdd);

			var typeId = ecs.expr_to_type(world, "Position, Speed");
			ecs.new_w_count(world, typeId, count);
		}
	}
}