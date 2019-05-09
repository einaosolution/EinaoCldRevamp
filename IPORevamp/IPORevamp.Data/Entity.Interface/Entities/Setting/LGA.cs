using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.Setting
{
   public  class lga : EntityBase
    {
        public string LGAName { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }
    }
}
