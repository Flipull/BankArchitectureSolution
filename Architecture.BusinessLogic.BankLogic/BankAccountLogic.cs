using Architecture.BusinessLogic.BankDTOs;
using Architecture.BusinessLogic.BankLogics.Infra;
using Architecture.BusinessLogic.BankMappers;
using Architecture.BusinessLogic.BankSROs;
using Architecture.BusinessLogic.CustomerLogics.Infra;
using Architecture.DataAccess.BankFactories;
using Architecture.DataAccess.BankRepositories.Infra;
using System;
using System.Collections.Generic;

namespace Architecture.BusinessLogic.BankLogics
{
    public class BankAccountLogic : IBankAccountLogic
    {
        private readonly IBankAccountRepository _repository;
        private readonly BankAccountDTOMapper _dtoMapper;
        private readonly BankAccountEntityMapper _entityMapper;
        private readonly BankAccountFactory _bankAccountFactory;
        private readonly ICustomerLogic _customerLogic;

        public BankAccountLogic(IBankAccountRepository repo,
                                    BankAccountDTOMapper dtomapper,
                                    BankAccountEntityMapper entitymapper,
                                    BankAccountFactory bankaccountfactory,
                                    ICustomerLogic customerlogic)
        {
            _repository = repo;
            _dtoMapper = dtomapper;
            _entityMapper = entitymapper;
            _bankAccountFactory = bankaccountfactory;
            _customerLogic = customerlogic;
        }

        //Do we need an event-based system? (so 
        //BankAccountLogic can be told to create an
        //account, when a new customer is made,
        //without circular references)

        public BankAccountDTO CreateAccount(Guid owner)
        {
            var customer = _customerLogic.ViewCustomer(owner);
            if (customer == null)
                return null;
            //designchoice: construct new content in an Entity
            //and trigger insert;
            var newaccount = _bankAccountFactory.Construct();
            newaccount.Iban = "NL59AFCA6611003322";
            newaccount.Worth = 0;
            newaccount.OwnerId = customer.Id;
            _repository.Insert(newaccount);
            _repository.SaveChanges();
            return _dtoMapper.Map(newaccount);
        }
        //WILL NEVER BE IN ANY STANDALONE-APP instead it should be transactionally coupled to the physical money-transaction
        public BankAccountDTO Withdraw(BankAccountLiquidizeSRO liquidize)
        {
            var account = _repository.GetByGuid(liquidize.GuidAccount);
            if (account == null
                    | account.Worth < liquidize.Value)
                return null;

            account.Worth -= liquidize.Value;
            //_transactionLogic.Transfer?
            //          .TransferFromInternalLiquid() for
            //concrete injection of BankTransactionLogic?
            //create my own (new BankTransactionLogic() )?
            _repository.SaveChanges();
            return _dtoMapper.Map(account);
        }
        public BankAccountDTO Deposit(BankAccountLiquidizeSRO deliquidize)
        {
            var account = _repository.GetByGuid(deliquidize.GuidAccount);
            if (account == null)
                return null;

            account.Worth += deliquidize.Value;
            _repository.SaveChanges();
            return _dtoMapper.Map(account);
        }


        public BankAccountDTO ViewAccount(Guid account)
        {
            var accountentity = _repository.GetByGuid(account);
            if (accountentity == null)
                return null;
            return _dtoMapper.Map(accountentity);
        }

        public IEnumerable<BankAccountDTO> ViewAccounts(Guid owner)
        {
            var customer = _customerLogic.ViewCustomer(owner);
            if (customer == null)
                return null;

            var accounts = _repository.Get(filter: c => c.OwnerId == customer.Id);

            return _dtoMapper.MapAll(accounts);
        }
    }
}
