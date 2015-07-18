using System;
using System.Runtime.InteropServices;

namespace PinnedArrays
{
    public class UnmanagedArray<T> : PinnedArrayBase<T> where T : struct
    {
        private readonly IntPtr _arrayPtr;
        private bool _disposed;
        private static readonly int TypeSize = Marshal.SizeOf(typeof(T));

        public override void Dispose()
        {
            Free();
            GC.SuppressFinalize(this);
        }

        ~UnmanagedArray()
        {
            Free();
        }

        private void Free()
        {
            if (_disposed)
                return;
            Marshal.FreeHGlobal(_arrayPtr);
            _disposed = true;
        }

        public override T this[int index]
        {
            get { return (T)Marshal.PtrToStructure(IntPtr.Add(_arrayPtr, index * TypeSize), typeof(T)); }
            set { Marshal.StructureToPtr(value, IntPtr.Add(_arrayPtr, index * TypeSize), false); }
        }

        public override IntPtr GetPtr()
        {
            return _arrayPtr;
        }

        public UnmanagedArray(int length)
            : base(length)
        {
            _arrayPtr = Marshal.AllocHGlobal(TypeSize * Length);
        }
    }
}