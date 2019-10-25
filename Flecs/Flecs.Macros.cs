using System;
using System.Runtime.InteropServices;


namespace Flecs
{
	public delegate void SystemAction<T>(ref Rows ids, Span<T> comp) where T : unmanaged;
	public delegate void SystemAction<T1, T2>(ref Rows ids, Span<T1> comp1, Span<T2> comp2) where T1 : unmanaged where T2 : unmanaged;
	public delegate void SystemAction<T1, T2, T3>(ref Rows ids, Span<T1> comp1, Span<T2> comp2, Span<T3> comp3)
		where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged;

	/// <summary>
	/// all of the methods here are reimplementations of the Flecs macros. Because C# does not support macros and C# has
	/// a Type system with method overloading there will always be some minor differences. Often, a Flecs macro is used to
	/// just drop some variables in scope to be used later. With C# we have to return those variables.
	/// </summary>
	public unsafe static partial class ecs
	{
		public static uint count(World world, TypeId type) => _ecs.count(world, type);

		public static EntityId new_entity(World world) => _ecs.@new(world, TypeId.Zero);

		public static EntityId new_entity(World world, TypeId typeId) => _ecs.@new(world, typeId);

		public static EntityId new_entity(World world, Type componentType) => _ecs.@new(world, Caches.GetComponentTypeId(world, componentType));

		public static EntityId new_entity<T>(World world) where T : unmanaged
			=> _ecs.@new(world, Caches.GetComponentTypeId(world, typeof(T)));

		public static EntityId new_entity(World world, string id)
		{
			var e = _ecs.@new(world, TypeId.Zero);
			set_id(world, e, id);
			return e;
		}

		public static EntityId new_entity<T1>(World world, T1 value1 = default) where T1 : unmanaged
		{
			var e = _ecs.@new(world, TypeId.Zero);
			set(world, e, value1);
			return e;
		}

		public static EntityId new_entity<T1, T2>(World world, T1 value1 = default, T2 value2 = default) where T1 : unmanaged where T2 : unmanaged
		{
			var e = _ecs.@new(world, TypeId.Zero);
			set(world, e, value1);
			set(world, e, value2);
			return e;
		}

		public static bool has(World world, EntityId entity, TypeId typeId)
			=> _ecs.has(world, entity, typeId);

		public static bool has(World world, EntityId entity, Type componentType)
			=> _ecs.has(world, entity, Caches.GetComponentTypeId(world, componentType));

		public static bool has<T>(World world, EntityId entity) where T : unmanaged
			=> _ecs.has(world, entity, Caches.GetComponentTypeId<T>(world));

		public static bool has_owned(World world, EntityId entity, TypeId type)
			=> _ecs.has_owned(world, entity, type);

		public static bool has_owned<T>(World world, EntityId entity) where T : unmanaged
			=> _ecs.has_owned(world, entity, Caches.GetComponentTypeId<T>(world));

		public static EntityId new_w_count(World world, TypeId typeId, uint count)
			=> _ecs.new_w_count(world, typeId, count);

		public static EntityId new_child(World world, EntityId parent)
			=> _ecs.new_child(world, parent, TypeId.Zero);

		public static EntityId new_child(World world, EntityId parent, TypeId type)
			=> _ecs.new_child(world, parent, type);

		public static EntityId new_child_w_count(World world, EntityId parent, TypeId type, uint count)
			=> _ecs.new_child_w_count(world, parent, type, count);

		public static EntityId new_instance(World world, EntityId baseEntityId)
			=> new_instance(world, baseEntityId, TypeId.Zero);

		public static EntityId new_instance(World world, EntityId baseEntityId, TypeId type)
			=> _ecs.new_instance(world, baseEntityId, type);

		public static EntityId new_instance_w_count(World world, EntityId baseEntityId, TypeId type, uint count)
			=> _ecs.new_instance_w_count(world, baseEntityId, type, count);

		public static T* column<T>(ref Rows rows, uint columnIndex) where T : unmanaged
			=> (T*)_ecs.column(ref rows, Heap.SizeOf<T>(), columnIndex);

		public static EntityId set_id(World world, EntityId entity, string id)
			=> set_ptr(world, entity, ecs.TEcsId, Caches.AddUnmanagedString(id).Ptr());

		public static EntityId set<T>(World world, EntityId entity, T value = default) where T : unmanaged
		{
			var type = Caches.GetComponentTypeId<T>(world);
			return _ecs.set_ptr(world, entity, type_to_entity(world, type), Heap.SizeOf<T>(), (IntPtr)(&value));
		}

		/// <summary>
		/// helper that allows
		/// </summary>
		public static EntityId set_typedef<TFrom>(World world, EntityId entity, TypeId type, TFrom value) where TFrom : unmanaged
			=> _ecs.set_ptr(world, entity, type_to_entity(world, type), Heap.SizeOf<TFrom>(), (IntPtr)(&value));

