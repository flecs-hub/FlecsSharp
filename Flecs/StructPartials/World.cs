using System;
using System.Runtime.CompilerServices;

namespace Flecs
{
	unsafe partial struct World : IDisposable
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static World Create()
		{
			var world = ecs.init();
			Caches.RegisterWorld(world);

			return world;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			Caches.DeregisterWorld(this);
			ecs.fini(this);
		}

		public void GetStats(out WorldStats stats) => ecs.get_stats(this, out stats);

		public void MeasureFrameTime(bool enable) => ecs.measure_frame_time(this, enable);

		public void MeasureSystemTime(bool enable) => ecs.measure_system_time(this, enable);

		public int Fini() => ecs.fini(this);

		public void Quit() => ecs.quit(this);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Progress(float deltaTime) => ecs.progress(this, deltaTime);

		public void SetContext(IntPtr ctx) => ecs.set_context(this, ctx);

		public IntPtr GetContext() => ecs.get_context(this);

		#region Entity Management

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId New(TypeId type) => _ecs.@new(this, type);

		///<summary>
		/// Convenience function to create an entity with id and component expression. This is equivalent to calling ecs_new with a type that contains all  components provided in the 'component' expression. In addition, this function also adds the EcsId component, which will be set to the provided id string.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewEntity(CharPtr id, CharPtr components) => ecs.new_entity(this, id, components);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewWCount(TypeId type, uint count) => _ecs.new_w_count(this, type, count);

		///<summary>
		/// Create a new child entity. Child entities are equivalent to normal entities, but can additionally be  created with a container entity. Container entities allow for the creation of entity hierarchies.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewChild(EntityId parent, TypeId type) => _ecs.new_child(this, parent, type);

		///<summary>
		/// Create new child entities in batch. This operation is similar to ecs_new_w_count, with as only difference that the parent is added to the type of the new entities.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewChildWCount(EntityId parent, TypeId type, uint count) => _ecs.new_child_w_count(this, parent, type, count);

		///<summary>
		/// Instantiate entity from a base entity. This operation returns a new entity that shares components with the provided  base entity.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewInstance(EntityId @base, TypeId type) => _ecs.new_instance(this, @base, type);

		///<summary>
		/// Instantiate entities from a base entity in batch. This operation returns a specified number of new entities that share  components with the provided base entity.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewInstanceWCount(EntityId @base, TypeId type, uint count) => _ecs.new_instance_w_count(this, @base, type, count);

		///<summary>
		/// Create new entity with same components as specified entity. This operation creates a new entity which has the same components as the specified entity. This includes prefabs and entity-components (entities to which the EcsComponent component has been added manually).
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId Clone(EntityId entity, bool copyValue) => ecs.clone(this, entity, copyValue);

		///<summary>
		/// Delete components for an entity. This operation will delete all components from the specified entity. As entities in Flecs do not have an explicit lifecycle, this operation will not invalidate the entity id.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Delete(EntityId entity) => ecs.delete(this, entity);

		///<summary>
		/// Delete all entities containing a (set of) component(s).  This operation provides a more efficient alternative to deleting entities one by one by deleting an entire table or set of tables in a single operation. The operation will clear all tables that match the specified table.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DeleteWFilter(ref TypeFilter filter) => ecs.delete_w_filter(this, ref filter);

		///<summary>
		/// Add a type to an entity. This operation will add one or more components (as per the specified type) to an entity. If the entity already contains a subset of the components in the type, only components that are not contained by the entity will be added. If the entity already contains all components, this operation has no effect.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add(EntityId entity, TypeId type) => _ecs.add(this, entity, type);

		///<summary>
		/// Remove a type from an entity. This operation will remove one or more components (as per the specified type) from an entity. If the entity contained a subset of the components in the type, only that subset will be removed. If the entity contains none of the components in the type, the operation has no effect.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Remove(EntityId entity, TypeId type) => _ecs.remove(this, entity, type);

		///<summary>
		/// Add and remove types from an entity. This operation is a combination of ecs_add and ecs_remove. The operation behaves as if the specified to_remove type is removed first, and  subsequently the to_add type is added. This operation is more efficient than adding/removing components separately with ecs_add/ecs_remove, as the entity is moved between tables at most once.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddRemove(EntityId entity, TypeId toAdd, TypeId toRemove) => _ecs.add_remove(this, entity, toAdd, toRemove);

		///<summary>
		/// Adopt a child entity by a parent. This operation adds the specified parent entity to the type of the specified entity, which effectively establishes a parent-child relationship. The parent entity, when added, behaves like a normal component in that it becomes part of the entity type.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Adopt(EntityId entity, EntityId parent) => ecs.adopt(this, entity, parent);

		///<summary>
		/// Orphan a child by a parent.  This operation removes the specified parent entity from the type of the specified entity. If the parent was not added to the entity, this operation has no effect.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Orphan(EntityId entity, EntityId parent) => ecs.orphan(this, entity, parent);

