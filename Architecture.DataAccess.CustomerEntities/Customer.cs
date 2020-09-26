using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.DataAccess.CustomerEntities
{
    public class Customer
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string Initials { get; set; }
        public string LastName { get; set; }
    }
}
