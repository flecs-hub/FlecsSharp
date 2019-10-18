using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{
    public unsafe static partial class FlecsSharp
    {
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
        public static void OsLog(CharPtr fmt)
        {
            ecs.os_log(fmt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OsWarn(CharPtr fmt)
        {
            ecs.os_warn(fmt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OsErr(CharPtr fmt)
        {
            ecs.os_err(fmt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OsDbg(CharPtr fmt)
        {
            ecs.os_dbg(fmt);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void OsEnableDbg(bool enable)
        {
            ecs.os_enable_dbg(enable);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool OsDbgEnabled()
        {
            return ecs.os_dbg_enabled();
        }

        ///<summary>
        /// Sleep with floating point time 
        ///</summary>
        ///<code>
        ///void ecs_sleepf(double t)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Sleepf(double t)
        {
            ecs.sleepf(t);
        }

        ///<summary>
        /// Get description for error code 
        ///</summary>
        ///<code>
        ///const char *ecs_strerror(uint32_t error_code)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CharPtr Strerror(uint errorCode)
        {
            return ecs.strerror(errorCode);
        }

        ///<summary>
        /// Abort 
        ///</summary>
        ///<code>
        ///void _ecs_abort(uint32_t error_code, const char *param, const char *file,
        ///                uint32_t line)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abort(uint errorCode, CharPtr param, CharPtr file, uint line)
        {
            _ecs.abort(errorCode, param, file, line);
        }

        ///<summary>
        /// Assert 
        ///</summary>
        ///<code>
        ///void _ecs_assert(bool condition, uint32_t error_code, const char *param,
        ///                 const char *condition_str, const char *file, uint32_t line)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert(bool condition, uint errorCode, CharPtr param, CharPtr conditionStr, CharPtr file, uint line)
        {
            _ecs.assert(condition, errorCode, param, conditionStr, file, line);
        }

    }

}

