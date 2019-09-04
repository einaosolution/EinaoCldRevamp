using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.DesignApplication
{
   public  class DesignApplication : EntityBase
    {
        public string TransactionID { get; set; }

        public string userid { get; set; }

        public string ApplicationStatus { get; set; }

        public string DataStatus { get; set; }

        public string CertificatePayReference { get; set; }


        public List<IPORevamp.Data.Entity.Interface.Entities.PatentApplicationHistory.PatentApplicationHistory> PatentApplicationHistory { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.DesignInformation.DesignInformation> DesignInformation { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.DesignAssigment.DesignAssignment> DesignAssignment { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.DesignInvention.DesignInvention> DesignInvention { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.DesignPriority.DesignPriority> DesignPriority { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService.DesignAddressOfService> DesignAddressOfService { get; set; }
        

    }
}
