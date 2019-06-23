using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Unit
{
  public   class UnitViewModel
    {
        public string Description { get; set; }
     
        public int id { get; set; }
        public String CreatedBy { get; set; }

        public int  department { get; set; }
    }
}
