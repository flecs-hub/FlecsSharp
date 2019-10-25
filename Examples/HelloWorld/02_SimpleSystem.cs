using System;
using static Flecs.Macros;

namespace Flecs.Examples
{
	public class SimpleSystem
	{
		struct Message
		{
			public CharPtr text;
		}

		public static void Run(World world)
		{
			/* Define component */
			ECS_COMPONENT<Message>(world);

			/* Define a system called PrintMessage that is executed every frame, and
			 * subscribes for the 'Message' component */
			ECS_SYSTEM(world, PrintMessage, SystemKind.OnUpdate, "Message");

			/* Create new entity, add the component to the entity */
			var e = ecs.new_entity<Message>(world);
			ecs.set(world, e, new Message {text = Caches.AddUnmanagedString("Hello Flecs#!")});

			/* Set target FPS for main loop to 1 frame per second */
			ecs.set_target_fps(world, 1);

			/* Run systems */
			ecs.progress(world, 0);
		}

		static void PrintMessage(ref Rows rows)
		{
			/* Get a pointer to the array of the first column in the system. The order
			 * of columns is the same as the one provided in the system signature. */
			ECS_COLUMN<Message>(ref rows, out var msg, 1);

			/* Iterate all the messages */
			for (int i = 0; i < rows.count; i ++)
				Console.WriteLine(msg[i].text);
		}
	}
}