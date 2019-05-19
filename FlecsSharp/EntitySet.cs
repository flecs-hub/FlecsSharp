using System;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{

    public unsafe ref struct Set<T> where T : unmanaged
    {
        private T* colPtr;
        public Set(T* tPtr)
        {
            this.colPtr = tPtr;
        }

        public ref T this[uint i]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref colPtr[i];
        }
    }

    unsafe readonly partial struct EntitySet
    {

        public EntityId this[uint i]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => this.ptr->entities[i];
        }

        public World World
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->world;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //set => ptr->world = value; }
        }

        public EntityId System
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->system;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //set => ptr->system = value; }
        }


        public IntPtr Param
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->param;
        }

        public float DeltaTime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->deltaTime;
        }

        public uint FrameOffset
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->frameOffset;
        }

        public uint Offset
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->offset;
        }

        public uint Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->count;
        }

        public EntityId InterruptedBy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->interruptedBy;
        }
    }

}

