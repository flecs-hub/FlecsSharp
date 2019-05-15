using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpC
{
    partial struct CharPtr
    {
        public CharPtr(IntPtr ptr) => this.Ptr = ptr;
        IntPtr Ptr { get; }

        //public static implicit operator CharPtr(ReadOnlySpan<char> d) => d.ptr != 0;
        //  public static implicit operator CBool(bool b) => new CBool { val = (b ? 1 : 0) };

        public unsafe ReadOnlySpan<byte> AsSpan()
        {
            byte* ptr = (byte*)Ptr;
            byte* current = ptr;
            var val = *current++;

            while (val != 0)
                val = *(current++);

            return new ReadOnlySpan<byte>(ptr, (int)(current - ptr - 1));
        }

        public unsafe override string ToString()
        {
            return Encoding.ASCII.GetString(AsSpan());
        }
    }

    partial struct CBool
    {
        int val;
        public static implicit operator bool(CBool d) => d.val != 0;
        public static implicit operator CBool(bool b) => new CBool { val = (b ? 1 : 0) };
    }

    internal unsafe static class CStringExtensions
    {
        public static AnsiString ToAnsiString(this string val) => new AnsiString(val);
        public static AnsiString ToAnsiString(this ReadOnlySpan<char> val) => new AnsiString(val);

    }

    internal unsafe struct AnsiString : IDisposable
    {

        public AnsiString(ReadOnlySpan<char> val)
        {

            var buffSize = val.Length * 2;
            var ptr = Marshal.AllocHGlobal(buffSize);
            var span = new Span<byte>((void*)ptr, buffSize);
            var len = Encoding.ASCII.GetBytes(val, span);

            Ptr = (ptr);
            Length = (uint)len;
        }

        public static implicit operator CharPtr(AnsiString d) => new CharPtr(d.Ptr);
        public IntPtr Ptr { get; internal set; }
        public uint Length { get; internal set; }
        public void Dispose()
        {
            Marshal.FreeHGlobal(Ptr);
        }
    }

    public static class SharpC
    {
        public static T Alloc<T>(T defaultVal = default) where T : unmanaged
        {
            return new T();
        }
    }
}
