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
		static Dictionary<IntPtr, Dictionary<string, TypeId>> typedefMap = new Dictionary<IntPtr, Dictionary<string, TypeId>>();
		static Dictionary<IntPtr, Dictionary<int, TypeId>> typeMap = new Dictionary<IntPtr, Dictionary<int, TypeId>>();
		static Dictionary<IntPtr, List<SystemActionDelegate>> systemActions = new Dictionary<IntPtr, List<SystemActionDelegate>>();
		static Dictionary<int, CharPtr> unmanagedStrings = new Dictionary<int, CharPtr>();
		static UnmanagedStringBuffer stringBuffer = UnmanagedStringBuffer.Create();


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
		/// adds a component of type newTypeName with a size of sizeof(TFrom). This allows you to use a single struct Type with multiple
		/// different component types. For example, Vector2 could be used for Position and Velocity. Beware name clashes though! You
		/// cannot use a typedef with a name that matches an actual Type.Name that you use as a component.
		/// </summary>
		public static TypeId AddComponentTypedef<TFrom>(World world, string newTypeName) where TFrom : unmanaged
			=> AddComponentTypedef(world, typeof(TFrom), newTypeName);

		/// <summary>
		/// adds a component of type newTypeName with a size of sizeof(type). This allows you to use a single struct Type with multiple
		/// different component types. For example, Vector2 could be used for Position and Velocity. Beware name clashes though! You
		/// cannot use a typedef with a name that matches an actual Type.Name that you use as a component.
		/// </summary>
		public static TypeId AddComponentTypedef(World world, Type type, string newTypeName)
		{
			if (typedefMap[world].ContainsKey(newTypeName))
				return typedefMap[world][newTypeName];

			var componentNamePtr = AddUnmanagedString(newTypeName);
			var entityId = ecs.new_component(world, componentNamePtr, Heap.SizeOf(type));
			var typeId = ecs.type_from_entity(world, entityId);
			typedefMap[world][newTypeName] = typeId;
			return typeId;
		}

		public static TypeId GetTypedef(World world, string typeName) => typedefMap[world][typeName];

		internal static void RegisterWorld(World world)
		{
			typedefMap[world] = new Dictionary<string, TypeId>();
			typeMap[world] = new Dictionary<int, TypeId>();
			systemActions[world] = new List<SystemActionDelegate>();
		}

		internal unsafe static void DeregisterWorld(World world)
		{
			if (!typeMap.ContainsKey(world))
				throw new Exception("Attempting to deregister a world that has not been registered");

			typedefMap.Remove(world);
			typeMap.Remove(world);
			systemActions.Remove(world);
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
			var typeHash = compType.GetHashCode();
			if (!typeMap[world].TryGetValue(typeHash, out var typeId))
			{
				var charPtr = AddUnmanagedString(compType.Name);
				var entityId = ecs.new_component(world, charPtr, Heap.SizeOf(compType));
				typeId = ecs.type_from_entity(world, entityId);
				typeMap[world][typeHash] = typeId;
			}

			return typeId;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void AddSystemAction(World world, SystemActionDelegate del) => systemActions[world].Add(del);
	}
}
