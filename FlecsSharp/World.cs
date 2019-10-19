using System;
using System.Runtime.CompilerServices;


namespace FlecsSharp
{
	unsafe partial struct World
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void GetStats(out WorldStats stats)
		{
			ecs.get_stats(this, out stats);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MeasureFrameTime(bool enable)
		{
			ecs.measure_frame_time(this, enable);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void MeasureSystemTime(bool enable)
		{
			ecs.measure_system_time(this, enable);
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
			return ecs.fini(this);
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
			ecs.quit(this);
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
		public EntityId Import(ModuleInitActionDelegate module, CharPtr moduleName, int flags, IntPtr handlesOut, UIntPtr handlesSize)
		{
			return _ecs.import(this, module, moduleName, flags, handlesOut, handlesSize);
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
		public EntityId ImportFromLibrary(CharPtr libraryName, CharPtr moduleName, int flags)
		{
			return ecs.import_from_library(this, libraryName, moduleName, flags);
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
			return ecs.progress(this, deltaTime);
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
			ecs.merge(this);
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
			ecs.set_automerge(this, autoMerge);
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
			ecs.set_threads(this, threads);
		}

		///<summary>
		/// Get number of configured threads
		///</summary>
		///<code>
		///uint32_t ecs_get_threads(ecs_world_t *world)
		///</code>
		public uint Threads
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return ecs.get_threads(this); }
		}

		///<summary>
		/// Get index of current worker thread
		///</summary>
		///<code>
		///uint16_t ecs_get_thread_index(ecs_world_t *world)
		///</code>
		public ushort ThreadIndex
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return ecs.get_thread_index(this); }
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
			ecs.set_target_fps(this, fps);
		}

