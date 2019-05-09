using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entities.Setting
{
   public class Country : EntityBase
    {
        public string Name { get; set; }
        public string  Code { get; set; }
        public int EnableForOtherCountry { get; set; }

    

    }
}
