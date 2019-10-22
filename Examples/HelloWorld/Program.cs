using Flecs;
using System;
using System.Diagnostics;


namespace HelloWorld
{
	class Program
	{
		public struct Position
		{
			public float X;
			public float Y;
		}

		public struct Speed
		{
			public int SpeedValue;
		}

		static void PositionSystem(ref Rows rows, Span<Position> position)
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
			Console.WriteLine($"MoveSystem: {rows.count}");
			for (var i = 0; i < (int)rows.count; i++)
			{
				var entityId = rows[i];
				ref var pos = ref position[i];
				pos.X += speed[i].SpeedValue * rows.deltaTime;
				pos.Y += speed[i].SpeedValue * rows.deltaTime;
			}
		}

		static void Main(string[] args)
		{
			using (var world = World.Create())
			{
				// world.AddSystem<Position, Speed>(MoveSystem, SystemKind.OnUpdate);
				ecs.ECS_SYSTEM<Position, Speed>(world, MoveSystem, SystemKind.OnUpdate);
				// world.AddSystem<Position>(PositionSystem, SystemKind.OnUpdate);
				ecs.ECS_SYSTEM<Position>(world, PositionSystem, SystemKind.OnUpdate);

				world.NewEntity("MyEntity1", new Position {X = 1, Y = 2}, new Speed {SpeedValue = 5});
				world.NewEntity("MyEntity2", new Position {X = 1, Y = 2}, new Speed {SpeedValue = 5});

				world.NewEntity("MyEntity3", new Position {X = 14, Y = 2});
				world.NewEntity("MyEntity4", new Position {X = 13, Y = 2});

				world.NewEntity<Position, Speed>();
				world.NewEntity<Speed, Position>();

				world.NewEntity(new Position {X = 75, Y = 23}, new Speed {SpeedValue = 66});

				var watch = System.Diagnostics.Stopwatch.StartNew();
				for (var j = 0; j < 10000; j++)
					world.NewEntity(new Position { X = 75, Y = 23 }, new Speed { SpeedValue = 66 });
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
			var templateEntity = ecs.ECS_ENTITY(world, "MyTemplate", "Position, Speed");
			world.Set(templateEntity, new Position { X = 999, Y = 999 });
			world.Set(templateEntity, new Speed { SpeedValue = 999 });

			var templateType = ecs.ECS_TYPE(world, "MyType", "INSTANCEOF | MyTemplate, Position, Speed");

			ecs.ecs_new_w_count(world, templateType, count);
		}

		static void BulkAddWithInitSystem(World world, uint count)
		{
			ecs.ECS_SYSTEM<Position, Speed>(world, OnAddMoveSystem, SystemKind.OnAdd);

			var typeId = ecs.expr_to_type(world, "Position, Speed");
			ecs.ecs_new_w_count(world, typeId, count);
		}
	}
}