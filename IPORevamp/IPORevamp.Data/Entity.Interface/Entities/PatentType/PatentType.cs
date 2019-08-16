using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.PatentType
{
  public  class PatentType : EntityBase
    {
        public string Description { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.PatentInformation.PatentInformation> PatentInformation { get; set; }
    }
}