		///<summary>
		/// Inherit from a base. This operation adds a base to an entity, which will cause the entity to inherit the components of the base. If the entity already inherited from the specified base, this operation does nothing.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Inherit(EntityId entity, EntityId @base) => ecs.inherit(this, entity, @base);

		///<summary>
		/// Disinherit from a base. This operation removes a base from an entity, which will cause the entity to no longer inherit the components of the base. If the entity did not inherit from the specified base, this operation does nothing.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Disinherit(EntityId entity, EntityId @base) => ecs.disinherit(this, entity, @base);

		///<summary>
		/// Add/remove one or more components from a set of tables. This operation adds/removes one or more components from a set of tables  matching a filter. This operation is more efficient than calling ecs_add  or ecs_remove on the individual entities.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddRemoveWFilter(TypeId toAdd, TypeId toRemove, ref TypeFilter filter) => _ecs.add_remove_w_filter(this, toAdd, toRemove, ref filter);

		///<summary>
		/// Get pointer to component data. This operation obtains a pointer to the component data of an entity. If the component was not added for the specified entity, the operation will return NULL.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr GetPtr(EntityId entity, TypeId type) => _ecs.get_ptr(this, entity, type);

		///<summary>
		/// Set value of component. This function sets the value of a component on the specified entity. If the component does not yet exist, it will be added to the entity.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId SetPtr(EntityId entity, EntityId component, UIntPtr size, IntPtr ptr) => _ecs.set_ptr(this, entity, component, size, ptr);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId SetSingletonPtr(EntityId component, UIntPtr size, IntPtr ptr) => _ecs.set_singleton_ptr(this, component, size, ptr);

		///<summary>
		/// Check if entity has the specified type. This operation checks if the entity has the components associated with the specified type. It accepts component handles, families and prefabs.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Has(EntityId entity, TypeId type) => _ecs.has(this, entity, type);

		///<summary>
		/// Same as ecs_has, but only returns true if entity owns the component(s).
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasOwned(EntityId entity, TypeId type) => _ecs.has_owned(this, entity, type);

		///<summary>
		/// Check if entity has any of the components in the specified type. This operation checks if the entity has any of the components associated with the specified type. It accepts component handles, families and prefabs.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasAny(EntityId entity, TypeId type) => _ecs.has_any(this, entity, type);

		///<summary>
		/// Same as ecs_has_any, but only returns true if entity owns the component(s).
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasAnyOwned(EntityId entity, TypeId type) => _ecs.has_any_owned(this, entity, type);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasEntity(EntityId entity, EntityId component) => ecs.has_entity(this, entity, component);

		///<summary>
		/// Check if parent entity contains child entity. This function tests if the specified parent entity has been added to the specified child entity.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Contains(EntityId parent, EntityId child) => ecs.contains(this, parent, child);

		///<summary>
		/// Return container for component. This function allows the application to query for a container of the specified entity that has the specified component. If there are multiple containers with this component, the function will return the first one it encounters.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId GetParent(EntityId entity, EntityId component) => _ecs.get_parent(this, entity, component);

		///<summary>
		/// Get type of entity. This operation returns the entity type, which is a handle to the a list of the current components an entity has.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId GetType(EntityId entity) => ecs.get_type(this, entity);

		///<summary>
		/// Return the entity id. This returns the string identifier of an entity, if the entity has the EcsId component. By default, all component, type, system and prefab entities add the EcsId component if they have been created with the ecs_new_* functions.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CharPtr GetId(EntityId entity) => ecs.get_id(this, entity);

		///<summary>
		/// Return if the entity is empty. This returns whether the provided entity handle is empty. An entity that is empty has no components.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsEmpty(EntityId entity) => ecs.is_empty(this, entity);

		///<summary>
		/// Lookup an entity by id. This operation is a convenient way to lookup entities by string identifier that have the EcsId component. It is recommended to cache the result of this function, as the function must iterates over all entities and all components in an entity.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId Lookup(string id) => ecs.lookup(this, id);

		///<summary>
		/// Lookup child of parent by id. This operation is the same as ecs_lookup, except for that it only searches entities that are children of the specified parent.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId LookupChild(EntityId parent, string id) => ecs.lookup_child(this, parent, id);

		#endregion

		#region Type Management

		///<summary>
		/// Get handle to type. This operation obtains a handle to a type that can be used with ecs_new. Predefining types has performance benefits over using ecs_add/ecs_remove multiple times, as it provides constant creation time regardless of the number of components. This function will internally create a table for the type.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewType(CharPtr id, string components) => ecs.new_type(this, id, components);

		///<summary>
		/// Get a type from an entity. This function returns a type that can be added/removed to entities. If you create a new component, type or prefab with the ecs_new_* function, you get an ecs_entity_t handle which provides access to builtin components associated with the component, type or prefab.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId TypeFromEntity(EntityId entity) => ecs.type_from_entity(this, entity);

