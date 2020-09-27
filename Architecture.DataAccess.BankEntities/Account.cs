using System;

namespace Architecture.DataAccess.BankEntities
{
    public class Account
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Iban { get; set; }
        public decimal Worth { get; set; }
    }
}
