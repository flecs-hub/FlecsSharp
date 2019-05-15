using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    public unsafe partial struct Map
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Free()
        {
            ecs.map_free( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Memory(uint* total, uint* used)
        {
            ecs.map_memory( this, total, used);
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
        public void Set64(ulong keyHash, ulong data)
        {
            ecs.map_set64( this, keyHash, data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong Get64(ulong keyHash)
        {
            return ecs.map_get64( this, keyHash);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enable Has(ulong keyHash, ulong* valueOut)
        {
            return ecs.map_has( this, keyHash, valueOut);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Remove(ulong keyHash)
        {
            return ecs.map_remove( this, keyHash);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Iter EcsMapIter(MapIter iterData)
        {
            return _ecs.map_iter( this, iterData);
        }

    }

}

