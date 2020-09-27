using System;

namespace Architecture.BusinessLogic.CustomerDTOs
{
    public class CustomerDTO
    {
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string Initials { get; set; }
        public string LastName { get; set; }
    }
}
