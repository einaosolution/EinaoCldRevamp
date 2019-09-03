using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.DelegateJob
{
  public   class DelegateJob : EntityBase
    {
        [ForeignKey("Id")]
        public int PatentApplicationID { get; set; }

        public string userid { get; set; }

        public string applicationstage { get; set; }






        public IPORevamp.Data.Entity.Interface.Entities.PatentApplication.PatentApplication PatentApplication { get; set; }
    }
}
