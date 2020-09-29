using Architecture.Core.GenericRepository;
using Architecture.DataAccess.BankEntities;
using System;

namespace Architecture.DataAccess.BankRepositories.Infra
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        BankAccount GetByGuid(Guid guid);
    }
}
