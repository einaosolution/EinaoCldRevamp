using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.ApplicationType
{
   public  class ApplicationType : EntityBase
    {
        public string name { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> pwallet { get; set; }
    }
}
