using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Pwallet
{
  public   class Application : EntityBase
    {
        [ForeignKey("Id")]
        public int Applicationtypeid { get; set; }

        public string TransactionID { get; set; }

        public string userid { get; set; }
        public string RtNumber { get; set; }

        public string ApplicationStatus { get; set; }
        public string Batchno { get; set; }
        public string CertificatePayReference { get; set; }

        public string migratedapplicationid { get; set; }

        public DateTime ? NextRenewalDate { get; set; }

        public string DataStatus { get; set; }

        public IPORevamp.Data.Entity.Interface.ApplicationType.ApplicationType ApplicationType { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.Opposition.NoticeOfOpposition> NoticeOfOpposition { get; set; }
       
        public List<IPORevamp.Data.Entity.Interface.Entities.Opposition.CounterOpposition> CounterOpposition { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> Mark_Info { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalRenewal> RecordalRenewal { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger> RecordalMerger { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> TrademarkApplicationHistory { get; set; }
    }
}
