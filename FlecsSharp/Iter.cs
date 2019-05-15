using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    public unsafe partial struct Iter
    {
        public IntPtr Ctx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.ctx;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.ctx = value;
        }

        public IntPtr _Data
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.data;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.data = value;
        }

        internal HasnextDelegate HasnextCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Marshal.GetDelegateForFunctionPointer<HasnextDelegate>(this._hasnext);
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set =>  this._hasnext = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal NextDelegate NextCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Marshal.GetDelegateForFunctionPointer<NextDelegate>(this._next);
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set =>  this._next = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal ReleaseDelegate ReleaseCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Marshal.GetDelegateForFunctionPointer<ReleaseDelegate>(this._release);
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set =>  this._release = Marshal.GetFunctionPointerForDelegate(value);
        }

    }

}

