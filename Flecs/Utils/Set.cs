using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Flecs
{
	/// <summary>
	/// convenience wrapper for accessing a pointer to an array
	/// </summary>
	public unsafe ref struct Set<T> where T : unmanaged
	{
		readonly T* _columnPtr;
		readonly uint _length;

		public Set(T* ptr, uint length)
		{
			_columnPtr = ptr;
			_length = length;
		}

		public ref T this[uint i]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				BoundsCheck(i);
				return ref _columnPtr[i];
			}
		}

		public ref T this[int i]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				BoundsCheck((uint)i);
				return ref _columnPtr[i];
			}
		}

		public uint Count => _length;

		[Conditional("DEBUG")]
		void BoundsCheck(uint i)
		{
			if (i >= _length)
				throw new System.IndexOutOfRangeException();
		}
	}
}
