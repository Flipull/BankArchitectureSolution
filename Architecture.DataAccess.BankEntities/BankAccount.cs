using System;

namespace Architecture.DataAccess.BankEntities
{
    public class BankAccount
    {
        //Private set-properties (or "set"-less properties):
        //read-only properties as seen by BLL
        //still writable by Entity Framework
        public int Id { get; /*private set;*/ }
        public Guid Guid { get; /*private set;*/ }
        public int OwnerId { get; set; }
        //public Customer Owner { get; set; }
        public string Iban { get; set; }
        public decimal Worth { get; set; }
    }
}
