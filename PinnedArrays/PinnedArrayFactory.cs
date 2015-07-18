namespace PinnedArrays
{
    /// <summary>
    /// The main factory class that provides static methods for creating
    /// pinned arrays and accessing the instances of the specific array factories.
    /// </summary>
    /// <typeparam name="T">Factory that inherits from PinnedArrayFactory.</typeparam>
    public abstract class PinnedArrayFactory<T> where T : PinnedArrayFactory<T>, new()
    {
        private static readonly T Factory = new T();

        /// <summary>
        /// Get the instance of the specific factory.
        /// </summary>
        /// <returns>A factory that inherits from PinnedArrayFactory.</returns>
        public static T GetFactory()
        {
            return Factory;
        }

        /// <summary>
        /// Create pinned array of the specified type.
        /// </summary>
        /// <typeparam name="TArray">The type of the elements of the array (only struct).</typeparam>
        /// <param name="length">The capacity of the array.</param>
        /// <returns>Pinned array corresponding to the factory.</returns>
        public static IPinnedArray<TArray> CreateArray<TArray>(int length) where TArray : struct
        {
            return Factory.Create<TArray>(length);
        }

        /// <summary>
        /// Create pinned array of the specified type.
        /// </summary>
        /// <typeparam name="TArray">The type of the elements of the array (only struct).</typeparam>
        /// <param name="values">The values to populate the array with.</param>
        /// <returns>Pinned array corresponding to the factory.</returns>
        public static IPinnedArray<TArray> CreateArray<TArray>(params TArray[] values) where TArray : struct
        {
            return Factory.Create(values);
        }

        /// <summary>
        /// Create pinned array of the specified type.
        /// </summary>
        /// <typeparam name="TArray">The type of the elements of the array (only struct).</typeparam>
        /// <param name="length">The capacity of the array.</param>
        /// <returns>Pinned array corresponding to the factory.</returns>
        public abstract IPinnedArray<TArray> Create<TArray>(int length) where TArray : struct;

        /// <summary>
        /// Create pinned array of the specified type.
        /// </summary>
        /// <typeparam name="TArray">The type of the elements of the array (only struct).</typeparam>
        /// <param name="values">The values to populate the array with.</param>
        /// <returns>Pinned array corresponding to the factory.</returns>
        public abstract IPinnedArray<TArray> Create<TArray>(params TArray[] values) where TArray : struct;
    }
}