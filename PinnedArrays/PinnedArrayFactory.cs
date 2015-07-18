namespace PinnedArrays
{
    public abstract class PinnedArrayFactory<T> where T : PinnedArrayFactory<T>, new()
    {
        private static readonly T Factory = new T();

        public static T GetFactory()
        {
            return Factory;
        }

        public static IPinnedArray<TArray> CreateArray<TArray>(int length) where TArray : struct
        {
            return Factory.Create<TArray>(length);
        }

        public static IPinnedArray<TArray> CreateArray<TArray>(params TArray[] values) where TArray : struct
        {
            return Factory.Create(values);
        }

        public abstract IPinnedArray<TArray> Create<TArray>(int length) where TArray : struct;

        public abstract IPinnedArray<TArray> Create<TArray>(params TArray[] values) where TArray : struct;
    }
}