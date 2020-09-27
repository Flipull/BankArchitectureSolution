namespace Architecture.Core.GenericFactory
{
    public class GenericFactory<T> : IFactory<T> where T : class, new()
    {
        public virtual T Construct()
        {
            return new T();
        }
    }
}
