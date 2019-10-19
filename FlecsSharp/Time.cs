using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace FlecsSharp
{
	// ecs_time_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L34
	unsafe partial struct Time
	{
		public int Sec
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => sec;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => sec = value;
		}

		public uint Nanosec
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => nanosec;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => nanosec = value;
		}

		///<summary>
		/// Measure time since provided timestamp
		///</summary>
		///<code>
		///double ecs_time_measure(ecs_time_t *start)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double Measure()
		{
			return ecs.time_measure(out this);
		}
	}
}