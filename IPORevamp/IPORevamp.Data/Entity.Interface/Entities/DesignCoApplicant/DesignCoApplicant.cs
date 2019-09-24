using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.DesignCoApplicant
{
   public  class DesignCoApplicant : Data.Entities.EntityBase
    {
        [ForeignKey("Id")]
        public int DesignApplicationID { get; set; }

        public string Fullname  { get; set; }

        public string email { get; set; }

        public string phonenumber { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.DesignApplication.DesignApplication DesignApplication { get; set; }
    }
}
