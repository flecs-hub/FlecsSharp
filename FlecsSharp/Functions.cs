using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    public unsafe static partial class Functions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OsSetApi(OsApi osApi)
        {
            ecs.os_set_api(osApi);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OsSetApiDefaults()
        {
            ecs.os_set_api_defaults();
        }

        ///<summary>
        /// Logging (use functions to avoid using variadic macro arguments) 
        ///</summary>
        ///<code>
        ///void ecs_os_log(const char *fmt, ...)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OsLog(ReadOnlySpan<char> fmt)
        {
            using(var fmtStr = fmt.ToAnsiString())
            ecs.os_log(fmtStr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OsErr(ReadOnlySpan<char> fmt)
        {
            using(var fmtStr = fmt.ToAnsiString())
            ecs.os_err(fmtStr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OsDbg(ReadOnlySpan<char> fmt)
        {
            using(var fmtStr = fmt.ToAnsiString())
            ecs.os_dbg(fmtStr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OsEnableDbg(bool enable)
        {
            ecs.os_enable_dbg(enable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector VectorNew(VectorParams @params, uint size)
        {
            return ecs.vector_new(@params, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector VectorNewFromBuffer(VectorParams @params, uint size, IntPtr buffer)
        {
            return ecs.vector_new_from_buffer(@params, size, buffer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map MapNew(uint size)
        {
            return ecs.map_new(size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FreeStats(WorldStats stats)
        {
            ecs.free_stats(stats);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Sleepf(double t)
        {
            ecs.sleepf(t);
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
        public static World InitWArgs(int argc, sbyte* argv)
        {
            return ecs.init_w_args(argc, argv);
        }

        ///<summary>
        /// Get description for error code 
        ///</summary>
        ///<code>
        ///const char *ecs_strerror(uint32_t error_code)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<char> Strerror(uint errorCode)
        {
            return ecs.strerror(errorCode).ToString();
        }

        ///<summary>
        /// Abort 
        ///</summary>
        ///<code>
        ///void _ecs_abort(uint32_t error_code, const char *param, const char *file,
        ///                uint32_t line)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abort(uint errorCode, ReadOnlySpan<char> param, ReadOnlySpan<char> file, uint line)
        {
            using(var paramStr = param.ToAnsiString())
            using(var fileStr = file.ToAnsiString())
            _ecs.abort(errorCode, paramStr, fileStr, line);
        }

        ///<summary>
        /// Assert 
        ///</summary>
        ///<code>
        ///void _ecs_assert(bool condition, uint32_t error_code, const char *param,
        ///                 const char *condition_str, const char *file, uint32_t line)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert(bool condition, uint errorCode, ReadOnlySpan<char> param, ReadOnlySpan<char> conditionStr, ReadOnlySpan<char> file, uint line)
        {
            using(var paramStr = param.ToAnsiString())
            using(var conditionStrStr = conditionStr.ToAnsiString())
            using(var fileStr = file.ToAnsiString())
            _ecs.assert(condition, errorCode, paramStr, conditionStrStr, fileStr, line);
        }

    }

}

