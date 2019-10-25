using System;
using Flecs;
using static Flecs.Macros;


namespace Samples
{
	public unsafe class AutoOverride
	{
		public static void Run(World world)
		{
			/* Define components */
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);

			/* Create entity with default values for Position and Velocity. Add the
			     * EcsDisabled tag to ensure the entity will not be matched by systems,
			     * since it is only used to provide initial component values. */
			var baseEntity = ECS_ENTITY(world, "Base", "Position, Velocity, EcsDisabled");
			ecs.set(world, baseEntity, new Position {X = 10, Y = 20});
			ecs.set(world, baseEntity, new Velocity {X = 30, Y = 40});

			/* This type inherits from Base, as well as adding Position and Velocity as
		     * private components. This will automatically override the components as an
		     * entity with this type is created, which will initialize the private
		     * values with the values of the Base entity. This is a common approach to
		     * creating entities with an initialized set of components. */
			var (_, movableType) = ECS_TYPE(world, "Movable", "INSTANCEOF | Base, Position, Velocity");

			/* Create new entity with type */
			var e = ecs.new_entity(world, movableType);

			/* Get pointers to component values */
			var p = ecs.get_ptr<Position>(world, e);
			var v = ecs.get_ptr<Velocity>(world, e);

			/* Print values of entity */
			Console.WriteLine("Position: ({0}, {1}) (owned = {2})", p->X, p->Y, ecs.has_owned<Position>(world, e));
			Console.WriteLine("Velocity: ({0}, {1}) (owned = {2})", v->X, v->Y, ecs.has_owned<Velocity>(world, e));
		}
	}
}