using System;

namespace Architecture.BusinessLogic.BankDTOs
{
    public class BankTransactionSearchResultDTO
    {
        public string IbanOther { get; set; }
        public decimal Value { get; set; }
        public DateTime PointInTime { get; set; }
    }
}
