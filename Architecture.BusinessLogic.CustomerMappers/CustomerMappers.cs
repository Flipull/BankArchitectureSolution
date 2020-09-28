﻿using Architecture.BusinessLogic.CustomerDTOFactories;
using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.DataAccess.CustomerEntities;
using Architecture.DataAccess.CustomerFactories;

namespace Architecture.BusinessLogic.CustomerMappers
{
    public class CustomerEntityDTOMapper : IConcreteCopier<Customer, CustomerDTO>,
                                            IConcreteMapper<Customer, CustomerDTO>
    {
        private readonly CustomerDTOFactory _factory;
        public CustomerEntityDTOMapper(CustomerDTOFactory dtofactory)
        {
            _factory = dtofactory;
        }
        public CustomerDTO Map(Customer source)
        {
            var newdto = _factory.Construct();
            CopyTo(source, newdto);
            return newdto;
        }
        public void CopyTo(Customer source, CustomerDTO target)
        {
            target.Guid = source.Guid;
            target.FirstName = source.FirstName;
            target.Initials = source.Initials;
            target.LastName = source.LastName;
        }
    }
    public class CustomerDTOEntityMapper : IConcreteMapper<CustomerDTO, Customer>,
                                            IConcreteCopier<CustomerDTO, Customer>
    {
        private readonly CustomerFactory _factory;
        public CustomerDTOEntityMapper(CustomerFactory entityfactory)
        {
            _factory = entityfactory;
        }
        public Customer Map(CustomerDTO source)
        {
            Customer newentity = _factory.Construct();
            CopyTo(source, newentity);
            return newentity;
        }
        public void CopyTo(CustomerDTO source, Customer target)
        {
            target.Guid = source.Guid;
            target.FirstName = source.FirstName;
            target.Initials = source.Initials;
            target.LastName = source.LastName;
        }
    }
}
