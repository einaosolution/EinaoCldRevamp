using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.DesignInvention
{
   public  class DesignInvention : EntityBase
    {
        [ForeignKey("Id")]
        public int DesignApplicationID { get; set; }
        [ForeignKey("Id")]
        public int CountryId { get; set; }

        public string InventorName { get; set; }
        public string InventorAddress { get; set; }
        public string InventorEmail { get; set; }
        public string InventorMobileNumber { get; set; }

        public IPORevamp.Data.Entities.Country.Country Country { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.DesignApplication.DesignApplication DesignApplication { get; set; }
    }
}
