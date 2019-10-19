using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace FlecsSharp
{
	unsafe partial struct Vector
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Free()
		{
			ecs.vector_free(this);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			ecs.vector_clear(this);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr Add(out VectorParams @params)
		{
			return ecs.vector_add(out this, out @params);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr Addn(out VectorParams @params, uint count)
		{
			return ecs.vector_addn(out this, out @params, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr Get(out VectorParams @params, uint index)
		{
			return ecs.vector_get(this, out @params, index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint GetIndex(out VectorParams @params, IntPtr elem)
		{
			return ecs.vector_get_index(this, out @params, elem);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr Last(out VectorParams @params)
		{
			return ecs.vector_last(this, out @params);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint Remove(out VectorParams @params, IntPtr elem)
		{
			return ecs.vector_remove(this, out @params, elem);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RemoveLast()
		{
			ecs.vector_remove_last(this);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Pop(out VectorParams @params, IntPtr @value)
		{
			return ecs.vector_pop(this, out @params, @value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint MoveIndex(Vector srcArray, out VectorParams @params, uint index)
		{
			return ecs.vector_move_index(out this, srcArray, out @params, index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint RemoveIndex(out VectorParams @params, uint index)
		{
			return ecs.vector_remove_index(this, out @params, index);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Reclaim(out VectorParams @params)
		{
			ecs.vector_reclaim(out this, out @params);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint SetSize(out VectorParams @params, uint size)
		{
			return ecs.vector_set_size(out this, out @params, size);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint SetCount(out VectorParams @params, uint size)
		{
			return ecs.vector_set_count(out this, out @params, size);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint Count()
		{
			return ecs.vector_count(this);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint Size()
		{
			return ecs.vector_size(this);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr First()
		{
			return ecs.vector_first(this);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Sort(out VectorParams @params, ComparatorDelegate compareAction)
		{
			ecs.vector_sort(this, out @params, compareAction);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Memory(out VectorParams @params, out uint allocd, out uint used)
		{
			ecs.vector_memory(this, out @params, out allocd, out used);
		}
	}
}