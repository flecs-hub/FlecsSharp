using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    public unsafe partial struct MemStat
    {
        //public static MemStat New(in Data val = default) => new MemStat(Alloc(in val));
        //public void Dispose()
        //{
            //if(ptr != null) Free(ptr);
        //}

        public uint Allocd
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->allocd;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->allocd = value;
        }

        public uint Used
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->used;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->used = value;
        }

    }

}

