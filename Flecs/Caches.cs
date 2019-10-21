using System;
using System.Collections.Generic;

namespace Flecs
{
	/// <summary>
	/// cache for holding potentially common lookups and to ensure delegates dont get garbage collected
	/// </summary>
	public static class Caches
	{
		static Dictionary<World, Dictionary<Type, TypeId>> typeMap = new Dictionary<World, Dictionary<Type, TypeId>>();
		static Dictionary<World, Dictionary<string, TypeId>> tagTypeMap = new Dictionary<World, Dictionary<string, TypeId>>();
		static Dictionary<World, List<SystemActionDelegate>> systemActions = new Dictionary<World, List<SystemActionDelegate>>();

		internal static void RegisterWorld(World world)
		{
			typeMap[world] = new Dictionary<Type, TypeId>();
			tagTypeMap[world] = new Dictionary<string, TypeId>();
			systemActions[world] = new List<SystemActionDelegate>();
		}

		internal static void DeregisterWorld(World world)
		{
			typeMap.Remove(world);
			tagTypeMap.Remove(world);
			systemActions.Remove(world);
		}

		#region Add/Remove Type Caches

		internal static bool TryGetComponentTypeId(World world, Type compType, out TypeId typeId) => typeMap[world].TryGetValue(compType, out typeId);

		internal static void AddComponentTypeToTypeId(World world, Type compType, TypeId typeId) => typeMap[world][compType] = typeId;

		internal static bool TryGetTagTypeId(World world, string tag, out TypeId typeId) => tagTypeMap[world].TryGetValue(tag, out typeId);

		internal static void AddTagToTypeId(World world, string tag, TypeId typeId) => tagTypeMap[world][tag] = typeId;

		internal static void AddSystemAction(World world, SystemActionDelegate del) => systemActions[world].Add(del);

		#endregion
	}
}
