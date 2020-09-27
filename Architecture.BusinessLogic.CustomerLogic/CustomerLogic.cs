using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.BusinessLogic.CustomerLogics.Infra;
using Architecture.BusinessLogic.CustomerMappers;
using Architecture.DataAccess.CustomerRepositories.Infra;
using System;

namespace Architecture.BusinessLogic.CustomerLogics
{
    sealed public class CustomerLogic : ICustomerLogic
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
            var new_customer = _entityMapper.Convert(customer);
            _repository.Insert(new_customer);
            //not working as .Insert not saving changes
            return _dtoMapper.Convert(new_customer);
        }

        public CustomerDTO UpdateCustomer(CustomerDTO customer)
        {
            var entity = _repository.GetByGuid(customer.Guid);
            
            if (entity != null)
            {
                var updated_customer = _entityMapper.Convert(customer);
                updated_customer.Id = entity.Id;
                _repository.Update(updated_customer);

                //not working as .Update not saving changes
                return _dtoMapper.Convert(updated_customer);
            }
            return null;
        }

        public CustomerDTO ViewCustomer(Guid guid)
        {
            if (guid == null | guid.Equals(Guid.Empty))
            {
                return null;
            }
            return _dtoMapper.Convert(_repository.GetByGuid(guid));
        }

    }
}
