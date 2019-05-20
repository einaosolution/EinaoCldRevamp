using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Sector
{
    public class SectorViewModel
    {
        public int SectorId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public int IsActive { get; set; }

    }
}
