using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Flecs
{
	[SuppressUnmanagedCodeSecurity]
	internal unsafe static class _ecs
	{

		///<summary>
		/// Create a new entity. Entities are light-weight objects that represent "things" in the application. Entities themselves do not have any state or logic, but instead are composed out of a set of zero or more components.
		///</summary>
		///<param name="world"> [in]  The world to which to add the entity. </param>
		///<param name="type"> [in]  Zero if no type, or handle to a component, type or prefab. </param>
		///<returns>
		/// A handle to the new entity.
		///</returns>
		///<remarks>
		/// Entities are accessed through handles instead of direct pointers. Certain operations may move an entity in memory. Handles provide a safe mechanism for addressing entities.
		/// Flecs does not require applications to explicitly create handles, as entities do not have an explicit lifecycle. The new_entity operation merely provides a convenient way to dispense handles. It is guaranteed that the handle returned by new_entity has not bee returned before.
		/// ecs_new_entity ecs_new_component ecs_new_system ecs_new_prefab ecs_new_type new_child new_w_count
		///</remarks>
		///<code>
		///ecs_entity_t _ecs_new(ecs_world_t *world, ecs_type_t type)
		///</code>
		// _ecs_new: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L687
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_new", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId @new(World world, TypeId type);

		///<summary>
		/// Create new entities in a batch. This operation creates the number of specified entities with one API call which is a more efficient alternative to calling new_entity in a loop.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="type"> [in]  Zero if no type, or handle to a component, type or prefab. </param>
		///<param name="count"> [in]  The number of entities to create. </param>
		///<param name="handles_out"> [in]  An array which contains the handles of the new entities. </param>
		///<returns>
		/// The handle to the first created entity.
		///</returns>
		///<code>
		///ecs_entity_t _ecs_new_w_count(ecs_world_t *world, ecs_type_t type,
		///                              uint32_t count)
		///</code>
		// _ecs_new_w_count: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L706
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_new_w_count", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId new_w_count(World world, TypeId type, uint count);

		///<summary>
		/// Get pointer to component data. This operation obtains a pointer to the component data of an entity. If the component was not added for the specified entity, the operation will return NULL.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  Handle to the entity from which to obtain the component data. </param>
		///<param name="component"> [in]  The component to retrieve the data for. </param>
		///<returns>
		/// A pointer to the data, or NULL of the component was not found.
		///</returns>
		///<remarks>
		/// Note that the returned pointer has temporary validity. Operations such as delete and add/remove may invalidate the pointer as data is potentially moved to different locations. After one of these operations is invoked, the pointer will have to be re-obtained.
		/// This function is wrapped by the ecs_get convenience macro, which can be used like this:
		/// Foo value = ecs_get(world, e, Foo);
		///</remarks>
		///<code>
		///void *_ecs_get_ptr(ecs_world_t *world, ecs_entity_t entity, ecs_type_t type)
		///</code>
		// _ecs_get_ptr: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1084
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_get_ptr", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr get_ptr(World world, EntityId entity, TypeId type);

		///<summary>
		/// Set value of component. This function sets the value of a component on the specified entity. If the component does not yet exist, it will be added to the entity.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity on which to set the component. </param>
		///<param name="component"> [in]  The component to set.</param>
		///<remarks>
		/// This function can be used like this: Foo value = {.x = 10, .y = 20}; set_ptr(world, e, ecs_type(Foo), &value);
		/// This function is wrapped by the set convenience macro, which can be used like this:
		/// set(world, e, Foo, {.x = 10, .y = 20});
		///</remarks>
		///<code>
		///ecs_entity_t _ecs_set_ptr(ecs_world_t *world, ecs_entity_t entity,
		///                          ecs_entity_t component, size_t size, void *ptr)
		///</code>
		// _ecs_set_ptr: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1121
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_set_ptr", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId set_ptr(World world, EntityId entity, EntityId component, UIntPtr size, IntPtr ptr);

		///<code>
		///ecs_entity_t _ecs_set_singleton_ptr(ecs_world_t *, ecs_entity_t, size_t, void *)
		///</code>
		// _ecs_set_singleton_ptr: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1129
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_set_singleton_ptr", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId set_singleton_ptr(World world, EntityId component, UIntPtr size, IntPtr ptr);





		///<code>
		///ecs_chunked_t * _ecs_chunked_new(uint32_t, uint32_t, uint32_t)
		///</code>
		// _ecs_chunked_new: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/chunked.h#L11
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_chunked_new", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern Chunked chunked_new(uint elementSize, uint chunkSize, uint chunkCount);

		///<code>
		///void * _ecs_chunked_add(ecs_chunked_t *, uint32_t)
		///</code>
		// _ecs_chunked_add: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/chunked.h#L28
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_chunked_add", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr chunked_add(Chunked chunked, uint size);

		///<code>
		///void * _ecs_chunked_remove(ecs_chunked_t *, uint32_t, uint32_t)
		///</code>
		// _ecs_chunked_remove: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/chunked.h#L36
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_chunked_remove", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr chunked_remove(Chunked chunked, uint size, uint index);

		///<code>
		///void * _ecs_chunked_get(const ecs_chunked_t *, uint32_t, uint32_t)
		///</code>
		// _ecs_chunked_get: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/chunked.h#L45
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_chunked_get", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr chunked_get(Chunked chunked, uint size, uint index);

		///<code>
		///void * _ecs_chunked_get_sparse(const ecs_chunked_t *, uint32_t, uint32_t)
		///</code>
		// _ecs_chunked_get_sparse: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/chunked.h#L58
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_chunked_get_sparse", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr chunked_get_sparse(Chunked chunked, uint size, uint index);

		///<code>
		///void * _ecs_map_set(ecs_map_t *, uint64_t, const void *, uint32_t)
		///</code>
		// _ecs_map_set: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L58
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_map_set", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr map_set(Map map, ulong keyHash, IntPtr data, uint size);

		///<code>
		///bool _ecs_map_has(ecs_map_t *, uint64_t, void *, uint32_t)
		///</code>
		// _ecs_map_has: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L68
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_map_has", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool map_has(Map map, ulong keyHash, IntPtr valueOut, uint size);

		///<summary>
		/// Import a flecs module. Flecs modules enable reusing components and systems across projects. To use a module, a project needs to link with its library and include its header file.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="module"> [in]  The module to load. </param>
		///<param name="flags"> [in]  A bitmask that specifies which parts of the module to load. </param>
		///<param name="handles_out"> [in]  A struct with handles to the module components/systems.</param>
		///<remarks>
		/// The module returns a struct with handles to the loaded components / systems so they can be accessed by the application. Note that if the module is loaded in different worlds, the handles may not be the same.
		/// These naming conventions are not enforced, and projects are free to use their own conventions, though these are the conventions used by the modules provided by flecs.
		/// The load function has an additional flags argument which is passed to the module, and is intended to allow applications to select only features they require from a module. The mapping granularity of flags to components/systems is to be defined by the module.
		/// This function is wrapped by the ECS_IMPORT convenience macro:
		/// ECS_IMPORT(world, EcsComponentsTransform 0);
		/// This macro automatically creates a variable called eEcsComponentsTransform that is the struct with the handles for that component.
		///</remarks>
		///<code>
		///ecs_entity_t _ecs_import(ecs_world_t *world, ecs_module_init_action_t module,
		///                         const char *module_name, int flags, void *handles_out,
		///                         size_t handles_size)
		///</code>
		// _ecs_import: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L327
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_import", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId import(World world, ModuleInitActionDelegate module, CharPtr moduleName, int flags, IntPtr handlesOut, UIntPtr handlesSize);

		///<summary>
		/// Dimension a type for a specified number of entities. This operation will preallocate memory for a type (table) for the specified number of entites. Specifying a number lower than the current number of entities in the table will have no effect.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="type"> [in]  Handle to the type, as obtained by ecs_type_get. </param>
		///<param name="entity_count"> [in]  The number of entities to preallocate.</param>
		///<remarks>
		/// If no table exists yet for this type (when no entities have been committed for the type) it will be created, even if the entity_count is zero. This operation can thus also be used to just preallocate empty tables.
		/// If the specified type is unknown, the behavior of this function is unspecified. To ensure that the type exists, use ecs_type_get or ECS_TYPE.
		///</remarks>
		///<code>
		///void _ecs_dim_type(ecs_world_t *world, ecs_type_t type, uint32_t entity_count)
		///</code>
		// _ecs_dim_type: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L621
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_dim_type", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void dim_type(World world, TypeId type, uint entityCount);

		///<summary>
		/// Create a new child entity. Child entities are equivalent to normal entities, but can additionally be  created with a container entity. Container entities allow for the creation of entity hierarchies.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="parent"> [in]  The container to which to add the child entity. </param>
		///<param name="type"> [in]  The type with which to create the child entity. </param>
		///<returns>
		/// A handle to the created entity.
		///</returns>
		///<remarks>
		/// This function is equivalent to calling new_entity with a type that combines both the type specified in this function and the type id for the container.
		/// ecs_new_entity ecs_new_component ecs_new_system ecs_new_prefab ecs_new_type new_entity new_w_count
		///</remarks>
		///<code>
		///ecs_entity_t _ecs_new_child(ecs_world_t *world, ecs_entity_t parent,
		///                            ecs_type_t type)
		///</code>
		// _ecs_new_child: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L777
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_new_child", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId new_child(World world, EntityId parent, TypeId type);

		///<summary>
		/// Create new child entities in batch. This operation is similar to new_w_count, with as only difference that the parent is added to the type of the new entities.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="parent"> [in]  The parent. </param>
		///<param name="type"> [in]  The type to create the new entities with. </param>
		///<param name="count"> [in]  The number of entities to create.</param>
		///<code>
		///ecs_entity_t _ecs_new_child_w_count(ecs_world_t *world, ecs_entity_t parent,
		///                                    ecs_type_t type, uint32_t count)
		///</code>
		// _ecs_new_child_w_count: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L796
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_new_child_w_count", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId new_child_w_count(World world, EntityId parent, TypeId type, uint count);

		///<summary>
		/// Instantiate entity from a base entity. This operation returns a new entity that shares components with the provided  base entity.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="base"> [in]  The base entity. </param>
		///<returns>
		/// A new entity that is an instance of base.
		///</returns>
		///<code>
		///ecs_entity_t _ecs_new_instance(ecs_world_t *world, ecs_entity_t base,
		///                               ecs_type_t type)
		///</code>
		// _ecs_new_instance: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L815
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_new_instance", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId new_instance(World world, EntityId @base, TypeId type);

		///<summary>
		/// Instantiate entities from a base entity in batch. This operation returns a specified number of new entities that share  components with the provided base entity.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="base"> [in]  The base entity. </param>
		///<returns>
		/// The id to the first new entity.
		///</returns>
		///<code>
		///ecs_entity_t _ecs_new_instance_w_count(ecs_world_t *world, ecs_entity_t base,
		///                                       ecs_type_t type, uint32_t count)
		///</code>
		// _ecs_new_instance_w_count: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L833
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_new_instance_w_count", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId new_instance_w_count(World world, EntityId @base, TypeId type, uint count);

		///<summary>
		/// Add a type to an entity. This operation will add one or more components (as per the specified type) to an entity. If the entity already contains a subset of the components in the type, only components that are not contained by the entity will be added. If the entity already contains all components, this operation has no effect.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity to which to add the type. </param>
		///<param name="type"> [in]  The type to add to the entity.</param>
		///<remarks>
		/// As a result of an add operation, EcsOnAdd systems will be invoked if applicable for any of the added components.
		///</remarks>
		///<code>
		///void _ecs_add(ecs_world_t *world, ecs_entity_t entity, ecs_type_t type)
		///</code>
		// _ecs_add: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L909
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_add", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void add(World world, EntityId entity, TypeId type);

		///<summary>
		/// Remove a type from an entity. This operation will remove one or more components (as per the specified type) from an entity. If the entity contained a subset of the components in the type, only that subset will be removed. If the entity contains none of the components in the type, the operation has no effect.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity from which to remove the type. </param>
		///<param name="type"> [in]  The type to remove from the entity.</param>
		///<remarks>
		/// As a result of a remove operation, EcsOnRemove systems will be invoked if applicable for any of the removed components.
		///</remarks>
		///<code>
		///void _ecs_remove(ecs_world_t *world, ecs_entity_t entity, ecs_type_t type)
		///</code>
		// _ecs_remove: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L932
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_remove", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void remove(World world, EntityId entity, TypeId type);

		///<summary>
		/// Add and remove types from an entity. This operation is a combination of add and remove. The operation behaves as if the specified to_remove type is removed first, and  subsequently the to_add type is added. This operation is more efficient than adding/removing components separately with add/remove, as the entity is moved between tables at most once.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity from which to remove, and to which to add the types. </param>
		///<param name="to_add"> [in]  The type to add to the entity. </param>
		///<param name="to_remove"> [in]  The type to remove from the entity.</param>
		///<code>
		///void _ecs_add_remove(ecs_world_t *world, ecs_entity_t entity, ecs_type_t to_add,
		///                     ecs_type_t to_remove)
		///</code>
		// _ecs_add_remove: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L954
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_add_remove", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void add_remove(World world, EntityId entity, TypeId toAdd, TypeId toRemove);

		///<summary>
		/// Add/remove one or more components from a set of tables. This operation adds/removes one or more components from a set of tables  matching a filter. This operation is more efficient than calling add  or remove on the individual entities.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="to_add"> [in]  The components to add. </param>
		///<param name="to_remove"> [in]  The components to remove. </param>
		///<param name="filter"> [in]  Filter that matches zero or more tables.</param>
		///<remarks>
		/// If no filter is provided, the component(s) will be added/removed from all the tables in which it/they (not) occur(s).
		/// After this operation it is guaranteed that no tables matching the filter will have the components in to_remove, and similarly, all will have the components in to_add. If to_add or to_remove has multiple components and only one of the components occurs in a table, that component will be added/removed from the entities in the table.
		///</remarks>
		///<code>
		///void _ecs_add_remove_w_filter(ecs_world_t *world, ecs_type_t to_add,
		///                              ecs_type_t to_remove, ecs_type_filter_t *filter)
		///</code>
		// _ecs_add_remove_w_filter: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1054
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_add_remove_w_filter", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void add_remove_w_filter(World world, TypeId toAdd, TypeId toRemove, ref TypeFilter filter);

		///<summary>
		/// Check if entity has the specified type. This operation checks if the entity has the components associated with the specified type. It accepts component handles, families and prefabs.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  Handle to a entity. </param>
		///<param name="type"> [in]  Handle to a component, type or prefab. </param>
		///<returns>
		/// true if entity has type, otherwise false.
		///</returns>
		///<remarks>
		/// For example, if an entity has component 'Foo' and a type has 'Foo, Bar' invoking this function with the entity and type as type will return false because the entity does not have 'Bar'. Invoking the entity with the 'Bar' component, or a type that contains only 'Bar' will return true.
		///</remarks>
		///<code>
		///bool _ecs_has(ecs_world_t *world, ecs_entity_t entity, ecs_type_t type)
		///</code>
		// _ecs_has: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1166
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_has", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool has(World world, EntityId entity, TypeId type);

		///<summary>
		/// Same as has, but only returns true if entity owns the component(s).
		///</summary>
		///<code>
		///bool _ecs_has_owned(ecs_world_t *world, ecs_entity_t entity, ecs_type_t type)
		///</code>
		// _ecs_has_owned: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1177
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_has_owned", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool has_owned(World world, EntityId entity, TypeId type);

		///<summary>
		/// Check if entity has any of the components in the specified type. This operation checks if the entity has any of the components associated with the specified type. It accepts component handles, families and prefabs.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  Handle to a entity. </param>
		///<param name="type"> [in]  Handle to a component, type or prefab. </param>
		///<returns>
		/// true if entity has one of the components, otherwise false.
		///</returns>
		///<remarks>
		/// For example, if an entity has component 'Foo' and a type has 'Foo, Bar' invoking this function with the entity and type as type will return true because the entity has one of the components.
		///</remarks>
		///<code>
		///bool _ecs_has_any(ecs_world_t *world, ecs_entity_t entity, ecs_type_t type)
		///</code>
		// _ecs_has_any: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1200
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_has_any", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool has_any(World world, EntityId entity, TypeId type);

		///<summary>
		/// Same as ecs_has_any, but only returns true if entity owns the component(s).
		///</summary>
		///<code>
		///bool _ecs_has_any_owned(ecs_world_t *world, ecs_entity_t entity,
		///                        ecs_type_t type)
		///</code>
		// _ecs_has_any_owned: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1210
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_has_any_owned", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool has_any_owned(World world, EntityId entity, TypeId type);

		///<summary>
		/// Return container for component. This function allows the application to query for a container of the specified entity that has the specified component. If there are multiple containers with this component, the function will return the first one it encounters.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity for which to resolve the container. </param>
		///<param name="component"> [in]  The component which the resovled container should have.</param>
		///<code>
		///ecs_entity_t _ecs_get_parent(ecs_world_t *world, ecs_entity_t entity,
		///                             ecs_entity_t component)
		///</code>
		// _ecs_get_parent: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1254
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_get_parent", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId get_parent(World world, EntityId entity, EntityId component);

		///<summary>
		/// Returns number of entities that have a given type.  This operation will count the number of entities that have all of the components in the specified type.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="type"> [in]  The type used to match entities.</param>
		///<remarks>
		/// This operation will not reflect entities created/deleted when invoked while iterating. To get a consistent count, the function should be invoked after data from an iteration has been merged.
		///</remarks>
		///<code>
		///uint32_t _ecs_count(ecs_world_t *world, ecs_type_t type)
		///</code>
		// _ecs_count: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1324
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_count", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint count(World world, TypeId type);

		///<summary>
		/// Run system with offset/limit and type filter. This operation is the same as ecs_run, but filters the entities that will be iterated by the system.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="system"> [in]  The system to invoke. </param>
		///<param name="filter"> [in]  A component or type to filter matched entities. </param>
		///<param name="param"> [in]  A user-defined parameter to pass to the system. </param>
		///<param name="delta_time:"> [in]  The time passed since the last system invocation. </param>
		///<returns>
		/// handle to last evaluated entity if system was interrupted.
		///</returns>
		///<remarks>
		/// Entities can be filtered in two ways. Offset and limit control the range of entities that is iterated over. The range is applied to all entities matched with the system, thus may cover multiple archetypes.
		/// The type filter controls which entity types the system will evaluate. Only types that contain all components in the type filter will be iterated over. A type filter is only evaluated once per table, which makes filtering cheap if the number of entities is large and the number of tables is small, but not as cheap as filtering in the system signature.
		///</remarks>
		///<code>
		///ecs_entity_t _ecs_run_w_filter(ecs_world_t *world, ecs_entity_t system,
		///                               float delta_time, uint32_t offset,
		///                               uint32_t limit, ecs_type_t filter, void *param)
		///</code>
		// _ecs_run_w_filter: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1674
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_run_w_filter", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId run_w_filter(World world, EntityId system, float deltaTime, uint offset, uint limit, TypeId filter, IntPtr param);

		///<summary>
		/// Obtain a pointer to column data.  This function is to be used inside a system to obtain data from a column in the system signature. The provided index corresponds with the index of the element in the system signature, starting from one. For example, for the following system signature:
		///</summary>
		///<param name="rows"> [in]  The rows parameter passed into the system. </param>
		///<param name="index"> [in]  The index identifying the column in a system signature. </param>
		///<returns>
		/// A pointer to the column data if index is valid, otherwise NULL.
		///</returns>
		///<remarks>
		/// Position, Velocity
		/// Position is at index 1, and Velocity is at index 2.
		/// This function is typically invoked through the `ECS_COLUMN` macro which automates declaring a variable of the correct type in the scope of the system function.
		/// When a valid pointer is obtained, it can be used as an array with rows->count elements if the column is owned by the entity being iterated over, or as a pointer if the column is shared (see ecs_is_shared).
		///</remarks>
		///<code>
		///void *_ecs_column(ecs_rows_t *rows, size_t size, uint32_t column)
		///</code>
		// _ecs_column: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1737
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_column", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr column(ref Rows rows, UIntPtr size, uint column);

		///<summary>
		/// Obtain a single field.  This is an alternative method to column to access data in a system, which accesses data from individual fields (one column per row). This method is slower than iterating over a column array, but has the added benefit that it automatically abstracts between shared components and owned components.
		///</summary>
		///<remarks>
		/// This is particularly useful if a system is unaware whether a particular  column is from a prefab, as a system does not explicitly state in an argument expression whether prefabs should be matched with, thus it is possible that a system receives both shared and non-shared data for the same column.
		/// When a system uses fields, these differences will be transparent, and it is therefore the method that provides the most flexibility with respect to the kind of data the system can accept.
		///</remarks>
		///<code>
		///void *_ecs_field(ecs_rows_t *rows, size_t size, uint32_t column, uint32_t row)
		///</code>
		// _ecs_field: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1787
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_field", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr field(ref Rows rows, UIntPtr size, uint column, uint row);

		///<summary>
		/// Abort
		///</summary>
		///<code>
		///void _ecs_abort(uint32_t error_code, const char *param, const char *file,
		///                uint32_t line)
		///</code>
		// _ecs_abort: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L2045
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_abort", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void abort(uint errorCode, string param, string file, uint line);

		///<summary>
		/// Assert
		///</summary>
		///<code>
		///void _ecs_assert(bool condition, uint32_t error_code, const char *param,
		///                 const char *condition_str, const char *file, uint32_t line)
		///</code>
		// _ecs_assert: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L2053
		[DllImport(ecs.NativeLibName, EntryPoint = "_ecs_assert", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void assert(bool condition, uint errorCode, string param, string conditionStr, string file, uint line);
	}
}

