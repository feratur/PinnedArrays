namespace PinnedArrays
{
    public class ManagedFactory : PinnedArrayFactory<ManagedFactory>
    {
        public override IPinnedArray<T> Create<T>(int length)
        {
            return new ManagedArray<T>(length);
        }

        public override IPinnedArray<TArray> Create<TArray>(params TArray[] values)
        {
            return new ManagedArray<TArray>(values);
        }
    }
}