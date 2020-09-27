using Architecture.BusinessLogic.CustomerDTOs;
using System;

namespace Architecture.BusinessLogic.CustomerLogics.Infra
{
    public interface ICustomerLogic
    {
        CustomerDTO CreateCustomer(CustomerDTO customer);
        CustomerDTO UpdateCustomer(CustomerDTO customer);
        CustomerDTO ViewCustomer(Guid guid);
    }
}
