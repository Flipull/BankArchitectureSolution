using Architecture.BusinessLogic.BankDTOs;
using System;
using System.Collections.Generic;

namespace Architecture.BusinessLogic.BankLogics.Infra
{
    public interface IBankAccountLogic
    {
        BankAccountDTO CreateAccount(Guid owner);
        BankAccountDTO ViewAccount(Guid account);
        IEnumerable<BankAccountDTO> ViewAccounts(Guid owner);
    }
}
