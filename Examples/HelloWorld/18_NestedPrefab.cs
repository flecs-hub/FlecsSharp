using System;
using Flecs;
using static Flecs.Macros;


namespace Samples
{
	public unsafe class NestedPrefab
	{
		public static void Run(World world)
		{
			/* Define components */
			ECS_COMPONENT<Position>(world);

			/* Create a prefab with a child entity. When this prefab is instantiated,
			 * the child will be instantiated too as a child of the instance.  */
			var (rootPrefab, typeId) = ECS_PREFAB(world, "RootPrefab", "Position");
			ecs.set(world, rootPrefab, new Position {X = 10, Y = 20});

			/* The child needs to explicitly set the parent in the EcsPrefab
	         * component. This is needed for Flecs to register the child with the
	         * parent prefab. */
			var (childPrefab, childTypeId) = ECS_PREFAB(world, "Child", "Position");
			ecs.set(world, childPrefab, new ecs.EcsPrefab {parent = rootPrefab});
			ecs.set(world, childPrefab, new Position {X = 30, Y = 40});

			/* Create instance of root */
			var e = ecs.new_instance(world, rootPrefab);

			/* Lookup child in the instance we just created. This child will have e in
		     * its type with a CHILDOF mask, and the prefab Child in its type with an
		     * INSTANCEOF mask. */
			var child = ecs.lookup_child(world, e, "Child");
			Console.WriteLine("Child type = [{0}]", ecs.type_to_expr(world, ecs.get_type(world, child)));

			/* Print position of e and of the child. Note that since we did not override
		     * any components, both position components are owned by the prefabs, not by
		     * the instances. */
			var p = ecs.get_ptr<Position>(world, e);
			Console.WriteLine("Position of e = ({0}, {1})", p->X, p->Y);

			p = ecs.get_ptr<Position>(world, child);
			Console.WriteLine("Position of Child = ({0}, {1})", p->X, p->Y);
		}
	}
}