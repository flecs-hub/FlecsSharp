using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Flecs
{
    public unsafe struct UnmanagedStringBuffer : IDisposable
    {
        private struct Header
        {
            internal int Size;
            internal int CurrentOffset;
            internal int Start;
        }

        const int NATURAL_ALIGNMENT = 16;
        const int STR_ALIGN = 8;
        Header* _header;

        byte* ThisPtr => (byte*)_header;

		private UnmanagedStringBuffer(Header* header) => _header = header;

		public static UnmanagedStringBuffer Create(int size = 4096) => new UnmanagedStringBuffer(AllocateHeader(size));

		static Header* AllocateHeader(int size = 4096)
		{
			var mem = (Header*)Heap.Alloc(size);
			mem->Size = size;

			var offset = Marshal.SizeOf<Header>();
			offset += offset % NATURAL_ALIGNMENT;

			mem->CurrentOffset = offset;
			mem->Start = offset;

			return mem;
		}

        public void Dispose() => Heap.Free(_header);

        Span<byte> GetAvailableSpan(int requiredSize, out IntPtr startPtr)
        {
            var aligned = ThisPtr + _header->CurrentOffset;
            var alignOffset = ((long)aligned) % STR_ALIGN;
            aligned += alignOffset;
            startPtr = (IntPtr)aligned;

            var sizeLeft = _header->Size - (int)(aligned - ThisPtr);
            if (sizeLeft <= requiredSize)
            {
				// when we run out of space we allocate a new block of memory and just let the previous one live
				_header = AllocateHeader();
				return GetAvailableSpan(requiredSize, out startPtr);
            }

            _header->CurrentOffset += requiredSize + (int)alignOffset;

            return new Span<byte>(aligned, requiredSize);
        }

        public CharPtr AddString(string str)
        {
            var strSize = Encoding.UTF8.GetByteCount(str);

            var available = GetAvailableSpan(strSize + 1, out var charPtr);
            var len = Encoding.UTF8.GetBytes(str, available);
            available[len] = 0; // null terminated

            return (CharPtr)charPtr;
        }

        public override string ToString()
        {
            var startPtr = ThisPtr + _header->Start;
            var offset = _header->CurrentOffset - _header->Start;
            var span = new Span<byte>(startPtr, offset);

            return Encoding.UTF8.GetString(span);
        }
    }


    public unsafe static class Heap
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IntPtr Alloc(int size) => Marshal.AllocHGlobal((int)size);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T* Alloc<T>(T defaultVal = default) where T : unmanaged
		{
			var ptr = (T*)Alloc(Marshal.SizeOf<T>());
			*ptr = defaultVal;
			return ptr;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Free<T>(T* ptr) where T : unmanaged => Marshal.FreeHGlobal((IntPtr)ptr);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Free(IntPtr ptr) => Marshal.FreeHGlobal(ptr);

		public static void* Realloc(void* ptr, int newSize)
			=> (void*)Marshal.ReAllocHGlobal((IntPtr)ptr, (IntPtr)newSize);

		public static UIntPtr SizeOf<T>() where T : unmanaged => (UIntPtr)Marshal.SizeOf<T>();

		public static UIntPtr SizeOf(Type type) => (UIntPtr)Marshal.SizeOf(type);
	}
}
