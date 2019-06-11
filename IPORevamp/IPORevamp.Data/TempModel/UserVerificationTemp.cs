using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPORevamp.Data.TempModel
{
  public   class UserVerificationTemp : EntityBase
    {
        
        public string Email { get; set; }   
        public string First_Name { get; set; }   
        public string Last_Name { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string Unit { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postal { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; }
        public DateTime ?  ExpiringDate { get; set; }
        public Boolean  expired  { get; set; }
        public DateTime? ConfirmationDate { get; set; }


    }
}
