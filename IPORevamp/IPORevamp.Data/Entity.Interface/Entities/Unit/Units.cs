using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Unit
{
 public    class Units : EntityBase
    {
        [Required]
        public string Description { get; set; }

        [ForeignKey("Id")]

        public int DepartmentId { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.Department.Department Department { get; set; }
    }
}
