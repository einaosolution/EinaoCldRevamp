using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.LGAs
{
   public  class LGA : EntityBase
    {
        public string LGAName { get; set; }
        [ForeignKey("StateId")]
        public int StateId { get; set; }
        public State State { get; set; }
    }
}
