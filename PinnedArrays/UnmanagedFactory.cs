namespace PinnedArrays
{
    public class UnmanagedFactory : PinnedArrayFactory<UnmanagedFactory>
    {
        public override IPinnedArray<T> Create<T>(int length)
        {
            return new UnmanagedArray<T>(length);
        }

        public override IPinnedArray<TArray> Create<TArray>(params TArray[] values)
        {
            var array = new UnmanagedArray<TArray>(values.Length);
            var index = 0;
            foreach (var val in values)
                array[index++] = val;
            return array;
        }
    }
}