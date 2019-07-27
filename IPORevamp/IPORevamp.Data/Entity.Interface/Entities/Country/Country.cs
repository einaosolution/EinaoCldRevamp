using IPORevamp.Data.Entities.Setting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entities.Country
{
   public class Country : EntityBase
    {
        public string Name { get; set; }
        public string  Code { get; set; }
        public int EnableForOtherCountry { get; set; }


        // Load All States 
        public List<State> States { get; set; }

        public List<Data.Entities.LGAs.LGA> LGA { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger> RecordalMerger { get; set; }

    }
}
