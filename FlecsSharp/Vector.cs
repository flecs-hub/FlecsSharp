using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    public unsafe partial struct Vector
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Free()
        {
            ecs.vector_free( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            ecs.vector_clear( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr Add(VectorParams @params)
        {
            fixed(Vector* thisPtr = &this)
            return ecs.vector_add( thisPtr, @params);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr Addn(VectorParams @params, uint count)
        {
            fixed(Vector* thisPtr = &this)
            return ecs.vector_addn( thisPtr, @params, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr Get(VectorParams @params, uint index)
        {
            return ecs.vector_get( this, @params, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetIndex(VectorParams @params, IntPtr elem)
        {
            return ecs.vector_get_index( this, @params, elem);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr Last(VectorParams @params)
        {
            return ecs.vector_last( this, @params);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint Remove(VectorParams @params, IntPtr elem)
        {
            return ecs.vector_remove( this, @params, elem);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveLast()
        {
            ecs.vector_remove_last( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint MoveIndex(Vector srcArray, VectorParams @params, uint index)
        {
            fixed(Vector* thisPtr = &this)
            return ecs.vector_move_index( thisPtr, srcArray, @params, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint RemoveIndex(VectorParams @params, uint index)
        {
            return ecs.vector_remove_index( this, @params, index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reclaim(VectorParams @params)
        {
            fixed(Vector* thisPtr = &this)
            ecs.vector_reclaim( thisPtr, @params);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint SetSize(VectorParams @params, uint size)
        {
            fixed(Vector* thisPtr = &this)
            return ecs.vector_set_size( thisPtr, @params, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint SetCount(VectorParams @params, uint size)
        {
            fixed(Vector* thisPtr = &this)
            return ecs.vector_set_count( thisPtr, @params, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint Count()
        {
            return ecs.vector_count( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint Size()
        {
            return ecs.vector_size( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr First()
        {
            return ecs.vector_first( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Sort(VectorParams @params, ComparatorDelegate compareAction)
        {
            ecs.vector_sort( this, @params, compareAction);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Memory(VectorParams @params, uint* allocd, uint* used)
        {
            ecs.vector_memory( this, @params, allocd, used);
        }

    }

}

