using System;
using System.Collections.Generic;

namespace PinnedArrays
{
    /// <summary>
    /// An interface providing basic functionality for operating with pinned arrays.
    /// </summary>
    /// <typeparam name="T">The type of the elements of the array (only struct).</typeparam>
    public interface IPinnedArray<T> : IDisposable, IEnumerable<T> where T : struct
    {
        /// <summary>
        /// An indexer to provide read/write access to the array.
        /// </summary>
        /// <param name="index">The index of the desired element.</param>
        /// <returns>An element corresponding to the specified index.</returns>
        T this[int index] { get; set; }

        /// <summary>
        /// Get the pointer to the first element of the array
        /// (can be used to pass the pointer to PInvoke).
        /// </summary>
        /// <returns>The pointer to the first element of the array.</returns>
        IntPtr GetPtr();

        /// <summary>
        /// The capacity of the array. 
        /// </summary>
        int Length { get; }
    }
}