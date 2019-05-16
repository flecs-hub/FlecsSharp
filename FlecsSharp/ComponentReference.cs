using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    public unsafe partial struct ComponentReference
    {
        //public static ComponentReference New(in Data val = default) => new ComponentReference(Alloc(in val));
        //public void Dispose()
        //{
            //if(ptr != null) Free(ptr);
        //}

        public EntityId Entity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->entity;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->entity = value; }
        }

        public EntityId Component
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->component;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->component = value; }
        }

        public IntPtr CachedPtr
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->cachedPtr;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->cachedPtr = value;
        }

    }

}

