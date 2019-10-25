using System;
using static Flecs.Macros;

namespace Flecs.Examples
{
	public class SystemFeatures
	{
		static void SystemA(ref Rows rows)
		{
			Console.WriteLine("System A called!");
		}

		static void SystemB(ref Rows rows)
		{
			Console.WriteLine("System B called!");
		}

		static void SystemC(ref Rows rows)
		{
			Console.WriteLine("System C called!");
		}

		static void SystemD(ref Rows rows)
		{
			Console.WriteLine("System D called!");
		}

		static void SystemE(ref Rows rows)
		{
			Console.WriteLine("System E called!");
		}

		public static void Run(World world)
		{
			/* Register components */
			ECS_COMPONENT<Position>(world);

			/* Create four systems */
			ECS_SYSTEM(world, SystemA, SystemKind.OnUpdate, "Position");
			ECS_SYSTEM(world, SystemB, SystemKind.OnUpdate, "Position");
			ECS_SYSTEM(world, SystemC, SystemKind.OnUpdate, "Position");
			ECS_SYSTEM(world, SystemD, SystemKind.OnUpdate, "Position");
			ECS_SYSTEM(world, SystemE, SystemKind.OnUpdate, "Position");

			/* Create two features, each with a set of systems. Features are regular
			 * types, and the name feature is just a convention to indicate that a type
			 * only contains systems. Since systems, just like components, are stored as
			 * entities, they can be contained by types. */
			var (feature1, _) = ECS_TYPE(world, "Feature1", "SystemA, SystemB");
			var (feature2, _) = ECS_TYPE(world, "Feature2", "SystemC, SystemD");

			/* Create a feature that includes Feature2 and SystemE. Types/features can
			 * be organized in hierarchies */
			var (feature3, _) = ECS_TYPE(world, "Feature3", "Feature2, SystemE");

			/* Create entity for testing */
			ECS_ENTITY(world, "MyEntity", "Position");

			/* First, disable Feature1 and Feature3. No systems will be executed. */
			Console.WriteLine("Feature1 disabled, Feature3 disabled:");
			ecs.enable(world, feature1, false);
			ecs.enable(world, feature3, false);
			ecs.progress(world, 1);

			/* Enable Feature3 */
			Console.WriteLine("\nFeature1 disabled, Feature3 enabled:");
			ecs.enable(world, feature3, true);
			ecs.progress(world, 1);

			/* Disable Feature2. This will disable some of the systems in Feature3 too */
			Console.WriteLine("\nFeature1 disabled, Feature3 partially enabled:");
			ecs.enable(world, feature2, false);
			ecs.progress(world, 1);

			/* Enable Feature1 */
			Console.WriteLine("\nFeature1 enabled, Feature3 partially enabled:");
			ecs.enable(world, feature1, true);
			ecs.progress(world, 1);
		}
	}
}