using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{
    // ecs_map_iter_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/map.h#L10
    unsafe partial struct MapIter
    {
        public Map Map
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => map;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => map = value;
        }

        public uint BucketIndex
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => bucketIndex;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => bucketIndex = value;
        }

        public uint Node
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => node;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => node = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MapHasnext()
        {
            return ecs.map_hasnext(out this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr MapNext()
        {
            return ecs.map_next(out this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr MapNextWSize(UIntPtr size)
        {
            return ecs.map_next_w_size(out this, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr MapNextWKey(out ulong keyOut)
        {
            return ecs.map_next_w_key(out this, out keyOut);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr MapNextWKeyWSize(out ulong keyOut, UIntPtr size)
        {
            return ecs.map_next_w_key_w_size(out this, out keyOut, size);
        }

    }

}

