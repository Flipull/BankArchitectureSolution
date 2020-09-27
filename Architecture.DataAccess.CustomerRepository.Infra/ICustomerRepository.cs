using Architecture.Core.GenericRepository;
using Architecture.DataAccess.CustomerEntities;
using System;

namespace Architecture.DataAccess.CustomerRepositories.Infra
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer GetByGuid(Guid guid);
    }
}
