using Architecture.Core.GenericRepository;
using Architecture.DataAccess.BankEntities;
using Architecture.DataAccess.BankRepositories.Infra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Architecture.DataAccess.BankRepositories
{
    sealed public class BankTransactionRepository :
                                GenericRepository<BankTransaction>,
                                IBankTransactionRepository
    {
        public BankTransactionRepository(DbContext context) : base(context)
        {
        }
        public BankTransaction GetByGuid(Guid guid)
        {
            var transaction =
                from e in _entitySet
                where e.Guid.Equals(guid)
                select e;
            return transaction.FirstOrDefault();
        }
    }
}
