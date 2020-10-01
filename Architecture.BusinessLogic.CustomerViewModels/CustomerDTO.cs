using System;

namespace Architecture.BusinessLogic.CustomerDTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        //bottom 4 properties can be removed as it's not DRY;
        //with class CustomerDTO: CustomerUpgradeRSO (and superclass CustomerCreateSRO)
        //not really interested as those SROs are incoming actions from user,
        //and this one is outgoing DTOs of complete objects.
        public Guid Guid { get; set; }
        public string FirstName { get; set; }
        public string Initials { get; set; }
        public string LastName { get; set; }
    }
}
