using Architecture.Core.GenericRepository;
using Architecture.Core.MappingService;

namespace Architecture.Core.GenericLogic
{
    [System.Obsolete]
    public abstract class GenericLogic<R, E, D, EDM, DEM>
            where R : IRepository<E>
            where E : class
            where D : class
            where EDM : IMapper<E, D>
            where DEM : IMapper<D, E>
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
