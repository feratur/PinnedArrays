namespace PinnedArrays
{
    /// <summary>
    /// Factory class for operating with managed pinned arrays.
    /// </summary>
    public class ManagedFactory : PinnedArrayFactory<ManagedFactory>
    {
        public override IPinnedArray<T> Create<T>(int length)
        {
            return new ManagedArray<T>(length);
        }

        public override IPinnedArray<T> Create<T>(params T[] values)
        {
            return new ManagedArray<T>(true, values);
        }

        /// <summary>
        /// Allocate a handle for an existing array to prevent it from being moved by the GC.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the array (only struct).</typeparam>
        /// <param name="array">An existing managed array.</param>
        /// <returns>A ManagedArray object containing a pinned handle to an existing array.</returns>
        public ManagedArray<T> PinArray<T>(T[] array) where T : struct
        {
            return new ManagedArray<T>(false, array);
        }
    }
}