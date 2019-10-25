using System;
using System.Runtime.InteropServices;
using Flecs;
using static Flecs.Macros;


namespace Samples
{
	public unsafe class AddType
	{
		public static void Run(World world)
		{
			/* Register components */
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);

			/* Create a type with both Position and Velocity. This allows applications
		     * to create entities with a specific type, or add multiple components in a
		     * single operation. This is much more efficient than adding each component
		     * individually to an entity */
			var (_, movableType) = ECS_TYPE(world, "Movable", "Position, Velocity");

			var e = ecs.new_entity(world, movableType);

			/* Test if entity has the components */
			Console.WriteLine("After new with type:");
			Console.WriteLine("Has Position? {0}", ecs.has<Position>(world, e));
			Console.WriteLine("Has Velocity? {0}", ecs.has<Velocity>(world, e));

			/* Remove both components in one operation */
			ecs.remove(world, e, movableType);

			/* Test if entity has the components */
			Console.WriteLine("After remove with type:");
			Console.WriteLine("Has Position? {0}", ecs.has<Position>(world, e));
			Console.WriteLine("Has Velocity? {0}", ecs.has<Velocity>(world, e));
		}
	}
}