		///<summary>
		/// Get an entity from a type. This function is the reverse of ecs_type_from_entity. It only works for types that contain exactly one entity.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId TypeToEntity(TypeId type) => ecs.type_to_entity(this, type);

		///<summary>
		/// Find or create type from existing type and entity.  This operation adds the specified entity to the specified type, and returns a new or existing type that is a union of the specified type and entity. The provided type will not be altered.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId TypeAdd(TypeId type, EntityId entity) => ecs.type_add(this, type, entity);

		///<summary>
		/// Find or create type that is the union of two types.  This operation will return a type that contains exactly the components in the specified type, plus the components in type_add, and not the components in type_remove.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId TypeMerge(TypeId type, TypeId typeAdd, TypeId typeRemove) => ecs.type_merge(this, type, typeAdd, typeRemove);

		///<summary>
		/// Find or create type from entity array. This operation will return a type that contains the entities in the specified array. If a type with the specified entities already exists, it will be returned, otherwise a new type will be created.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId TypeFind(out EntityId array, uint count) => ecs.type_find(this, out array, count);

		///<summary>
		/// Get component from type at index.  This operation returns the components (or entities) that are contained in the type at the specified index.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId TypeGetEntity(TypeId type, uint index) => ecs.type_get_entity(this, type, index);

		///<summary>
		/// Check if type has entity. This operation returns whether a type has a specified entity.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TypeHasEntity(TypeId type, EntityId entity) => ecs.type_has_entity(this, type, entity);

		///<summary>
		/// Get type from type expression. This function obtains a type from a type expression. A type expression is a comma-deliminated list of the type's entity identifiers. For example, a type with entities Position and Velocity is: "Position, Velocity".
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId ExprToType(string expr) => ecs.expr_to_type(this, expr);

		///<summary>
		/// Get type expression from type.  This function converts a type to a type expression, which is a string representation of the type as it is provided to the ecs_new_entity and ecs_new_type functions. For more information on type expressions, see  ecs_expr_to_type.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CharPtr TypeToExpr(TypeId type) => ecs.type_to_expr(this, type);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TypeMatchWFilter(TypeId type, out TypeFilter filter) => ecs.type_match_w_filter(this, type, out filter);

		///<summary>
		/// Enable or disable a system. This operation enables or disables a system. A disabled system will not be ran during ecs_progress or when components must be initialized or deinitialized. Systems are enabled by default.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Enable(EntityId system, bool enabled) => ecs.enable(this, system, enabled);

		///<summary>
		/// Configure how often a system should be invoked. This operation lets an application control how often a system should be invoked. The provided period is the minimum interval between two invocations.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetPeriod(EntityId system, float period) => ecs.set_period(this, system, period);

		///<summary>
		/// Returns the enabled status for a system / entity. This operation will return whether a system is enabled or disabled. Currently only systems can be enabled or disabled, but this operation does not fail when a handle to an entity is provided that is not a system. If this operation is called on a non-system entity, the operation will return true.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsEnabled(EntityId system) => ecs.is_enabled(this, system);

		///<summary>
		/// Run a specific system manually. This operation runs a single system manually. It is an efficient way to invoke logic on a set of entities, as manual systems are only matched to tables at creation time or after creation time, when a new table is created.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId Run(EntityId system, float deltaTime, IntPtr param) => ecs.run(this, system, deltaTime, param);

		///<summary>
		/// Run system with offset/limit and type filter. This operation is the same as ecs_run, but filters the entities that will be iterated by the system.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId RunWFilter(EntityId system, float deltaTime, uint offset, uint limit, TypeId filter, IntPtr param) => _ecs.run_w_filter(this, system, deltaTime, offset, limit, filter, param);

		///<summary>
		/// Set system context. This operation allows an application to register custom data with a system. This data can be accessed using the ecs_get_system_context operation, or through the 'param' field in the ecs_rows_t parameter passed into the system callback.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetSystemContext(EntityId system, IntPtr ctx) => ecs.set_system_context(this, system, ctx);

		///<summary>
		/// Get system context. Get custom data from a system previously set with ecs_set_system_context.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr GetSystemContext(EntityId system) => ecs.get_system_context(this, system);

		#endregion

		///<summary>
		/// Create a new component. This operation creates a new component with a specified id and size. After this operation is called, the component can be added to entities by using the returned handle with ecs_add.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewComponent(CharPtr id, UIntPtr size) => ecs.new_component(this, id, size);

		///<summary>
		/// Create a new system. This operation creates a new system with a specified id, kind and action. After this operation is called, the system will be active. Systems can be created with three different kinds:
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewSystem(CharPtr id, SystemKind kind, string sig, SystemActionDelegate action) => ecs.new_system(this, id, kind, sig, action);

		///<summary>
		/// Create a new prefab entity. Prefab entities allow entities to share a set of components. Components of the prefab will appear on the specified entity when using any of the API functions and ECS systems.
		///</summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewPrefab(CharPtr id, string sig) => ecs.new_prefab(this, id, sig);
	}
}