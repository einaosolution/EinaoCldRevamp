using IPORevamp.Data.Entities.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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


        [InverseProperty("AssigneeNationality")]
        public virtual ICollection<IPORevamp.Data.Entity.Interface.Entities.PatentAssignment.PatentAssignment> AssigneeNationality { get; set; }
        [InverseProperty("AssignorNationality")]
        public virtual ICollection<IPORevamp.Data.Entity.Interface.Entities.PatentAssignment.PatentAssignment> AssignorNationality { get; set; }

        [InverseProperty("AssigneeNationality2")]
        public virtual ICollection<IPORevamp.Data.Entity.Interface.Entities.DesignAssigment.DesignAssignment> AssigneeNationality2 { get; set; }

        [InverseProperty("AssignorNationality2")]
        public virtual ICollection<IPORevamp.Data.Entity.Interface.Entities.DesignAssigment.DesignAssignment> AssignorNationality2 { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.DesignInvention.DesignInvention> DesignInvention { get; set; }


        public List<IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation.PatentPriorityInformation> PatentPriorityInformation { get; set; }


       
        public List<IPORevamp.Data.Entity.Interface.Entities.PatentInvention.PatentInvention> PatentInvention { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.DesignPriority.DesignPriority> DesignPriority { get; set; }

    }
}
