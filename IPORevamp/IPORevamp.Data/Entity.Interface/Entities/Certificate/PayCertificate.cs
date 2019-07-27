using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Certificate
{
  public   class PayCertificate : EntityBase
    {
        public string ApplicantName { get; set; }
        public string ApplicantAddress { get; set; }
      
        public string PaymentReference { get; set; }
      
        public string Status { get; set; }
        public string userid { get; set; }
         public int quantity { get; set; }

        public string  ApplicationId { get; set; }

       
    }
}
