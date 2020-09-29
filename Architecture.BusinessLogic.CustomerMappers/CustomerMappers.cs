using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.BusinessLogic.CustomerFactories;
using Architecture.BusinessLogic.CustomerSROs;
using Architecture.Core.MappingService;
using Architecture.DataAccess.CustomerEntities;
using Architecture.DataAccess.CustomerFactories;

namespace Architecture.BusinessLogic.CustomerMappers
{
    public class CustomerCreateEntityMapper : MapperAbstract<CustomerCreateSRO, Customer>
    {
        private readonly CustomerFactory _factory;
        public CustomerCreateEntityMapper(CustomerFactory entityfactory)
        {
            _factory = entityfactory;
        }
        public override Customer Map(CustomerCreateSRO source)
        {
            var newentity = _factory.Construct();
            newentity.FirstName = source.FirstName;
            newentity.Initials = source.Initials;
            newentity.LastName = source.LastName;
            return newentity;
        }
    }

    public class CustomerUpdateEntityMapper : ICopier<CustomerUpdateSRO, Customer>
    {
        //private readonly CustomerCreateEntityMapper _superMapper;
        public CustomerUpdateEntityMapper()
        //CustomerCreateEntityMapper supermapper
        {
            //supermapper decorating chain is nice; could be
            //implemented in CustomerDTO as well,
            //though I don't like the inheritence tree
            //_superMapper = supermapper;
        }
        public void CopyTo(CustomerUpdateSRO source, Customer target)
        {
            //_superMapper.CopyTo(source, target);
            target.FirstName = source.FirstName;
            target.Initials = source.Initials;
            target.LastName = source.LastName;
        }
    }

    public class CustomerDTOMapper : MapperAbstract<Customer, CustomerDTO>
    {
        private readonly CustomerDTOFactory _factory;
        public CustomerDTOMapper(CustomerDTOFactory dtofactory)
        {
            _factory = dtofactory;
        }
        public override CustomerDTO Map(Customer source)
        {
            var newdto = _factory.Construct();
            newdto.Id = source.Id;
            newdto.Guid = source.Guid;
            newdto.FirstName = source.FirstName;
            newdto.Initials = source.Initials;
            newdto.LastName = source.LastName;
            return newdto;
        }
    }
    [System.Obsolete]
    public class CustomerEntityMapper : MapperAbstract<CustomerDTO, Customer>,
                                        ICopier<CustomerDTO, Customer>
    {
        private readonly CustomerFactory _factory;
        public CustomerEntityMapper(CustomerFactory entityfactory)
        {
            _factory = entityfactory;
        }
        public override Customer Map(CustomerDTO source)
        {
            Customer newentity = _factory.Construct();
            CopyTo(source, newentity);
            return newentity;
        }
        public void CopyTo(CustomerDTO source, Customer target)
        {
            //target.Id = source.Id;
            //target.Guid = source.Guid;
            target.FirstName = source.FirstName;
            target.Initials = source.Initials;
            target.LastName = source.LastName;
        }
    }
}
