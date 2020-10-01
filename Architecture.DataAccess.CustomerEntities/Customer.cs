using System;

namespace Architecture.DataAccess.CustomerEntities
{
    public class Customer
    {
        public int Id { get; /*private set;*/ }
        public Guid Guid { get; /*private set;*/ }
        public string FirstName { get; set; }
        public string Initials { get; set; }
        public string LastName { get; set; }
    }
}
