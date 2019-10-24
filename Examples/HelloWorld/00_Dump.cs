using System;
using System.Diagnostics;
using Flecs;
using static Flecs.Macros;

namespace Samples
{
	public class Dump
	{
		public static void Run(World world)
		{
			ECS_SYSTEM<Position, Speed>(world, MoveSystem, SystemKind.OnUpdate);
			ECS_SYSTEM<Position>(world, PositionSystem, SystemKind.OnUpdate);

			ecs.new_entity(world, new Position {X = 1, Y = 2}, new Speed {SpeedValue = 5});
			ecs.new_entity(world, new Position {X = 1, Y = 2}, new Speed {SpeedValue = 5});
			ecs.new_entity(world, new Position {X = 1, Y = 2}, new Speed {SpeedValue = 5});

			ecs.new_entity(world, new Position {X = 14, Y = 2});
			ecs.new_entity(world, new Position {X = 13, Y = 2});

			ecs.new_entity<Position, Speed>(world);
			ecs.new_entity<Position, Speed>(world);

			ecs.new_entity(world, new Position {X = 75, Y = 23}, new Speed {SpeedValue = 66});

			var watch = System.Diagnostics.Stopwatch.StartNew();
			for (var j = 0; j < 10000; j++)
				ecs.new_entity(world, new Position { X = 75, Y = 23 }, new Speed { SpeedValue = 66 });
			Console.WriteLine($"-- add one-by-one: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");

			// bulk add with a template
			watch.Restart();
			BulkAddWithTemplate(world, 10000);
			Console.WriteLine($"-- bulk add w template: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");

			// bulk add with an init system
			watch.Restart();
			BulkAddWithInitSystem(world, 10000);
			Console.WriteLine($"-- add w init system: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");

			ProfileProgressTicks(world, 3);

			ecs.set_target_fps(world, 60);
			var i = 0;
			while (ecs.progress(world, 0))
			{
				if (i++ > 3)
					break;
			}
		}

		static void PositionSystem(ref Rows rows, Set<Position> position)
		{
			Console.WriteLine($"PositionSystem: {rows.count}");
			for (int i = 0; i < (int)rows.count; i++)
			{
				var entityId = rows[i];
				ref var pos = ref position[i];
				pos.X += 1;
				pos.Y += 1;
			}
		}

		static void OnAddMoveSystem(ref Rows rows, Set<Position> position, Set<Speed> speed)
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

		static void MoveSystem(ref Rows rows, Set<Position> position, Set<Speed> speed)
		{
			Console.WriteLine($"MoveSystem: {rows.count}");
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

			var templateType = ECS_TYPE(world, "MyType", "INSTANCEOF | MyTemplate, Position, Speed");
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