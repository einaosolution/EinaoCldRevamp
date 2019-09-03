using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory
{
  public   class DesignApplicationHistory : EntityBase
    {
        public string patentcomment { get; set; }
        public string description { get; set; }

        [ForeignKey("Id")]
        public int DesignApplicationID { get; set; }

        public string TransactionID { get; set; }

        public string FromDataStatus { get; set; }
        public string FromStatus { get; set; }
        public string ToStatus { get; set; }
        public string ToDataStatus { get; set; }

        public string UploadsPath1 { get; set; }
        public int userid { get; set; }
        public string UploadsPath2 { get; set; }

        public string AcceptanceFilePath { get; set; }

        public string RefusalFilePath { get; set; }
        public string Role { get; set; }
        public IPORevamp.Data.Entity.Interface.Entities.DesignApplication.DesignApplication DesignApplication { get; set; }
    }
}
