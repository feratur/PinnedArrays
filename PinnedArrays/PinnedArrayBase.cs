using System;
using System.Collections;
using System.Collections.Generic;

namespace PinnedArrays
{
    /// <summary>
    /// A base class for pinned arrays.
    /// </summary>
    /// <typeparam name="T">The type of the elements of the array (only struct).</typeparam>
    public abstract class PinnedArrayBase<T> : IPinnedArray<T> where T : struct
    {
        public abstract T this[int index] { get; set; }
        public abstract IntPtr GetPtr();
        public int Length { get; private set; }

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

        /// <summary>
        /// A base constructor to define the capacity of the pinned array.
        /// </summary>
        /// <param name="length">The capacity of the array.</param>
        protected PinnedArrayBase(int length)
        {
            Length = length;
        }
    }
}