using System;
using System.Collections;
using System.Collections.Generic;

namespace PinnedArrays
{
    public abstract class PinnedArrayBase<T> : IPinnedArray<T> where T : struct
    {
        public abstract T this[int index] { get; set; }
        public abstract IntPtr GetPtr();
        public int Length { get; private set; }

        protected PinnedArrayBase(int length)
        {
            Length = length;
        }

        public abstract void Dispose();

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Length; ++i)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}