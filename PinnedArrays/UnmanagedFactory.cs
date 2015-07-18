using System;

namespace PinnedArrays
{
    /// <summary>
    /// Factory class for operating with unmanaged arrays.
    /// </summary>
    public class UnmanagedFactory : PinnedArrayFactory<UnmanagedFactory>
    {
        public override IPinnedArray<T> Create<T>(int length)
        {
            return new UnmanagedArray<T>(length);
        }

        public override IPinnedArray<T> Create<T>(params T[] values)
        {
            var array = new UnmanagedArray<T>(values.Length);
            var index = 0;
            foreach (var val in values)
                array[index++] = val;
            return array;
        }

        /// <summary>
        /// Create new unmanaged array from the pointer to an existing array
        /// (without allocating new memory).
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array (only struct).</typeparam>
        /// <param name="pointer">The pointer to the first element of the array.</param>
        /// <param name="length">The length of the existing array.</param>
        /// <param name="registerForDispose">Specify true if Marshal.FreeHGlobal should be called on finalization.</param>
        /// <returns>An UnmanagedArray object linked to the specified pointer.</returns>
        public UnmanagedArray<T> GetFromPointer<T>(IntPtr pointer, int length, bool registerForDispose) where T : struct
        {
            return new UnmanagedArray<T>(pointer, length, registerForDispose);
        }
    }
}