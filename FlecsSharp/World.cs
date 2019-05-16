using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    unsafe partial struct World
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetStats(WorldStats stats)
        {
            ecs.get_stats( this, stats);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MeasureFrameTime(bool enable)
        {
            ecs.measure_frame_time( this, enable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MeasureSystemTime(bool enable)
        {
            ecs.measure_system_time( this, enable);
        }

        ///<summary>
        /// Delete a world. This operation deletes the world, and all entities, components and systems within the world.
        ///</summary>
        ///<param name="world"> [in]  The world to delete.</param>
        ///<code>
        ///int ecs_fini(ecs_world_t *world)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Fini()
        {
            return ecs.fini( this);
        }

        ///<summary>
        /// Signal exit This operation signals that the application should quit. It will cause ecs_progress to return false.
        ///</summary>
        ///<param name="world"> [in]  The world to quit.</param>
        ///<code>
        ///void ecs_quit(ecs_world_t *world)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Quit()
        {
            ecs.quit( this);
        }

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EntityId Import(ModuleInitActionDelegate module, ReadOnlySpan<char> moduleName, int flags, IntPtr handlesOut, UIntPtr handlesSize)
        {
            using(var moduleNameStr = moduleName.ToAnsiString())
            return _ecs.import( this, module, moduleNameStr, flags, handlesOut, handlesSize);
        }

        ///<summary>
        /// Import a module from a library. If a module is stored in another library, it can be dynamically loaded with this operation. A library may contain multiple modules, and to disambiguate the function allows applications to specify the 'module_name' aprameter.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="library_name"> [in]  The name of the library to load. </param>
        ///<param name="module_name"> [in]  The name of the module to load. </param>
        ///<param name="flags"> [in]  The flags to pass to the module.</param>
        ///<remarks>
        /// A library name typically looks like 'flecs.components.transform', whereas a module name typically looks like 'FlecsComponentsTransform'.
        /// To use this function, Flecs needs to be built with bake, as it relies on bake's package discovery utility API.
        ///</remarks>
        ///<code>
        ///ecs_entity_t ecs_import_from_library(ecs_world_t *world,
        ///                                     const char *library_name,
        ///                                     const char *module_name, int flags)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId ImportFromLibrary(ReadOnlySpan<char> libraryName, ReadOnlySpan<char> moduleName, int flags)
        {
            using(var libraryNameStr = libraryName.ToAnsiString())
            using(var moduleNameStr = moduleName.ToAnsiString())
            return ecs.import_from_library( this, libraryNameStr, moduleNameStr, flags);
        }

        ///<summary>
        /// Progress a world. This operation progresses the world by running all systems that are both enabled and periodic on their matching entities.
        ///</summary>
        ///<param name="world"> [in]  The world to progress. </param>
        ///<param name="delta_time"> [in]  The time passed since the last frame.</param>
        ///<remarks>
        /// To ensure consistency of the data, mutations that add/remove components or create/delete entities are staged and merged after all systems are evaluated. When using multiple threads, each thread will have its own "staging area". Threads will be able to see their own changes, but may not see changes from other threads until changes are merged.
        /// Staging only occurs when ecs_progress is executing systems. The operations that use staging are:
        /// - ecs_new - ecs_new_w_count - ecs_clone - ecs_delete - ecs_add - ecs_remove - ecs_set
        /// By default, staged data is merged each time ecs_progress has evaluated all systems. An application may choose to manually merge instead, by setting auto-merging to false with ecs_set_automerge and invoking ecs_merge when a merge is required. In applications with relatively lots of data to merge, this can significantly boost performance.
        /// It should be noted that delaying a merge in a multithreaded application causes temporary inconsistencies between threads. A thread will be able to see changes from the previous iteration, but will not be able to see updates from other threads until a merge has taken place.
        /// Note that staging only occurs for changes caused by the aforementioned functions. If a system makes in-place modifications to components (through pointers obtained with ecs_data) they will be "instantly" visible to other threads.
        /// An application can pass a delta_time into the function, which is the time passed since the last frame. This value is passed to systems so they can update entity values proportional to the elapsed time since their last invocation.
        /// By passing the delta_time into ecs_progress, an application can take full control of the "speed" at which entities are progressed. This can be particularly useful in simulations, where this feature can be used to control playback speed.
        /// When an application passes 0 to delta_time, ecs_progress will automatically measure the time passed since the last frame. If an application does not uses time management, it should pass a non-zero value for delta_time (1.0 is recommended). That way, no time will be wasted measuring the time.
        ///</remarks>
        ///<code>
        ///bool ecs_progress(ecs_world_t *world, float delta_time)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Progress(float deltaTime)
        {
            return ecs.progress( this, deltaTime);
        }

        ///<summary>
        /// Merge staged data. This operation merges data from one or more stages (if there are multiple threads) to the world state. By default, this happens every time ecs_progress is called. To change this to manual merging, call ecs_set_automerge.
        ///</summary>
        ///<param name="world"> [in]  The world.</param>
        ///<remarks>
        /// Calling ecs_merge manually is a performance optimization which trades consistency for speed. By default thread-specific staging areas are merged automatically after each time ecs_progress is called. For some applications this may impact performance too much, in which case manual merging may be used.
        /// Manual merging requires that the application logic is capable of handling application state that is out of sync for multiple iterations.
        ///</remarks>
        ///<code>
        ///void ecs_merge(ecs_world_t *world)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Merge()
        {
            ecs.merge( this);
        }

        ///<summary>
        /// Set whether the world should merge data each frame. By default, ecs_progress merges data each frame. With this operation that behavior can be changed to merge manually, using ecs_merge.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="auto_merge:"> [in]  When true, ecs_progress performs merging.</param>
        ///<remarks>
        /// Merging is an expensive task, and having to merge each time ecs_progress is called can slow down the application. If ecs_progress is invoked at high frequencies, it may be sufficient to merge at a reduced rate.
        /// As a result of delayed merging, any operation that requires adding or removing components from an entity will not be visible to all threads until the merge occurs.
        ///</remarks>
        ///<code>
        ///void ecs_set_automerge(ecs_world_t *world, bool auto_merge)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetAutomerge(bool autoMerge)
        {
            ecs.set_automerge( this, autoMerge);
        }

        ///<summary>
        /// Set number of worker threads. This operation sets the number of worker threads to which to distribute the processing load. If this function is called multiple times, the total number of threads running will reflect the number specified in the last call.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="threads:"> [in]  The number of threads. </param>
        ///<returns>
        /// 0 if successful, or -1 if failed.
        ///</returns>
        ///<remarks>
        /// This function should not be called while processing an iteration, but should only be called before or after calling ecs_progress.
        /// The initial value is zero, which means that ecs_progress will only use the mainthread.
        ///</remarks>
        ///<code>
        ///void ecs_set_threads(ecs_world_t *world, uint32_t threads)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetThreads(uint threads)
        {
            ecs.set_threads( this, threads);
        }

        ///<summary>
        /// Set target frames per second (FPS) for application. Setting the target FPS ensures that ecs_progress is not invoked faster than the specified FPS. When enabled, ecs_progress tracks the time passed since the last invocation, and sleeps the remaining time of the frame (if any).
        ///</summary>
        ///<remarks>
        /// This feature ensures systems are ran at a consistent interval, as well as conserving CPU time by not running systems more often than required.
        /// This feature depends upon frame profiling. When this operation is called, frame profiling is automatically enabled. Frame profiling can be manually turned on/off with ecs_measure_frame_time. It is not possible to turn off frame profiling if a target FPS is set.
        /// Note that ecs_progress only sleeps if there is time left in the frame. Both time spent in flecs as time spent outside of flecs are taken into account.
        /// Setting a target FPS can be more efficient than letting the application do it manually, as the feature can reuse clock measurements that are taken for frame profiling as well as automatically measuring delta_time.
        ///</remarks>
        ///<code>
        ///void ecs_set_target_fps(ecs_world_t *world, float fps)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetTargetFps(float fps)
        {
            ecs.set_target_fps( this, fps);
        }

        ///<summary>
        /// Enables admin web server This operation allows an profile and enable/disable registered systems
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="port"> [in]  A port number for server.</param>
        ///<returns>
        /// The error code          0 - success          1 - failed to dynamically load `flecs.systems.civetweb` module          2 - failed to dynamically load `lecs.systems.admin` module
        ///</returns>
        ///<code>
        ///int ecs_enable_admin(ecs_world_t *world, uint16_t port)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int EnableAdmin(ushort port)
        {
            return ecs.enable_admin( this, port);
        }

        ///<summary>
        /// Get last used delta time from world 
        ///</summary>
        ///<code>
        ///float ecs_get_delta_time(ecs_world_t *world)
        ///</code>
        public float DeltaTime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ecs.get_delta_time( this);
            }

        }

        ///<summary>
        /// Set a world context. This operation allows an application to register custom data with a world that can be accessed anywhere where the application has the world object.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="ctx"> [in]  A pointer to a user defined structure.</param>
        ///<remarks>
        /// A typical usecase is to register a struct with handles to the application entities, components and systems.
        ///</remarks>
        ///<code>
        ///void ecs_set_context(ecs_world_t *world, void *ctx)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetContext(IntPtr ctx)
        {
            ecs.set_context( this, ctx);
        }

        ///<summary>
        /// Get the world context. This operation retrieves a previously set world context.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<returns>
        /// The context set with ecs_set_context. If no context was set, the          function returns NULL.
        ///</returns>
        ///<code>
        ///void *ecs_get_context(ecs_world_t *world)
        ///</code>
        public IntPtr Context
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ecs.get_context( this);
            }

        }

        ///<summary>
        /// Get the world tick. This operation retrieves the tick count (frame number). The tick count is 0 when ecs_process is called for the first time, and increases by one for every subsequent call.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<returns>
        /// The current tick.
        ///</returns>
        ///<code>
        ///uint32_t ecs_get_tick(ecs_world_t *world)
        ///</code>
        public uint Tick
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ecs.get_tick( this);
            }

        }

        ///<summary>
        /// Dimension the world for a specified number of entities. This operation will preallocate memory in the world for the specified number of entities. Specifying a number lower than the current number of entities in the world will have no effect.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="entity_count"> [in]  The number of entities to preallocate.</param>
        ///<remarks>
        /// When using this operation, note that flecs uses entities for storing systems, components and builtin components. For an exact calculation of entities, do user_entity_count + component_count + system_count + 3. The 3 stands for the number of builtin components.
        /// Note that this operation does not allocate memory in tables. To preallocate memory in a table, use ecs_dim_type. Correctly using these functions prevents flecs from doing dynamic memory allocations in the main loop.
        ///</remarks>
        ///<code>
        ///void ecs_dim(ecs_world_t *world, uint32_t entity_count)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dim(uint entityCount)
        {
            ecs.dim( this, entityCount);
        }

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DimType(TypeId type, uint entityCount)
        {
            _ecs.dim_type( this, type, entityCount);
        }

        ///<summary>
        /// Set a range for issueing new entity ids. This function constrains the entity identifiers returned by ecs_new to the  specified range. This operation can be used to ensure that multiple processes can run in the same simulation without requiring a central service that coordinates issueing identifiers.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="id_start"> [in]  The start of the range. </param>
        ///<param name="id_end"> [in]  The end of the range.</param>
        ///<remarks>
        /// If id_end is set to 0, the range is infinite. If id_end is set to a non-zero value, it has to be larger than id_start. If id_end is set and ecs_new is invoked after an id is issued that is equal to id_end, the application will abort. Flecs does not automatically recycle ids.
        /// The id_end parameter has to be smaller than the last issued identifier.
        ///</remarks>
        ///<code>
        ///void ecs_set_entity_range(ecs_world_t *world, ecs_entity_t id_start,
        ///                          ecs_entity_t id_end)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetEntityRange(EntityId idStart, EntityId idEnd)
        {
            ecs.set_entity_range( this, idStart, idEnd);
        }

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
        /// Flecs does not require applications to explicitly create handles, as entities do not have an explicit lifecycle. The ecs_new operation merely provides a convenient way to dispense handles. It is guaranteed that the handle returned by ecs_new has not bee returned before.
        /// ecs_new_entity ecs_new_component ecs_new_system ecs_new_prefab ecs_new_type ecs_new_child ecs_new_w_count
        ///</remarks>
        ///<code>
        ///ecs_entity_t _ecs_new(ecs_world_t *world, ecs_type_t type)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId New(TypeId type)
        {
            return _ecs.@new( this, type);
        }

        ///<summary>
        /// Create new entities in a batch. This operation creates the number of specified entities with one API call which is a more efficient alternative to calling ecs_new in a loop.
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId NewWCount(TypeId type, uint count)
        {
            return _ecs.new_w_count( this, type, count);
        }

        ///<summary>
        /// Create new entity with same components as specified entity. This operation creates a new entity which has the same components as the specified entity. This includes prefabs and entity-components (entities to which the EcsComponent component has been added manually).
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="entity"> [in]  The source entity. </param>
        ///<param name="copy_value"> [in]  Whether to copy the entity value. </param>
        ///<returns>
        /// The handle to the new entity.
        ///</returns>
        ///<remarks>
        /// The application can optionally copy the values of the specified entity by passing true to copy_value. In that case, the resulting entity will have the same value as source specified entity.
        ///</remarks>
        ///<code>
        ///ecs_entity_t ecs_clone(ecs_world_t *world, ecs_entity_t entity, bool copy_value)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId Clone(EntityId entity, bool copyValue)
        {
            return ecs.clone( this, entity, copyValue);
        }

        ///<summary>
        /// Delete components for an entity. This operation will delete all components from the specified entity. As entities in Flecs do not have an explicit lifecycle, this operation will not invalidate the entity id.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="entity"> [in]  The entity to empty.</param>
        ///<remarks>
        /// When the delete operation is invoked upon an already deleted entity, the operation will have no effect, as it will attempt to delete components from an already empty entity.
        /// As a result of a delete operation, EcsOnRemove systems will be invoked if applicable for any of the removed components.
        ///</remarks>
        ///<code>
        ///void ecs_delete(ecs_world_t *world, ecs_entity_t entity)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Delete(EntityId entity)
        {
            ecs.delete( this, entity);
        }

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(EntityId entity, TypeId type)
        {
            _ecs.add( this, entity, type);
        }

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove(EntityId entity, TypeId type)
        {
            _ecs.remove( this, entity, type);
        }

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
        /// Note that the returned pointer has temporary validity. Operations such as delete and commit may invalidate the pointer as data is potentially moved to different locations. After one of these operations is invoked, the pointer will have to be re-obtained.
        /// This function is wrapped by the ecs_get convenience macro, which can be used like this:
        /// Foo value = ecs_get(world, e, Foo);
        ///</remarks>
        ///<code>
        ///void *_ecs_get_ptr(ecs_world_t *world, ecs_entity_t entity, ecs_type_t type)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr GetPtr(EntityId entity, TypeId type)
        {
            return _ecs.get_ptr( this, entity, type);
        }

        ///<summary>
        /// Set value of component. This function sets the value of a component on the specified entity. If the component does not yet exist, it will be added to the entity.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="entity"> [in]  The entity on which to set the component. </param>
        ///<param name="component"> [in]  The component to set.</param>
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
        public EntityId SetPtr(EntityId entity, TypeId type, UIntPtr size, IntPtr ptr)
        {
            return _ecs.set_ptr( this, entity, type, size, ptr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId SetSingletonPtr(TypeId type, UIntPtr size, IntPtr ptr)
        {
            return _ecs.set_singleton_ptr( this, type, size, ptr);
        }

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
        /// This function is equivalent to calling ecs_new with a type that combines both the type specified in this function and the type id for the container.
        /// If the provided parent entity does not have the 'EcsContainer' component, it will be added automatically.
        /// ecs_new_entity ecs_new_component ecs_new_system ecs_new_prefab ecs_new_type ecs_new ecs_new_w_count
        ///</remarks>
        ///<code>
        ///ecs_entity_t _ecs_new_child(ecs_world_t *world, ecs_entity_t parent,
        ///                            ecs_type_t type)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId NewChild(EntityId parent, TypeId type)
        {
            return _ecs.new_child( this, parent, type);
        }

        ///<summary>
        /// Create new child entities in batch. This operation is similar to ecs_new_w_count, with as only difference that the parent is added to the type of the new entities.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="parent"> [in]  The parent. </param>
        ///<param name="type"> [in]  The type to create the new entities with. </param>
        ///<param name="count"> [in]  The number of entities to create.</param>
        ///<code>
        ///ecs_entity_t _ecs_new_child_w_count(ecs_world_t *world, ecs_entity_t parent,
        ///                                    ecs_type_t type, uint32_t count)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId NewChildWCount(EntityId parent, TypeId type, uint count)
        {
            return _ecs.new_child_w_count( this, parent, type, count);
        }

        ///<summary>
        /// Adopt a child entity by a parent. This operation adds the specified parent entity to the type of the specified entity, which effectively establishes a parent-child relationship. The parent entity, when added, behaves like a normal component in that it becomes part of the entity type.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="entity"> [in]  The entity to adopt. </param>
        ///<param name="parent"> [in]  The parent entity to add to the entity.</param>
        ///<remarks>
        /// If the parent was already added to the entity, this operation will have no effect. If this is the first time a child is added to the parent entity, the EcsContainer component will be added to the parent.
        /// This operation is similar to an ecs_add, with as difference that instead of a  type it accepts any entity handle.
        ///</remarks>
        ///<code>
        ///void ecs_adopt(ecs_world_t *world, ecs_entity_t entity, ecs_entity_t parent)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Adopt(EntityId entity, EntityId parent)
        {
            ecs.adopt( this, entity, parent);
        }

        ///<summary>
        /// Orphan a child by a parent.  This operation removes the specified parent entity from the type of the specified entity. If the parent was not added to the entity, this operation has no effect.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="parent"> [in]  The parent entity to remove from the entity.</param>
        ///<param name="entity"> [in]  The entity to orphan. </param>
        ///<remarks>
        /// This operation is similar to ecs_remove, with as difference that instead of a type it accepts any entity handle.
        ///</remarks>
        ///<code>
        ///void ecs_orphan(ecs_world_t *world, ecs_entity_t child, ecs_entity_t parent)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Orphan(EntityId child, EntityId parent)
        {
            ecs.orphan( this, child, parent);
        }

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has(EntityId entity, TypeId type)
        {
            return _ecs.has( this, entity, type);
        }

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasAny(EntityId entity, TypeId type)
        {
            return _ecs.has_any( this, entity, type);
        }

        ///<summary>
        /// Check if parent entity contains child entity. This function tests if the specified parent entity has been added to the specified child entity.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="parent"> [in]  The parent. </param>
        ///<param name="child"> [in]  The child. </param>
        ///<returns>
        /// true if the parent contains the child, otherwise false.
        ///</returns>
        ///<remarks>
        /// This function is similar to ecs_has, with as difference that instead of a  type it accepts a handle to any entity.
        ///</remarks>
        ///<code>
        ///bool ecs_contains(ecs_world_t *world, ecs_entity_t parent, ecs_entity_t child)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(EntityId parent, EntityId child)
        {
            return ecs.contains( this, parent, child);
        }

        ///<summary>
        /// Return container for component. This function allows the application to query for a container of the specified entity that has the specified component. If there are multiple containers with this component, the function will return the first one it encounters.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="entity"> [in]  The entity for which to resolve the container. </param>
        ///<param name="component"> [in]  The component which the resovled container should have.</param>
        ///<code>
        ///ecs_entity_t ecs_get_parent(ecs_world_t *world, ecs_entity_t entity,
        ///                            ecs_entity_t component)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId GetParent(EntityId entity, EntityId component)
        {
            return ecs.get_parent( this, entity, component);
        }

        ///<summary>
        /// Get type of entity. This operation returns the entity type, which is a handle to the a list of the current components an entity has.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="entity"> [in]  The entity for which to obtain the type. </param>
        ///<returns>
        /// The type of the entity.
        ///</returns>
        ///<remarks>
        /// Note that this function is different from ecs_type_from_entity, which returns a type which only contains the specified entity.
        /// This operation is mostly intended for debugging, as it is considered a bad practice to rely on the type for logic, as the type changes when components are added/removed to the entity.
        ///</remarks>
        ///<code>
        ///ecs_type_t ecs_get_type(ecs_world_t *world, ecs_entity_t entity)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TypeId GetType(EntityId entity)
        {
            return ecs.get_type( this, entity);
        }

        ///<summary>
        /// Return the entity id. This returns the string identifier of an entity, if the entity has the EcsId component. By default, all component, type, system and prefab entities add the EcsId component if they have been created with the ecs_new_* functions.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="entity"> [in]  The entity for which to resolve the id. </param>
        ///<returns>
        /// The id of the entity.
        ///</returns>
        ///<remarks>
        /// If the entity does not contain the EcsId component, this function will return NULL.
        ///</remarks>
        ///<code>
        ///const char *ecs_get_id(ecs_world_t *world, ecs_entity_t entity)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<char> GetId(EntityId entity)
        {
            return ecs.get_id( this, entity).ToString();
        }

        ///<summary>
        /// Return if the entity is empty. This returns whether the provided entity handle is empty. An entity that is empty has no components.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="entity"> [in]  The entity handle. </param>
        ///<returns>
        /// true if empty, false if not empty.
        ///</returns>
        ///<code>
        ///bool ecs_is_empty(ecs_world_t *world, ecs_entity_t entity)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty(EntityId entity)
        {
            return ecs.is_empty( this, entity);
        }

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint Count(TypeId type)
        {
            return _ecs.count( this, type);
        }

        ///<summary>
        /// Lookup an entity by id. This operation is a convenient way to lookup entities by string identifier that have the EcsId component. It is recommended to cache the result of this function, as the function must iterates over all entities and all components in an entity.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="id"> [in]  The id to lookup. </param>
        ///<returns>
        /// The entity handle if found, or 0 if not found.
        ///</returns>
        ///<code>
        ///ecs_entity_t ecs_lookup(ecs_world_t *world, const char *id)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId Lookup(ReadOnlySpan<char> id)
        {
            using(var idStr = id.ToAnsiString())
            return ecs.lookup( this, idStr);
        }

        ///<summary>
        /// Lookup child of parent by id. This operation is the same as ecs_lookup, except for that it only searches entities that are children of the specified parent.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="parent"> [in]  The parent. </param>
        ///<param name="id"> [in]  The id to lookup. </param>
        ///<returns>
        /// The entity handle if found, or 0 if not found.
        ///</returns>
        ///<remarks>
        /// This operation can also be used to only lookup entities with a certain component, in the following way:
        /// ecs_lookup_child(world, ecs_entity(Component), "child_id");
        /// Here, 'Component' refers to the component (type) identifier.
        ///</remarks>
        ///<code>
        ///ecs_entity_t ecs_lookup_child(ecs_world_t *world, ecs_entity_t parent,
        ///                              const char *id)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId LookupChild(EntityId parent, ReadOnlySpan<char> id)
        {
            using(var idStr = id.ToAnsiString())
            return ecs.lookup_child( this, parent, idStr);
        }

        ///<summary>
        /// Get a type from an entity. This function returns a type that can be added/removed to entities. If you create a new component, type or prefab with the ecs_new_* function, you get an ecs_entity_t handle which provides access to builtin components associated with the component, type or prefab.
        ///</summary>
        ///<remarks>
        /// To add a component to an entity, you first have to obtain its type. Types uniquely identify sets of one or more components, and can be used with functions like ecs_add and ecs_remove.
        /// You can only obtain types from entities that have EcsComponent, EcsPrefab, EcsTypeComponent or EcsContainer. These components are automatically added by the ecs_new_* functions, but can also be added manually.
        /// The ECS_COMPONENT, ECS_TAG, ECS_TYPE or ECS_PREFAB macro's will auto- declare a variable containing the type called tFoo (where 'Foo' is the id provided to the macro).
        ///</remarks>
        ///<code>
        ///ecs_type_t ecs_type_from_entity(ecs_world_t *world, ecs_entity_t entity)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TypeId TypeFromEntity(EntityId entity)
        {
            return ecs.type_from_entity( this, entity);
        }

        ///<summary>
        /// Get an entity from a type. This function is the reverse of ecs_type_from_entity. It only works for types that contain exactly one entity. 
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="type"> [in]  The entity for which to obtain the type. </param>
        ///<returns>
        /// The entity associated with the type.
        ///</returns>
        ///<remarks>
        /// If this operation is invoked on a type that contains more than just one  entity, the function will abort. Applications should only use types with this function that are guaranteed to have one entity, such as the types created  for prefabs. 
        ///</remarks>
        ///<code>
        ///ecs_entity_t ecs_entity_from_type(ecs_world_t *world, ecs_type_t type)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId EntityFromType(TypeId type)
        {
            return ecs.entity_from_type( this, type);
        }

        ///<summary>
        /// Merge two types.  This operation will return a type that contains exactly the components in the specified type, plus the components in type_add, and not the components in type_remove.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="type"> [in]  The original type. </param>
        ///<param name="type_add"> [in]  The type to add to the original type. </param>
        ///<param name="type_remove"> [in]  The type to remove from the original type.</param>
        ///<remarks>
        /// The result of the operation is as if type_remove is subtracted before adding  type_add. If type_add contains components that are removed by type_remove, the result will contain the components in type_add.
        ///</remarks>
        ///<code>
        ///ecs_type_t _ecs_merge_type(ecs_world_t *world, ecs_type_t type,
        ///                           ecs_type_t type_add, ecs_type_t type_remove)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TypeId MergeType(TypeId type, TypeId typeAdd, TypeId typeRemove)
        {
            return _ecs.merge_type( this, type, typeAdd, typeRemove);
        }

        ///<summary>
        /// Get component from type at index.  This operation returns the components (or entities) that are contained in the type at the specified index.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="type"> [in]  The type for which to obtain the component. </param>
        ///<param name="index"> [in]  The index at which to obtain the component. </param>
        ///<returns>
        /// zero if out of bounds, a component if within bounds.
        ///</returns>
        ///<code>
        ///ecs_entity_t ecs_type_get_component(ecs_world_t *world, ecs_type_t type,
        ///                                    uint32_t index)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId TypeGetComponent(TypeId type, uint index)
        {
            return ecs.type_get_component( this, type, index);
        }

        ///<summary>
        /// Enable or disable a system. This operation enables or disables a system. A disabled system will not be ran during ecs_progress or when components must be initialized or deinitialized. Systems are enabled by default.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="system"> [in]  The system to enable or disable. </param>
        ///<param name="enabled"> [in]  true to enable the system, false to disable the system. </param>
        ///<returns>
        /// 0 if succeeded, -1 if the operation failed.
        ///</returns>
        ///<remarks>
        /// This operation expects a valid system handle, or in other words, an entity with the EcsSystem component. If a handle to an entity is provided that does not have this component, the operation will fail.
        ///</remarks>
        ///<code>
        ///void ecs_enable(ecs_world_t *world, ecs_entity_t system, bool enabled)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Enable(EntityId system, bool enabled)
        {
            ecs.enable( this, system, enabled);
        }

        ///<summary>
        /// Configure how often a system should be invoked. This operation lets an application control how often a system should be invoked. The provided period is the minimum interval between two invocations.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="system"> [in]  The system for which to set the period. </param>
        ///<param name="period"> [in]  The period.</param>
        ///<remarks>
        /// Correct operation of this feature relies on an application providing a delta_time value to ecs_progress. Once the delta_time exceeds the period that is specified for a system, ecs_progress will invoke it.
        /// This operation is only valid on EcsPeriodic systems. If it is invoked on handles of other systems or entities it will be ignored. An application may only set the period outside ecs_progress.
        /// Note that a system will never be invoked more often than ecs_progress is invoked. If the specified period is smaller than the interval at which ecs_progress is invoked, the system will be invoked at every ecs_progress, provided that the delta_time provided to ecs_progress is accurate.
        ///</remarks>
        ///<code>
        ///void ecs_set_period(ecs_world_t *world, ecs_entity_t system, float period)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPeriod(EntityId system, float period)
        {
            ecs.set_period( this, system, period);
        }

        ///<summary>
        /// Returns the enabled status for a system / entity. This operation will return whether a system is enabled or disabled. Currently only systems can be enabled or disabled, but this operation does not fail when a handle to an entity is provided that is not a system. If this operation is called on a non-system entity, the operation will return true.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="system"> [in]  The system to check. </param>
        ///<returns>
        /// True if the system is enabled, false if the system is disabled.
        ///</returns>
        ///<code>
        ///bool ecs_is_enabled(ecs_world_t *world, ecs_entity_t system)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEnabled(EntityId system)
        {
            return ecs.is_enabled( this, system);
        }

        ///<summary>
        /// Run a specific system manually. This operation runs a single system on demand. It is an efficient way to invoke logic on a set of entities, as on demand systems are only matched to tables at creation time or after creation time, when a new table is created.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="system"> [in]  The system to run. </param>
        ///<param name="param"> [in]  A user-defined parameter to pass to the system. </param>
        ///<param name="delta_time:"> [in]  The time passed since the last system invocation. </param>
        ///<param name="filter"> [in]  A component or type to filter matched entities. </param>
        ///<returns>
        /// handle to last evaluated entity if system was interrupted.
        ///</returns>
        ///<remarks>
        /// On demand systems are useful to evaluate lists of prematched entities at application defined times. Because none of the matching logic is evaluated before the system is invoked, on demand systems are much more efficient than manually obtaining a list of entities and retrieving their components.
        /// An application may however want to apply a filter to an on demand system for fast-changing unpredictable selections of entity subsets. The filter parameter lets applications pass handles to components or component families, and only entities that have said components will be evaluated.
        /// Because the filter is evaluated not on a per-entity basis, but on a per table basis, filter evaluation is still very cheap, especially when compared to tables with large numbers of entities.
        /// An application may pass custom data to a system through the param parameter. This data can be accessed by the system through the param member in the ecs_rows_t value that is passed to the system callback.
        /// Any system may interrupt execution by setting the interrupted_by member in the ecs_rows_t value. This is particularly useful for on demand systems, where the value of interrupted_by is returned by this operation. This, in cominbation with the param argument lets applications use on demand systems to lookup entities: once the entity has been found its handle is passed to interrupted_by, which is then subsequently returned.
        ///</remarks>
        ///<code>
        ///ecs_entity_t ecs_run(ecs_world_t *world, ecs_entity_t system, float delta_time,
        ///                     void *param)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId Run(EntityId system, float deltaTime, IntPtr param)
        {
            return ecs.run( this, system, deltaTime, param);
        }

        ///<summary>
        /// Run system with offset/limit and type filter 
        ///</summary>
        ///<code>
        ///ecs_entity_t _ecs_run_w_filter(ecs_world_t *world, ecs_entity_t system,
        ///                               float delta_time, uint32_t offset,
        ///                               uint32_t limit, ecs_type_t filter, void *param)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId RunWFilter(EntityId system, float deltaTime, uint offset, uint limit, TypeId filter, IntPtr param)
        {
            return _ecs.run_w_filter( this, system, deltaTime, offset, limit, filter, param);
        }

        ///<summary>
        /// Convenience function to create an entity with id and component expression. This is equivalent to calling ecs_new with a type that contains all  components provided in the 'component' expression. In addition, this function also adds the EcsId component, which will be set to the provided id string.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="id"> [in]  The entity id. </param>
        ///<param name="components"> [in]  A component expression. </param>
        ///<returns>
        /// A handle to the created entity. 
        ///</returns>
        ///<remarks>
        /// This function is wrapped by the ECS_ENTITY convenience macro.
        /// ecs_new_component ecs_new_system ecs_new_prefab ecs_new_type ecs_new_child ecs_new ecs_new_w_count
        ///</remarks>
        ///<code>
        ///ecs_entity_t ecs_new_entity(ecs_world_t *world, const char *id,
        ///                            const char *components)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId NewEntity(ReadOnlySpan<char> id, ReadOnlySpan<char> components)
        {
            using(var idStr = id.ToAnsiString())
            using(var componentsStr = components.ToAnsiString())
            return ecs.new_entity( this, idStr, componentsStr);
        }

        ///<summary>
        /// Create a new component. This operation creates a new component with a specified id and size. After this operation is called, the component can be added to entities by using the returned handle with ecs_add.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="id"> [in]  A unique component identifier. </param>
        ///<param name="size"> [in]  The size of the component type (as obtained by sizeof). </param>
        ///<returns>
        /// A handle to the new component, or 0 if failed.
        ///</returns>
        ///<remarks>
        /// Components represent the data of entities. An entity can be composed out of zero or more components. Internally compoments are stored in tables that are created for active combinations of components in a world.
        /// This operation accepts a size, which is the size of the type that contains the component data. Any native type can be used, and the size can be obtained with the built-in sizeof function. For convenience, an application may use the ECS_COMPONENT macro instead of calling this function directly.
        /// Components are stored internally as entities. This operation is equivalent to creating an entity with the EcsComponent and EcsId components. The returned handle can be used in any function that accepts an entity handle.
        ///</remarks>
        ///<code>
        ///ecs_entity_t ecs_new_component(ecs_world_t *world, const char *id, size_t size)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId NewComponent(ReadOnlySpan<char> id, UIntPtr size)
        {
            using(var idStr = id.ToAnsiString())
            return ecs.new_component( this, idStr, size);
        }

        ///<summary>
        /// Create a new system. This operation creates a new system with a specified id, kind and action. After this operation is called, the system will be active. Systems can be created with three different kinds:
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="id"> [in]  The identifier of the system. </param>
        ///<param name="kind"> [in]  The kind of system. </param>
        ///<param name="action"> [in]  The action that is invoked for matching entities. </param>
        ///<param name="signature"> [in]  The signature that describes the components. </param>
        ///<returns>
        /// A handle to the system.
        ///</returns>
        ///<remarks>
        /// - EcsOnUpdate: the system is invoked when ecs_progress is called. - EcsOnAdd: the system is invoked when a component is committed to memory. - EcsOnRemove: the system is invoked when a component is removed from memory. - EcsManual: the system is only invoked on demand (ecs_run)
        /// The signature of the system is a string formatted as a comma separated list of component identifiers. For example, a system that wants to receive the Location and Speed components, should provide "Location, Speed" as its signature.
        /// The action is a function that is invoked for every entity that has the components the system is interested in. The action has three parameters:
        /// - ecs_entity_t system: Handle to the system (same as returned by this function) - ecs_entity_t entity: Handle to the current entity - void *data[]: Array of pointers to the component data
        /// Systems are stored internally as entities. This operation is equivalent to creating an entity with the EcsSystem and EcsId components. The returned handle can be used in any function that accepts an entity handle.
        ///</remarks>
        ///<code>
        ///ecs_entity_t ecs_new_system(ecs_world_t *world, const char *id,
        ///                            EcsSystemKind kind, const char *sig,
        ///                            ecs_system_action_t action)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EntityId NewSystem(ReadOnlySpan<char> id, SystemKind kind, ReadOnlySpan<char> sig, SystemActionDelegate action)
        {
            using(var idStr = id.ToAnsiString())
            using(var sigStr = sig.ToAnsiString())
            return ecs.new_system( this, idStr, kind, sigStr, action);
        }

        ///<summary>
        /// Get handle to type. This operation obtains a handle to a type that can be used with ecs_new. Predefining types has performance benefits over using ecs_add/ecs_remove multiple times, as it provides constant creation time regardless of the number of components. This function will internally create a table for the type.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="components"> [in]  A comma-separated string with the component identifiers. </param>
        ///<returns>
        /// Handle to the type, zero if failed.
        ///</returns>
        ///<remarks>
        /// If a type had been created for this set of components before with the same identifier, the existing type is returned. If the type had been created with a different identifier, this function will fail.
        /// The ECS_TYPE macro wraps around this function.
        ///</remarks>
        ///<code>
        ///ecs_entity_t ecs_new_type(ecs_world_t *world, const char *id,
        ///                          const char *components)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId NewType(ReadOnlySpan<char> id, ReadOnlySpan<char> components)
        {
            using(var idStr = id.ToAnsiString())
            using(var componentsStr = components.ToAnsiString())
            return ecs.new_type( this, idStr, componentsStr);
        }

        ///<summary>
        /// Create a new prefab entity. Prefab entities allow entities to share a set of components. Components of the prefab will appear on the specified entity when using any of the API functions and ECS systems.
        ///</summary>
        ///<remarks>
        /// A prefab is a regular entity, with the only difference that it has the EcsPrefab component.
        /// The ECS_PREFAB macro wraps around this function.
        /// Changing the value of one of the components on the prefab will change the value for all entities that added the prefab, as components are stored only once in memory. This makes prefabs also a memory-saving mechanism; there can be many entities that reuse component records from the prefab.
        /// Entities can override components from a prefab by adding the component with ecs_add. When a component is overridden, its value will be copied from the prefab. This technique can be combined with families to automatically initialize entities, like this:
        /// ECS_PREFAB(world, MyPrefab, Foo); ECS_TYPE(world, MyType, MyPrefab, Foo); ecs_entity_t e = ecs_new(world, MyType);
        /// In this code, the entity will be created with the prefab and directly override 'Foo', which will copy the value of 'Foo' from the prefab.
        /// Prefabs are explicitly stored on the component list of an entity. This means that two entities with the same set of components but a different prefab are stored in different tables.
        /// Prefabs can be part of the component list of other prefabs. This allows for creating hierarchies of prefabs, where the leaves are the most specialized.
        /// Only one prefab may be added to an entity.
        ///</remarks>
        ///<code>
        ///ecs_entity_t ecs_new_prefab(ecs_world_t *world, const char *id, const char *sig)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId NewPrefab(ReadOnlySpan<char> id, ReadOnlySpan<char> sig)
        {
            using(var idStr = id.ToAnsiString())
            using(var sigStr = sig.ToAnsiString())
            return ecs.new_prefab( this, idStr, sigStr);
        }

    }

}

