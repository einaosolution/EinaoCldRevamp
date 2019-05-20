using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entities
{
   public  class Sector : EntityBase
    {
        public string Type { get; set; }
        public string Description  { get; set; }
    }
}
