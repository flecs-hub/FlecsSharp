using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{
    unsafe partial struct Map
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Free()
        {
            ecs.map_free( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Memory(out uint total, out uint used)
        {
            ecs.map_memory( this, out total, out used);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint Count()
        {
            return ecs.map_count( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint SetSize(uint size)
        {
            return ecs.map_set_size( this, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint DataSize()
        {
            return ecs.map_data_size( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint Grow(uint size)
        {
            return ecs.map_grow( this, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint BucketCount()
        {
            return ecs.map_bucket_count( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            ecs.map_clear( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr Set(ulong keyHash, IntPtr data, uint size)
        {
            return _ecs.map_set( this, keyHash, data, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has(ulong keyHash, IntPtr valueOut, uint size)
        {
            return _ecs.map_has( this, keyHash, valueOut, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr GetPtr(ulong keyHash)
        {
            return ecs.map_get_ptr( this, keyHash);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Remove(ulong keyHash)
        {
            return ecs.map_remove( this, keyHash);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MapIter Iter()
        {
            return ecs.map_iter( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map MapNew(uint size, uint elemSize)
        {
            return ecs.map_new(size, elemSize);
        }

    }

}

