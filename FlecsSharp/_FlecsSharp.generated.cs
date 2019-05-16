using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    using System.Security;
#region Enums
    //EcsSystemKind
    public enum SystemKind : Int32
    {
        OnLoad = 0,
        PostLoad = 1,
        PreUpdate = 2,
        OnUpdate = 3,
        OnValidate = 4,
        PostUpdate = 5,
        PreStore = 6,
        OnStore = 7,
        Manual = 8,
        OnAdd = 9,
        OnRemove = 10,
        OnSet = 11,
    }

#endregion

#region Typedefs
    unsafe partial struct OsThread
    {
        public OsThread(UInt64 value)
        {
            Value = value;
        }

        public UInt64 Value;
    }

    unsafe partial struct OsMutex
    {
        public OsMutex(UInt64 value)
        {
            Value = value;
        }

        public UInt64 Value;
    }

    unsafe partial struct OsCond
    {
        public OsCond(UInt64 value)
        {
            Value = value;
        }

        public UInt64 Value;
    }

    unsafe partial struct Bool
    {
        public Bool(sbyte value)
        {
            Value = value;
        }

        public sbyte Value;
    }

    unsafe partial struct EntityId
    {
        public EntityId(UInt64 value)
        {
            Value = value;
        }

        public UInt64 Value;
    }

    unsafe partial struct TypeId
    {
        public TypeId(UInt32 value)
        {
            Value = value;
        }

        public UInt32 Value;
    }

#endregion

