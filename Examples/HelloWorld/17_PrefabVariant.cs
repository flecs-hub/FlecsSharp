using System;
using Flecs;
using static Flecs.Macros;


namespace Samples
{
	public unsafe class PrefabVariant
	{
		public static void Run(World world)
		{
			/* Define components */
			ECS_COMPONENT<Position>(world);
			ECS_COMPONENT<Velocity>(world);

			/* Create a BasePrefab from which will be specialized by other prefabs */
			var (basePrefab, typeId) = ECS_PREFAB(world, "BasePrefab", "Position, Velocity");
			ecs.set(world, basePrefab, new Position {X = 10, Y = 20});

			/* Create two prefab specializations. This uses the same inheritance mechanism as is used with regular entities. */
			var (subPrefab, subTypeId) = ECS_PREFAB(world, "SubPrefab1", "INSTANCEOF | BasePrefab, Velocity");
			ecs.set(world, subPrefab, new Velocity {X = 30, Y = 40});

			var (subPrefab2, subTypeIds) = ECS_PREFAB(world, "SubPrefab2", "INSTANCEOF | BasePrefab, Velocity");
			ecs.set(world, subPrefab2, new Velocity {X = 50, Y = 60});

			/* Create two types for SubPrefab1 and SubPrefab2 which automatically
		     * override the component values. The Position component will be overridden
		     * from the BasePrefab, while Velocity will be overridden from SubPrefab1
		     * and SubPrefab2 respectively. */
			var (_, subType1) = ECS_TYPE(world, "Sub1", "INSTANCEOF | SubPrefab1, Position, Velocity");
			var (_, subType2) = ECS_TYPE(world, "Sub2", "INSTANCEOF | SubPrefab2, Position, Velocity");

			/* Create new entities from Sub1 and Sub2 */
			var e1 = ecs.new_entity(world, subType1);
			var e2 = ecs.new_entity(world, subType2);

			/* Print values of e1 */
			var p = ecs.get_ptr<Position>(world, e1);
			var v = ecs.get_ptr<Velocity>(world, e1);

			Console.WriteLine("e1 Position: ({0}, {1}) Velocity = ({2}, {3})", p->X, p->Y, v->X, v->Y);

			/* Print values of e2 */
			p = ecs.get_ptr<Position>(world, e2);
			v = ecs.get_ptr<Velocity>(world, e2);

			Console.WriteLine("e1 Position: ({0}, {1}) Velocity = ({2}, {3})", p->X, p->Y, v->X, v->Y);
		}
	}
}