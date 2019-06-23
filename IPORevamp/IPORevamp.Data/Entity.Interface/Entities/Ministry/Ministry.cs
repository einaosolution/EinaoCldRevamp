using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IPORevamp.Data.Entities;

namespace IPORevamp.Data.Entity.Interface.Entities.Ministry
{
   public  class Ministry : EntityBase
    {
        [Required]
        public string Description { get; set; }
    }
}
