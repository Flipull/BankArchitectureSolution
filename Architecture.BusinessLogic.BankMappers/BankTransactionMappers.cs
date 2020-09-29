using Architecture.BusinessLogic.BankDTOs;
using Architecture.BusinessLogic.BankFactories;
using Architecture.BusinessLogic.BankSROs;
using Architecture.Core.MappingService;
using Architecture.DataAccess.BankEntities;
using Architecture.DataAccess.BankFactories;
using System;

namespace Architecture.BusinessLogic.BankMappers
{
    public class BankTransactionDTOMapper : MapperAbstract<BankTransaction, BankTransactionDTO>
    {
        private readonly BankTransactionDTOFactory _factory;
        public BankTransactionDTOMapper(BankTransactionDTOFactory dtofactory)
        {
            _factory = dtofactory;
        }
        public override BankTransactionDTO Map(BankTransaction source)
        {
            var newdto = _factory.Construct();
            newdto.Guid = source.Guid;
            newdto.IbanSource = source.IbanSource;
            newdto.IbanTarget = source.IbanTarget;
            newdto.PointInTime = source.PointInTime;
            newdto.Value = source.Value;
            return newdto;
        }
    }
    public class BankTransactionDepositsSearchResultDTOMapper : MapperAbstract<BankTransaction, BankTransactionSearchResultDTO>
    {
        private readonly BankTransactionSearchResultDTOFactory _factory;
        public BankTransactionDepositsSearchResultDTOMapper(BankTransactionSearchResultDTOFactory dtofactory)
        {
            _factory = dtofactory;
        }
        public override BankTransactionSearchResultDTO Map(BankTransaction source)
        {
            var newdto = _factory.Construct();
            newdto.IbanOther = source.IbanSource;
            newdto.PointInTime = source.PointInTime;
            newdto.Value = source.Value;
            return newdto;
        }
    }
    public class BankTransactionWithdrawalsSearchResultDTOMapper : MapperAbstract<BankTransaction, BankTransactionSearchResultDTO>
    {
        private readonly BankTransactionSearchResultDTOFactory _factory;
        public BankTransactionWithdrawalsSearchResultDTOMapper(BankTransactionSearchResultDTOFactory dtofactory)
        {
            _factory = dtofactory;
        }
        public override BankTransactionSearchResultDTO Map(BankTransaction source)
        {
            var newdto = _factory.Construct();
            newdto.IbanOther = source.IbanTarget;
            newdto.PointInTime = source.PointInTime;
            newdto.Value = source.Value;
            return newdto;
        }
    }
    public class BankTransactionExecuteEntityMapper : ICopier<BankTransactionExecuteSRO, BankTransaction>
    {
        private readonly BankTransactionFactory _factory;
        public BankTransactionExecuteEntityMapper(BankTransactionFactory entityfactory)
        {
            _factory = entityfactory;
        }
        public void CopyTo(BankTransactionExecuteSRO source, BankTransaction target)
        {
            target.IbanSource = source.IbanAccount;
            target.IbanTarget = source.IbanTarget;
            target.PointInTime = DateTime.Now;
            target.Value = source.Value;
        }
    }
}
