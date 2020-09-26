using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.Core.MappingService
{
    public interface IConcreteMapper<in S, out T>
    {
        T Convert(S source);
    }
}
