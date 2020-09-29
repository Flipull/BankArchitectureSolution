using Architecture.BusinessLogic.BankDTOs;
using Architecture.BusinessLogic.BankFactories;
using Architecture.Core.MappingService;
using Architecture.DataAccess.BankEntities;
using Architecture.DataAccess.BankFactories;

namespace Architecture.BusinessLogic.BankMappers
{
    public class BankAccountDTOMapper : MapperAbstract<BankAccount, BankAccountDTO>
    {
        private readonly BankAccountDTOFactory _factory;
        public BankAccountDTOMapper(BankAccountDTOFactory dtofactory)
        {
            _factory = dtofactory;
        }
        public override BankAccountDTO Map(BankAccount source)
        {
            var newdto = _factory.Construct();
            newdto.Id = source.Id;
            newdto.Guid = source.Guid;
            newdto.Iban = source.Iban;
            newdto.Worth = source.Worth;
            return newdto;
        }
    }
    [System.Obsolete]
    public class BankAccountEntityMapper : MapperAbstract<BankAccountDTO, BankAccount>,
                                            ICopier<BankAccountDTO, BankAccount>
    {
        private readonly BankAccountFactory _factory;
        public BankAccountEntityMapper(BankAccountFactory entityfactory)
        {
            _factory = entityfactory;
        }
        public override BankAccount Map(BankAccountDTO source)
        {
            BankAccount newentity = _factory.Construct();
            CopyTo(source, newentity);
            return newentity;
        }
        public void CopyTo(BankAccountDTO source, BankAccount target)
        {
            target.Worth = source.Worth;
        }
    }
}