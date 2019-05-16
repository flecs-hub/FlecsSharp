using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    unsafe partial struct Iterator
    {
        public IntPtr Ctx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.ctx;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.ctx = value;
        }

        public IntPtr _Data
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.data;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)] set => this.data = value;
        }

        internal HasnextDelegate HasnextCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<HasnextDelegate>(this._hasnext);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  this._hasnext = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal NextDelegate NextCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<NextDelegate>(this._next);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  this._next = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal ReleaseDelegate ReleaseCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<ReleaseDelegate>(this._release);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  this._release = Marshal.GetFunctionPointerForDelegate(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IterHasnext()
        {
            fixed(Iterator* thisPtr = &this)
            return ecs.iter_hasnext( thisPtr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr IterNext()
        {
            fixed(Iterator* thisPtr = &this)
            return ecs.iter_next( thisPtr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void IterRelease()
        {
            fixed(Iterator* thisPtr = &this)
            ecs.iter_release( thisPtr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong MapNext(ulong* keyOut)
        {
            fixed(Iterator* thisPtr = &this)
            return ecs.map_next( thisPtr, keyOut);
        }

    }

}

