using System;
using System.Collections.Generic;
using System.Text;
using Architecture.Core.GenericRepository;
using Architecture.DataAccess.CustomerEntities;
using Architecture.DataAccess.CustomerRepositories.Infra;
using Microsoft.EntityFrameworkCore;

namespace Architecture.DataAccess.CustomerRepositories
{
    sealed public class CustomerRepository : RepositoryAbstract<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        { }
    }
}
