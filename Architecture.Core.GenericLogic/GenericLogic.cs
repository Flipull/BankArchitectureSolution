using Architecture.Core.GenericRepository;
using Architecture.Core.MappingService;
using System;

namespace Architecture.Core.GenericLogic
{
    public class GenericLogic<R, E, D, EDM, DEM>
            where R : IRepository<E>
            where E : class
            where D : class
            where EDM : IConcreteMapper<E, D>, IConcreteCopier<E, D>
            where DEM : IConcreteMapper<D, E>, IConcreteCopier<D, E>
    {
        protected readonly R _repository;
        protected readonly EDM _dtoMapper;
        protected readonly DEM _entityMapper;
        public GenericLogic(R repo, EDM dtomapper, DEM entitymapper)
        {
            _repository = repo;
            _dtoMapper = dtomapper;
            _entityMapper = entitymapper;
        }
    }
}
