using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.PreliminarySearch
{
   public  class PreliminarySearch : EntityBase
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string userid { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string payment_reference { get; set; }

        public  string status { get; set; }
    }
}
