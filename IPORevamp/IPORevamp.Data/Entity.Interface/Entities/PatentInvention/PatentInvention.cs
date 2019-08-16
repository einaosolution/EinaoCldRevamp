using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.PatentInvention
{
  public   class PatentInvention : EntityBase
    {
        [ForeignKey("Id")]
        public int PatentApplicationID { get; set; }

        [ForeignKey("Id")]
        public int CountryId { get; set; }

        public string InventorName { get; set; }
        public string InventorAddress { get; set; }
        public string InventorEmail { get; set; }
        public string InventorMobileNumber { get; set; }

        public IPORevamp.Data.Entities.Country.Country Country { get; set; }
        public IPORevamp.Data.Entity.Interface.Entities.PatentApplication.PatentApplication PatentApplication { get; set; }
    }
}
