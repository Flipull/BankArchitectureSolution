using System;

namespace Architecture.BusinessLogic.CustomerSROs
{
    public class CustomerUpdateSRO : CustomerCreateSRO
    {
        public Guid Guid { get; set; }
    }
}
