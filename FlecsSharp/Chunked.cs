using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{
    unsafe partial struct Chunked
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Free()
        {
            ecs.chunked_free( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            ecs.chunked_clear( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr Add(uint size)
        {
            return _ecs.chunked_add( this, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr Remove(uint size, uint index)
        {
            return _ecs.chunked_remove( this, size, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr Get(uint size, uint index)
        {
            return _ecs.chunked_get( this, size, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint Count()
        {
            return ecs.chunked_count( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr GetSparse(uint size, uint index)
        {
            return _ecs.chunked_get_sparse( this, size, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref uint Indices()
        {
            return ref ecs.chunked_indices( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Memory(out uint allocd, out uint used)
        {
            ecs.chunked_memory( this, out allocd, out used);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Chunked ChunkedNew(uint elementSize, uint chunkSize, uint chunkCount)
        {
            return _ecs.chunked_new(elementSize, chunkSize, chunkCount);
        }

    }

}

