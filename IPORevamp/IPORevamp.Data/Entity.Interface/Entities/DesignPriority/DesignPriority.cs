using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.DesignPriority
{
   public  class DesignPriority : EntityBase
    {
        [ForeignKey("Id")]
        public int DesignApplicationID { get; set; }
        [ForeignKey("Id")]
        public int CountryId { get; set; }
        public string ApplicationNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public IPORevamp.Data.Entities.Country.Country Country { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.DesignApplication.DesignApplication DesignApplication { get; set; }
    }
}
