using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    public unsafe partial struct VectorParams
    {
        //public static VectorParams New(in Data val = default) => new VectorParams(Alloc(in val));
        //public void Dispose()
        //{
            //if(ptr != null) Free(ptr);
        //}

        internal MoveDelegate MoveActionCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Marshal.GetDelegateForFunctionPointer<MoveDelegate>(ptr->_moveAction);
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set =>  ptr->_moveAction = Marshal.GetFunctionPointerForDelegate(value);
        }

        public IntPtr MoveCtx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->moveCtx;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->moveCtx = value;
        }

        public IntPtr Ctx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->ctx;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->ctx = value;
        }

        public uint ElementSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->elementSize;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->elementSize = value;
        }

    }

}

