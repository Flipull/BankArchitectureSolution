using System;
using System.Collections.Generic;

namespace Architecture.Core.MappingService
{
    [System.Obsolete]
    public interface IMapperServiceCollection
    {
        IMapper<S, T> GetMapper<S, T>();
    }
    [System.Obsolete]
    public class MapperServicecollection : IMapperServiceCollection
    {
        private readonly Dictionary<Tuple<Type, Type>, Func<object>> _mappers =
                            new Dictionary<Tuple<Type, Type>, Func<object>>();
        public void RegisterMapping<S, T>(Func<object> mapper)
        {
            if (mapper() is T)
            {
                _mappers.Add(new Tuple<Type, Type>(typeof(S), typeof(T)),
                            mapper);
            }
            else
            {
                throw new InvalidProgramException("Invalid mapper provided: " + mapper.ToString());
            }
        }
        public IMapper<S, T> GetMapper<S, T>()
        {
            return (IMapper<S, T>)_mappers[new Tuple<Type, Type>(typeof(S), typeof(T))]();
        }
    }
}
