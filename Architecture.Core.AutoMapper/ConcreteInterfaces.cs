namespace Architecture.Core.MappingService
{
    //removed co/contra-variance; not needed
    public interface IConcreteMapper<S, T>
    {
        T Map(S source);
    }
    public interface IConcreteCopier<S, T>
    {
        void CopyTo(S source, T target);
    }
}
