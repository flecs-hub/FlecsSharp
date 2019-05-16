using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    unsafe partial struct Time
    {
        public int Sec
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.sec;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.sec = value;
        }

        public uint Nanosec
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.nanosec;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.nanosec = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double ToDouble()
        {
            return ecs.time_to_double( this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Time Sub(Time t2)
        {
            return ecs.time_sub( this, t2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Measure()
        {
            fixed(Time* thisPtr = &this)
            return ecs.time_measure( thisPtr);
        }

    }

}

