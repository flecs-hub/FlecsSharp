using System;
using static Flecs.Macros;


namespace Flecs.Examples
{
	public unsafe class AutoOverrideNestedPrefab
	{
		public static void Run(World world)
		{
			/* Define components */
			ECS_COMPONENT<Position>(world);

			/* Create root prefab */
			var (rootPrefab, typeId) = ECS_PREFAB(world, "RootPrefab", "Position");
			ecs.set(world, rootPrefab, new Position {X = 10, Y = 20});

			/* Create child prefab. Instead of adding the child directly to
	         * RootPrefab, create a type that overrides the components from the
	         * ChildPrefab. This ensures that when the prefab is instantiated, the
	         * components from the child prefab are owned by the instance. */
			var (childPrefab, childTypeId) = ECS_PREFAB(world, "ChildPrefab", "Position");
			var (childTypeEntity, childPrefabType) = ECS_TYPE(world, "Child", "INSTANCEOF | ChildPrefab, Position");

			/* Instead of the ChildPrefab, add the Child type to RootPrefab */
			ecs.set(world, childTypeEntity, new ecs.EcsPrefab {parent = rootPrefab});
			ecs.set(world, childPrefab, new Position {X = 30, Y = 40});

			/* Create type that automatically overrides Position from RootPrefab */
			var (_, rootType) = ECS_TYPE(world, "Root", "INSTANCEOF | RootPrefab, Position");

			/* Create new entity from Root. Don't use ecs_new_instance, as we're using a
			 * regular type which already has the INSTANCEOF relationship. */
			var e = ecs.new_entity(world, rootType);

			/* Lookup child in the instance we just created. This child will have e in
		     * its type with a CHILDOF mask, and the prefab ChildPrefab in its type with
		     * an INSTANCEOF mask. Note how the identifier is Child, not ChildPrefab. */
			var child = ecs.lookup_child(world, e, "Child");
			Console.WriteLine("Child type = [{0}]", ecs.type_to_expr(world, ecs.get_type(world, child)));

			/* Print position of e and of the child. Note that since types were used to
		     * automatically override the components, the components are owned by both
		     * e and child. */
			var p = ecs.get_ptr<Position>(world, e);
			Console.WriteLine("Position of e = ({0}, {1}) (owrned = {2})", p->X, p->Y, ecs.has_owned<Position>(world, e));

			p = ecs.get_ptr<Position>(world, child);
			Console.WriteLine("Position of Child = ({0}, {1}) (owned = {2})", p->X, p->Y, ecs.has_owned<Position>(world, child));
		}
	}
}