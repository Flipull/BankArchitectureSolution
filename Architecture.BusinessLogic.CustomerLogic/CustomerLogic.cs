using Architecture.BusinessLogic.CustomerLogics.Infra;
using Architecture.DataAccess.CustomerRepositories.Infra;
using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.BusinessLogic.CustomerLogics
{
    sealed public class CustomerLogic: ICustomerLogic
    {
        private readonly ICustomerRepository _repository;
        public CustomerLogic(ICustomerRepository repo)
        {
            _repository = repo;
        }

        public CustomerCreationResultDTO CreateCustomer(CustomerDTO customer)
        {
            
        }

        public CustomerUpdateResultDTO UpdateCustomer(CustomerDTO customer)
        {
            throw new NotImplementedException();
        }

        public CustomerViewResultDTO ViewCustomer(string guid_str)
        {
            Guid guid;
            Guid.TryParse(guid_str, out guid);
            
            if (guid == null | guid.Equals(Guid.Empty) )
            {
                return null;
            }
            return _repository.Get( (c) => c.Guid.Equals(guid), )
            return _repository.GetByID(guid).;
        }

    }
}