#region Structs
    //ecs_os_api_t
    unsafe partial struct OsApi
    {
        internal Data* ptr;
        internal OsApi(Data* ptr) => this.ptr = ptr;
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct Data
        {
            public static implicit operator OsApi(Data data) => data.Ptr();
            public static implicit operator Data(OsApi _ref) => *_ref.ptr;
            public OsApi Ptr() { fixed(Data* ptr = &this) return new OsApi(ptr); }
            internal IntPtr _malloc;
            internal IntPtr _realloc;
            internal IntPtr _calloc;
            internal IntPtr _free;
            internal IntPtr _threadNew;
            internal IntPtr _threadJoin;
            internal IntPtr _mutexNew;
            internal IntPtr _mutexFree;
            internal IntPtr _mutexLock;
            internal IntPtr _mutexUnlock;
            internal IntPtr _condNew;
            internal IntPtr _condFree;
            internal IntPtr _condSignal;
            internal IntPtr _condBroadcast;
            internal IntPtr _condWait;
            internal IntPtr _sleep;
            internal IntPtr _getTime;
            internal IntPtr _log;
            internal IntPtr _logError;
            internal IntPtr _logDebug;
            internal IntPtr _abort;
        }

    }

    //ecs_time_t
    [StructLayout(LayoutKind.Sequential)]
    unsafe partial struct Time
    {
        public Time* Ptr() { fixed(Time* ptr = &this) return ptr; }
        internal int sec;
        internal uint nanosec;
    }

    //EcsIter
    [StructLayout(LayoutKind.Sequential)]
    unsafe partial struct Iterator
    {
        public Iterator* Ptr() { fixed(Iterator* ptr = &this) return ptr; }
        internal IntPtr ctx;
        internal IntPtr data;
        internal IntPtr _hasnext;
        internal IntPtr _next;
        internal IntPtr _release;
    }

    //ecs_vector_params_t
    unsafe partial struct VectorParams
    {
        internal Data* ptr;
        internal VectorParams(Data* ptr) => this.ptr = ptr;
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct Data
        {
            public static implicit operator VectorParams(Data data) => data.Ptr();
            public static implicit operator Data(VectorParams _ref) => *_ref.ptr;
            public VectorParams Ptr() { fixed(Data* ptr = &this) return new VectorParams(ptr); }
            internal IntPtr _moveAction;
            internal IntPtr moveCtx;
            internal IntPtr ctx;
            internal uint elementSize;
        }

    }

    //EcsMapIter
    unsafe partial struct MapIter
    {
        internal Data* ptr;
        internal MapIter(Data* ptr) => this.ptr = ptr;
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct Data
        {
            public static implicit operator MapIter(Data data) => data.Ptr();
            public static implicit operator Data(MapIter _ref) => *_ref.ptr;
            public MapIter Ptr() { fixed(Data* ptr = &this) return new MapIter(ptr); }
            internal uint bucketIndex;
            internal uint node;
        }

    }

    //ecs_world_stats_t
    unsafe partial struct WorldStats
    {
        internal Data* ptr;
        internal WorldStats(Data* ptr) => this.ptr = ptr;
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct Data
        {
            public static implicit operator WorldStats(Data data) => data.Ptr();
            public static implicit operator Data(WorldStats _ref) => *_ref.ptr;
            public WorldStats Ptr() { fixed(Data* ptr = &this) return new WorldStats(ptr); }
            internal uint systemCount;
            internal uint tableCount;
            internal uint componentCount;
            internal uint entityCount;
            internal uint threadCount;
            internal uint tickCount;
            internal float systemTime;
            internal float frameTime;
            internal float mergeTime;
            internal MemoryStats.Data memory;
            internal Vector features;
            internal Vector onLoadSystems;
            internal Vector postLoadSystems;
            internal Vector preUpdateSystems;
            internal Vector onUpdateSystems;
            internal Vector onValidateSystems;
            internal Vector postUpdateSystems;
            internal Vector preStoreSystems;
            internal Vector onStoreSystems;
            internal Vector taskSystems;
            internal Vector inactiveSystems;
            internal Vector onDemandSystems;
            internal Vector onAddSystems;
            internal Vector onRemoveSystems;
            internal Vector onSetSystems;
            internal Vector components;
            internal Bool frameProfiling;
            internal Bool systemProfiling;
        }

    }

    //memory
    unsafe partial struct MemoryStats
    {
        internal Data* ptr;
        internal MemoryStats(Data* ptr) => this.ptr = ptr;
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct Data
        {
            public static implicit operator MemoryStats(Data data) => data.Ptr();
            public static implicit operator Data(MemoryStats _ref) => *_ref.ptr;
            public MemoryStats Ptr() { fixed(Data* ptr = &this) return new MemoryStats(ptr); }
            internal MemStat.Data total;
            internal MemStat.Data components;
            internal MemStat.Data entities;
            internal MemStat.Data systems;
            internal MemStat.Data families;
            internal MemStat.Data tables;
            internal MemStat.Data stage;
            internal MemStat.Data world;
        }

    }

    //total
    unsafe partial struct MemStat
    {
        internal Data* ptr;
        internal MemStat(Data* ptr) => this.ptr = ptr;
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct Data
        {
            public static implicit operator MemStat(Data data) => data.Ptr();
            public static implicit operator Data(MemStat _ref) => *_ref.ptr;
            public MemStat Ptr() { fixed(Data* ptr = &this) return new MemStat(ptr); }
            internal uint allocd;
            internal uint used;
        }

    }

    //ecs_rows_t
    unsafe partial struct Rows
    {
        internal Data* ptr;
        internal Rows(Data* ptr) => this.ptr = ptr;
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct Data
        {
            public static implicit operator Rows(Data data) => data.Ptr();
            public static implicit operator Data(Rows _ref) => *_ref.ptr;
            public Rows Ptr() { fixed(Data* ptr = &this) return new Rows(ptr); }
            internal World world;
            internal EntityId system;
            internal int* columns;
            internal ushort columnCount;
            internal IntPtr table;
            internal IntPtr tableColumns;
            internal ComponentReference references;
            internal EntityId* components;
            internal EntityId* entities;
            internal IntPtr param;
            internal float deltaTime;
            internal uint frameOffset;
            internal uint offset;
            internal uint count;
            internal EntityId interruptedBy;
        }

    }

    //ecs_reference_t
    unsafe partial struct ComponentReference
    {
        internal Data* ptr;
        internal ComponentReference(Data* ptr) => this.ptr = ptr;
        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct Data
        {
            public static implicit operator ComponentReference(Data data) => data.Ptr();
            public static implicit operator Data(ComponentReference _ref) => *_ref.ptr;
            public ComponentReference Ptr() { fixed(Data* ptr = &this) return new ComponentReference(ptr); }
            internal EntityId entity;
            internal EntityId component;
            internal IntPtr cachedPtr;
        }

    }

