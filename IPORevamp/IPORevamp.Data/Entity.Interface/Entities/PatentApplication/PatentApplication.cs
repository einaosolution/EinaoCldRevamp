using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.PatentApplication
{
  public   class PatentApplication : EntityBase
    {
        public string TransactionID { get; set; }

        public string userid { get; set; }

        public string ApplicationStatus { get; set; }

        public string DataStatus { get; set; }


        public List<IPORevamp.Data.Entity.Interface.Entities.PatentInformation.PatentInformation> PatentInformation { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.PatentApplicationHistory.PatentApplicationHistory> PatentApplicationHistory { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.PatentAssignment.PatentAssignment> PatentAssignment { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation.PatentPriorityInformation> PatentPriorityInformation { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.PatentInvention.PatentInvention> PatentInvention { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.AddressOfService.AddressOfService> AddressOfService { get; set; }






    }
}
