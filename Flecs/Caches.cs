using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


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
		static Dictionary<int, CharPtr> unmanagedStrings = new Dictionary<int, CharPtr>();
		static UnmanagedStringBuffer stringBuffer = UnmanagedStringBuffer.Create();

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

		/// <summary>
		/// adds an unmanaged string to the buffer and returns a pointer to the null-terminated string. Caches the result so each string is only
		/// ever created once.
		/// </summary>
		public static CharPtr AddUnmanagedString(string str)
		{
			var strHashCode = str.GetHashCode();
			if (unmanagedStrings.ContainsKey(strHashCode))
				return unmanagedStrings[strHashCode];

			var ptr = stringBuffer.AddString(str);
			unmanagedStrings[strHashCode] = ptr;

			return ptr;
		}

		internal static TypeId GetComponentTypeId<T>(World world) where T : unmanaged
			=> GetComponentTypeId(world, typeof(T));

		internal static TypeId GetComponentTypeId(World world, Type compType)
		{
			if (!TryGetComponentTypeId(world, compType, out var typeId))
			{
				var charPtr = AddUnmanagedString(compType.Name);
				var entityId = ecs.new_component(world, charPtr, (UIntPtr)Marshal.SizeOf(compType));
				typeId = ecs.type_from_entity(world, entityId);
				AddComponentTypeToTypeId(world, compType, typeId);
			}

			return typeId;
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
