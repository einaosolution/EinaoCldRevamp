using IPORevamp.Data.Entities.LGAs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities
{
   public  class State : EntityBase
    {
        public string StateName { get; set; }

        [ForeignKey("CountryId")]

        public int CountryId { get; set; }
        public Data.Entities.Country.Country Country { get; set; }
       
        public List<Data.Entities.LGAs.LGA> LGA { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.AddressOfService.AddressOfService> AddressOfService { get; set; }
    }
}
