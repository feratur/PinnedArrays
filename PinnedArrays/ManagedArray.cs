using System;
using System.Runtime.InteropServices;

namespace PinnedArrays
{
    /// <summary>
    /// Managed array with pinned GC handle
    /// (the address of the object can be taken as the array will not be moved by the GC).
    /// </summary>
    /// <typeparam name="T">The type of the elements of the array (only struct).</typeparam>
    public class ManagedArray<T> : PinnedArrayBase<T> where T : struct
    {
        private bool _disposed;
        private GCHandle _arrayHandle;
        private readonly T[] _array;

        public override T this[int index]
        {
            get { return _array[index]; }
            set { _array[index] = value; }
        }

        public override IntPtr GetPtr()
        {
            return _arrayHandle.AddrOfPinnedObject();
        }

        public override void Dispose()
        {
            Free();
            GC.SuppressFinalize(this);
        }

        ~ManagedArray()
        {
            Free();
        }

        private void Free()
        {
            if (_disposed)
                return;
            _arrayHandle.Free();
            _disposed = true;
        }

        private void AllocHandle()
        {
            _arrayHandle = GCHandle.Alloc(_array, GCHandleType.Pinned);
        }

        /// <summary>
        /// Construct a ManagedArray object.
        /// </summary>
        /// <param name="length">The capacity of the array.</param>
        public ManagedArray(int length)
            : base(length)
        {
            _array = new T[Length];
            AllocHandle();
        }

        /// <summary>
        /// Construct a ManagedArray object.
        /// </summary>
        /// <param name="copy">Specify whether the 'values' array should be copied to a new array.</param>
        /// <param name="values">The values to initialize the array with.</param>
        public ManagedArray(bool copy, params T[] values)
            : base(values.Length)
        {
            _array = copy ? (T[])values.Clone() : values;
            AllocHandle();
        }
    }
}