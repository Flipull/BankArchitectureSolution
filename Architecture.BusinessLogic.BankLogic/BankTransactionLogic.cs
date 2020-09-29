using Architecture.BusinessLogic.BankDTOs;
using Architecture.BusinessLogic.BankLogics.Infra;
using Architecture.BusinessLogic.BankMappers;
using Architecture.BusinessLogic.BankSROs;
using Architecture.BusinessLogic.CustomerLogics.Infra;
using Architecture.DataAccess.BankFactories;
using Architecture.DataAccess.BankRepositories.Infra;
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
                                orderDescending: t => t.PointInTime,
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
                                orderDescending: t => t.PointInTime,
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
                                orderDescending: t => t.PointInTime,
                                skip: (search.PageNumber - 1) * search.PageSize,
                                take: search.PageSize
                               );
            return _dtoMapper.MapAll(searched_transactions);
        }

        public BankTransactionDTO TransferMoney(BankTransactionExecuteSRO transaction_sro)
        {
            //check validity User/Account-guids before accepting a new transaction
            var account = _bankAccountLogic.ViewAccount(transaction_sro.GuidAccount);
            if (account == null |
                account.Iban != transaction_sro.IbanAccount)
                return null;
            //need check if Iban is valid and (Internal or external) and existing),
            //then continue transaction
            var newtransaction = _transactionFactory.Construct();
            _executeEntityMapper.CopyTo(transaction_sro, newtransaction);
            _repository.Insert(newtransaction);
            _repository.SaveChanges();
            return _dtoMapper.Map(newtransaction);
        }

    }
}
