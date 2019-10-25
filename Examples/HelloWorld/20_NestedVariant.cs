using System;
using Flecs;
using static Flecs.Macros;


namespace Samples
{
	public unsafe class NestedVariant
	{
		public static void Run(World world)
		{
			/* Define components */
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);

			/* Create a base prefab which will be inherited from by a child prefab */
			var (basePrefab, _) = ECS_PREFAB(world, "BasePrefab", "Position");
			ecs.set(world, basePrefab, new Position {X = 10, Y = 20});

			/* Create a prefab with a child entity. When this prefab is instantiated,
			 * the child will be instantiated too as a child of the instance.  */
			var (rootPrefab, _) = ECS_PREFAB(world, "RootPrefab", "Position");
			ecs.set(world, rootPrefab, new Position {X = 10, Y = 20});

			/* Create two child prefabs that inherit from BasePrefab */
			var (child1Prefab, _) = ECS_PREFAB(world, "Child1", "INSTANCEOF | BasePrefab, Velocity");
			ecs.set(world, child1Prefab, new ecs.EcsPrefab {parent = rootPrefab});
			ecs.set(world, child1Prefab, new Velocity {X = 30, Y = 40});

			/* Create two child prefabs that inherit from BasePrefab */
			var (child2Prefab, _) = ECS_PREFAB(world, "Child2", "INSTANCEOF | BasePrefab, Velocity");
			ecs.set(world, child2Prefab, new ecs.EcsPrefab {parent = rootPrefab});
			ecs.set(world, child2Prefab, new Velocity {X = 50, Y = 60});

			/* Create instance of RootPrefab */
			var e = ecs.new_instance(world, rootPrefab);

			/* Print types of child1 and child2 */
			var child1 = ecs.lookup_child(world, e, "Child1");
			Console.WriteLine("Child1 type = [{0}]", ecs.type_to_expr(world, ecs.get_type(world, child1)));

			var child2 = ecs.lookup_child(world, e, "Child2");
			Console.WriteLine("Child2 type = [{0}]", ecs.type_to_expr(world, ecs.get_type(world, child2)));

			var p = ecs.get_ptr<Position>(world, e);
			Console.WriteLine("Position of e = ({0}, {1})", p->X, p->Y);

			p = ecs.get_ptr<Position>(world, child1);
			var v = ecs.get_ptr<Velocity>(world, child1);
			Console.WriteLine("Child1 Position = ({0}, {1}), Velocity = ({2}, {3})", p->X, p->Y, v->X, v->Y);

			p = ecs.get_ptr<Position>(world, child2);
			v = ecs.get_ptr<Velocity>(world, child2);
			Console.WriteLine("Child2 Position = ({0}, {1}), Velocity = ({2}, {3})", p->X, p->Y, v->X, v->Y);
		}
	}
}