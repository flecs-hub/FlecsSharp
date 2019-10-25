using System;
using Flecs;
using static Flecs.Macros;


namespace Samples
{
	public unsafe class Prefab
	{
		public static void Run(World world)
		{
			/* Define components */
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);

			/* Create a prefab. Prefabs are entities that are solely intended as
		     * templates for other entities. Prefabs are by default not matched with
		     * systems. In that way they are similar to regular entities with the
		     * EcsDisbled tag, except that they have more features which are
		     * demonstrated in the nested_prefab example. */
			var (basePrefab, typeId) = ECS_PREFAB(world, "BasePrefab", "Position, Velocity");
			ecs.set(world, basePrefab, new Position {X = 10, Y = 20});
			ecs.set(world, basePrefab, new Velocity {X = 30, Y = 40});

			/* Use the same technique as used in the auto_override example to create a
			 * type that causes components to be automatically overriden from the base.
			 * Note that prefabs use inheritance. */
			var (_, baseType) = ECS_TYPE(world, "Base", "INSTANCEOF | BasePrefab, Position, Velocity");

			/* Create new entity with type */
			var e = ecs.new_entity(world, baseType);

			/* Get pointers to component values */
			var p = ecs.get_ptr<Position>(world, e);
			var v = ecs.get_ptr<Velocity>(world, e);

			/* Print values of entity */
			Console.WriteLine("Position: ({0}, {1}) (owned = {2})", p->X, p->Y, ecs.has_owned<Position>(world, e));
			Console.WriteLine("Velocity: ({0}, {1}) (owned = {2})", v->X, v->Y, ecs.has_owned<Velocity>(world, e));
		}
	}
}