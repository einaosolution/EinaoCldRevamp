using IPORevamp.Data.Entities.Country;
using IPORevamp.Data.Entities.Setting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entities
{
   public class StateCountryViewModel
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CreatedBy { get; set; }
        public int CountryID { get; set; }
        
    

    }
}
