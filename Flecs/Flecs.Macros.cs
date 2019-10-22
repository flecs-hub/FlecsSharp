using System;
using System.Runtime.InteropServices;


namespace Flecs
{
	public delegate void SystemAction<T>(ref Rows ids, Span<T> comp) where T : unmanaged;
	public delegate void SystemAction<T1, T2>(ref Rows ids, Span<T1> comp1, Span<T2> comp2) where T1 : unmanaged where T2 : unmanaged;
	public delegate void SystemAction<T1, T2, T3>(ref Rows ids, Span<T1> comp1, Span<T2> comp2, Span<T3> comp3) where T1 : unmanaged where T2 : unmanaged;

	public unsafe static partial class ecs
	{
		#region Imperitive Macros

		public static EntityId ecs_new(World world, TypeId typeId) => _ecs.@new(world, typeId);

		public static EntityId ecs_new<T>(World world) where T : unmanaged
			=> _ecs.@new(world, Caches.GetComponentTypeId(world, typeof(T)));

		public static EntityId ecs_new(World world, Type componentType) => _ecs.@new(world, Caches.GetComponentTypeId(world, componentType));

		public static bool ecs_has(World world, EntityId entity, TypeId typeId)
		{
			return _ecs.has(world, entity, typeId);
		}

		public static bool ecs_has(World world, EntityId entity, Type componentType)
		{
			return _ecs.has(world, entity, Caches.GetComponentTypeId(world, componentType));
		}

		public static bool ecs_has<T>(World world, EntityId entity) where T : unmanaged
		{
			return _ecs.has(world, entity, Caches.GetComponentTypeId<T>(world));
		}

		public static EntityId ecs_new_w_count(World world, TypeId typeId, uint count)
		{
			return _ecs.new_w_count(world, typeId, count);
		}

		public static EntityId ecs_new_child(World world, EntityId parent, TypeId type)
		{
			return _ecs.new_child(world, parent, type);
		}

		public static EntityId ecs_new_child_w_count(World world, EntityId parent, TypeId type, uint count)
		{
			return _ecs.new_child_w_count(world, parent, type, count);
		}

		public static EntityId ecs_new_instance(World world, EntityId baseEntityId, TypeId type)
		{
			return _ecs.new_instance(world, baseEntityId, type);
		}

		public static EntityId ecs_new_instance_w_count(World world, EntityId baseEntityId, TypeId type, uint count)
		{
			return _ecs.new_instance_w_count(world, baseEntityId, type, count);
		}

		public static T* ecs_column<T>(ref Rows rows, uint columnIndex) where T : unmanaged
		{
			return (T*)_ecs.column(ref rows, (UIntPtr)Marshal.SizeOf<T>(), columnIndex);
		}

		public static EntityId ecs_set<T>(World world, EntityId entity, T value = default) where T : unmanaged
		{
			var type = Caches.GetComponentTypeId<T>(world);
			T* val = &value;
			return _ecs.set_ptr(world, entity, ecs.type_to_entity(world, type), (UIntPtr)Marshal.SizeOf<T>(), (IntPtr)val);
		}

		public static EntityId ecs_set_ptr<T>(World world, EntityId entity, T* value) where T : unmanaged
		{
			var type = Caches.GetComponentTypeId<T>(world);
			return _ecs.set_ptr(world, entity, ecs.type_to_entity(world, type), (UIntPtr)Marshal.SizeOf<T>(), (IntPtr)value);
		}

		public static IntPtr ecs_get_ptr(World world, EntityId entity, TypeId type) => _ecs.get_ptr(world, entity, type);

		public static T* ecs_get_ptr<T>(World world, EntityId entity, TypeId type) where T : unmanaged
			=> (T*)_ecs.get_ptr(world, entity, type);

