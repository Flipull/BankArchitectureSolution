using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.DataAccess.BankEntities
{
    public class Transaction
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string IbanSource { get; set; }
        public string IbanTarget { get; set; }
        public decimal Value { get; set; }
        public DateTime PointInTime { get; set; }
    }
}
