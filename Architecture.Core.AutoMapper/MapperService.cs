namespace Architecture.Core.MappingService
{
    public interface IMapperService
    {
        T Convert<S, T>(S source);
    }
    public class MapperService : IMapperService
    {
        private readonly IMapperServiceCollection _mappingservice;
        public MapperService(IMapperServiceCollection mappingservice)
        {
            _mappingservice = mappingservice;
        }
        public T Convert<S, T>(S source)
        {
            return _mappingservice.GetMapper<S, T>().Convert(source);
        }
    }
}
