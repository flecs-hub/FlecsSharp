using System;

namespace Flecs
{
	public unsafe readonly struct CharPtr
	{
		readonly IntPtr _ptr;

		public CharPtr(IntPtr ptr) => this._ptr = ptr;
		public static explicit operator CharPtr(IntPtr ptr) => new CharPtr(ptr);
		public static implicit operator IntPtr(CharPtr charPtr) => charPtr._ptr;
		public CharPtr* Ptr() { fixed (CharPtr* ptr = &this) return ptr; }

		public unsafe ReadOnlySpan<byte> AsSpan()
		{
			byte* start = (byte*)_ptr;
			byte* current = start;

			while (*current != 0)
				current++;

			return new ReadOnlySpan<byte>(start, (int)(current - start));
		}

		public unsafe override string ToString() => System.Text.Encoding.UTF8.GetString(AsSpan());
	}

}
