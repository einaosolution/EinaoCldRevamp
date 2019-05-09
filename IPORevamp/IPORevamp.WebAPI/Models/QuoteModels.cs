using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPORevamp.WebAPI.Models
{
    public class QuoteModel
    {        
        public int Id { get; set; }
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
        public int[] SelectedFeatures { get; set; }        
        public string[] SelectedFeaturesString { get; set; }
        public decimal Pricing { get; set; }
    }
}
