using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using IPORevamp.Data.UserManagement.Model;

namespace IPORevamp.Data.Entities
{
    public class PriceQuote:EntityBase
    {
        [Key] // Primary key
        public int PriceID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }        
        public string Email { get; set; }
        public string EventName { get; set; }
        public string CountryCode { get; set; }        
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int NumberofAttendees { get; set; }
        public int Duration { get; set; }
        public string Referer { get; set; }        
        public int? RequestedById { get; set; }
        [ForeignKey("RequestedById")]
        public  ApplicationUser RequestdBy { get; set; }        

    }
}
