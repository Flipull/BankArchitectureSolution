using Architecture.BusinessLogic.BankDTOs;
using Architecture.BusinessLogic.BankLogics.Infra;
using Architecture.BusinessLogic.BankMappers;
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

            var newaccount = _bankAccountFactory.Construct();
            newaccount.Iban = "NL59 AFCA 6611 0033 22";
            newaccount.Worth = 0;
            //how can we use a dto to add a model here?
            //maybe need an OwnerId property and fill that,
            //and let EF fill Owner property?
            newaccount.Owner = customer;
            _repository.Insert(newaccount);
            _repository.SaveChanges();
            return _dtoMapper.Map(newaccount);
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

            var accounts = _repository.Get(filter: c => c.Owner.Id == customer.Id,
                                    includeProperties: "Owner");

            return _dtoMapper.MapAll(accounts);
        }
    }
}
