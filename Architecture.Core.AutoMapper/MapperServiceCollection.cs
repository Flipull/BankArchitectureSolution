using System;
using System.Collections.Generic;

namespace Architecture.Core.MappingService
{
    public interface IMapperServiceCollection
    {
        IConcreteMapper<S, T> GetMapper<S, T>();
    }
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
        public IConcreteMapper<S, T> GetMapper<S, T>()
        {
            return (IConcreteMapper<S, T>)_mappers[new Tuple<Type, Type>(typeof(S), typeof(T))]();
        }
    }
}