		public static EntityId ecs_set_singleton<T>(World world, T value = default) where T : unmanaged
		{
			var componentType = ecs.ECS_COMPONENT<T>(world);
			var componentEntityId = ecs.type_to_entity(world, componentType);
			T* val = &value;
			return _ecs.set_singleton_ptr(world, componentEntityId, (UIntPtr)Marshal.SizeOf<T>(), (IntPtr)val);
		}

		public static EntityId ecs_set_singleton_ptr<T>(World world, T* value) where T : unmanaged
		{
			var componentType = ecs.ECS_COMPONENT<T>(world);
			var componentEntityId = ecs.type_to_entity(world, componentType);
			return _ecs.set_singleton_ptr(world, componentEntityId, (UIntPtr)Marshal.SizeOf<T>(), (IntPtr)value);
		}

		public static IntPtr ecs_get_singleton_ptr(World world, TypeId type) => _ecs.get_ptr(world, ECS_SINGLETON, type);

		public static T* ecs_get_singleton_ptr<T>(World world, TypeId type) where T : unmanaged
				=> (T*)_ecs.get_ptr(world, ECS_SINGLETON, type);

		public static void ecs_add(World world, EntityId entity, TypeId type) => _ecs.add(world, entity, type);

		public static void ecs_add<T>(World world, EntityId entity) where T : unmanaged
			=> _ecs.add(world, entity, Caches.GetComponentTypeId<T>(world));

		public static void ecs_remove(World world, EntityId entity, TypeId type) => _ecs.remove(world, entity, type);

		public static void ecs_remove<T>(World world, EntityId entity) where T : unmanaged
			=> _ecs.remove(world, entity, Caches.GetComponentTypeId<T>(world));

		public static void ecs_add_remove(World world, EntityId entity, TypeId typeToAdd, TypeId typeToRemove)
			=> _ecs.add_remove(world, entity, typeToAdd, typeToRemove);

		#endregion

		#region Declarative Macros

		public static TypeId ECS_COMPONENT(World world, Type componentType) => Caches.GetComponentTypeId(world, componentType);

		public static TypeId ECS_COMPONENT<T>(World world) where T : unmanaged => Caches.GetComponentTypeId<T>(world);

		public static EntityId ECS_SYSTEM(World world, SystemActionDelegate method, SystemKind kind, string expr)
		{
			var systemNamePtr = Caches.AddUnmanagedString(method.Method.Name);
			var signaturePtr = Caches.AddUnmanagedString(expr);
			Caches.AddSystemAction(world, method);

			return ecs.new_system(world, systemNamePtr, kind, signaturePtr, method);
		}

		public static EntityId ECS_SYSTEM<T1>(World world, SystemAction<T1> systemImpl, SystemKind kind) where T1 : unmanaged
		{
			SystemActionDelegate del = delegate(ref Rows rows)
			{
				var set1 = (T1*)_ecs.column(ref rows, (UIntPtr)Marshal.SizeOf<T1>(), 1);
				systemImpl(ref rows, new Span<T1>(set1, (int)rows.count));
			};

			// ensure our system doesnt get GCd and that our Component is registered
			Caches.AddSystemAction(world, del);
			Caches.GetComponentTypeId<T1>(world);

			var systemNamePtr = Caches.AddUnmanagedString(systemImpl.Method.Name);
			var signaturePtr = Caches.AddUnmanagedString(typeof(T1).Name);
			return ecs.new_system(world, systemNamePtr, kind, signaturePtr, del);
		}