#endregion

#region OpaquePtrs
    //ecs_vector_t
    unsafe partial struct Vector
    {
        IntPtr ptr;
        public Vector(IntPtr ptr) => this.ptr = ptr;
        internal Vector* Ptr => (Vector*) ptr;
    }

    //ecs_map_t
    unsafe partial struct Map
    {
        IntPtr ptr;
        public Map(IntPtr ptr) => this.ptr = ptr;
        internal Map* Ptr => (Map*) ptr;
    }

    //ecs_world_t
    unsafe partial struct World
    {
        IntPtr ptr;
        public World(IntPtr ptr) => this.ptr = ptr;
        internal World* Ptr => (World*) ptr;
    }

#endregion

#region Delegates
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate IntPtr OsApiMallocDelegate(UIntPtr size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate IntPtr OsApiReallocDelegate(IntPtr ptr, UIntPtr size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate IntPtr OsApiCallocDelegate(UIntPtr num, UIntPtr size);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void OsApiFreeDelegate(IntPtr ptr);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate OsThread OsApiThreadNewDelegate(OsThread ecsOsThreadT, OsThreadCallbackDelegate callback, IntPtr param);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate IntPtr OsThreadCallbackDelegate(IntPtr param0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate IntPtr OsApiThreadJoinDelegate(OsThread thread);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate OsMutex OsApiMutexNewDelegate(OsMutex ecsOsMutexT);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void OsApiMutexFreeDelegate(OsMutex mutex);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void OsApiMutexLockDelegate(OsMutex mutex);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate OsCond OsApiCondNewDelegate(OsCond ecsOsCondT);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void OsApiCondFreeDelegate(OsCond cond);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void OsApiCondSignalDelegate(OsCond cond);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void OsApiCondBroadcastDelegate(OsCond cond);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void OsApiCondWaitDelegate(OsCond cond, OsMutex mutex);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void OsApiSleepDelegate(uint sec, uint nanosec);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void OsApiGetTimeDelegate(Time* timeOut);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void OsApiLogDelegate(CharPtr fmt, IntPtr args);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void OsApiAbortDelegate();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate Bool HasnextDelegate(Bool @bool, Iterator* param1);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate IntPtr NextDelegate(Iterator* param0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void ReleaseDelegate(Iterator* param0);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void MoveDelegate(Vector array, VectorParams @params, IntPtr to, IntPtr @from, IntPtr ctx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate int ComparatorDelegate(IntPtr p1, IntPtr p2);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void ModuleInitActionDelegate(World world, int flags);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal unsafe delegate void SystemActionDelegate(Rows data);

#endregion

    internal unsafe static partial class ecs
    {
        [DllImport(DLL, EntryPoint = "ecs_os_set_api", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void os_set_api(OsApi osApi);

        [DllImport(DLL, EntryPoint = "ecs_os_set_api_defaults", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void os_set_api_defaults();

        ///<summary>
        /// Logging (use functions to avoid using variadic macro arguments) 
        ///</summary>
        ///<code>
        ///void ecs_os_log(const char *fmt, ...)
        ///</code>
        [DllImport(DLL, EntryPoint = "ecs_os_log", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void os_log(CharPtr fmt);

        [DllImport(DLL, EntryPoint = "ecs_os_err", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void os_err(CharPtr fmt);

        [DllImport(DLL, EntryPoint = "ecs_os_dbg", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void os_dbg(CharPtr fmt);

        [DllImport(DLL, EntryPoint = "ecs_os_enable_dbg", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void os_enable_dbg(Bool enable);

        [DllImport(DLL, EntryPoint = "ecs_iter_hasnext", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Bool iter_hasnext(Iterator* iter);

        [DllImport(DLL, EntryPoint = "ecs_iter_next", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr iter_next(Iterator* iter);

        [DllImport(DLL, EntryPoint = "ecs_iter_release", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void iter_release(Iterator* iter);

        [DllImport(DLL, EntryPoint = "ecs_vector_new", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Vector vector_new(VectorParams @params, uint size);

        [DllImport(DLL, EntryPoint = "ecs_vector_new_from_buffer", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Vector vector_new_from_buffer(VectorParams @params, uint size, IntPtr buffer);

        [DllImport(DLL, EntryPoint = "ecs_vector_free", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void vector_free(Vector array);

        [DllImport(DLL, EntryPoint = "ecs_vector_clear", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void vector_clear(Vector array);

        [DllImport(DLL, EntryPoint = "ecs_vector_add", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr vector_add(Vector* arrayInout, VectorParams @params);

        [DllImport(DLL, EntryPoint = "ecs_vector_addn", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr vector_addn(Vector* arrayInout, VectorParams @params, uint count);

        [DllImport(DLL, EntryPoint = "ecs_vector_get", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr vector_get(Vector array, VectorParams @params, uint index);

        [DllImport(DLL, EntryPoint = "ecs_vector_get_index", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint vector_get_index(Vector array, VectorParams @params, IntPtr elem);

        [DllImport(DLL, EntryPoint = "ecs_vector_last", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr vector_last(Vector array, VectorParams @params);

        [DllImport(DLL, EntryPoint = "ecs_vector_remove", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint vector_remove(Vector array, VectorParams @params, IntPtr elem);

        [DllImport(DLL, EntryPoint = "ecs_vector_remove_last", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void vector_remove_last(Vector array);

        [DllImport(DLL, EntryPoint = "ecs_vector_move_index", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint vector_move_index(Vector* dstArray, Vector srcArray, VectorParams @params, uint index);

        [DllImport(DLL, EntryPoint = "ecs_vector_remove_index", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint vector_remove_index(Vector array, VectorParams @params, uint index);

        [DllImport(DLL, EntryPoint = "ecs_vector_reclaim", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void vector_reclaim(Vector* array, VectorParams @params);

        [DllImport(DLL, EntryPoint = "ecs_vector_set_size", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint vector_set_size(Vector* array, VectorParams @params, uint size);

        [DllImport(DLL, EntryPoint = "ecs_vector_set_count", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint vector_set_count(Vector* array, VectorParams @params, uint size);

        [DllImport(DLL, EntryPoint = "ecs_vector_count", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint vector_count(Vector array);

        [DllImport(DLL, EntryPoint = "ecs_vector_size", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint vector_size(Vector array);

        [DllImport(DLL, EntryPoint = "ecs_vector_first", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr vector_first(Vector array);

        [DllImport(DLL, EntryPoint = "ecs_vector_sort", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void vector_sort(Vector array, VectorParams @params, ComparatorDelegate compareAction);

        [DllImport(DLL, EntryPoint = "ecs_vector_memory", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void vector_memory(Vector array, VectorParams @params, uint* allocd, uint* used);

        [DllImport(DLL, EntryPoint = "ecs_map_new", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Map map_new(uint size);

        [DllImport(DLL, EntryPoint = "ecs_map_free", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void map_free(Map map);

        [DllImport(DLL, EntryPoint = "ecs_map_memory", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void map_memory(Map map, uint* total, uint* used);

        [DllImport(DLL, EntryPoint = "ecs_map_count", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint map_count(Map map);

        [DllImport(DLL, EntryPoint = "ecs_map_set_size", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint map_set_size(Map map, uint size);

        [DllImport(DLL, EntryPoint = "ecs_map_bucket_count", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint map_bucket_count(Map map);

        [DllImport(DLL, EntryPoint = "ecs_map_clear", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void map_clear(Map map);

        [DllImport(DLL, EntryPoint = "ecs_map_set64", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void map_set64(Map map, ulong keyHash, ulong data);

        [DllImport(DLL, EntryPoint = "ecs_map_get64", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern ulong map_get64(Map map, ulong keyHash);

        [DllImport(DLL, EntryPoint = "ecs_map_has", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Bool map_has(Map map, ulong keyHash, ulong* valueOut);

        [DllImport(DLL, EntryPoint = "ecs_map_remove", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern int map_remove(Map map, ulong keyHash);

        [DllImport(DLL, EntryPoint = "ecs_map_next", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern ulong map_next(Iterator* it, ulong* keyOut);

        [DllImport(DLL, EntryPoint = "ecs_get_stats", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void get_stats(World world, WorldStats stats);

        [DllImport(DLL, EntryPoint = "ecs_free_stats", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void free_stats(WorldStats stats);

        [DllImport(DLL, EntryPoint = "ecs_measure_frame_time", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void measure_frame_time(World world, Bool enable);

        [DllImport(DLL, EntryPoint = "ecs_measure_system_time", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void measure_system_time(World world, Bool enable);

        [DllImport(DLL, EntryPoint = "ecs_sleepf", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void sleepf(double t);

        [DllImport(DLL, EntryPoint = "ecs_time_to_double", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern double time_to_double(Time t);

        [DllImport(DLL, EntryPoint = "ecs_time_sub", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Time time_sub(Time t1, Time t2);

        [DllImport(DLL, EntryPoint = "ecs_time_measure", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern double time_measure(Time* start);

        ///<summary>
        /// Create a new world. A world manages all the ECS objects. Applications must have at least one world. Entities, component and system handles are local to a world and cannot be shared between worlds.
        ///</summary>
        ///<returns>
        /// A new world object
        ///</returns>
        ///<code>
        ///ecs_world_t *ecs_init()
        ///</code>
        [DllImport(DLL, EntryPoint = "ecs_init", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern World init();

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
        [DllImport(DLL, EntryPoint = "ecs_init_w_args", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern World init_w_args(int argc, sbyte* argv);

        ///<summary>
        /// Delete a world. This operation deletes the world, and all entities, components and systems within the world.
        ///</summary>
        ///<param name="world"> [in]  The world to delete.</param>
        ///<code>
        ///int ecs_fini(ecs_world_t *world)
        ///</code>
        [DllImport(DLL, EntryPoint = "ecs_fini", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern int fini(World world);

        ///<summary>
        /// Signal exit This operation signals that the application should quit. It will cause ecs_progress to return false.
        ///</summary>
        ///<param name="world"> [in]  The world to quit.</param>
        ///<code>
        ///void ecs_quit(ecs_world_t *world)
        ///</code>
        [DllImport(DLL, EntryPoint = "ecs_quit", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void quit(World world);

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
        [DllImport(DLL, EntryPoint = "ecs_import_from_library", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId import_from_library(World world, CharPtr libraryName, CharPtr moduleName, int flags);

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
        [DllImport(DLL, EntryPoint = "ecs_progress", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Bool progress(World world, float deltaTime);

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
        [DllImport(DLL, EntryPoint = "ecs_merge", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void merge(World world);

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
        [DllImport(DLL, EntryPoint = "ecs_set_automerge", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void set_automerge(World world, Bool autoMerge);

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
        [DllImport(DLL, EntryPoint = "ecs_set_threads", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void set_threads(World world, uint threads);

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
        [DllImport(DLL, EntryPoint = "ecs_set_target_fps", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void set_target_fps(World world, float fps);

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
        [DllImport(DLL, EntryPoint = "ecs_enable_admin", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern int enable_admin(World world, ushort port);

        ///<summary>
        /// Get last used delta time from world 
        ///</summary>
        ///<code>
        ///float ecs_get_delta_time(ecs_world_t *world)
        ///</code>
        [DllImport(DLL, EntryPoint = "ecs_get_delta_time", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern float get_delta_time(World world);

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
        [DllImport(DLL, EntryPoint = "ecs_set_context", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void set_context(World world, IntPtr ctx);

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
        [DllImport(DLL, EntryPoint = "ecs_get_context", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr get_context(World world);

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
        [DllImport(DLL, EntryPoint = "ecs_get_tick", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint get_tick(World world);

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
        [DllImport(DLL, EntryPoint = "ecs_dim", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void dim(World world, uint entityCount);

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
        [DllImport(DLL, EntryPoint = "ecs_set_entity_range", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void set_entity_range(World world, EntityId idStart, EntityId idEnd);

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
        [DllImport(DLL, EntryPoint = "ecs_clone", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId clone(World world, EntityId entity, Bool copyValue);

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
        [DllImport(DLL, EntryPoint = "ecs_delete", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void delete(World world, EntityId entity);

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
        [DllImport(DLL, EntryPoint = "ecs_adopt", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void adopt(World world, EntityId entity, EntityId parent);

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
        [DllImport(DLL, EntryPoint = "ecs_orphan", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void orphan(World world, EntityId child, EntityId parent);

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
        [DllImport(DLL, EntryPoint = "ecs_contains", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Bool contains(World world, EntityId parent, EntityId child);

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
        [DllImport(DLL, EntryPoint = "ecs_get_parent", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId get_parent(World world, EntityId entity, EntityId component);

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
        [DllImport(DLL, EntryPoint = "ecs_get_type", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern TypeId get_type(World world, EntityId entity);

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
        [DllImport(DLL, EntryPoint = "ecs_get_id", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern CharPtr get_id(World world, EntityId entity);

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
        [DllImport(DLL, EntryPoint = "ecs_is_empty", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Bool is_empty(World world, EntityId entity);

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
        [DllImport(DLL, EntryPoint = "ecs_lookup", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId lookup(World world, CharPtr id);

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
        [DllImport(DLL, EntryPoint = "ecs_lookup_child", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId lookup_child(World world, EntityId parent, CharPtr id);

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
        [DllImport(DLL, EntryPoint = "ecs_type_from_entity", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern TypeId type_from_entity(World world, EntityId entity);

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
        [DllImport(DLL, EntryPoint = "ecs_entity_from_type", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId entity_from_type(World world, TypeId type);

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
        [DllImport(DLL, EntryPoint = "ecs_type_get_component", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId type_get_component(World world, TypeId type, uint index);

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
        [DllImport(DLL, EntryPoint = "ecs_enable", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void enable(World world, EntityId system, Bool enabled);

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
        [DllImport(DLL, EntryPoint = "ecs_set_period", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void set_period(World world, EntityId system, float period);

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
        [DllImport(DLL, EntryPoint = "ecs_is_enabled", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Bool is_enabled(World world, EntityId system);

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
        [DllImport(DLL, EntryPoint = "ecs_run", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId run(World world, EntityId system, float deltaTime, IntPtr param);

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
        [DllImport(DLL, EntryPoint = "ecs_new_entity", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId new_entity(World world, CharPtr id, CharPtr components);

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
        [DllImport(DLL, EntryPoint = "ecs_new_component", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId new_component(World world, CharPtr id, UIntPtr size);

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
        [DllImport(DLL, EntryPoint = "ecs_new_system", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId new_system(World world, CharPtr id, SystemKind kind, CharPtr sig, SystemActionDelegate action);

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
        [DllImport(DLL, EntryPoint = "ecs_new_type", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId new_type(World world, CharPtr id, CharPtr components);

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
        [DllImport(DLL, EntryPoint = "ecs_new_prefab", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId new_prefab(World world, CharPtr id, CharPtr sig);

        ///<summary>
        /// Get description for error code 
        ///</summary>
        ///<code>
        ///const char *ecs_strerror(uint32_t error_code)
        ///</code>
        [DllImport(DLL, EntryPoint = "ecs_strerror", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern CharPtr strerror(uint errorCode);

    }

    internal unsafe static partial class _ecs
    {
        [DllImport(DLL, EntryPoint = "_ecs_map_iter", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Iterator map_iter(Map map, MapIter iterData);

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
        [DllImport(DLL, EntryPoint = "_ecs_import", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
        [DllImport(DLL, EntryPoint = "_ecs_dim_type", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void dim_type(World world, TypeId type, uint entityCount);

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
        [DllImport(DLL, EntryPoint = "_ecs_new", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId @new(World world, TypeId type);

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
        [DllImport(DLL, EntryPoint = "_ecs_new_w_count", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId new_w_count(World world, TypeId type, uint count);

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
        [DllImport(DLL, EntryPoint = "_ecs_add", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void @add(World world, EntityId entity, TypeId type);

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
        [DllImport(DLL, EntryPoint = "_ecs_remove", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void @remove(World world, EntityId entity, TypeId type);

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
        [DllImport(DLL, EntryPoint = "_ecs_get_ptr", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr get_ptr(World world, EntityId entity, TypeId type);

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
        [DllImport(DLL, EntryPoint = "_ecs_set_ptr", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId set_ptr(World world, EntityId entity, TypeId type, UIntPtr size, IntPtr ptr);

        [DllImport(DLL, EntryPoint = "_ecs_set_singleton_ptr", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId set_singleton_ptr(World world, TypeId type, UIntPtr size, IntPtr ptr);

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
        [DllImport(DLL, EntryPoint = "_ecs_new_child", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId new_child(World world, EntityId parent, TypeId type);

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
        [DllImport(DLL, EntryPoint = "_ecs_new_child_w_count", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId new_child_w_count(World world, EntityId parent, TypeId type, uint count);

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
        [DllImport(DLL, EntryPoint = "_ecs_has", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Bool has(World world, EntityId entity, TypeId type);

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
        [DllImport(DLL, EntryPoint = "_ecs_has_any", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern Bool has_any(World world, EntityId entity, TypeId type);

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
        [DllImport(DLL, EntryPoint = "_ecs_count", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern uint count(World world, TypeId type);

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
        [DllImport(DLL, EntryPoint = "_ecs_merge_type", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern TypeId merge_type(World world, TypeId type, TypeId typeAdd, TypeId typeRemove);

        ///<summary>
        /// Run system with offset/limit and type filter 
        ///</summary>
        ///<code>
        ///ecs_entity_t _ecs_run_w_filter(ecs_world_t *world, ecs_entity_t system,
        ///                               float delta_time, uint32_t offset,
        ///                               uint32_t limit, ecs_type_t filter, void *param)
        ///</code>
        [DllImport(DLL, EntryPoint = "_ecs_run_w_filter", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId run_w_filter(World world, EntityId system, float deltaTime, uint offset, uint limit, TypeId filter, IntPtr param);

        ///<summary>
        /// Obtain a column from inside a system 
        ///</summary>
        ///<code>
        ///void *_ecs_column(ecs_rows_t *rows, uint32_t index, bool test)
        ///</code>
        [DllImport(DLL, EntryPoint = "_ecs_column", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr column(Rows rows, uint index, Bool test);

        ///<summary>
        /// Obtain a reference to a shared component 
        ///</summary>
        ///<code>
        ///void *_ecs_shared(ecs_rows_t *rows, uint32_t index, bool test)
        ///</code>
        [DllImport(DLL, EntryPoint = "_ecs_shared", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr shared(Rows rows, uint index, Bool test);

        ///<summary>
        /// Obtain a single field.  This is an alternative method to ecs_column to access data in a system, which accesses data from individual fields (one column per row). This method is slower than iterating over a column array, but has the added benefit that it automatically abstracts between shared components and owned components. 
        ///</summary>
        ///<remarks>
        /// This is particularly useful if a system is unaware whether a particular  column is from a prefab, as a system does not explicitly state in an argument expression whether prefabs should be matched with, thus it is possible that a system receives both shared and non-shared data for the same column.
        /// When a system uses fields, these differences will be transparent, and it is therefore the method that provides the most flexibility with respect to the kind of data the system can accept.
        ///</remarks>
        ///<code>
        ///void *_ecs_field(ecs_rows_t *rows, uint32_t index, uint32_t column, bool test)
        ///</code>
        [DllImport(DLL, EntryPoint = "_ecs_field", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern IntPtr field(Rows rows, uint index, uint column, Bool test);

        ///<summary>
        /// Obtain the source of a column from inside a system. This operation lets you obtain the entity from which the column data was resolved. In most cases a component will come from the entities being iterated over, but when using prefabs or containers, the component can be shared between entities. For shared components, this function will return the original entity on which the component is stored.
        ///</summary>
        ///<param name="rows"> [in]  Pointer to the rows object passed into the system callback. </param>
        ///<param name="index"> [in]  An index identifying the column for which to obtain the component. </param>
        ///<returns>
        /// The source entity for the column. 
        ///</returns>
        ///<remarks>
        /// If a column is specified for which the component is stored on the entities being iterated over, the operation will return 0, as the entity id in that case depends on the row, not on the column. To obtain the entity ids for a row, a system should access the entity column (column zero) like this:
        /// ecs_entity_t *entities = ecs_column(rows, ecs_entity_t, 0);
        ///</remarks>
        ///<code>
        ///ecs_entity_t _ecs_column_source(ecs_rows_t *rows, uint32_t column, bool test)
        ///</code>
        [DllImport(DLL, EntryPoint = "_ecs_column_source", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId column_source(Rows rows, uint column, Bool test);

        ///<summary>
        /// Obtain the component for a column inside a system. This operation obtains the component handle for a column in the system. This function wraps around the 'components' array in the ecs_rows_t type.
        ///</summary>
        ///<param name="rows"> [in]  Pointer to the rows object passed into the system callback. </param>
        ///<param name="index"> [in]  An index identifying the column for which to obtain the component. </param>
        ///<returns>
        /// The component for the specified column, or 0 if failed.
        ///</returns>
        ///<remarks>
        /// Note that since component identifiers are obtained from the same pool as regular entities, the return type of this function is ecs_entity_t.
        /// When a system contains an argument that is prefixed with 'ID', the resolved entity will be accessible through this function as well.
        /// Column indices for system arguments start from 1, where 0 is reserved for a column that contains entity identifiers. Passing 0 to this function for the column index will return 0.
        ///</remarks>
        ///<code>
        ///ecs_entity_t _ecs_column_entity(ecs_rows_t *rows, uint32_t column, bool test)
        ///</code>
        [DllImport(DLL, EntryPoint = "_ecs_column_entity", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern EntityId column_entity(Rows rows, uint column, Bool test);

        ///<summary>
        /// Obtain the type of a column from inside a system.  This operation is equivalent to ecs_column_entity, except that it returns a type, instead of an entity handle. Invoking this function is the same as doing:
        ///</summary>
        ///<param name="rows"> [in]  Pointer to the rows object passed into the system callback. </param>
        ///<param name="index"> [in]  An index identifying the column for which to obtain the component. </param>
        ///<returns>
        /// The type for the specified column, or 0 if failed.
        ///</returns>
        ///<remarks>
        /// ecs_type_from_entity( ecs_column_entity(rows, index));
        /// This function is wrapped in the following convenience macro which ensures that the type variable is named so it can be used with functions like ecs_add and ecs_set:
        /// ECS_COLUMN_COMPONENT(rows, Position, 1);
        /// After this macro you can invoke functions like ecs_set as you normally would:
        /// ecs_set(world, e, Position, {10, 20});
        ///</remarks>
        ///<code>
        ///ecs_type_t _ecs_column_type(ecs_rows_t *rows, uint32_t column, bool test)
        ///</code>
        [DllImport(DLL, EntryPoint = "_ecs_column_type", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern TypeId column_type(Rows rows, uint column, Bool test);

        ///<summary>
        /// Abort 
        ///</summary>
        ///<code>
        ///void _ecs_abort(uint32_t error_code, const char *param, const char *file,
        ///                uint32_t line)
        ///</code>
        [DllImport(DLL, EntryPoint = "_ecs_abort", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void abort(uint errorCode, CharPtr param, CharPtr file, uint line);

        ///<summary>
        /// Assert 
        ///</summary>
        ///<code>
        ///void _ecs_assert(bool condition, uint32_t error_code, const char *param,
        ///                 const char *condition_str, const char *file, uint32_t line)
        ///</code>
        [DllImport(DLL, EntryPoint = "_ecs_assert", CallingConvention=CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        public static extern void assert(Bool condition, uint errorCode, CharPtr param, CharPtr conditionStr, CharPtr file, uint line);

    }

}

