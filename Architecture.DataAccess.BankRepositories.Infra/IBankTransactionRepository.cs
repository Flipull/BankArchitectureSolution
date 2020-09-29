using Architecture.Core.GenericRepository;
using Architecture.DataAccess.BankEntities;

namespace Architecture.DataAccess.BankRepositories.Infra
{
    public interface IBankTransactionRepository : IRepository<BankTransaction>
    {
    }
}