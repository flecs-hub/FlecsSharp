using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace Flecs
{
	#region Enums

	// ecs_type_filter_kind_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L161
	public enum TypeFilterKind
	{
		Default = 0,
		All = 1,
		Any = 2,
		Exact = 3,
	}

	// EcsSystemKind: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L76
	public enum SystemKind
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

	public unsafe partial struct OsThread
	{
		public OsThread(UInt64 value) => Value = value;

		UInt64 Value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator OsThread(UInt64 val) => new OsThread(val);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator UInt64(OsThread val) => val.Value;
	}

	public unsafe partial struct OsMutex
	{
		public OsMutex(UInt64 value) => Value = value;

		UInt64 Value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator OsMutex(UInt64 val) => new OsMutex(val);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator UInt64(OsMutex val) => val.Value;
	}

	public unsafe partial struct OsCond
	{
		public OsCond(UInt64 value) => Value = value;

		UInt64 Value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator OsCond(UInt64 val) => new OsCond(val);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator UInt64(OsCond val) => val.Value;
	}

	public unsafe partial struct OsDl
	{
		public OsDl(UInt64 value) => Value = value;

		UInt64 Value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator OsDl(UInt64 val) => new OsDl(val);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator UInt64(OsDl val) => val.Value;
	}

	public unsafe partial struct EntityId : IEquatable<EntityId>
	{
		public static EntityId Zero = (EntityId)0;

		public EntityId(UInt64 value) => Value = value;

		public readonly UInt64 Value;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator EntityId(UInt64 val) => new EntityId(val);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator UInt64(EntityId val) => val.Value;

		public static bool operator ==(EntityId obj1, EntityId obj2) => obj1.Value == obj2.Value;
		public static bool operator !=(EntityId obj1, EntityId obj2) => obj1.Value != obj2.Value;
		public bool Equals(EntityId other) => Value == other.Value;
		public override bool Equals(object obj) => obj is EntityId other && Equals(other);
		public override int GetHashCode() => Value.GetHashCode();

		public override string ToString() => Value.ToString();
	}

	#endregion

	#region Structs

	//ecs_os_api_t
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct OsApi
	{
		public OsApi* Ptr()
		{
			fixed (OsApi* ptr = &this) return ptr;
		}

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
		internal IntPtr _logWarning;
		internal IntPtr _abort;
		internal IntPtr _dlopen;
		internal IntPtr _dlproc;
		internal IntPtr _dlclose;
		internal IntPtr _moduleToDl;
	}

	//ecs_time_t
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Time
	{
		public Time* Ptr()
		{
			fixed (Time* ptr = &this) return ptr;
		}

		public int sec; //size: 4, offset:0
		public uint nanosec; //size: 4, offset:4
	}

	//ecs_vector_params_t
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct VectorParams
	{
		public VectorParams* Ptr()
		{
			fixed (VectorParams* ptr = &this) return ptr;
		}

		internal IntPtr _moveAction;
		public IntPtr moveCtx; //size: 8, offset:8
		public IntPtr ctx; //size: 8, offset:16
		public uint elementSize; //size: 4, offset:24
	}

	//ecs_map_iter_t
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct MapIter
	{
		public MapIter* Ptr()
		{
			fixed (MapIter* ptr = &this) return ptr;
		}

		public Map map; //size: 8, offset:0
		public uint bucketIndex; //size: 4, offset:8
		public uint node; //size: 4, offset:12
	}

	//ecs_world_stats_t
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct WorldStats
	{
		public WorldStats* Ptr()
		{
			fixed (WorldStats* ptr = &this) return ptr;
		}

		public uint systemCount; //size: 4, offset:0
		public uint tableCount; //size: 4, offset:4
		public uint componentCount; //size: 4, offset:8
		public uint entityCount; //size: 4, offset:12
		public uint threadCount; //size: 4, offset:16
		public uint tickCount; //size: 4, offset:20
		public float systemTime; //size: 4, offset:24
		public float frameTime; //size: 4, offset:28
		public float mergeTime; //size: 4, offset:32
		public MemoryStats memory; //size: 64, offset:36
		public Vector features; //size: 8, offset:104
		public Vector onLoadSystems; //size: 8, offset:112
		public Vector postLoadSystems; //size: 8, offset:120
		public Vector preUpdateSystems; //size: 8, offset:128
		public Vector onUpdateSystems; //size: 8, offset:136
		public Vector onValidateSystems; //size: 8, offset:144
		public Vector postUpdateSystems; //size: 8, offset:152
		public Vector preStoreSystems; //size: 8, offset:160
		public Vector onStoreSystems; //size: 8, offset:168
		public Vector taskSystems; //size: 8, offset:176
		public Vector inactiveSystems; //size: 8, offset:184
		public Vector onDemandSystems; //size: 8, offset:192
		public Vector onAddSystems; //size: 8, offset:200
		public Vector onRemoveSystems; //size: 8, offset:208
		public Vector onSetSystems; //size: 8, offset:216
		public Vector components; //size: 8, offset:224
		[MarshalAs(UnmanagedType.U1)] public bool frameProfiling; //size: 1, offset:232
		[MarshalAs(UnmanagedType.U1)] public bool systemProfiling; //size: 1, offset:233

		public void Free() => ecs.free_stats(ref this);
	}

	//EcsMemoryStats
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct MemoryStats
	{
		public MemoryStats* Ptr()
		{
			fixed (MemoryStats* ptr = &this) return ptr;
		}

		public MemoryStat total; //size: 8, offset:0
		public MemoryStat components; //size: 8, offset:8
		public MemoryStat entities; //size: 8, offset:16
		public MemoryStat systems; //size: 8, offset:24
		public MemoryStat families; //size: 8, offset:32
		public MemoryStat tables; //size: 8, offset:40
		public MemoryStat stage; //size: 8, offset:48
		public MemoryStat world; //size: 8, offset:56
	}

	//EcsMemoryStat
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct MemoryStat
	{
		public MemoryStat* Ptr()
		{
			fixed (MemoryStat* ptr = &this) return ptr;
		}

		internal uint allocd; //size: 4, offset:0
		internal uint used; //size: 4, offset:4
	}

	//ecs_table_data_t
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TableData
	{
		public TableData* Ptr()
		{
			fixed (TableData* ptr = &this) return ptr;
		}

		public uint rowCount; //size: 4, offset:0
		public uint columnCount; //size: 4, offset:4
		internal EntityId* entities; //size: 8, offset:8
		internal EntityId* components; //size: 8, offset:16
		internal void* columns; //size: 8, offset:24
	}

	//ecs_type_filter_t
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TypeFilter
	{
		public TypeFilter* Ptr()
		{
			fixed (TypeFilter* ptr = &this) return ptr;
		}

		public TypeId include; //size: 8, offset:0
		public TypeId exclude; //size: 8, offset:8
		public TypeFilterKind includeKind; //size: 4, offset:16
		public TypeFilterKind excludeKind; //size: 4, offset:20
	}

	//ecs_rows_t
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Rows
	{
		public Rows* Ptr()
		{
			fixed (Rows* ptr = &this) return ptr;
		}

		public World world; //size: 8, offset:0
		public EntityId system; //size: 8, offset:8
		internal int* columns; //size: 8, offset:16
		public ushort columnCount; //size: 2, offset:24
		public IntPtr table; //size: 8, offset:32
		public IntPtr tableColumns; //size: 8, offset:40
		public Reference* references; //size: 8, offset:48
		internal EntityId* components; //size: 8, offset:56
		internal EntityId* entities; //size: 8, offset:64
		public IntPtr param; //size: 8, offset:72
		public float deltaTime; //size: 4, offset:80
		public float worldTime; //size: 4, offset:84
		public uint frameOffset; //size: 4, offset:88
		public uint table_offset; //Manually added
		public uint offset; //size: 4, offset:92
		public uint count; //size: 4, offset:96
		public EntityId interruptedBy; //size: 8, offset:104
	}

	//ecs_reference_t
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Reference
	{
		public Reference* Ptr()
		{
			fixed (Reference* ptr = &this) return ptr;
		}

		internal EntityId entity; //size: 8, offset:0
		internal EntityId component; //size: 8, offset:8
		internal IntPtr cachedPtr; //size: 8, offset:16
	}

	#endregion

	#region RefPtr

	//type
	public unsafe partial struct TypeId : IEquatable<TypeId>
	{
		public static TypeId Zero = (TypeId)0;

		public readonly IntPtr ptr;
		public TypeId(IntPtr ptr) => this.ptr = ptr;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator TypeId(int val) => new TypeId(new IntPtr(val));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector*(TypeId type) => (Vector*)type.ptr;

		public Vector* AsVector => (Vector*)ptr;

		/// <summary>
		/// returns all the EntityIds present in this TypeId
		/// </summary>
		public Span<EntityId> Entities => new Span<EntityId>((void*)(ptr + 8), (int)AsVector->count);

		public static bool operator ==(TypeId obj1, TypeId obj2) => obj1.ptr == obj2.ptr;
		public static bool operator !=(TypeId obj1, TypeId obj2) => obj1.ptr != obj2.ptr;
		public bool Equals(TypeId other) => ptr == other.ptr;
		public override bool Equals(object obj) => obj is TypeId other && Equals(other);
		public override int GetHashCode() => ptr.GetHashCode();
	}

	//ecs_table_columns_t
	public unsafe partial struct TableColumns
	{
		IntPtr ptr;
		public TableColumns(IntPtr ptr) => this.ptr = ptr;
		internal TableColumns* Ptr => (TableColumns*)ptr;
	}

	#endregion

	#region OpaquePtrs

	//ecs_vector_t
	public unsafe partial struct Vector
	{
		public uint count;
		public uint size;

		public Span<T> GetContents<T>() where T : unmanaged
		{
			fixed (void* voidPtr = &this)
			{
				// we add 8 to the pointer to account for the header (count and size)
				var intPtr = (IntPtr)voidPtr + 8;
				return new Span<T>((void*)intPtr, (int)count);
			}
		}
	}

	//ecs_chunked_t
	public unsafe partial struct Chunked
	{
		readonly IntPtr ptr;
		public Chunked(IntPtr ptr) => this.ptr = ptr;
		internal Chunked* Ptr => (Chunked*)ptr;
	}

	//ecs_map_t
	public unsafe partial struct Map
	{
		readonly IntPtr ptr;
		public Map(IntPtr ptr) => this.ptr = ptr;
		internal Map* Ptr => (Map*)ptr;
	}

	//ecs_world_t
	public unsafe partial struct World
	{
		readonly IntPtr ptr;
		public World(IntPtr ptr) => this.ptr = ptr;
		internal World* Ptr => (World*)ptr;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator IntPtr(World w) => w.ptr;
	}

	#endregion

	#region Delegates

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate IntPtr OsApiMallocDelegate(UIntPtr size);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate IntPtr OsApiReallocDelegate(IntPtr ptr, UIntPtr size);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate IntPtr OsApiCallocDelegate(UIntPtr num, UIntPtr size);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsApiFreeDelegate(IntPtr ptr);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate OsThread OsApiThreadNewDelegate(OsThread ecsOsThreadT, OsThreadCallbackDelegate callback, IntPtr param);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate IntPtr OsThreadCallbackDelegate(IntPtr param0);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate IntPtr OsApiThreadJoinDelegate(OsThread thread);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate OsMutex OsApiMutexNewDelegate(OsMutex ecsOsMutexT);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsApiMutexFreeDelegate(OsMutex mutex);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsApiMutexLockDelegate(OsMutex mutex);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate OsCond OsApiCondNewDelegate(OsCond ecsOsCondT);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsApiCondFreeDelegate(OsCond cond);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsApiCondSignalDelegate(OsCond cond);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsApiCondBroadcastDelegate(OsCond cond);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsApiCondWaitDelegate(OsCond cond, OsMutex mutex);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsApiSleepDelegate(uint sec, uint nanosec);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsApiGetTimeDelegate(Time* timeOut);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsApiLogDelegate(CharPtr fmt, IntPtr args);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsApiAbortDelegate();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate OsDl OsApiDlopenDelegate(OsDl ecsOsDlT, CharPtr libname);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate OsProcDelegate OsApiDlprocDelegate(OsProcDelegate ecsOsProcT, OsDl lib, CharPtr procname);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsProcDelegate();

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void OsApiDlcloseDelegate(OsDl lib);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate CharPtr OsApiModuleToDlDelegate(CharPtr moduleId);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void MoveDelegate(Vector array, VectorParams* @params, IntPtr to, IntPtr @from, IntPtr ctx);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate int ComparatorDelegate(IntPtr p1, IntPtr p2);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void ModuleInitActionDelegate(World world, int flags);

	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public unsafe delegate void SystemActionDelegate(ref Rows data);

	#endregion
}