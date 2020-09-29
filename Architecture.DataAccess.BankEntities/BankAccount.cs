using Architecture.DataAccess.CustomerEntities;
using System;

namespace Architecture.DataAccess.BankEntities
{
    public class BankAccount
    {
        //Private set-properties:
        //read-only properties as seen by BLL
        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public Customer Owner { get; set; }
        public string Iban { get; set; }
        public decimal Worth { get; set; }
    }
}
