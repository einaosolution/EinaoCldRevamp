using IPORevamp.Data.Entities.Country;
using IPORevamp.Data.Entities.Setting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entities
{
    public class LGAStateViewModel
    {
        public int StateId { get; set; }
        public int LGAId { get; set; }
        public string LGAName { get; set; }
        public int CreatedBy { get; set; }

    }
}
