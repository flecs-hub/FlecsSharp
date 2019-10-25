namespace Flecs.Examples
{
	public class HelloWorld
	{
		public static void Run(World world)
		{
			/* Set target FPS for main loop */
			ecs.set_target_fps(world, 60);

			/* Run systems */
			ecs.progress(world, 0);
		}
	}
}