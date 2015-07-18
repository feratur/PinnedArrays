using System;
using System.Runtime.InteropServices;

namespace PinnedArrays
{
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

        public ManagedArray(int length)
            : base(length)
        {
            _array = new T[Length];
            AllocHandle();
        }

        public ManagedArray(params T[] values)
            : base(values.Length)
        {
            _array = (T[])values.Clone();
            AllocHandle();
        }
    }
}