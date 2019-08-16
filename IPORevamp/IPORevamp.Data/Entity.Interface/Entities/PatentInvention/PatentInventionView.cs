using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.PatentInvention
{
  public   class PatentInventionView
    {
        public int CountryId { get; set; }
        public int PatentApplicationID { get; set; }
        public string InventorName { get; set; }
        public string InventorAddress { get; set; }
        public string InventorEmail { get; set; }
        public string InventorMobileNumber { get; set; }
    }
}
