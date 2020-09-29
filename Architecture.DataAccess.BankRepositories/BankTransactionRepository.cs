using Architecture.Core.GenericRepository;
using Architecture.DataAccess.BankEntities;
using Architecture.DataAccess.BankRepositories.Infra;
using Microsoft.EntityFrameworkCore;

namespace Architecture.DataAccess.BankRepositories
{
    sealed public class BankTransactionRepository :
                                GenericRepository<BankTransaction>,
                                IBankTransactionRepository
    {
        public BankTransactionRepository(DbContext context) : base(context)
        { }
    }
}
