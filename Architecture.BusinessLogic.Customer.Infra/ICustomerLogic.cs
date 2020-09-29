using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.BusinessLogic.CustomerSROs;
using System;

namespace Architecture.BusinessLogic.CustomerLogics.Infra
{
    public interface ICustomerLogic
    {
        CustomerDTO CreateCustomer(CustomerCreateSRO customer);
        CustomerDTO UpdateCustomer(CustomerUpdateSRO customer);
        CustomerDTO ViewCustomer(Guid guid);
    }
}
