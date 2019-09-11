using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.DelegateJob
{
  public   class DelegateDesignJob : EntityBase
    {
        [ForeignKey("Id")]
        public int DesignApplicationID { get; set; }

        public string userid { get; set; }

        public string applicationstage { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.DesignApplication.DesignApplication DesignApplication { get; set; }
    }
}
