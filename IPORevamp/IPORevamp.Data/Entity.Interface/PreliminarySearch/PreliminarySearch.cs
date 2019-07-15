using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.PreliminarySearch
{
   public  class PreliminarySearch : EntityBase
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string userid { get; set; }
        public string Email { get; set; }
        [ForeignKey("Sectorid")]
        public int sectorid { get; set; }
      
        public string   product { get; set; }
       
        public string payment_reference { get; set; }

        public  string status { get; set; }

      
        public IPORevamp.Data.Entities.Sector Sector { get; set; }
    }
}
