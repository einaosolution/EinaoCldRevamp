using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Twallet
{
 public    class Twallet : EntityBase
    {
        public string  transactionid { get; set; }
        public string userid { get; set; }
        public string transaction_status { get; set; }

        public string pay_ref { get; set; }


        [ForeignKey("Id")]
        public int paymentid { get; set; }

        [ForeignKey("Id")]
        public int feelistid { get; set; }


        public IPORevamp.Data.Entity.Interface.Entities.Payment.Payment Payment { get; set; }
        public IPORevamp.Data.Entities.Fee.FeeList FeeList { get; set; }
       
    }
}
