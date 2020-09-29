using System.Collections.Generic;

namespace Architecture.Core.MappingService
{
    public abstract class MapperAbstract<S, T> : IMapper<S, T>
    {
        public abstract T Map(S source);

        public IEnumerable<T> MapAll(IEnumerable<S> source)
        {
            var list = new List<T>();
            foreach (S s in source)
                list.Add(Map(s));
            return list;
        }
    }
}
