using System;
using Flecs;


namespace Samples
{
	public unsafe class OverrideInit
	{
		public static void Run(World world)
		{
			/* Register components */
			var massType = Caches.AddComponentTypedef<float>(world, "Mass");

			/* Create base entity. Create entity as disabled, which will prevent it from
			     * being matched with systems. This is a common approach to creating
			     * entities that are only used as templates for other entities, or in this
			     * case, for providing initial component values. */
			var baseEntity = ecs.new_entity(world, ecs.TEcsDisabled);
			ecs.set_typedef(world, baseEntity, massType, 10f);

			/* Create instances which share the Mass component from a base */
			var instance = ecs.new_instance(world, baseEntity);

			/* Add component without setting it. This will initialize the new component
			     * with the value from the base, which is a common approach to initializing
			     * components with a value when they are added. */
			ecs.add(world, instance, massType);

			/* Print value of mass. The value will be equal to base, and the instance will own the component. */
			Console.WriteLine("instance: {0} (owned = {1})", ecs.ecs_get<float>(world, instance, massType), ecs.has_owned(world, instance, massType));
		}
	}
}