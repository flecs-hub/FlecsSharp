using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    unsafe partial struct MemStat
    {
        public uint Allocd
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->allocd;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->allocd = value;
        }

        public uint Used
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->used;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->used = value;
        }

    }

}

