using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.BusinessLogic.CustomerLogics.Infra;
using Architecture.BusinessLogic.CustomerMappers;
using Architecture.DataAccess.CustomerRepositories.Infra;
using System;

namespace Architecture.BusinessLogic.CustomerLogics
{
    public class CustomerLogic : ICustomerLogic
    {
        private readonly ICustomerRepository _repository;
        private readonly CustomerEntityDTOMapper _dtoMapper;
        private readonly CustomerDTOEntityMapper _entityMapper;
        public CustomerLogic(ICustomerRepository repo,
                            CustomerEntityDTOMapper dtomapper,
                            CustomerDTOEntityMapper entitymapper
                            )
        {
            _repository = repo;
            _dtoMapper = dtomapper;
            _entityMapper = entitymapper;
        }

        public CustomerDTO CreateCustomer(CustomerDTO customer)
        {
            var new_customer = _entityMapper.MapTo(customer);
            _repository.Insert(new_customer);
            _repository.SaveChanges();
            return _dtoMapper.MapTo(new_customer);
        }

        public CustomerDTO UpdateCustomer(CustomerDTO customer)
        {
            var entity = _repository.GetByGuid(customer.Guid);

            if (entity != null)
            {
                _entityMapper.CopyTo(customer, entity);
                _repository.Update(entity);
                _repository.SaveChanges();
                return _dtoMapper.MapTo(entity);
            }
            return null;
        }

        public CustomerDTO ViewCustomer(Guid guid)
        {
            if (guid == null | guid.Equals(Guid.Empty))
                return null;
            return _dtoMapper.MapTo(_repository.GetByGuid(guid));
        }

    }
}
