namespace Architecture.Core.MappingService
{
    //no co/contra-variance; not needed to support sub- and/or super-types
    public interface IMapper<S, T>
    {
        T Map(S source);
        void CopyTo(S source, T target);
    }
}
