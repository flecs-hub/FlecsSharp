using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Flecs
{
	[SuppressUnmanagedCodeSecurity]
	public unsafe static partial class ecs
	{
		public struct EcsPrefab
		{
			public EntityId parent;
		}

		internal const string NativeLibName = "flecs_shared";

		public static readonly TypeId TEcsComponent = InteropUtils.LoadTypedSymbol<TypeId>("TEcsComponent");
		public static readonly TypeId TEcsTypeComponent = InteropUtils.LoadTypedSymbol<TypeId>("TEcsTypeComponent");
		public static readonly TypeId TEcsPrefab = InteropUtils.LoadTypedSymbol<TypeId>("TEcsPrefab");
		public static readonly TypeId TEcsPrefabParent = InteropUtils.LoadTypedSymbol<TypeId>("TEcsPrefabParent");
		public static readonly TypeId TEcsPrefabBuilder = InteropUtils.LoadTypedSymbol<TypeId>("TEcsPrefabBuilder");
		public static readonly TypeId TEcsRowSystem = InteropUtils.LoadTypedSymbol<TypeId>("TEcsRowSystem");
		public static readonly TypeId TEcsColSystem = InteropUtils.LoadTypedSymbol<TypeId>("TEcsColSystem");
		public static readonly TypeId TEcsId = InteropUtils.LoadTypedSymbol<TypeId>("TEcsId");
		public static readonly TypeId TEcsHidden = InteropUtils.LoadTypedSymbol<TypeId>("TEcsHidden");
		public static readonly TypeId TEcsDisabled = InteropUtils.LoadTypedSymbol<TypeId>("TEcsDisabled");

		public static readonly EntityId ECS_INSTANCEOF = (EntityId)(1UL << 63);
		public static readonly EntityId ECS_CHILDOF = (EntityId)(1UL << 62);
		public static readonly EntityId ECS_ENTITY_FLAGS_MASK = (EntityId)(ECS_INSTANCEOF.Value | ECS_CHILDOF.Value);
		public static readonly EntityId ECS_ENTITY_MASK = (EntityId)~ECS_ENTITY_FLAGS_MASK.Value;
		public static readonly EntityId ECS_SINGLETON = (EntityId)(ECS_ENTITY_MASK.Value - 1);
		public static readonly EntityId ECS_INVALID_ENTITY = (EntityId)0;

		public static OsApi ecs_os_api => InteropUtils.LoadTypedSymbol<OsApi>("ecs_os_api");

		///<code>
		///void ecs_os_set_api(ecs_os_api_t *)
		///</code>
		// ecs_os_set_api: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/os_api.h#L189
		[DllImport(NativeLibName, EntryPoint = "ecs_os_set_api", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void os_set_api(ref OsApi osApi);

		///<code>
		///void ecs_os_set_api_defaults()
		///</code>
		// ecs_os_set_api_defaults: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/os_api.h#L193
		[DllImport(NativeLibName, EntryPoint = "ecs_os_set_api_defaults", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void os_set_api_defaults();

		///<summary>
		/// Logging (use functions to avoid using variadic macro arguments)
		///</summary>
		///<code>
		///void ecs_os_log(const char *fmt, ...)
		///</code>
		// ecs_os_log: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/os_api.h#L232
		[DllImport(NativeLibName, EntryPoint = "ecs_os_log", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void os_log(string fmt);

		///<code>
		///void ecs_os_warn(const char *, ...)
		///</code>
		// ecs_os_warn: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/os_api.h#L235
		[DllImport(NativeLibName, EntryPoint = "ecs_os_warn", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void os_warn(string fmt);

		///<code>
		///void ecs_os_err(const char *, ...)
		///</code>
		// ecs_os_err: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/os_api.h#L238
		[DllImport(NativeLibName, EntryPoint = "ecs_os_err", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void os_err(string fmt);

		///<code>
		///void ecs_os_dbg(const char *, ...)
		///</code>
		// ecs_os_dbg: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/os_api.h#L241
		[DllImport(NativeLibName, EntryPoint = "ecs_os_dbg", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void os_dbg(string fmt);

		///<code>
		///void ecs_os_enable_dbg(bool)
		///</code>
		// ecs_os_enable_dbg: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/os_api.h#L244
		[DllImport(NativeLibName, EntryPoint = "ecs_os_enable_dbg", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void os_enable_dbg(bool enable);

		///<code>
		///bool ecs_os_dbg_enabled()
		///</code>
		// ecs_os_dbg_enabled: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/os_api.h#L247
		[DllImport(NativeLibName, EntryPoint = "ecs_os_dbg_enabled", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool os_dbg_enabled();

		///<summary>
		/// Sleep with floating point time
		///</summary>
		///<code>
		///void ecs_sleepf(double t)
		///</code>
		// ecs_sleepf: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/os_api.h#L262
		[DllImport(NativeLibName, EntryPoint = "ecs_sleepf", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void sleepf(double t);

		///<summary>
		/// Measure time since provided timestamp
		///</summary>
		///<code>
		///double ecs_time_measure(ecs_time_t *start)
		///</code>
		// ecs_time_measure: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/os_api.h#L267
		[DllImport(NativeLibName, EntryPoint = "ecs_time_measure", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern double time_measure(out Time start);

		#region Vector

		///<code>
		///ecs_vector_t * ecs_vector_new(const ecs_vector_params_t *, uint32_t)
		///</code>
		// ecs_vector_new: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L30
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_new", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern Vector* vector_new(ref VectorParams @params, uint size);

		///<code>
		///ecs_vector_t * ecs_vector_new_from_buffer(const ecs_vector_params_t *, uint32_t, void *)
		///</code>
		// ecs_vector_new_from_buffer: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L35
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_new_from_buffer", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern Vector* vector_new_from_buffer(ref VectorParams @params, uint size, IntPtr buffer);

		///<code>
		///void ecs_vector_free(ecs_vector_t *)
		///</code>
		// ecs_vector_free: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L41
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_free", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void vector_free(Vector* array);

		///<code>
		///void ecs_vector_clear(ecs_vector_t *)
		///</code>
		// ecs_vector_clear: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L45
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_clear", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void vector_clear(Vector* array);

		///<code>
		///void * ecs_vector_add(ecs_vector_t **, const ecs_vector_params_t *)
		///</code>
		// ecs_vector_add: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L49
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_add", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr vector_add(ref Vector* arrayInout, ref VectorParams @params);

		///<code>
		///void * ecs_vector_addn(ecs_vector_t **, const ecs_vector_params_t *, uint32_t)
		///</code>
		// ecs_vector_addn: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L54
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_addn", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr vector_addn(ref Vector* arrayInout, ref VectorParams @params, uint count);

		///<code>
		///void * ecs_vector_get(const ecs_vector_t *, const ecs_vector_params_t *, uint32_t)
		///</code>
		// ecs_vector_get: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L60
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_get", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr vector_get(Vector* array, ref VectorParams @params, uint index);

		///<code>
		///uint32_t ecs_vector_get_index(const ecs_vector_t *, const ecs_vector_params_t *, void *)
		///</code>
		// ecs_vector_get_index: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L66
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_get_index", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint vector_get_index(Vector* array, out VectorParams @params, IntPtr elem);

		///<code>
		///void * ecs_vector_last(const ecs_vector_t *, const ecs_vector_params_t *)
		///</code>
		// ecs_vector_last: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L72
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_last", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr vector_last(Vector* array, out VectorParams @params);

		///<code>
		///uint32_t ecs_vector_remove(ecs_vector_t *, const ecs_vector_params_t *, void *)
		///</code>
		// ecs_vector_remove: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L77
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_remove", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint vector_remove(Vector* array, out VectorParams @params, IntPtr elem);

		///<code>
		///void ecs_vector_remove_last(ecs_vector_t *)
		///</code>
		// ecs_vector_remove_last: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L83
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_remove_last", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void vector_remove_last(Vector* array);

		///<code>
		///bool ecs_vector_pop(ecs_vector_t *, const ecs_vector_params_t *, void *)
		///</code>
		// ecs_vector_pop: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L87
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_pop", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool vector_pop(Vector* array, out VectorParams @params, IntPtr @value);

		///<code>
		///uint32_t ecs_vector_move_index(ecs_vector_t **, ecs_vector_t *, const ecs_vector_params_t *, uint32_t)
		///</code>
		// ecs_vector_move_index: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L93
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_move_index", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint vector_move_index(ref Vector* dstArray, Vector* srcArray, out VectorParams @params, uint index);

		///<code>
		///uint32_t ecs_vector_remove_index(ecs_vector_t *, const ecs_vector_params_t *, uint32_t)
		///</code>
		// ecs_vector_remove_index: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L100
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_remove_index", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint vector_remove_index(Vector* array, out VectorParams @params, uint index);

		///<code>
		///void ecs_vector_reclaim(ecs_vector_t **, const ecs_vector_params_t *)
		///</code>
		// ecs_vector_reclaim: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L106
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_reclaim", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void vector_reclaim(ref Vector* array, out VectorParams @params);

		///<code>
		///uint32_t ecs_vector_set_size(ecs_vector_t **, const ecs_vector_params_t *, uint32_t)
		///</code>
		// ecs_vector_set_size: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L111
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_set_size", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint vector_set_size(ref Vector* array, out VectorParams @params, uint size);

		///<code>
		///uint32_t ecs_vector_set_count(ecs_vector_t **, const ecs_vector_params_t *, uint32_t)
		///</code>
		// ecs_vector_set_count: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L117
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_set_count", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint vector_set_count(ref Vector* array, out VectorParams @params, uint size);

		///<code>
		///uint32_t ecs_vector_count(const ecs_vector_t *)
		///</code>
		// ecs_vector_count: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L123
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_count", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint vector_count(Vector* array);

		///<code>
		///uint32_t ecs_vector_size(const ecs_vector_t *)
		///</code>
		// ecs_vector_size: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L127
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_size", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint vector_size(Vector* array);

		///<code>
		///void * ecs_vector_first(const ecs_vector_t *)
		///</code>
		// ecs_vector_first: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L131
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_first", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr vector_first(Vector* array);

		///<code>
		///void ecs_vector_sort(ecs_vector_t *, const ecs_vector_params_t *, EcsComparator)
		///</code>
		// ecs_vector_sort: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L135
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_sort", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void vector_sort(Vector* array, out VectorParams @params, ComparatorDelegate compareAction);

		///<code>
		///void ecs_vector_memory(const ecs_vector_t *, const ecs_vector_params_t *, uint32_t *, uint32_t *)
		///</code>
		// ecs_vector_memory: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L141
		[DllImport(NativeLibName, EntryPoint = "ecs_vector_memory", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void vector_memory(Vector* array, out VectorParams @params, out uint allocd, out uint used);

		#endregion

		///<code>
		///void ecs_chunked_free(ecs_chunked_t *)
		///</code>
		// ecs_chunked_free: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/chunked.h#L20
		[DllImport(NativeLibName, EntryPoint = "ecs_chunked_free", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void chunked_free(Chunked chunked);

		///<code>
		///void ecs_chunked_clear(ecs_chunked_t *)
		///</code>
		// ecs_chunked_clear: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/chunked.h#L24
		[DllImport(NativeLibName, EntryPoint = "ecs_chunked_clear", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void chunked_clear(Chunked chunked);

		///<code>
		///uint32_t ecs_chunked_count(const ecs_chunked_t *)
		///</code>
		// ecs_chunked_count: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/chunked.h#L54
		[DllImport(NativeLibName, EntryPoint = "ecs_chunked_count", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint chunked_count(Chunked chunked);

		///<code>
		///const uint32_t * ecs_chunked_indices(const ecs_chunked_t *)
		///</code>
		// ecs_chunked_indices: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/chunked.h#L67
		[DllImport(NativeLibName, EntryPoint = "ecs_chunked_indices", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern ref uint chunked_indices(Chunked chunked);

		///<code>
		///void ecs_chunked_memory(ecs_chunked_t *, uint32_t *, uint32_t *)
		///</code>
		// ecs_chunked_memory: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/chunked.h#L71
		[DllImport(NativeLibName, EntryPoint = "ecs_chunked_memory", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void chunked_memory(Chunked chunked, out uint allocd, out uint used);

		///<code>
		///ecs_map_t * ecs_map_new(uint32_t, uint32_t)
		///</code>
		// ecs_map_new: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L17
		[DllImport(NativeLibName, EntryPoint = "ecs_map_new", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern Map map_new(uint size, uint elemSize);

		///<code>
		///void ecs_map_free(ecs_map_t *)
		///</code>
		// ecs_map_free: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L22
		[DllImport(NativeLibName, EntryPoint = "ecs_map_free", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void map_free(Map map);

		///<code>
		///void ecs_map_memory(ecs_map_t *, uint32_t *, uint32_t *)
		///</code>
		// ecs_map_memory: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L26
		[DllImport(NativeLibName, EntryPoint = "ecs_map_memory", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void map_memory(Map map, out uint total, out uint used);

		///<code>
		///uint32_t ecs_map_count(ecs_map_t *)
		///</code>
		// ecs_map_count: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L32
		[DllImport(NativeLibName, EntryPoint = "ecs_map_count", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint map_count(Map map);

		///<code>
		///uint32_t ecs_map_set_size(ecs_map_t *, uint32_t)
		///</code>
		// ecs_map_set_size: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L36
		[DllImport(NativeLibName, EntryPoint = "ecs_map_set_size", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint map_set_size(Map map, uint size);

		///<code>
		///uint32_t ecs_map_data_size(ecs_map_t *)
		///</code>
		// ecs_map_data_size: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L41
		[DllImport(NativeLibName, EntryPoint = "ecs_map_data_size", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint map_data_size(Map map);

		///<code>
		///uint32_t ecs_map_grow(ecs_map_t *, uint32_t)
		///</code>
		// ecs_map_grow: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L45
		[DllImport(NativeLibName, EntryPoint = "ecs_map_grow", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint map_grow(Map map, uint size);

		///<code>
		///uint32_t ecs_map_bucket_count(ecs_map_t *)
		///</code>
		// ecs_map_bucket_count: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L50
		[DllImport(NativeLibName, EntryPoint = "ecs_map_bucket_count", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint map_bucket_count(Map map);

		///<code>
		///void ecs_map_clear(ecs_map_t *)
		///</code>
		// ecs_map_clear: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L54
		[DllImport(NativeLibName, EntryPoint = "ecs_map_clear", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void map_clear(Map map);

		///<code>
		///void * ecs_map_get_ptr(ecs_map_t *, uint64_t)
		///</code>
		// ecs_map_get_ptr: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L78
		[DllImport(NativeLibName, EntryPoint = "ecs_map_get_ptr", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr map_get_ptr(Map map, ulong keyHash);

		///<code>
		///int ecs_map_remove(ecs_map_t *, uint64_t)
		///</code>
		// ecs_map_remove: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L83
		[DllImport(NativeLibName, EntryPoint = "ecs_map_remove", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern int map_remove(Map map, ulong keyHash);

		///<code>
		///ecs_map_iter_t ecs_map_iter(ecs_map_t *)
		///</code>
		// ecs_map_iter: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L88
		[DllImport(NativeLibName, EntryPoint = "ecs_map_iter", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern MapIter map_iter(Map map);

		///<code>
		///bool ecs_map_hasnext(ecs_map_iter_t *)
		///</code>
		// ecs_map_hasnext: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L92
		[DllImport(NativeLibName, EntryPoint = "ecs_map_hasnext", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool map_hasnext(out MapIter it);

		///<code>
		///void * ecs_map_next(ecs_map_iter_t *)
		///</code>
		// ecs_map_next: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L96
		[DllImport(NativeLibName, EntryPoint = "ecs_map_next", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr map_next(out MapIter it);

		///<code>
		///void * ecs_map_next_w_size(ecs_map_iter_t *, size_t)
		///</code>
		// ecs_map_next_w_size: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L100
		[DllImport(NativeLibName, EntryPoint = "ecs_map_next_w_size", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr map_next_w_size(out MapIter it, UIntPtr size);

		///<code>
		///void * ecs_map_next_w_key(ecs_map_iter_t *, uint64_t *)
		///</code>
		// ecs_map_next_w_key: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L114
		[DllImport(NativeLibName, EntryPoint = "ecs_map_next_w_key", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr map_next_w_key(out MapIter it, out ulong keyOut);

		///<code>
		///void * ecs_map_next_w_key_w_size(ecs_map_iter_t *, uint64_t *, size_t)
		///</code>
		// ecs_map_next_w_key_w_size: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L119
		[DllImport(NativeLibName, EntryPoint = "ecs_map_next_w_key_w_size", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr map_next_w_key_w_size(out MapIter it, out ulong keyOut, UIntPtr size);

		///<code>
		///void ecs_get_stats(ecs_world_t *, ecs_world_stats_t *)
		///</code>
		// ecs_get_stats: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/stats.h#L87
		[DllImport(NativeLibName, EntryPoint = "ecs_get_stats", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void get_stats(World world, out WorldStats stats);

		///<code>
		///void ecs_free_stats(ecs_world_stats_t *)
		///</code>
		// ecs_free_stats: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/stats.h#L92
		[DllImport(NativeLibName, EntryPoint = "ecs_free_stats", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void free_stats(ref WorldStats stats);

		///<code>
		///void ecs_measure_frame_time(ecs_world_t *, bool)
		///</code>
		// ecs_measure_frame_time: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/stats.h#L96
		[DllImport(NativeLibName, EntryPoint = "ecs_measure_frame_time", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void measure_frame_time(World world, bool enable);

		///<code>
		///void ecs_measure_system_time(ecs_world_t *, bool)
		///</code>
		// ecs_measure_system_time: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/stats.h#L101
		[DllImport(NativeLibName, EntryPoint = "ecs_measure_system_time", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void measure_system_time(World world, bool enable);

		///<summary>
		/// Create a new world. A world manages all the ECS objects. Applications must have at least one world. Entities, component and system handles are local to a world and cannot be shared between worlds.
		///</summary>
		///<returns>
		/// A new world object
		///</returns>
		///<code>
		///ecs_world_t *ecs_init()
		///</code>
		// ecs_init: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L245
		[DllImport(NativeLibName, EntryPoint = "ecs_init", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
		// ecs_init_w_args: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L272
		[DllImport(NativeLibName, EntryPoint = "ecs_init_w_args", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern World init_w_args(int argc, out sbyte argv);

		///<summary>
		/// Delete a world. This operation deletes the world, and all entities, components and systems within the world.
		///</summary>
		///<param name="world"> [in]  The world to delete.</param>
		///<code>
		///int ecs_fini(ecs_world_t *world)
		///</code>
		// ecs_fini: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L283
		[DllImport(NativeLibName, EntryPoint = "ecs_fini", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern int fini(World world);

		///<summary>
		/// Signal exit This operation signals that the application should quit. It will cause ecs_progress to return false.
		///</summary>
		///<param name="world"> [in]  The world to quit.</param>
		///<code>
		///void ecs_quit(ecs_world_t *world)
		///</code>
		// ecs_quit: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L293
		[DllImport(NativeLibName, EntryPoint = "ecs_quit", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
		// ecs_import_from_library: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L355
		[DllImport(NativeLibName, EntryPoint = "ecs_import_from_library", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId import_from_library(World world, CharPtr libraryName, CharPtr moduleName, int flags);

		///<summary>
		/// Progress a world. This operation progresses the world by running all systems that are both enabled and periodic on their matching entities.
		///</summary>
		///<param name="world"> [in]  The world to progress. </param>
		///<param name="delta_time"> [in]  The time passed since the last frame.</param>
		///<remarks>
		/// To ensure consistency of the data, mutations that add/remove components or create/delete entities are staged and merged after all systems are evaluated. When using multiple threads, each thread will have its own "staging area". Threads will be able to see their own changes, but may not see changes from other threads until changes are merged.
		/// Staging only occurs when ecs_progress is executing systems. The operations that use staging are:
		/// - new_entity - new_w_count - ecs_clone - ecs_delete - add - remove - set
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
		// ecs_progress: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L417
		[DllImport(NativeLibName, EntryPoint = "ecs_progress", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool progress(World world, float deltaTime);

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
		// ecs_merge: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L438
		[DllImport(NativeLibName, EntryPoint = "ecs_merge", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
		// ecs_set_automerge: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L457
		[DllImport(NativeLibName, EntryPoint = "ecs_set_automerge", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void set_automerge(World world, bool autoMerge);

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
		// ecs_set_threads: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L477
		[DllImport(NativeLibName, EntryPoint = "ecs_set_threads", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void set_threads(World world, uint threads);

		///<summary>
		/// Get number of configured threads
		///</summary>
		///<code>
		///uint32_t ecs_get_threads(ecs_world_t *world)
		///</code>
		// ecs_get_threads: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L483
		[DllImport(NativeLibName, EntryPoint = "ecs_get_threads", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint get_threads(World world);

		///<summary>
		/// Get index of current worker thread
		///</summary>
		///<code>
		///uint16_t ecs_get_thread_index(ecs_world_t *world)
		///</code>
		// ecs_get_thread_index: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L488
		[DllImport(NativeLibName, EntryPoint = "ecs_get_thread_index", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern ushort get_thread_index(World world);

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
		// ecs_set_target_fps: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L513
		[DllImport(NativeLibName, EntryPoint = "ecs_set_target_fps", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void set_target_fps(World world, float fps);

		///<summary>
		/// Get number of configured threads
		///</summary>
		///<code>
		///uint32_t ecs_get_target_fps(ecs_world_t *world)
		///</code>
		// ecs_get_target_fps: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L519
		[DllImport(NativeLibName, EntryPoint = "ecs_get_target_fps", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern uint get_target_fps(World world);

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
		// ecs_enable_admin: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L534
		[DllImport(NativeLibName, EntryPoint = "ecs_enable_admin", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern int enable_admin(World world, ushort port);

		///<summary>
		/// Get last used delta time from world
		///</summary>
		///<code>
		///float ecs_get_delta_time(ecs_world_t *world)
		///</code>
		// ecs_get_delta_time: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L540
		[DllImport(NativeLibName, EntryPoint = "ecs_get_delta_time", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
		// ecs_set_context: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L554
		[DllImport(NativeLibName, EntryPoint = "ecs_set_context", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
		// ecs_get_context: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L566
		[DllImport(NativeLibName, EntryPoint = "ecs_get_context", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
		// ecs_get_tick: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L578
		[DllImport(NativeLibName, EntryPoint = "ecs_get_tick", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
		// ecs_dim: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L599
		[DllImport(NativeLibName, EntryPoint = "ecs_dim", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void dim(World world, uint entityCount);

		///<summary>
		/// Set a range for issueing new entity ids. This function constrains the entity identifiers returned by new_entity to the  specified range. This operation can be used to ensure that multiple processes can run in the same simulation without requiring a central service that coordinates issueing identifiers.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="id_start"> [in]  The start of the range. </param>
		///<param name="id_end"> [in]  The end of the range.</param>
		///<remarks>
		/// If id_end is set to 0, the range is infinite. If id_end is set to a non-zero value, it has to be larger than id_start. If id_end is set and new_entity is invoked after an id is issued that is equal to id_end, the application will abort. Flecs does not automatically recycle ids.
		/// The id_end parameter has to be smaller than the last issued identifier.
		///</remarks>
		///<code>
		///void ecs_set_entity_range(ecs_world_t *world, ecs_entity_t id_start,
		///                          ecs_entity_t id_end)
		///</code>
		// ecs_set_entity_range: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L648
		[DllImport(NativeLibName, EntryPoint = "ecs_set_entity_range", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void set_entity_range(World world, EntityId idStart, EntityId idEnd);

		///<summary>
		/// Temporarily enable/disable range limits. When an application is both a receiver of range-limited entities and a producer of range-limited entities, range checking needs to be temporarily disabled when receiving entities.
		///</summary>
		///<remarks>
		/// Range checking is disabled on a stage, so setting this value is thread safe.
		///</remarks>
		///<code>
		///bool ecs_enable_range_check(ecs_world_t *world, bool enable)
		///</code>
		// ecs_enable_range_check: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L661
		[DllImport(NativeLibName, EntryPoint = "ecs_enable_range_check", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool enable_range_check(World world, bool enable);

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
		// ecs_set_w_data: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L758
		[DllImport(NativeLibName, EntryPoint = "ecs_set_w_data", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId set_w_data(World world, ref TableData data);

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
		// ecs_clone: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L857
		[DllImport(NativeLibName, EntryPoint = "ecs_clone", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId clone(World world, EntityId entity, bool copyValue);

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
		// ecs_delete: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L878
		[DllImport(NativeLibName, EntryPoint = "ecs_delete", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void delete(World world, EntityId entity);

		///<summary>
		/// Delete all entities containing a (set of) component(s).  This operation provides a more efficient alternative to deleting entities one by one by deleting an entire table or set of tables in a single operation. The operation will clear all tables that match the specified table.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="filter"> [in]  Filter that matches zero or more tables.</param>
		///<code>
		///void ecs_delete_w_filter(ecs_world_t *world, ecs_type_filter_t *filter)
		///</code>
		// ecs_delete_w_filter: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L891
		[DllImport(NativeLibName, EntryPoint = "ecs_delete_w_filter", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void delete_w_filter(World world, out TypeFilter filter);

		///<summary>
		/// Adopt a child entity by a parent. This operation adds the specified parent entity to the type of the specified entity, which effectively establishes a parent-child relationship. The parent entity, when added, behaves like a normal component in that it becomes part of the entity type.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity to adopt. </param>
		///<param name="parent"> [in]  The parent entity to add to the entity.</param>
		///<remarks>
		/// If the parent was already added to the entity, this operation will have no effect.
		/// This operation is similar to an add, with as difference that instead of a  type it accepts any entity handle.
		///</remarks>
		///<code>
		///void ecs_adopt(ecs_world_t *world, ecs_entity_t entity, ecs_entity_t parent)
		///</code>
		// ecs_adopt: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L981
		[DllImport(NativeLibName, EntryPoint = "ecs_adopt", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void adopt(World world, EntityId entity, EntityId parent);

		///<summary>
		/// Orphan a child by a parent.  This operation removes the specified parent entity from the type of the specified entity. If the parent was not added to the entity, this operation has no effect.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity to orphan. </param>
		///<param name="parent"> [in]  The parent entity to remove from the entity.</param>
		///<remarks>
		/// This operation is similar to remove, with as difference that instead of a type it accepts any entity handle.
		///</remarks>
		///<code>
		///void ecs_orphan(ecs_world_t *world, ecs_entity_t entity, ecs_entity_t parent)
		///</code>
		// ecs_orphan: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L999
		[DllImport(NativeLibName, EntryPoint = "ecs_orphan", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void orphan(World world, EntityId entity, EntityId parent);

		///<summary>
		/// Inherit from a base. This operation adds a base to an entity, which will cause the entity to inherit the components of the base. If the entity already inherited from the specified base, this operation does nothing.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity to add the base to. </param>
		///<param name="base"> [in]  The base to add to the entity.</param>
		///<code>
		///void ecs_inherit(ecs_world_t *world, ecs_entity_t entity, ecs_entity_t base)
		///</code>
		// ecs_inherit: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1014
		[DllImport(NativeLibName, EntryPoint = "ecs_inherit", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void inherit(World world, EntityId entity, EntityId @base);

		///<summary>
		/// Disinherit from a base. This operation removes a base from an entity, which will cause the entity to no longer inherit the components of the base. If the entity did not inherit from the specified base, this operation does nothing.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="entity"> [in]  The entity to remove the base from. </param>
		///<param name="base"> [in]  The base to remove from the entity.</param>
		///<code>
		///void ecs_disinherit(ecs_world_t *world, ecs_entity_t entity, ecs_entity_t base)
		///</code>
		// ecs_disinherit: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1029
		[DllImport(NativeLibName, EntryPoint = "ecs_disinherit", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void disinherit(World world, EntityId entity, EntityId @base);

		///<code>
		///bool ecs_has_entity(ecs_world_t *, ecs_entity_t, ecs_entity_t)
		///</code>
		// ecs_has_entity: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1220
		[DllImport(NativeLibName, EntryPoint = "ecs_has_entity", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool has_entity(World world, EntityId entity, EntityId component);

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
		/// This function is similar to has, with as difference that instead of a  type it accepts a handle to any entity.
		///</remarks>
		///<code>
		///bool ecs_contains(ecs_world_t *world, ecs_entity_t parent, ecs_entity_t child)
		///</code>
		// ecs_contains: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1238
		[DllImport(NativeLibName, EntryPoint = "ecs_contains", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool contains(World world, EntityId parent, EntityId child);

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
		// ecs_get_type: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1278
		[DllImport(NativeLibName, EntryPoint = "ecs_get_type", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
		// ecs_get_id: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1295
		[DllImport(NativeLibName, EntryPoint = "ecs_get_id", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
		// ecs_is_empty: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1308
		[DllImport(NativeLibName, EntryPoint = "ecs_is_empty", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool is_empty(World world, EntityId entity);

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
		// ecs_lookup: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1341
		[DllImport(NativeLibName, EntryPoint = "ecs_lookup", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId lookup(World world, string id);

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
		// ecs_lookup_child: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1362
		[DllImport(NativeLibName, EntryPoint = "ecs_lookup_child", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId lookup_child(World world, EntityId parent, string id);

		///<summary>
		/// Get a type from an entity. This function returns a type that can be added/removed to entities. If you create a new component, type or prefab with the ecs_new_* function, you get an ecs_entity_t handle which provides access to builtin components associated with the component, type or prefab.
		///</summary>
		///<remarks>
		/// To add a component to an entity, you first have to obtain its type. Types uniquely identify sets of one or more components, and can be used with functions like add and remove.
		/// You can only obtain types from entities that have EcsComponent, EcsPrefab, or EcsTypeComponent. These components are automatically added by the ecs_new_* functions, but can also be added manually.
		/// The ECS_COMPONENT, ECS_TAG, ECS_TYPE or ECS_PREFAB macro's will auto- declare a variable containing the type called tFoo (where 'Foo' is the id provided to the macro).
		///</remarks>
		///<code>
		///ecs_type_t ecs_type_from_entity(ecs_world_t *world, ecs_entity_t entity)
		///</code>
		// ecs_type_from_entity: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1389
		[DllImport(NativeLibName, EntryPoint = "ecs_type_from_entity", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
		///ecs_entity_t ecs_type_to_entity(ecs_world_t *world, ecs_type_t type)
		///</code>
		// ecs_type_to_entity: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1407
		[DllImport(NativeLibName, EntryPoint = "ecs_type_to_entity", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId type_to_entity(World world, TypeId type);

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
		// ecs_type_add: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1422
		[DllImport(NativeLibName, EntryPoint = "ecs_type_add", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern TypeId type_add(World world, TypeId type, EntityId entity);

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
		// ecs_type_merge: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1442
		[DllImport(NativeLibName, EntryPoint = "ecs_type_merge", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern TypeId type_merge(World world, TypeId type, TypeId typeAdd, TypeId typeRemove);

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
		// ecs_type_find: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1459
		[DllImport(NativeLibName, EntryPoint = "ecs_type_find", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern TypeId type_find(World world, out EntityId array, uint count);

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
		// ecs_type_get_entity: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1474
		[DllImport(NativeLibName, EntryPoint = "ecs_type_get_entity", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId type_get_entity(World world, TypeId type, uint index);

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
		// ecs_type_has_entity: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1488
		[DllImport(NativeLibName, EntryPoint = "ecs_type_has_entity", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool type_has_entity(World world, TypeId type, EntityId entity);

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
		// ecs_expr_to_type: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1528
		[DllImport(NativeLibName, EntryPoint = "ecs_expr_to_type", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern TypeId expr_to_type(World world, string expr);

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
		// ecs_type_to_expr: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1544
		[DllImport(NativeLibName, EntryPoint = "ecs_type_to_expr", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern CharPtr type_to_expr(World world, TypeId type);

		///<code>
		///bool ecs_type_match_w_filter(ecs_world_t *, ecs_type_t, ecs_type_filter_t *)
		///</code>
		// ecs_type_match_w_filter: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1549
		[DllImport(NativeLibName, EntryPoint = "ecs_type_match_w_filter", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool type_match_w_filter(World world, TypeId type, out TypeFilter filter);

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
		// ecs_enable: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1571
		[DllImport(NativeLibName, EntryPoint = "ecs_enable", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void enable(World world, EntityId system, bool enabled);

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
		// ecs_set_period: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1598
		[DllImport(NativeLibName, EntryPoint = "ecs_set_period", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
		// ecs_is_enabled: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1614
		[DllImport(NativeLibName, EntryPoint = "ecs_is_enabled", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool is_enabled(World world, EntityId system);

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
		// ecs_run: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1646
		[DllImport(NativeLibName, EntryPoint = "ecs_run", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId run(World world, EntityId system, float deltaTime, IntPtr param);

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
		// ecs_set_system_context: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1697
		[DllImport(NativeLibName, EntryPoint = "ecs_set_system_context", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern void set_system_context(World world, EntityId system, IntPtr ctx);

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
		// ecs_get_system_context: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1710
		[DllImport(NativeLibName, EntryPoint = "ecs_get_system_context", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr get_system_context(World world, EntityId system);

		///<summary>
		/// Test if column is shared or not.  The following signature shows an example of owned components and shared components:
		///</summary>
		///<param name="rows"> [in]  The rows parameter passed into the system. </param>
		///<param name="index"> [in]  The index identifying the column in a system signature. </param>
		///<returns>
		/// true if the column is shared, false if it is owned.
		///</returns>
		///<remarks>
		/// Position, CONTAINER.Velocity, MyEntity.Mass
		/// Position is an owned component, while Velocity and Mass are shared  components. While these kinds of relationships are expressed explicity in a system signature, inheritance relationships are implicit. The above signature matches both entities for which Position is owned as well as entities for which Position appears in an entity that they inherit from.
		/// If a system needs to support both cases, it needs to test whether the component is shared or not. This test only needs to happen once per system callback invocation, as all the entities being iterated over will either own or not own the component.
		///</remarks>
		///<code>
		///bool ecs_is_shared(ecs_rows_t *rows, uint32_t column)
		///</code>
		// ecs_is_shared: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1767
		[DllImport(NativeLibName, EntryPoint = "ecs_is_shared", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern bool is_shared(ref Rows rows, uint column);

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
		/// ecs_entity_t *entities = column(rows, ecs_entity_t, 0);
		///</remarks>
		///<code>
		///ecs_entity_t ecs_column_source(ecs_rows_t *rows, uint32_t column)
		///</code>
		// ecs_column_source: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1815
		[DllImport(NativeLibName, EntryPoint = "ecs_column_source", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId column_source(ref Rows rows, uint column);

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
		///ecs_entity_t ecs_column_entity(ecs_rows_t *rows, uint32_t column)
		///</code>
		// ecs_column_entity: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1838
		[DllImport(NativeLibName, EntryPoint = "ecs_column_entity", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId column_entity(ref Rows rows, uint column);

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
		/// This function is wrapped in the following convenience macro which ensures that the type variable is named so it can be used with functions like add and set:
		/// ECS_COLUMN_COMPONENT(rows, Position, 1);
		/// After this macro you can invoke functions like set as you normally would:
		/// set(world, e, Position, {10, 20});
		///</remarks>
		///<code>
		///ecs_type_t ecs_column_type(ecs_rows_t *rows, uint32_t column)
		///</code>
		// ecs_column_type: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1864
		[DllImport(NativeLibName, EntryPoint = "ecs_column_type", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern TypeId column_type(ref Rows rows, uint column);

		///<summary>
		/// Get type of table that system is currently iterating over.
		///</summary>
		///<code>
		///ecs_type_t ecs_table_type(ecs_rows_t *rows)
		///</code>
		// ecs_table_type: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1870
		[DllImport(NativeLibName, EntryPoint = "ecs_table_type", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern TypeId table_type(ref Rows rows);

		///<summary>
		/// Get type of table that system is currently iterating over.
		///</summary>
		///<code>
		///void *ecs_table_column(ecs_rows_t *rows, uint32_t column)
		///</code>
		// ecs_table_column: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1875
		[DllImport(NativeLibName, EntryPoint = "ecs_table_column", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr table_column(ref Rows rows, uint column);

		///<summary>
		/// Convenience function to create an entity with id and component expression. This is equivalent to calling new_entity with a type that contains all  components provided in the 'component' expression. In addition, this function also adds the EcsId component, which will be set to the provided id string.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="id"> [in]  The entity id. </param>
		///<param name="expr"> [in]  A component expression. </param>
		///<returns>
		/// A handle to the created entity.
		///</returns>
		///<remarks>
		/// This function is wrapped by the ECS_ENTITY convenience macro.
		/// ecs_new_component ecs_new_system ecs_new_prefab ecs_new_type new_child new_entity new_w_count
		///</remarks>
		///<code>
		///ecs_entity_t ecs_new_entity(ecs_world_t *world, const char *id,
		///                            const char *components)
		///</code>
		// ecs_new_entity: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1895
		[DllImport(NativeLibName, EntryPoint = "ecs_new_entity", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId new_entity(World world, CharPtr id, string expr);

		///<summary>
		/// Create a new component. This operation creates a new component with a specified id and size. After this operation is called, the component can be added to entities by using the returned handle with add.
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
		// ecs_new_component: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1924
		[DllImport(NativeLibName, EntryPoint = "ecs_new_component", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
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
		// ecs_new_system: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1963
		[DllImport(NativeLibName, EntryPoint = "ecs_new_system", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId new_system(World world, CharPtr id, SystemKind kind, string sig, SystemActionDelegate action);

		///<summary>
		/// Get handle to type. This operation obtains a handle to a type that can be used with new_entity. Predefining types has performance benefits over using add/remove multiple times, as it provides constant creation time regardless of the number of components. This function will internally create a table for the type.
		///</summary>
		///<param name="world"> [in]  The world. </param>
		///<param name="expr"> [in]  A comma-separated string with the component identifiers. </param>
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
		// ecs_new_type: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L1988
		[DllImport(NativeLibName, EntryPoint = "ecs_new_type", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId new_type(World world, CharPtr id, string expr);

		///<summary>
		/// Create a new prefab entity. Prefab entities allow entities to share a set of components. Components of the prefab will appear on the specified entity when using any of the API functions and ECS systems.
		///</summary>
		///<remarks>
		/// A prefab is a regular entity, with the only difference that it has the EcsPrefab component.
		/// The ECS_PREFAB macro wraps around this function.
		/// Changing the value of one of the components on the prefab will change the value for all entities that added the prefab, as components are stored only once in memory. This makes prefabs also a memory-saving mechanism; there can be many entities that reuse component records from the prefab.
		/// Entities can override components from a prefab by adding the component with add. When a component is overridden, its value will be copied from the prefab. This technique can be combined with families to automatically initialize entities, like this:
		/// ECS_PREFAB(world, MyPrefab, Foo); ECS_TYPE(world, MyType, MyPrefab, Foo); ecs_entity_t e = new_entity(world, MyType);
		/// In this code, the entity will be created with the prefab and directly override 'Foo', which will copy the value of 'Foo' from the prefab.
		/// Prefabs are explicitly stored on the component list of an entity. This means that two entities with the same set of components but a different prefab are stored in different tables.
		/// Prefabs can be part of the component list of other prefabs. This allows for creating hierarchies of prefabs, where the leaves are the most specialized.
		/// Only one prefab may be added to an entity.
		///</remarks>
		///<code>
		///ecs_entity_t ecs_new_prefab(ecs_world_t *world, const char *id, const char *sig)
		///</code>
		// ecs_new_prefab: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L2030
		[DllImport(NativeLibName, EntryPoint = "ecs_new_prefab", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern EntityId new_prefab(World world, CharPtr id, string sig);

		///<summary>
		/// Get description for error code
		///</summary>
		///<code>
		///const char *ecs_strerror(uint32_t error_code)
		///</code>
		// ecs_strerror: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L2040
		[DllImport(NativeLibName, EntryPoint = "ecs_strerror", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
		public static extern IntPtr strerror(uint errorCode);
	}
}