		public static EntityId ECS_SYSTEM<T1, T2>(World world, SystemAction<T1, T2> systemImpl, SystemKind kind) where T1 : unmanaged where T2 : unmanaged
		{
			SystemActionDelegate del = delegate(ref Rows rows)
			{
				var set1 = (T1*)_ecs.column(ref rows, (UIntPtr)Marshal.SizeOf<T1>(), 1);
				var set2 = (T2*)_ecs.column(ref rows, (UIntPtr)Marshal.SizeOf<T2>(), 2);
				systemImpl(ref rows, new Span<T1>(set1, (int)rows.count), new Span<T2>(set2, (int)rows.count));
			};

			// ensure our system doesnt get GCd and that our Component is registered
			Caches.AddSystemAction(world, del);
			Caches.GetComponentTypeId<T1>(world);
			Caches.GetComponentTypeId<T2>(world);

			var systemNamePtr = Caches.AddUnmanagedString(systemImpl.Method.Name);
			var signaturePtr = Caches.AddUnmanagedString($"{typeof(T1).Name}, {typeof(T2).Name}");
			return ecs.new_system(world, systemNamePtr, kind, signaturePtr, del);
		}

		public static EntityId ECS_SYSTEM<T1, T2, T3>(World world, SystemAction<T1, T2, T3> systemImpl, SystemKind kind)
			where T1 : unmanaged where T2 : unmanaged where T3 : unmanaged
		{
			SystemActionDelegate del = delegate(ref Rows rows)
			{
				var set1 = (T1*)_ecs.column(ref rows, (UIntPtr)Marshal.SizeOf<T1>(), 1);
				var set2 = (T2*)_ecs.column(ref rows, (UIntPtr)Marshal.SizeOf<T2>(), 2);
				var set3 = (T3*)_ecs.column(ref rows, (UIntPtr)Marshal.SizeOf<T3>(), 3);
				systemImpl(ref rows, new Span<T1>(set1, (int)rows.count), new Span<T2>(set2, (int)rows.count), new Span<T3>(set3, (int)rows.count));
			};

			// ensure our system doesnt get GCd and that our Component is registered
			Caches.AddSystemAction(world, del);
			Caches.GetComponentTypeId<T1>(world);
			Caches.GetComponentTypeId<T2>(world);

			var systemNamePtr = Caches.AddUnmanagedString(systemImpl.Method.Name);
			var signaturePtr = Caches.AddUnmanagedString($"{typeof(T1).Name}, {typeof(T2).Name}");
			return ecs.new_system(world, systemNamePtr, kind, signaturePtr, del);
		}

		public static void ECS_COLUMN<T>(ref Rows rows, out Span<T> column, uint columnIndex) where T : unmanaged
		{
			var set = ecs_column<T>(ref rows, columnIndex);
			column = new Span<T>(set, (int)rows.count);
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

		public static TypeId ECS_TYPE(World world, string id, string expr)
		{
			var idPtr = Caches.AddUnmanagedString(id);
			var entityId = ecs.new_type(world, idPtr, expr);
			return ecs.type_from_entity(world, entityId);
		}

		public static void ECS_PREFAB(World world)
		{
//#define ECS_PREFAB(world, id, ...) \
//			ecs_entity_t id = ecs_new_prefab(world, #id, #__VA_ARGS__);\
//    ECS_TYPE_VAR(id) = ecs_type_from_entity(world, id);\
//    (void)id;\
//    (void)ecs_type(id);\
		}

		#endregion

		#region Module Imperitive Macros

		public static EntityId ecs_import(World world, string id, ModuleInitActionDelegate module, int flags)
		{
			var moduleNamePtr = Caches.AddUnmanagedString(id);
			return _ecs.import(world, module, moduleNamePtr, flags, (IntPtr)0, (UIntPtr)0);
			//_ecs_import(world, module##Import, #module, flags, handles_out, sizeof(module))
		}

		#endregion

		#region Module Declarative Macros

		public static T* ECS_MODULE<T>(World world) where T : unmanaged
		{
			var typeId = ECS_COMPONENT<T>(world);
			ecs.ecs_set_singleton<T>(world);
			return (T*)ecs.ecs_get_singleton_ptr(world, typeId);
		}

		public static TypeId ECS_IMPORT(World world, string id, ModuleInitActionDelegate module, int flags)
		{
			var moduleEntityId = ecs_import(world, id, module, flags);
			return ecs.type_from_entity(world, moduleEntityId);
		}

		#endregion
	}
}
