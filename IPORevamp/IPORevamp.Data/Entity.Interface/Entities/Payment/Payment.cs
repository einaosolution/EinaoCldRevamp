using System;
using System.Collections.Generic;
using IPORevamp.Data.Entities;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPORevamp.Data.Entity.Interface.Entities.Payment
{
   public  class Payment : EntityBase
    {
        public string transaction_reference { get; set; }
        public string userid { get; set; }
        public string pay_reference { get; set; }
        public string payment_mode { get; set; }

        public string payment_status{ get; set; }

        [ForeignKey("Id")]
         public int productid { get; set; }

        public Double Quantity { get; set; }
        public Double Amount { get; set; }
        public IPORevamp.Data.Entity.Interface.Entities.Product.Product Product { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.Twallet.Twallet> twallet { get; set; }


    }
}
