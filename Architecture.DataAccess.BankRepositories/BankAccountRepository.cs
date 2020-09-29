using Architecture.Core.GenericRepository;
using Architecture.DataAccess.BankEntities;
using Architecture.DataAccess.BankRepositories.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Architecture.DataAccess.BankRepositories
{
    public class BankAccountRepository :
                                GenericRepository<BankAccount>,
                                IBankAccountRepository
    {
        public BankAccountRepository(DbContext context) : base(context)
        { }

        public BankAccount GetByGuid(Guid guid)
        {
            var customer =
                from e in _entitySet
                where e.Guid.Equals(guid)
                select e;
            return customer.FirstOrDefault();
        }

    }
}
