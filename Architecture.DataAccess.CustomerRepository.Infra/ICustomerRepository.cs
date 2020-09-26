using Architecture.Core.GenericRepository;
using Architecture.DataAccess.CustomerEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.DataAccess.CustomerRepositories.Infra
{
    public interface ICustomerRepository: IRepository<Customer>
    {
    }
}
