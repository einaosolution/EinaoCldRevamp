using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation
{
  public   class PatentPriorityInformationView
    {
     
        public int PatentApplicationID { get; set; }

      
        public string  CountryId { get; set; }

        public string ApplicationNumber { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
