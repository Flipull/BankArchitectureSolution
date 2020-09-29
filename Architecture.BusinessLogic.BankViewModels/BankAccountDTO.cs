using System;

namespace Architecture.BusinessLogic.BankDTOs
{
    public class BankAccountDTO
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Iban { get; set; }
        public decimal Worth { get; set; }
    }
}
