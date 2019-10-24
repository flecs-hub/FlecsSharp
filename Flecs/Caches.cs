using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Flecs
{
	/// <summary>
	/// cache for holding potentially common lookups and to ensure delegates dont get garbage collected
	/// </summary>
	public static class Caches
	{
		static Dictionary<IntPtr, Dictionary<Type, TypeId>> typeMap = new Dictionary<IntPtr, Dictionary<Type, TypeId>>();
		static Dictionary<IntPtr, Dictionary<string, TypeId>> tagTypeMap = new Dictionary<IntPtr, Dictionary<string, TypeId>>();
		static Dictionary<IntPtr, List<SystemActionDelegate>> systemActions = new Dictionary<IntPtr, List<SystemActionDelegate>>();
		static Dictionary<int, CharPtr> unmanagedStrings = new Dictionary<int, CharPtr>();
		static UnmanagedStringBuffer stringBuffer = UnmanagedStringBuffer.Create();

		internal static void RegisterWorld(World world)
		{
			typeMap[world] = new Dictionary<Type, TypeId>();
			tagTypeMap[world] = new Dictionary<string, TypeId>();
			systemActions[world] = new List<SystemActionDelegate>();
		}

		internal unsafe static void DeregisterWorld(World world)
		{
			if (!typeMap.ContainsKey(world))
				throw new Exception("Attempting to deregister a world that has not been registered");

			typeMap.Remove(world);
			tagTypeMap.Remove(world);
			systemActions.Remove(world);
		}

		/// <summary>
		/// adds an unmanaged string to the buffer and returns a pointer to the null-terminated string. Caches the result so each string is only
		/// ever created once.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static CharPtr AddUnmanagedString(string str)
		{
			var strHashCode = str.GetHashCode();
			if (unmanagedStrings.ContainsKey(strHashCode))
				return unmanagedStrings[strHashCode];

			var ptr = stringBuffer.AddString(str);
			unmanagedStrings[strHashCode] = ptr;

			return ptr;
		}

		/// <summary>
		/// Provides a cache of all C# Types to TypeId. This avoids having to create unmanaged strings excessively. Each Type only needs
		/// to have one unmanaged string with this cache.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static TypeId GetComponentTypeId<T>(World world) where T : unmanaged
			=> GetComponentTypeId(world, typeof(T));

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryGetComponentTypeId(World world, Type compType, out TypeId typeId) => typeMap[world].TryGetValue(compType, out typeId);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AddComponentTypeToTypeId(World world, Type compType, TypeId typeId) => typeMap[world][compType] = typeId;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool TryGetTagTypeId(World world, string tag, out TypeId typeId) => tagTypeMap[world].TryGetValue(tag, out typeId);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AddTagToTypeId(World world, string tag, TypeId typeId) => tagTypeMap[world][tag] = typeId;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AddSystemAction(World world, SystemActionDelegate del) => systemActions[world].Add(del);

		#endregion
	}
}
