using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Payment
{
  public   class PaymentReport
    {
        [Key]
        public string sn { get; set; }

        public DateTime DateCreated { get; set; }
        public Decimal  Amount { get; set; }
        public String  OrderId { get; set; }
        public String PayerName { get; set; }
        public String PayerEmail { get; set; }
        public String Description { get; set; }
        public String RRR { get; set; }
       
        public DateTime PaymentDate { get; set; }
        public Decimal TechFee { get; set; }
        public String  TotalAmount { get; set; }
        public int  PaymentStatus { get; set; }
        public int FeeId { get; set; }
        public String FeeItemName { get; set; }




    }
}
