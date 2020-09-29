using System.Collections.Generic;

namespace Architecture.Core.MappingService
{
    //no co/contra-variance; not needed to support sub- and/or super-types
    public interface IMapper<S, T>
    {
        T Map(S source);
        IEnumerable<T> MapAll(IEnumerable<S> source);
    }
    public interface ICopier<S, T>
    {
        void CopyTo(S source, T target);
    }
}
