using System;

namespace Architecture.BusinessLogic.BankSROs
{
    public abstract class PaginationAbstract
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
    public class BankTransactionSearchSRO : PaginationAbstract
    {
        public Guid GuidAccount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}