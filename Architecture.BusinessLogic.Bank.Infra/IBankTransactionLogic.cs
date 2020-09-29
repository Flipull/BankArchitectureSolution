using Architecture.BusinessLogic.BankDTOs;
using Architecture.BusinessLogic.BankSROs;
using System.Collections.Generic;

namespace Architecture.BusinessLogic.BankLogics.Infra
{
    public interface IBankTransactionLogic
    {
        IEnumerable<BankTransactionSearchResultDTO> SearchDeposits(BankTransactionSearchSRO search);
        IEnumerable<BankTransactionSearchResultDTO> SearchWithdrawals(BankTransactionSearchSRO search);
        BankTransactionDTO TransferMoney(BankTransactionExecuteSRO transaction);
        IEnumerable<BankTransactionDTO> ViewTransactions(BankTransactionSearchSRO search);
    }
}
