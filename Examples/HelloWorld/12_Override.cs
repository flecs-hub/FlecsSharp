using System;
using Flecs;


namespace Samples
{
	public unsafe class Override
	{
		public static void Run(World world)
		{
			/* Register components */
			var massType = Caches.AddComponentTypedef<float>(world, "Mass");

			/* Create two base entities */
			var baseEntity = ecs.new_entity(world);
			ecs.set_typedef(world, baseEntity, massType, 10f);

			/* Create instances which share the Mass component from a base */
			var instance = ecs.new_instance(world, baseEntity);

			/* Print value before overriding Mass. The component is not owned, as it is shared with the base entity. */
			Console.WriteLine("Before overriding:");
			Console.WriteLine("instance: {0} (owned = {1})", ecs.ecs_get<float>(world, instance, massType), ecs.has_owned(world, instance, massType));

			/* Override Mass of instance, which will give instance a private copy of the Mass component. */
			ecs.set_typedef(world, instance, massType, 20f);

			/* Print values after overriding Mass. The value of Mass for instance_1 will
			 * be the value assigned in the override (20). Instance now owns Mass,
			 * confirming it has a private copy of the component. */
			Console.WriteLine("\nAfter overriding:");
			Console.WriteLine("instance: {0} (owned = {1})", ecs.ecs_get<float>(world, instance, massType), ecs.has_owned(world, instance, massType));

			/* Remove override of Mass. This will remove the private copy of the Mass component from instance. */
			ecs.remove(world, instance, massType);

			/* Print value after removing the override Mass. The component is no longer
			 * owned, as the instance is again sharing the component with base. */
			Console.WriteLine("\nAfter removing override:");
			Console.WriteLine("instance: {0} (owned = {1})", ecs.ecs_get<float>(world, instance, massType), ecs.has_owned(world, instance, massType));
		}
	}
}