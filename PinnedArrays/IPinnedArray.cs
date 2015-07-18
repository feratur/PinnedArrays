using System;
using System.Collections.Generic;

namespace PinnedArrays
{
    public interface IPinnedArray<T> : IDisposable, IEnumerable<T> where T : struct
    {
        T this[int index] { get; set; }

        IntPtr GetPtr();

        int Length { get; }
    }
}