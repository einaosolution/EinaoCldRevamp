using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Pwallet
{
  public   class Pwallet : EntityBase
    {
        [ForeignKey("Id")]
        public int Applicationtypeid { get; set; }

        public string transactionid { get; set; }

        public string userid { get; set; }

        public string application_status { get; set; }

        public string data_status { get; set; }

        public IPORevamp.Data.Entity.Interface.ApplicationType.ApplicationType ApplicationType { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.Comments.TrademarkComments> TrademarkComments { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.Mark_Info> Mark_Info { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> TrademarkApplicationHistory { get; set; }
    }
}
