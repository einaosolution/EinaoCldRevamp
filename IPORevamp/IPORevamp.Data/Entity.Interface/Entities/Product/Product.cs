using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Product
{
   public  class Product : EntityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.Payment.Payment> Payment { get; set; }
    }
}
