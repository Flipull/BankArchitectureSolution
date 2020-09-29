using System;

namespace Architecture.BusinessLogic.BankDTOs
{
    public class BankTransactionDTO
    {
        public Guid Guid { get; set; }
        public string IbanSource { get; set; }
        public string IbanTarget { get; set; }
        public decimal Value { get; set; }
        public DateTime PointInTime { get; set; }
    }
}