		///<summary>
		/// Get number of configured threads
		///</summary>
		///<code>
		///uint32_t ecs_get_target_fps(ecs_world_t *world)
		///</code>
		public uint TargetFps
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return ecs.get_target_fps(this); }
		}

		///<summary>
		/// Enables admin web server This operation allows an profile and enable/disable registered systems
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="port"> [in]  A port number for server.</param>
		///<returns>
		/// The error code          0 - success          1 - failed to dynamically load `flecs.systems.civetweb` module          2 - failed to dynamically load `flecs.systems.admin` module
		///</returns>
		///<code>
		///int ecs_enable_admin(ecs_world_t *world, uint16_t port)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int EnableAdmin(ushort port)
		{
			return ecs.enable_admin(this, port);
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
			get { return ecs.get_delta_time(this); }
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
			ecs.set_context(this, ctx);
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
			get { return ecs.get_context(this); }
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
			get { return ecs.get_tick(this); }
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
			ecs.dim(this, entityCount);
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
			_ecs.dim_type(this, type, entityCount);
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
			ecs.set_entity_range(this, idStart, idEnd);
		}

		///<summary>
		/// Temporarily enable/disable range limits. When an application is both a receiver of range-limited entities and a producer of range-limited entities, range checking needs to be temporarily disabled when receiving entities.
		///</summary>
		///<remarks>
		/// Range checking is disabled on a stage, so setting this value is thread safe.
		///</remarks>
		///<code>
		///bool ecs_enable_range_check(ecs_world_t *world, bool enable)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool EnableRangeCheck(bool enable)
		{
			return ecs.enable_range_check(this, enable);
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
			return _ecs.@new(this, type);
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
			return _ecs.new_w_count(this, type, count);
		}

		///<summary>
		/// Insert data in bulk. This operation allows applications to insert data in bulk by providing the entity and component data as arrays. The data is passed in using the ecs_table_data_t type, which has to be populated with the data that has to be inserted.
		///</summary>
		///<remarks>
		/// The application must at least provide the row_count, column_count and  components fields. The latter is an array of component identifiers that identifies the components to be added to the entitiy.
		/// The entities array must be populated with the entity identifiers to set. If this field is left NULL, Flecs will create row_count new entities.
		/// The component data must be provided in the columns field. This is an array of component arrays. The component arrays must be provided in the same order as the components have been provided in the components array. For example, if the components array is set to {ecs_entity(Position), ecs_entity(Velocity)}, the columns must first specify the Position, and then the Velocity array. If no component data is provided, the components will be left uninitialized.
		/// Both the entities array and the component data arrays in columns must contain exactly row_count elements. The columns array must contain exactly  column_count elements.
		/// The operation allows for efficient insertion of data for the same set of entities, provided that the entities are specified in the same order for every invocation of this function. After executing this operation, entities will be ordered in the same order specified in the entities array.
		/// If entities already exist in another table, they will be deleted from that table and inserted into the new table.
		///</remarks>
		///<code>
		///ecs_entity_t ecs_set_w_data(ecs_world_t *world, ecs_table_data_t *data)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId SetWData(out TableData data)
		{
			return ecs.set_w_data(this, out data);
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
		/// ecs_new_entity ecs_new_component ecs_new_system ecs_new_prefab ecs_new_type ecs_new ecs_new_w_count
		///</remarks>
		///<code>
		///ecs_entity_t _ecs_new_child(ecs_world_t *world, ecs_entity_t parent,
		///                            ecs_type_t type)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewChild(EntityId parent, TypeId type)
		{
			return _ecs.new_child(this, parent, type);
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
			return _ecs.new_child_w_count(this, parent, type, count);
		}

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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewInstance(EntityId @base, TypeId type)
		{
			return _ecs.new_instance(this, @base, type);
		}

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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId NewInstanceWCount(EntityId @base, TypeId type, uint count)
		{
			return _ecs.new_instance_w_count(this, @base, type, count);
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
			return ecs.clone(this, entity, copyValue);
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
			ecs.delete(this, entity);
		}

		///<summary>
		/// Delete all entities containing a (set of) component(s).  This operation provides a more efficient alternative to deleting entities one by one by deleting an entire table or set of tables in a single operation. The operation will clear all tables that match the specified table.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="filter"> [in]  Filter that matches zero or more tables.</param>
		///<code>
		///void ecs_delete_w_filter(ecs_world_t *world, ecs_type_filter_t *filter)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DeleteWFilter(out TypeFilter filter)
		{
			ecs.delete_w_filter(this, out filter);
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
			_ecs.add(this, entity, type);
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
			_ecs.remove(this, entity, type);
		}

		///<summary>
		/// Add and remove types from an entity. This operation is a combination of ecs_add and ecs_remove. The operation behaves as if the specified to_remove type is removed first, and  subsequently the to_add type is added. This operation is more efficient than adding/removing components separately with ecs_add/ecs_remove, as the entity is moved between tables at most once.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity from which to remove, and to which to add the types. </param>
		///<param name="to_add"> [in]  The type to add to the entity. </param>
		///<param name="to_remove"> [in]  The type to remove from the entity.</param>
		///<code>
		///void _ecs_add_remove(ecs_world_t *world, ecs_entity_t entity, ecs_type_t to_add,
		///                     ecs_type_t to_remove)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddRemove(EntityId entity, TypeId toAdd, TypeId toRemove)
		{
			_ecs.add_remove(this, entity, toAdd, toRemove);
		}

		///<summary>
		/// Adopt a child entity by a parent. This operation adds the specified parent entity to the type of the specified entity, which effectively establishes a parent-child relationship. The parent entity, when added, behaves like a normal component in that it becomes part of the entity type.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity to adopt. </param>
		///<param name="parent"> [in]  The parent entity to add to the entity.</param>
		///<remarks>
		/// If the parent was already added to the entity, this operation will have no effect.
		/// This operation is similar to an ecs_add, with as difference that instead of a  type it accepts any entity handle.
		///</remarks>
		///<code>
		///void ecs_adopt(ecs_world_t *world, ecs_entity_t entity, ecs_entity_t parent)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Adopt(EntityId entity, EntityId parent)
		{
			ecs.adopt(this, entity, parent);
		}

		///<summary>
		/// Orphan a child by a parent.  This operation removes the specified parent entity from the type of the specified entity. If the parent was not added to the entity, this operation has no effect.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity to orphan. </param>
		///<param name="parent"> [in]  The parent entity to remove from the entity.</param>
		///<remarks>
		/// This operation is similar to ecs_remove, with as difference that instead of a type it accepts any entity handle.
		///</remarks>
		///<code>
		///void ecs_orphan(ecs_world_t *world, ecs_entity_t entity, ecs_entity_t parent)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Orphan(EntityId entity, EntityId parent)
		{
			ecs.orphan(this, entity, parent);
		}

		///<summary>
		/// Inherit from a base. This operation adds a base to an entity, which will cause the entity to inherit the components of the base. If the entity already inherited from the specified base, this operation does nothing.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity to add the base to. </param>
		///<param name="base"> [in]  The base to add to the entity.</param>
		///<code>
		///void ecs_inherit(ecs_world_t *world, ecs_entity_t entity, ecs_entity_t base)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Inherit(EntityId entity, EntityId @base)
		{
			ecs.inherit(this, entity, @base);
		}

		///<summary>
		/// Disinherit from a base. This operation removes a base from an entity, which will cause the entity to no longer inherit the components of the base. If the entity did not inherit from the specified base, this operation does nothing.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity to remove the base from. </param>
		///<param name="base"> [in]  The base to remove from the entity.</param>
		///<code>
		///void ecs_disinherit(ecs_world_t *world, ecs_entity_t entity, ecs_entity_t base)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Disinherit(EntityId entity, EntityId @base)
		{
			ecs.disinherit(this, entity, @base);
		}

		///<summary>
		/// Add/remove one or more components from a set of tables. This operation adds/removes one or more components from a set of tables  matching a filter. This operation is more efficient than calling ecs_add  or ecs_remove on the individual entities.
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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void AddRemoveWFilter(TypeId toAdd, TypeId toRemove, out TypeFilter filter)
		{
			_ecs.add_remove_w_filter(this, toAdd, toRemove, out filter);
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
		/// Note that the returned pointer has temporary validity. Operations such as delete and add/remove may invalidate the pointer as data is potentially moved to different locations. After one of these operations is invoked, the pointer will have to be re-obtained.
		/// This function is wrapped by the ecs_get convenience macro, which can be used like this:
		/// Foo value = ecs_get(world, e, Foo);
		///</remarks>
		///<code>
		///void *_ecs_get_ptr(ecs_world_t *world, ecs_entity_t entity, ecs_type_t type)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr GetPtr(EntityId entity, TypeId type)
		{
			return _ecs.get_ptr(this, entity, type);
		}

		///<summary>
		/// Set value of component. This function sets the value of a component on the specified entity. If the component does not yet exist, it will be added to the entity.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity on which to set the component. </param>
		///<param name="component"> [in]  The component to set.</param>
		///<remarks>
		/// This function can be used like this: Foo value = {.x = 10, .y = 20}; ecs_set_ptr(world, e, ecs_type(Foo), &value);
		/// This function is wrapped by the ecs_set convenience macro, which can be used like this:
		/// ecs_set(world, e, Foo, {.x = 10, .y = 20});
		///</remarks>
		///<code>
		///ecs_entity_t _ecs_set_ptr(ecs_world_t *world, ecs_entity_t entity,
		///                          ecs_entity_t component, size_t size, void *ptr)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId SetPtr(EntityId entity, EntityId component, UIntPtr size, IntPtr ptr)
		{
			return _ecs.set_ptr(this, entity, component, size, ptr);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId SetSingletonPtr(EntityId component, UIntPtr size, IntPtr ptr)
		{
			return _ecs.set_singleton_ptr(this, component, size, ptr);
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
			return _ecs.has(this, entity, type);
		}

		///<summary>
		/// Same as ecs_has, but only returns true if entity owns the component(s).
		///</summary>
		///<code>
		///bool _ecs_has_owned(ecs_world_t *world, ecs_entity_t entity, ecs_type_t type)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasOwned(EntityId entity, TypeId type)
		{
			return _ecs.has_owned(this, entity, type);
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
			return _ecs.has_any(this, entity, type);
		}

		///<summary>
		/// Same as ecs_has_any, but only returns true if entity owns the component(s).
		///</summary>
		///<code>
		///bool _ecs_has_any_owned(ecs_world_t *world, ecs_entity_t entity,
		///                        ecs_type_t type)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasAnyOwned(EntityId entity, TypeId type)
		{
			return _ecs.has_any_owned(this, entity, type);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasEntity(EntityId entity, EntityId component)
		{
			return ecs.has_entity(this, entity, component);
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
			return ecs.contains(this, parent, child);
		}

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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId GetParent(EntityId entity, EntityId component)
		{
			return _ecs.get_parent(this, entity, component);
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
			return ecs.get_type(this, entity);
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
		public CharPtr GetId(EntityId entity)
		{
			return ecs.get_id(this, entity);
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
			return ecs.is_empty(this, entity);
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
			return _ecs.count(this, type);
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
		public EntityId Lookup(CharPtr id)
		{
			return ecs.lookup(this, id);
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
		public EntityId LookupChild(EntityId parent, CharPtr id)
		{
			return ecs.lookup_child(this, parent, id);
		}

		///<summary>
		/// Get a type from an entity. This function returns a type that can be added/removed to entities. If you create a new component, type or prefab with the ecs_new_* function, you get an ecs_entity_t handle which provides access to builtin components associated with the component, type or prefab.
		///</summary>
		///<remarks>
		/// To add a component to an entity, you first have to obtain its type. Types uniquely identify sets of one or more components, and can be used with functions like ecs_add and ecs_remove.
		/// You can only obtain types from entities that have EcsComponent, EcsPrefab, or EcsTypeComponent. These components are automatically added by the ecs_new_* functions, but can also be added manually.
		/// The ECS_COMPONENT, ECS_TAG, ECS_TYPE or ECS_PREFAB macro's will auto- declare a variable containing the type called tFoo (where 'Foo' is the id provided to the macro).
		///</remarks>
		///<code>
		///ecs_type_t ecs_type_from_entity(ecs_world_t *world, ecs_entity_t entity)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId TypeFromEntity(EntityId entity)
		{
			return ecs.type_from_entity(this, entity);
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
		///ecs_entity_t ecs_type_to_entity(ecs_world_t *world, ecs_type_t type)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId TypeToEntity(TypeId type)
		{
			return ecs.type_to_entity(this, type);
		}

		///<summary>
		/// Find or create type from existing type and entity.  This operation adds the specified entity to the specified type, and returns a new or existing type that is a union of the specified type and entity. The provided type will not be altered.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="type"> [in]  The type to which to add the entity. </param>
		///<param name="entity"> [in]  The entity to add to the type. </param>
		///<returns>
		/// A type that is the union of the specified type and entity.
		///</returns>
		///<code>
		///ecs_type_t ecs_type_add(ecs_world_t *world, ecs_type_t type,
		///                        ecs_entity_t entity)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId TypeAdd(TypeId type, EntityId entity)
		{
			return ecs.type_add(this, type, entity);
		}

		///<summary>
		/// Find or create type that is the union of two types.  This operation will return a type that contains exactly the components in the specified type, plus the components in type_add, and not the components in type_remove.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="type"> [in]  The original type. </param>
		///<param name="type_add"> [in]  The type to add to the original type. </param>
		///<param name="type_remove"> [in]  The type to remove from the original type.</param>
		///<remarks>
		/// The result of the operation is as if type_remove is subtracted before adding  type_add. If type_add contains components that are removed by type_remove, the result will contain the components in type_add.
		///</remarks>
		///<code>
		///ecs_type_t ecs_type_merge(ecs_world_t *world, ecs_type_t type,
		///                          ecs_type_t type_add, ecs_type_t type_remove)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId TypeMerge(TypeId type, TypeId typeAdd, TypeId typeRemove)
		{
			return ecs.type_merge(this, type, typeAdd, typeRemove);
		}

		///<summary>
		/// Find or create type from entity array. This operation will return a type that contains the entities in the specified array. If a type with the specified entities already exists, it will be returned, otherwise a new type will be created.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="array"> [in]  A C array with entity identifiers. </param>
		///<param name="count"> [in]  The number of elements in the array. </param>
		///<returns>
		/// A type that contains the specified number of entities.
		///</returns>
		///<code>
		///ecs_type_t ecs_type_find(ecs_world_t *world, ecs_entity_t *array,
		///                         uint32_t count)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId TypeFind(out EntityId array, uint count)
		{
			return ecs.type_find(this, out array, count);
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
		///ecs_entity_t ecs_type_get_entity(ecs_world_t *world, ecs_type_t type,
		///                                 uint32_t index)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId TypeGetEntity(TypeId type, uint index)
		{
			return ecs.type_get_entity(this, type, index);
		}

		///<summary>
		/// Check if type has entity. This operation returns whether a type has a specified entity.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="type"> [in]  The type to check. </param>
		///<param name="entity"> [in]  The entity to check for. </param>
		///<returns>
		/// true if the type contains the entity, otherwise false.
		///</returns>
		///<code>
		///bool ecs_type_has_entity(ecs_world_t *world, ecs_type_t type,
		///                         ecs_entity_t entity)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TypeHasEntity(TypeId type, EntityId entity)
		{
			return ecs.type_has_entity(this, type, entity);
		}

		///<summary>
		/// Get type from type expression. This function obtains a type from a type expression. A type expression is a comma-deliminated list of the type's entity identifiers. For example, a type with entities Position and Velocity is: "Position, Velocity".
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="expr"> [in]  The type expression. </param>
		///<returns>
		/// A type if the expression is valid, otherwise NULL.
		///</returns>
		///<remarks>
		/// Type expressions may include type flags that indicate the role of the entity within the type. The following type flags are supported: - INSTANCEOF: share components from this entity - CHILDOF:    treat entity as parent
		/// Type flags can be added with the OR (|) operator. More than one type flag may be specified. This is an example of a type expression with type flags:
		/// Position, Velocity, INSTANCEOF | my_prefab, CHILDOF | my_parent
		/// Entities created with this type will have the Position and Velocity  components, will share components from my_prefab, and will be children of my_parent. The following is also a valid type expression:
		/// INSTANCEOF | CHILDOF | my_prefab
		/// Entities of this type will both share components from my_prefab, as well as be treated as children of my_prefab.
		/// The order in which components are specified has no effect. The following type expressions are equivalent:
		/// - Position, Velocity - Velocity, Position
		///</remarks>
		///<code>
		///ecs_type_t ecs_expr_to_type(ecs_world_t *world, const char *expr)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId ExprToType(CharPtr expr)
		{
			return ecs.expr_to_type(this, expr);
		}

		///<summary>
		/// Get type expression from type.  This function converts a type to a type expression, which is a string representation of the type as it is provided to the ecs_new_entity and ecs_new_type functions. For more information on type expressions, see  ecs_expr_to_type.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="type"> [in]  The type for which to obtain the expression. </param>
		///<returns>
		/// The type expression string. This string needs to be deallocated in          order to prevent memory leaks.
		///</returns>
		///<code>
		///char *ecs_type_to_expr(ecs_world_t *world, ecs_type_t type)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public CharPtr TypeToExpr(TypeId type)
		{
			return ecs.type_to_expr(this, type);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TypeMatchWFilter(TypeId type, out TypeFilter filter)
		{
			return ecs.type_match_w_filter(this, type, out filter);
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
			ecs.enable(this, system, enabled);
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
			ecs.set_period(this, system, period);
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
			return ecs.is_enabled(this, system);
		}

		///<summary>
		/// Run a specific system manually. This operation runs a single system manually. It is an efficient way to invoke logic on a set of entities, as manual systems are only matched to tables at creation time or after creation time, when a new table is created.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="system"> [in]  The system to run. </param>
		///<param name="param"> [in]  A user-defined parameter to pass to the system. </param>
		///<param name="delta_time:"> [in]  The time passed since the last system invocation. </param>
		///<returns>
		/// handle to last evaluated entity if system was interrupted.
		///</returns>
		///<remarks>
		/// Manual systems are useful to evaluate lists of prematched entities at application defined times. Because none of the matching logic is evaluated before the system is invoked, manual systems are much more efficient than manually obtaining a list of entities and retrieving their components.
		/// An application may pass custom data to a system through the param parameter. This data can be accessed by the system through the param member in the ecs_rows_t value that is passed to the system callback.
		/// Any system may interrupt execution by setting the interrupted_by member in the ecs_rows_t value. This is particularly useful for manual systems, where the value of interrupted_by is returned by this operation. This, in cominbation with the param argument lets applications use manual systems to lookup entities: once the entity has been found its handle is passed to interrupted_by, which is then subsequently returned.
		///</remarks>
		///<code>
		///ecs_entity_t ecs_run(ecs_world_t *world, ecs_entity_t system, float delta_time,
		///                     void *param)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId Run(EntityId system, float deltaTime, IntPtr param)
		{
			return ecs.run(this, system, deltaTime, param);
		}

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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId RunWFilter(EntityId system, float deltaTime, uint offset, uint limit, TypeId filter, IntPtr param)
		{
			return _ecs.run_w_filter(this, system, deltaTime, offset, limit, filter, param);
		}

		///<summary>
		/// Set system context. This operation allows an application to register custom data with a system. This data can be accessed using the ecs_get_system_context operation, or through the 'param' field in the ecs_rows_t parameter passed into the system callback.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="system"> [in]  The system on which to set the context. </param>
		///<param name="ctx"> [in]  A pointer to a user defined structure.</param>
		///<code>
		///void ecs_set_system_context(ecs_world_t *world, ecs_entity_t system,
		///                            const void *ctx)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetSystemContext(EntityId system, IntPtr ctx)
		{
			ecs.set_system_context(this, system, ctx);
		}

		///<summary>
		/// Get system context. Get custom data from a system previously set with ecs_set_system_context.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="system"> [in]  The system of which to obtain the context. </param>
		///<returns>
		/// The system context.
		///</returns>
		///<code>
		///void *ecs_get_system_context(ecs_world_t *world, ecs_entity_t system)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr GetSystemContext(EntityId system)
		{
			return ecs.get_system_context(this, system);
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
		EntityId NewEntity(CharPtr id, CharPtr components)
		{
			return ecs.new_entity(this, id, components);
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
		public EntityId NewComponent(CharPtr id, UIntPtr size)
		{
			return ecs.new_component(this, id, size);
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
		public EntityId NewSystem(CharPtr id, SystemKind kind, CharPtr sig, SystemActionDelegate action)
		{
			return ecs.new_system(this, id, kind, sig, action);
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
		public EntityId NewType(CharPtr id, CharPtr components)
		{
			return ecs.new_type(this, id, components);
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
		public EntityId NewPrefab(CharPtr id, CharPtr sig)
		{
			return ecs.new_prefab(this, id, sig);
		}

		///<summary>
		/// Create a new world. A world manages all the ECS objects. Applications must have at least one world. Entities, component and system handles are local to a world and cannot be shared between worlds.
		///</summary>
		///<returns>
		/// A new world object
		///</returns>
		///<code>
		///ecs_world_t *ecs_init()
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static World Init()
		{
			return ecs.init();
		}

		///<summary>
		/// Create a new world with arguments. Same as ecs_init, but allows passing in command line arguments. These can be used to dynamically enable flecs features to an application, like performance monitoring or the web dashboard (if it is installed) without having to modify the code of an application.
		///</summary>
		///<returns>
		/// A new world object
		///</returns>
		///<remarks>
		/// If the functionality requested by the arguments is not available, an error message will be printed to stderr, but the function will not fail. Thus it is important that the application code does not rely on any functionality that is realized through the arguments.
		/// If the arguments specify a setting that is explicity set as well by the application, the application setting will be ignored. For example, if an application specifies it will run on 2 threads, but an argument specify it will run on 6 threads, the argument will take precedence.
		/// The following options are available: --threads [n]   Use n worker threads --fps [hz]      Run at hz FPS --admin [port]  Enable admin dashboard (requires flecs-systems-admin & flecs-systems-civetweb) --debug         Enables debug tracing
		///</remarks>
		///<code>
		///ecs_world_t *ecs_init_w_args(int argc, char *argv[])
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static World InitWArgs(int argc, out sbyte argv)
		{
			return ecs.init_w_args(argc, out argv);
		}
	}
}