using System;
using System.Runtime.InteropServices;

namespace PinnedArrays
{
    /// <summary>
    /// Unmanaged array allocated on the process heap.
    /// </summary>
    /// <typeparam name="T">The type of the elements of the array (only struct).</typeparam>
    public class UnmanagedArray<T> : PinnedArrayBase<T> where T : struct
    {
        private readonly IntPtr _arrayPtr;
        private bool _disposed;
        private static readonly int TypeSize = Marshal.SizeOf(typeof (T));

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
            get { return (T) Marshal.PtrToStructure(IntPtr.Add(_arrayPtr, index*TypeSize), typeof (T)); }
            set { Marshal.StructureToPtr(value, IntPtr.Add(_arrayPtr, index*TypeSize), false); }
        }

        public override IntPtr GetPtr()
        {
            return _arrayPtr;
        }

        /// <summary>
        /// Construct an UnmanagedArray object.
        /// </summary>
        /// <param name="length">The capacity of the array.</param>
        public UnmanagedArray(int length)
            : base(length)
        {
            _arrayPtr = Marshal.AllocHGlobal(TypeSize*Length);
        }

        /// <summary>
        /// Construct an UnmanagedArray object.
        /// </summary>
        /// <param name="pointer">The pointer to an existing array.</param>
        /// <param name="length">The length of the existing array.</param>
        /// <param name="registerForDispose">Specify true if Marshal.FreeHGlobal should be called on finalization.</param>
        public UnmanagedArray(IntPtr pointer, int length, bool registerForDispose)
            : base(length)
        {
            _arrayPtr = pointer;
            _disposed = !registerForDispose;
        }
    }
}