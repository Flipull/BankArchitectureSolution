using System;

namespace Architecture.DataAccess.BankEntities
{
    public class BankTransaction
    {
        public Guid Guid { get; private set; }
        public string IbanSource { get; set; }
        public string IbanTarget { get; set; }
        public decimal Value { get; set; }
        public DateTime PointInTime { get; set; }
    }
}
