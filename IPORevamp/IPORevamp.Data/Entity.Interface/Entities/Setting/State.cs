using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.Setting
{
   public  class State : EntityBase
    {
        public string StateName { get; set; }
        [ForeignKey("CountryId")]

        public Country Country { get; set; }
    }
}
