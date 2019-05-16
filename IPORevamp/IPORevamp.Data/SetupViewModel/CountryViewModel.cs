using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.SetupViewModel
{
   public class CountryViewModel
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public int EnableForOtherCountry { get; set; }
        public int CreatedBy { get; set; }
        public bool DeletedBy { get; set; }

        public int CountryId { get; set; }
        public int IsActive { get; set; }
    }
}


