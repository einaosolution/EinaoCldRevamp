using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entities
{
   public  class RemitaBankCode : EntityBase
    {
        public string BankName { get; set; }
        public string BankCode  { get; set; }
    }
}
