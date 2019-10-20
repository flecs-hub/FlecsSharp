using Flecs;
using System;

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
			for (int i = 0; i < (int)rows.count; i++)
			{
				var entityId = rows[i];
				ref var pos = ref position[i];
				if (i == 0)
					Console.WriteLine($"entityId: {entityId}, pos: {pos.X}, {pos.Y}");
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
			for (int i = 0; i < (int)rows.count; i++)
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
				world.AddSystem<Position, Speed>(MoveSystem, SystemKind.OnUpdate);
				world.AddSystem<Position>(PositionSystem, SystemKind.OnUpdate);

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
				Console.WriteLine($"-- add: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");

				// bulk add with an init system
				world.AddSystem<Position, Speed>(OnAddMoveSystem, SystemKind.OnAdd);
				watch.Restart();
				world.NewEntitiesWithCount<Position, Speed>(10000);
				Console.WriteLine($"-- add w count: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");

				for (var j = 0; j < 10; j++)
				{
					watch.Restart();
					ecs.progress(world, 1);
					Console.WriteLine($"-- progress: {watch.ElapsedMilliseconds} ms, ticks: {watch.ElapsedTicks}");
				}

				ecs.set_target_fps(world, 60);
				var i = 0;
				while (ecs.progress(world, 0))
				{
					if (i++ > 3)
						break;
				}
			}
		}
	}
}