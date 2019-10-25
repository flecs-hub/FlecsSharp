using System;
using System.Reflection;
using Flecs;

namespace Samples
{
	class Program
	{
		static Type[] exampleTypes = new[]
		{
			typeof(HelloWorld), typeof(SimpleSystem), typeof(MoveSystem), typeof(SimpleModuleExample),
			typeof(AddRemoveSystem), typeof(SetSystem), typeof(NoMacros), typeof(Hierarchy), typeof(HierarchyApi),
			typeof(Inheritance), typeof(InheritanceApi), typeof(Override), typeof(OverrideInit), typeof(AddType),
			typeof(AutoOverride), typeof(Prefab), typeof(PrefabVariant), typeof(NestedPrefab), typeof(AutoOverrideNestedPrefab),
			typeof(NestedVariant), typeof(SystemFeatures), typeof(Optional), typeof(GetChildren)
		};

		static void Main(string[] args)
		{
			// using (var world = World.Create())
			// 	Dump.Run(world);
			// return;

			foreach (var type in exampleTypes)
			{
				var runMethod = type.GetMethod("Run", BindingFlags.Static | BindingFlags.Public);

				Console.WriteLine($"---- Running {type.Name} ----");
				using (var world = World.Create())
					runMethod?.Invoke(null, new object[] {world});
				Console.WriteLine();
			}
		}
	}
}