		public static EntityId set_ptr<T>(World world, EntityId entity, T* value) where T : unmanaged
		{
			var type = Caches.GetComponentTypeId<T>(world);
			return _ecs.set_ptr(world, entity, type_to_entity(world, type), Heap.SizeOf<T>(), (IntPtr)value);
		}

		public static EntityId set_ptr(World world, EntityId entity, TypeId type, void* value)
			=> _ecs.set_ptr(world, entity, type_to_entity(world, type), Heap.SizeOf<IntPtr>(), (IntPtr)value);

		public static IntPtr get_ptr(World world, EntityId entity, TypeId type) => _ecs.get_ptr(world, entity, type);

		public static T* get_ptr<T>(World world, EntityId entity) where T : unmanaged
			=> (T*)_ecs.get_ptr(world, entity, Caches.GetComponentTypeId<T>(world));

		public static T ecs_get<T>(World world, EntityId entity) where T : unmanaged => *(T*)get_ptr<T>(world, entity);

		public static T ecs_get<T>(World world, EntityId entity, TypeId type) where T : unmanaged
			=> *(T*)get_ptr(world, entity, type);

		public static EntityId set_singleton<T>(World world, T value = default) where T : unmanaged
		{
			var componentType = Macros.ECS_COMPONENT<T>(world);
			var componentEntityId = type_to_entity(world, componentType);
			T* val = &value;
			return _ecs.set_singleton_ptr(world, componentEntityId, Heap.SizeOf<T>(), (IntPtr)val);
		}

		public static EntityId set_singleton_ptr<T>(World world, ref T value) where T : unmanaged
		{
			fixed (T* val = &value)
				return set_singleton_ptr(world, val);
		}

		public static EntityId set_singleton_ptr<T>(World world, T* value) where T : unmanaged
		{
			var componentType = Macros.ECS_COMPONENT<T>(world);
			var componentEntityId = type_to_entity(world, componentType);
			return _ecs.set_singleton_ptr(world, componentEntityId, Heap.SizeOf<T>(), (IntPtr)value);
		}

		public static IntPtr get_singleton_ptr(World world, TypeId type) => _ecs.get_ptr(world, ECS_SINGLETON, type);

		public static T* get_singleton_ptr<T>(World world) where T : unmanaged
			=> (T*)_ecs.get_ptr(world, ECS_SINGLETON, Caches.GetComponentTypeId<T>(world));

		public static void add(World world, EntityId entity, TypeId type) => _ecs.add(world, entity, type);

		public static void add<T>(World world, EntityId entity) where T : unmanaged
			=> _ecs.add(world, entity, Caches.GetComponentTypeId<T>(world));

		public static void remove(World world, EntityId entity, TypeId type) => _ecs.remove(world, entity, type);

		public static void remove<T>(World world, EntityId entity) where T : unmanaged
			=> _ecs.remove(world, entity, Caches.GetComponentTypeId<T>(world));

		public static void add_remove(World world, EntityId entity, TypeId typeToAdd, TypeId typeToRemove)
			=> _ecs.add_remove(world, entity, typeToAdd, typeToRemove);

		public static void add_remove_w_filter(World world, TypeId toAdd, TypeId toRemove, ref TypeFilter filter)
			=> _ecs.add_remove_w_filter(world, toAdd, toRemove, ref filter);

		public static void run_w_filter(World world, EntityId system, float deltaTime, uint offset, uint limit, TypeId filter, IntPtr param)
			=> _ecs.run_w_filter(world, system, deltaTime, offset, limit, filter, param);

		public static EntityId import(World world, string id, ModuleInitActionDelegate module, int flags)
		{
			var moduleNamePtr = Caches.AddUnmanagedString(id);
			return _ecs.import(world, module, moduleNamePtr, flags, (IntPtr)0, (UIntPtr)0);

			//_ecs_import(world, module##Import, #module, flags, handles_out, sizeof(module))
		}

		public static EntityId set_w_data<T1>(World world, uint rowCount, EntityId[] entities, T1[] compValues) where T1 : unmanaged
		{
			var compType = Caches.GetComponentTypeId<T1>(world);
			var compEntityId = type_to_entity(world, compType);
			var components = new[] {compEntityId};

			fixed (void* compValuesPtr = compValues)
			{
				var columns = new[] {(IntPtr)compValuesPtr};
				fixed (void* columnsPtr = columns)
				fixed (EntityId* componentsPtr = components)
				fixed (EntityId* entitiesPtr = entities)
				{
					var tableData = new TableData
					{
						columnCount = 2,
						rowCount = rowCount,
						entities = entitiesPtr,
						components = componentsPtr,
						columns = columnsPtr
					};

					return set_w_data(world, ref tableData);
				}
			}
		}

