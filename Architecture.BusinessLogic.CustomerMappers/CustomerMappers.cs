using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.Core.MappingService;
using Architecture.DataAccess.CustomerEntities;
using System;

namespace Architecture.BusinessLogic.CustomerMappers
{
    public class CustomerEntityMapper : IConcreteMapper<Customer, CustomerDTO>
    {
        public CustomerDTO Convert(Customer source)
        {
            throw new NotImplementedException();
        }
    }
    public class CustomerDTOMapper : IConcreteMapper<CustomerDTO, Customer>
    {
        public Customer Convert(CustomerDTO source)
        {
            throw new NotImplementedException();
        }
    }
}
