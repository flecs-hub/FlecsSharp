using Flecs;

namespace Samples
{
	class Program
	{
		static void Main(string[] args)
		{
			// using (var world = World.Create())
			// 	Dump.Run(world);

			using (var world = World.Create())
				HelloWorld.Run(world);

			using (var world = World.Create())
				SimpleSystem.Run(world);

			using (var world = World.Create())
				MoveSystem.Run(world);
		}
	}
}