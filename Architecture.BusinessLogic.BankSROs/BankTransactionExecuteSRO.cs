using System;

namespace Architecture.BusinessLogic.BankSROs
{
    public class BankTransactionExecuteSRO
    {
        public Guid GuidAccount { get; set; }
        public string IbanAccount { get; set; }
        public string IbanTarget { get; set; }
        public decimal Value { get; set; }
    }
}
