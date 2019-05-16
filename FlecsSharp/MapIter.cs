using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    unsafe partial struct MapIter
    {
        public uint BucketIndex
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->bucketIndex;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->bucketIndex = value;
        }

        public uint Node
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->node;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->node = value;
        }

    }

}

