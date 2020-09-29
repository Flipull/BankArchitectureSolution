namespace Architecture.Core.MappingService
{
    [System.Obsolete]
    public interface IMapperService
    {
        T Convert<S, T>(S source);
    }
    [System.Obsolete]
    public class MapperService : IMapperService
    {
        private readonly IMapperServiceCollection _mappingservice;
        public MapperService(IMapperServiceCollection mappingservice)
        {
            _mappingservice = mappingservice;
        }
        public T Convert<S, T>(S source)
        {
            return _mappingservice.GetMapper<S, T>().MapTo(source);
        }
    }
}
