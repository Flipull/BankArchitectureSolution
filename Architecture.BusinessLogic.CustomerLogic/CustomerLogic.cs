using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.BusinessLogic.CustomerFactories;
using Architecture.BusinessLogic.CustomerLogics.Infra;
using Architecture.BusinessLogic.CustomerMappers;
using Architecture.BusinessLogic.CustomerSROs;
using Architecture.DataAccess.CustomerRepositories.Infra;
using System;

namespace Architecture.BusinessLogic.CustomerLogics
{
    public class CustomerLogic : ICustomerLogic
    {
        private readonly ICustomerRepository _repository;
        private readonly CustomerDTOMapper _dtoMapper;
        private readonly CustomerCreateEntityMapper _createEntityMapper;
        private readonly CustomerUpdateEntityMapper _updateEntityMapper;
        //unused objects, made obsolete by other design-decisions
        private readonly CustomerDTOFactory _customerDTOFactory;
        
        public CustomerLogic(ICustomerRepository repo,
                            CustomerDTOMapper dtomapper,
                            CustomerCreateEntityMapper create_entitymapper,
                            CustomerUpdateEntityMapper update_entitymapper,
                            CustomerDTOFactory customerdtofactory
                            )
        {
            _repository = repo;
            _dtoMapper = dtomapper;
            _customerDTOFactory = customerdtofactory;
            _createEntityMapper = create_entitymapper;
            _updateEntityMapper = update_entitymapper;
        }

        public CustomerDTO CreateCustomer(CustomerCreateSRO customer)
        {
            var new_customer = _createEntityMapper.Map(customer);
            _repository.Insert(new_customer);
            _repository.SaveChanges();
            return _dtoMapper.Map(new_customer);
        }

        public CustomerDTO UpdateCustomer(CustomerUpdateSRO customer)
        {
            var entity = _repository.GetByGuid(customer.Guid);
            if (entity != null)
            {
                _updateEntityMapper.CopyTo(customer, entity);
                _repository.Update(entity);
                _repository.SaveChanges();
                return _dtoMapper.Map(entity);
            }
            return null;
        }

        public CustomerDTO ViewCustomer(Guid guid)
        {
            if (guid == null | guid.Equals(Guid.Empty))
                return null;
            return _dtoMapper.Map(_repository.GetByGuid(guid));
        }

    }
}
