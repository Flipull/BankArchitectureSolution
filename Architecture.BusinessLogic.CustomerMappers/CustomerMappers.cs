using Architecture.BusinessLogic.CustomerDTOFactories;
using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.Core.MappingService;
using Architecture.DataAccess.CustomerEntities;
using Architecture.DataAccess.CustomerFactories;

namespace Architecture.BusinessLogic.CustomerMappers
{
    public class CustomerEntityDTOMapper : IConcreteMapper<Customer, CustomerDTO>
    {
        private readonly CustomerDTOFactory _factory;
        public CustomerEntityDTOMapper(CustomerDTOFactory dtofactory)
        {
            _factory = dtofactory;
        }
        public CustomerDTO Convert(Customer source)
        {
            var newdto = _factory.Construct();
            newdto.Guid = source.Guid;
            newdto.FirstName = source.FirstName;
            newdto.Initials = source.Initials;
            newdto.LastName = source.LastName;
            return newdto;
        }
    }
    public class CustomerDTOEntityMapper : IConcreteMapper<CustomerDTO, Customer>
    {
        private readonly CustomerFactory _factory;
        public CustomerDTOEntityMapper(CustomerFactory entityfactory)
        {
            _factory = entityfactory;
        }
        public Customer Convert(CustomerDTO source)
        {
            var newentity = _factory.Construct();
            newentity.Guid = source.Guid;
            newentity.FirstName = source.FirstName;
            newentity.Initials = source.Initials;
            newentity.LastName = source.LastName;
            return newentity;
        }
    }
}
