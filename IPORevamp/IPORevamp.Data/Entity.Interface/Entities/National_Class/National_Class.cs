using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.National_Class
{
  public   class NationalClass : EntityBase
    {
        public string Type { get; set; }
        public string Description { get; set; }


        public List<IPORevamp.Data.Entity.Interface.Entities.DesignInformation.DesignInformation> DesignInformation { get; set; }

    }
}
