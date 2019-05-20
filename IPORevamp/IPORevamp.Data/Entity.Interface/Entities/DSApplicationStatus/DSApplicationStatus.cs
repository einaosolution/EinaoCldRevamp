using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.DSApplicationStatus
{
    public class DSApplicationStatus : EntityBase
    {
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        [Required]
        public string StatusDescription { get; set; }
    }
}
