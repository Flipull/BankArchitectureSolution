using Architecture.BusinessLogic.BankDTOs;
using Architecture.BusinessLogic.BankSROs;
using System;
using System.Collections.Generic;

namespace Architecture.BusinessLogic.BankLogics.Infra
{
    public interface IBankAccountLogic
    {
        BankAccountDTO CreateAccount(Guid owner);
        BankAccountDTO Deposit(BankAccountLiquidizeSRO liquidize);
        BankAccountDTO Withdraw(BankAccountLiquidizeSRO liquidize);
        BankAccountDTO ViewAccount(Guid account);
        IEnumerable<BankAccountDTO> ViewAccounts(Guid owner);
    }
}
