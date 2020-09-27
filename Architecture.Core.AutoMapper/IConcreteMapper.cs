namespace Architecture.Core.MappingService
{
    public interface IConcreteMapper<in S, out T>
    {
        T Convert(S source);
    }
}