		public static EntityId set_w_data<T1, T2>(World world, uint rowCount, EntityId[] entities, T1[] comp1Values, T2[] comp2Values)
			where T1 : unmanaged where T2 : unmanaged
		{
			var comp1Type = Caches.GetComponentTypeId<T1>(world);
			var comp1EntityId = type_to_entity(world, comp1Type);

			var comp2Type = Caches.GetComponentTypeId<T2>(world);
			var comp2EntityId = type_to_entity(world, comp2Type);

			var components = new[] {comp1EntityId, comp2EntityId};

			fixed (void* comp1ValuesPtr = comp1Values)
			fixed (void* comp2ValuesPtr = comp2Values)
			{
				var columns = new[] {(IntPtr)comp1ValuesPtr, (IntPtr)comp2ValuesPtr};
				fixed (void* columnsPtr = columns)
				fixed (EntityId* componentsPtr = components)
				fixed (EntityId* entitiesPtr = entities)
				{
					var tableData = new TableData
					{
						columnCount = 2,
						rowCount = rowCount,
						entities = entitiesPtr,
						components = componentsPtr,
						columns = columnsPtr
					};

					return set_w_data(world, ref tableData);
				}
			}
		}

		public static EntityId set_w_data<T1, T2, T3>(World world, uint rowCount, EntityId[] entities, T1[] comp1Values, T2[] comp2Values, T3[] comp3Values)
			where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
		{
			var comp1Type = Caches.GetComponentTypeId<T1>(world);
			var comp1EntityId = type_to_entity(world, comp1Type);

			var comp2Type = Caches.GetComponentTypeId<T2>(world);
			var comp2EntityId = type_to_entity(world, comp2Type);

			var comp3Type = Caches.GetComponentTypeId<T3>(world);
			var comp3EntityId = type_to_entity(world, comp3Type);

			var components = new[] {comp1EntityId, comp2EntityId, comp3EntityId};

			fixed (void* comp1ValuesPtr = comp1Values)
			fixed (void* comp2ValuesPtr = comp2Values)
			fixed (void* comp3ValuesPtr = comp3Values)
			{
				var columns = new[] {(IntPtr)comp1ValuesPtr, (IntPtr)comp2ValuesPtr, (IntPtr)comp3ValuesPtr};
				fixed (void* columnsPtr = columns)
				fixed (EntityId* componentsPtr = components)
				fixed (EntityId* entitiesPtr = entities)
				{
					var tableData = new TableData
					{
						columnCount = 3,
						rowCount = rowCount,
						entities = entitiesPtr,
						components = componentsPtr,
						columns = columnsPtr
					};

					return set_w_data(world, ref tableData);
				}
			}
		}

		public static T* field<T>(ref Rows rows, uint column, uint row) where T : unmanaged
			=> (T*)_ecs.field(ref rows, Heap.SizeOf<T>(), column, row);

	}

	/// <summary>
	/// all SCREAMING_SNAKE_CASE declarative macros from Flecs are implemented here. The idea is to add "using Flecs.Macros" to
	/// your file so that the declarative API will match what Flecs does directly. The differences between macros explained above
	/// also applies here.
	/// </summary>
	public unsafe static class Macros
	{
		public static TypeId ECS_COMPONENT(World world, Type componentType) => Caches.GetComponentTypeId(world, componentType);

		public static TypeId ECS_COMPONENT<T>(World world) where T : unmanaged => Caches.GetComponentTypeId<T>(world);

		public static EntityId ECS_SYSTEM(World world, SystemActionDelegate method, SystemKind kind, string expr)
		{
			var systemNamePtr = Caches.AddUnmanagedString(method.Method.Name);
			Caches.AddSystemAction(world, method);
			return ecs.new_system(world, systemNamePtr, kind, expr, method);
		}

		public static EntityId ECS_SYSTEM<T1>(World world, SystemAction<T1> systemImpl, SystemKind kind) where T1 : unmanaged
		{
			SystemActionDelegate del = delegate(ref Rows rows)
			{
				var set1 = (T1*)_ecs.column(ref rows, Heap.SizeOf<T1>(), 1);
				systemImpl(ref rows, new Span<T1>(set1, (int)rows.count));
			};

			// ensure our system doesnt get GCd and that our Component is registered
			Caches.AddSystemAction(world, del);
			Caches.GetComponentTypeId<T1>(world);

			var systemNamePtr = Caches.AddUnmanagedString(systemImpl.Method.Name);
			return ecs.new_system(world, systemNamePtr, kind, typeof(T1).Name, del);
		}

