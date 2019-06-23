using IPORevamp.Data.Entities;
using IPORevamp.Data.Entity.Interface.Entities.Unit;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Department
{
    public class Department : EntityBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        // Load All  units
        public List<Units> Units { get; set; }

       
    }
}
