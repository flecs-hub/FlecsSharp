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

			using (var world = World.Create())
				SimpleModuleExample.Run(world);

			using (var world = World.Create())
				AddRemoveSystem.Run(world);

			using (var world = World.Create())
				SetSystem.Run(world);

			using (var world = World.Create())
				NoMacros.Run(world);
		}
	}
}