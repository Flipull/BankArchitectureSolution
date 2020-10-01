using Architecture.BusinessLogic.BankDTOs;
using Architecture.BusinessLogic.BankLogics.Infra;
using Architecture.BusinessLogic.BankMappers;
using Architecture.BusinessLogic.BankSROs;
using Architecture.BusinessLogic.CustomerLogics.Infra;
using Architecture.Core.GenericRepository;
using Architecture.DataAccess.BankFactories;
using Architecture.DataAccess.BankRepositories.Infra;
using System;
using System.Collections.Generic;

namespace Architecture.BusinessLogic.BankLogics
{
    public class BankTransactionLogic : IBankTransactionLogic
    {
        private readonly IBankTransactionRepository _repository;
        private readonly BankTransactionDTOMapper _dtoMapper;
        private readonly BankTransactionExecuteEntityMapper _executeEntityMapper;
        private readonly BankTransactionFactory _transactionFactory;
        private readonly ICustomerLogic _customerLogic;
        private readonly IBankAccountLogic _bankAccountLogic;
        private readonly BankTransactionDepositsSearchResultDTOMapper _depositSearchMapper;
        private readonly BankTransactionWithdrawalsSearchResultDTOMapper _withdrawalSearchMapper;

        public BankTransactionLogic(IBankTransactionRepository repo,
                                    BankTransactionDTOMapper dtomapper,
                                    BankTransactionExecuteEntityMapper executeentitymapper,
                                    BankTransactionFactory transactionfactory,
                                    ICustomerLogic customerlogic,
                                    IBankAccountLogic bankaccountlogic,
                                    BankTransactionDepositsSearchResultDTOMapper depositsearchmapper,
                                    BankTransactionWithdrawalsSearchResultDTOMapper withdrawalsearchmapper)
        {
            _repository = repo;
            _dtoMapper = dtomapper;
            _executeEntityMapper = executeentitymapper;
            _transactionFactory = transactionfactory;

            _customerLogic = customerlogic;
            _bankAccountLogic = bankaccountlogic;

            _depositSearchMapper = depositsearchmapper;
            _withdrawalSearchMapper = withdrawalsearchmapper;
        }

        public IEnumerable<BankTransactionSearchResultDTO> SearchDeposits(BankTransactionSearchSRO search)
        {
            var account = _bankAccountLogic.ViewAccount(search.GuidAccount);
            if (account == null)
                return null;

            var searched_transactions =
                _repository.Get(filter: t => t.IbanTarget == account.Iban,
                                orderBy: o => new Tuple<object, SortDirection>(o.PointInTime, SortDirection.Descending),
                                skip: (search.PageNumber - 1) * search.PageSize,
                                take: search.PageSize
                               );
            return _depositSearchMapper.MapAll(searched_transactions);
        }
        public IEnumerable<BankTransactionSearchResultDTO> SearchWithdrawals(BankTransactionSearchSRO search)
        {
            var account = _bankAccountLogic.ViewAccount(search.GuidAccount);
            if (account == null)
                return null;

            var searched_transactions =
                _repository.Get(filter: t => t.IbanSource == account.Iban,
                                orderBy: t => t.PointInTime,
                                skip: (search.PageNumber - 1) * search.PageSize,
                                take: search.PageSize
                               );
            return _withdrawalSearchMapper.MapAll(searched_transactions);
        }

        public IEnumerable<BankTransactionDTO> ViewTransactions(BankTransactionSearchSRO search)
        {
            var account = _bankAccountLogic.ViewAccount(search.GuidAccount);
            if (account == null)
                return null;

            var searched_transactions =
                _repository.Get(filter: t => t.IbanSource == account.Iban
                                            | t.IbanTarget == account.Iban,
                                orderBy: t => t.PointInTime,
                                skip: (search.PageNumber - 1) * search.PageSize,
                                take: search.PageSize
                               );
            return _dtoMapper.MapAll(searched_transactions);
        }

        public BankTransactionDTO Transfer(BankTransactionExecuteSRO transaction_sro)
        {
            //check validity User/Account-guids before accepting a new transaction
            var account = _bankAccountLogic.ViewAccount(transaction_sro.GuidAccount);
            if (account == null |
                account.Iban != transaction_sro.IbanAccount)
                return null;
            //need check if Iban is valid and (Internal or external) and existing),
            var account_target = _bankAccountLogic.ViewAccount(transaction_sro.GuidAccount);
            if (account_target == null |
                account_target.Iban != transaction_sro.IbanTarget)
                return null;

            //then continue transaction
            var newtransaction = _transactionFactory.Construct();
            _executeEntityMapper.CopyTo(transaction_sro, newtransaction);
            //we never change moneyvalues on the accounts tho, 
            //maybe most of this code should be moved to accountlogic,
            //which can update account-values when needed
            _repository.Insert(newtransaction);
            _repository.SaveChanges();
            return _dtoMapper.Map(newtransaction);
        }

    }
}
