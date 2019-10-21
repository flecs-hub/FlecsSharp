using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Flecs
{
	public delegate void SystemAction<T>(ref Rows ids, Span<T> comp) where T : unmanaged;
	public delegate void SystemAction<T1, T2>(ref Rows ids, Span<T1> comp1, Span<T2> comp2) where T1 : unmanaged where T2 : unmanaged;

	unsafe partial struct World : IDisposable
	{
		static Dictionary<World, Dictionary<Type, TypeId>> typeMap = new Dictionary<World, Dictionary<Type, TypeId>>();
		static Dictionary<World, Dictionary<string, TypeId>> tagTypeMap = new Dictionary<World, Dictionary<string, TypeId>>();
		static Dictionary<World, List<SystemActionDelegate>> systemActions = new Dictionary<World, List<SystemActionDelegate>>();


		public struct ContextData
		{
			internal DynamicBuffer stringBuffer;
		}

		ContextData* ctx => (ContextData*)ecs.get_context(this);
		public DynamicBuffer StringBuffer => ctx->stringBuffer;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static World Create()
		{
			var world = ecs.init();
			typeMap[world] = new Dictionary<Type, TypeId>();
			tagTypeMap[world] = new Dictionary<string, TypeId>();
			systemActions[world] = new List<SystemActionDelegate>();

			var context = Heap.Alloc<ContextData>();
			context->stringBuffer = DynamicBuffer.Create(4096 * 100);
			ecs.set_context(world, (IntPtr)context);

			return world;
		}

		///<summary>
		/// Delete a world. This operation deletes the world, and all entities, components and systems within the world.
		///</summary>
		///<code>
		///int ecs_fini(ecs_world_t *world)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			StringBuffer.Dispose();
			Heap.Free(ctx);
			ecs.fini(this);
		}

		//public EntityId NewComponent<T>() where T : unmanaged
		//{
		//    var name = typeof(T).Name;
		//    var charPtr = StringBuffer.AddUTF8String(name);
		//    var componentId = ecs.new_component(this, charPtr, (UIntPtr)Marshal.SizeOf<T>());
		//    typeMap.Add((this, typeof(T)), TypeFromEntity(componentId));
		//    return componentId;
		//}

		internal TypeId GetComponentTypeId(Type compType)
		{
			if (!typeMap[this].TryGetValue(compType, out var val))
			{
				var charPtr = StringBuffer.AddString(compType.Name);
				var entityId = ecs.new_component(this, charPtr, (UIntPtr)Marshal.SizeOf(compType));
				var typeId = ecs.type_from_entity(this, entityId);
				typeMap[this][compType] = typeId;
				return typeId;
			}

			return val;
		}

		internal TypeId GetTagTypeId(string tag)
		{
			if (!tagTypeMap[this].TryGetValue(tag, out var val))
			{
				var charPtr = StringBuffer.AddString(tag);
				var entityId = ecs.new_component(this, charPtr, (UIntPtr)0);
				var typeId = ecs.type_from_entity(this, entityId);
				tagTypeMap[this][tag] = typeId;
				return typeId;
			}

			return val;
		}

		public EntityId AddSystem(SystemKind kind, string name, SystemActionDelegate systemImpl, params Type[] componentTypes)
		{
			var systemNamePtr = StringBuffer.AddString(name);
			var components = BuildComponentQuery(componentTypes);
			var signaturePtr = StringBuffer.AddString(components);
			var entityId = ecs.new_system(this, systemNamePtr, kind, signaturePtr, systemImpl);
			systemActions[this].Add(systemImpl);
			return entityId;
		}

		public EntityId AddSystem(SystemKind kind, SystemActionDelegate systemImpl, params Type[] componentTypes)
			=> AddSystem(kind, systemImpl.Method.Name, systemImpl, componentTypes);


		public void AddSystem<T1>(SystemAction<T1> systemImpl, SystemKind kind) where T1 : unmanaged
		{
			SystemActionDelegate del = delegate(ref Rows rows)
			{
				var set1 = (T1*)_ecs.column(ref rows, (UIntPtr)Marshal.SizeOf<T1>(), 1);
				systemImpl(ref rows, new Span<T1>(set1, (int)rows.count));
			};

			AddSystem(kind, systemImpl.Method.Name, del, typeof(T1));
		}

		public void AddSystem<T1, T2>(SystemAction<T1, T2> systemImpl, SystemKind kind)
			where T1 : unmanaged
			where T2 : unmanaged
		{
			SystemActionDelegate del = delegate(ref Rows rows)
			{
				var wtf = (UIntPtr)Marshal.SizeOf<T1>();
				var ftw = new UIntPtr((uint)sizeof(T1));

				var set1 = (T1*)_ecs.column(ref rows, (UIntPtr)Marshal.SizeOf<T1>(), 1);
				var set2 = (T2*)_ecs.column(ref rows, (UIntPtr)Marshal.SizeOf<T2>(), 2);
				systemImpl(ref rows, new Span<T1>(set1, (int)rows.count), new Span<T2>(set2, (int)rows.count));
			};

			AddSystem(kind, systemImpl.Method.Name, del, typeof(T1), typeof(T2));
		}


		private string BuildComponentQuery(params Type[] componentTypes)
		{
			var sb = new StringBuilder(64);
			for (int i = 0; i < componentTypes.Length; i++)
			{
				GetComponentTypeId(componentTypes[i]);
				sb.Append(componentTypes[i].Name);

				if (i != componentTypes.Length - 1)
					sb.Append(", ");
			}

			var components = sb.ToString();
			return components;
		}

		EntityId NewEntity(string entityName, params Type[] componentTypes)
		{
			var entityNamePtr = StringBuffer.AddString(entityName);
			var components = BuildComponentQuery(componentTypes);
			var entityId = ecs.new_entity(this, entityNamePtr, components);
			return entityId;
		}

		EntityId NewEntity(params Type[] componentTypes)
		{
			var components = BuildComponentQuery(componentTypes);
			var componentsQueryPtr = StringBuffer.AddString(components);
			var typeId = ecs.expr_to_type(this, componentsQueryPtr);
			return _ecs.@new(this, typeId);
		}

		EntityId NewEntitiesWithCount(uint count, params Type[] componentTypes)
		{
			var components = BuildComponentQuery(componentTypes);
			var componentsQueryPtr = StringBuffer.AddString(components);
			var typeId = ecs.expr_to_type(this, componentsQueryPtr);
			return _ecs.new_w_count(this, typeId, count);
		}

		public EntityId NewEntity<T1>() where T1 : unmanaged
		{
			var typeId = GetComponentTypeId(typeof(T1));
			return _ecs.@new(this, typeId);
		}

		public EntityId NewEntitiesWithCount<T1, T2>(uint count) where T1 : unmanaged where T2 : unmanaged
			=> NewEntitiesWithCount(count, typeof(T1), typeof(T2));

		public EntityId NewEntitiesWithCount<T1>(uint count) where T1 : unmanaged
		{
			var typeId = GetComponentTypeId(typeof(T1));
			return _ecs.new_w_count(this, typeId, count);
		}

		public EntityId NewEntity<T1, T2>() where T1 : unmanaged where T2 : unmanaged => NewEntity(typeof(T1), typeof(T2));

		public EntityId NewEntity<T1>(T1 comp1 = default) where T1 : unmanaged
		{
			var entityId = NewEntity<T1>();
			Set(entityId, comp1);
			return entityId;
		}

		public EntityId NewEntity<T1, T2>(T1 comp1 = default, T2 comp2 = default) where T1 : unmanaged where T2 : unmanaged
		{
			var entityId = NewEntity(typeof(T1), typeof(T2));
			Set(entityId, comp1);
			Set(entityId, comp2);
			return entityId;
		}

		public unsafe EntityId NewEntity<T1>(string entityName, T1 comp1 = default) where T1 : unmanaged
		{
			var entt = NewEntity(entityName, typeof(T1));
			Set(entt, comp1);
			return entt;
		}

		public unsafe EntityId NewEntity<T1, T2>(string entityName, T1 comp1 = default, T2 comp2 = default) where T1 : unmanaged where T2 : unmanaged
		{
			var entt = NewEntity(entityName, typeof(T1), typeof(T2));
			Set(entt, comp1);
			Set(entt, comp2);
			return entt;
		}

		///<summary>
		/// Set value of component. This function sets the value of a component on the specified entity. If the component does not yet exist, it will be added to the entity.
		///</summary>
		///<param name="entity"> [in]  The entity on which to set the component. </param>
		///<param name="value"> [in]  The component to set.</param>
		///<remarks>
		/// This function can be used like this: Foo value = {.x = 10, .y = 20}; ecs_set_ptr(world, e, tFoo, &value);
		/// This function is wrapped by the ecs_set convenience macro, which can be used like this:
		/// ecs_set(world, e, Foo, {.x = 10, .y = 20});
		///</remarks>
		///<code>
		///ecs_entity_t _ecs_set_ptr(ecs_world_t *world, ecs_entity_t entity,
		///                          ecs_type_t type, size_t size, void *ptr)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId Set<T>(EntityId entity, T value) where T : unmanaged
		{
			var type = GetComponentTypeId(typeof(T));
			T* val = &value;
			return _ecs.set_ptr(this, entity, ecs.type_to_entity(this, type), (UIntPtr)Marshal.SizeOf<T>(), (IntPtr)val);
		}

		public void Add<T>(EntityId entity)
		{
			_ecs.add(this, entity, GetComponentTypeId(typeof(T)));
		}
	}
}