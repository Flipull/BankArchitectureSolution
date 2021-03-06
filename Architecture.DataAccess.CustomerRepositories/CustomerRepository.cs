﻿using Architecture.Core.GenericRepository;
using Architecture.DataAccess.CustomerEntities;
using Architecture.DataAccess.CustomerRepositories.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Architecture.DataAccess.CustomerRepositories
{
    public class CustomerRepository : GenericRepository<Customer>,
                                    ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        { }

        public Customer GetByGuid(Guid guid)
        {
            var customer =
                from e in _entitySet
                where e.Guid.Equals(guid)
                select e;
            return customer.FirstOrDefault();
        }
    }
}