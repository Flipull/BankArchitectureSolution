using Architecture.Core.GenericRepository;
using Architecture.DataAccess.BankEntities;
using System;

namespace Architecture.DataAccess.BankRepositories.Infra
{
    public interface IBankTransactionRepository : IRepository<BankTransaction>
    {
        BankTransaction GetByGuid(Guid guid);
    }
}