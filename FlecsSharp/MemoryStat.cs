using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{
    // EcsMemoryStat: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/stats.h#L39
    unsafe partial struct MemoryStat
    {
        public uint Allocd
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => allocd;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => allocd = value;
        }

        public uint Used
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => used;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => used = value;
        }

    }

}

