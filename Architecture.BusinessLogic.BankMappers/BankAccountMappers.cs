using Architecture.BusinessLogic.BankDTOs;
using Architecture.BusinessLogic.BankFactories;
using Architecture.Core.MappingService;
using Architecture.DataAccess.BankEntities;
using Architecture.DataAccess.BankFactories;

namespace Architecture.BusinessLogic.BankMappers
{
    /*
    public static class BankMappingMethods
    {
        static readonly BankAccountFactory _entityFactory = new BankAccountFactory();
        static readonly BankAccountDTOFactory _dtoFactory = new BankAccountDTOFactory();
        static Expression<Func<BankAccount, BankAccountDTO>> a = 
                (account=> { //create object, fill object, return object
                    var d = _dtoFactory.Construct();
                    d.Id = account.Id;
                    d.Guid = account.Guid;
                    d.Iban = account.Iban;
                    d.Worth = account.Worth;
                    d.Owner = account.Owner;
                    return d;
                });
    }
    */
    /*
    public static class ExtensionMethods
    {
        static readonly BankAccountFactory _entityFactory = new BankAccountFactory();
        static readonly BankAccountDTOFactory _dtoFactory = new BankAccountDTOFactory();

        public static BankAccount ToEntity(this BankAccountDTO account)
        {
            var e = _entityFactory.Construct();
            e.Id = account.Id;
            e.Guid = account.Guid;
            e.Iban = account.Iban;
            e.Worth = account.Worth;
            e.Owner = account.;
            return e;
        }
        public static BankAccountDTO ToDTO(this BankAccount account)
        {
            var d = _dtoFactory.Construct();
            d.Id = account.Id;
            d.Guid = account.Guid;
            d.Iban = account.Iban;
            d.Worth = account.Worth;
            d.Owner = account.Owner;
            return d;
        }
    }
    */

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