		public static EntityId ECS_SYSTEM<T1, T2>(World world, SystemAction<T1, T2> systemImpl, SystemKind kind) where T1 : unmanaged where T2 : unmanaged
		{
			SystemActionDelegate del = delegate(ref Rows rows)
			{
				var set1 = (T1*)_ecs.column(ref rows, Heap.SizeOf<T1>(), 1);
				var set2 = (T2*)_ecs.column(ref rows, Heap.SizeOf<T2>(), 2);
				systemImpl(ref rows, new Span<T1>(set1, (int)rows.count), new Span<T2>(set2, (int)rows.count));
			};

			// ensure our system doesnt get GCd and that our Component is registered
			Caches.AddSystemAction(world, del);
			Caches.GetComponentTypeId<T1>(world);
			Caches.GetComponentTypeId<T2>(world);

			var systemNamePtr = Caches.AddUnmanagedString(systemImpl.Method.Name);
			return ecs.new_system(world, systemNamePtr, kind, $"{typeof(T1).Name}, {typeof(T2).Name}", del);
		}

		public static EntityId ECS_SYSTEM<T1, T2, T3>(World world, SystemAction<T1, T2, T3> systemImpl, SystemKind kind)
			where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
		{
			SystemActionDelegate del = delegate(ref Rows rows)
			{
				var set1 = (T1*)_ecs.column(ref rows, Heap.SizeOf<T1>(), 1);
				var set2 = (T2*)_ecs.column(ref rows, Heap.SizeOf<T2>(), 2);
				var set3 = (T3*)_ecs.column(ref rows, Heap.SizeOf<T3>(), 3);
				systemImpl(ref rows, new Span<T1>(set1, (int)rows.count), new Span<T2>(set2, (int)rows.count), new Span<T3>(set3, (int)rows.count));
			};

			// ensure our system doesnt get GCd and that our Component is registered
			Caches.AddSystemAction(world, del);
			Caches.GetComponentTypeId<T1>(world);
			Caches.GetComponentTypeId<T2>(world);
			Caches.GetComponentTypeId<T3>(world);

			var systemNamePtr = Caches.AddUnmanagedString(systemImpl.Method.Name);
			return ecs.new_system(world, systemNamePtr, kind, $"{typeof(T1).Name}, {typeof(T2).Name}, {typeof(T3).Name}", del);
		}

		public static void ECS_COLUMN<T>(ref Rows rows, out Span<T> column, uint columnIndex) where T : unmanaged
		{
			var set = ecs.column<T>(ref rows, columnIndex);
			column = set != null ? new Span<T>(set, (int)rows.count) : null;
		}

		public static EntityId ECS_ENTITY(World world, string id, string expr)
		{
			var idPtr = Caches.AddUnmanagedString(id);
			return ecs.new_entity(world, idPtr, expr);
		}

		public static EntityId ECS_ENTITY<T1>(World world, string id, string expr) where T1 : unmanaged
		{
			var idPtr = Caches.AddUnmanagedString(id);
			return ecs.new_entity(world, idPtr, typeof(T1).Name);
		}

		public static EntityId ECS_ENTITY<T1, T2>(World world, string id, string expr) where T1 : unmanaged where T2 : unmanaged
		{
			var idPtr = Caches.AddUnmanagedString(id);
			return ecs.new_entity(world, idPtr, $"{typeof(T1).Name}, {typeof(T2).Name}");
		}

		public static TypeId ECS_TAG(World world, string tag)
		{
			var idPtr = Caches.AddUnmanagedString(tag);
			var entityId = ecs.new_component(world, idPtr, (UIntPtr)0);
			return ecs.type_from_entity(world, entityId);
		}

		public static (EntityId, TypeId) ECS_TYPE(World world, string id, string expr)
		{
			var idPtr = Caches.AddUnmanagedString(id);
			var entityId = ecs.new_type(world, idPtr, expr);
			return (entityId, ecs.type_from_entity(world, entityId));
		}

		public static (EntityId, TypeId) ECS_PREFAB(World world, string id, string expr)
		{
			var idPtr = Caches.AddUnmanagedString(id);
			var entityId = ecs.new_prefab(world, idPtr, expr);
			var typeId = ecs.type_from_entity(world, entityId);
			return (entityId, typeId);
		}

		public static T* ECS_MODULE<T>(World world) where T : unmanaged
		{
			var typeId = ECS_COMPONENT<T>(world);
			ecs.set_singleton<T>(world);
			return (T*)ecs.get_singleton_ptr(world, typeId);
		}

		public static TypeId ECS_IMPORT(World world, string id, ModuleInitActionDelegate module, int flags)
		{
			var moduleEntityId = ecs.import(world, id, module, flags);
			return ecs.type_from_entity(world, moduleEntityId);
		}
	}
}