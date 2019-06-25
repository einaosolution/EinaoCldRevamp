using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory
{
  public   class TrademarkApplicationHistory : EntityBase
    {
        [ForeignKey("Id")]
        public int trademarkcommentid { get; set; }

        [ForeignKey("Id")]
        public int pwalletid { get; set; }

        public string transaction_id { get; set; }

        public string from_datastatus { get; set; }
        public string to_datastatus { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.Comments.TrademarkComments TrademarkComments { get; set; }
        public IPORevamp.Data.Entity.Interface.Entities.Pwallet.Pwallet pwallet { get; set; }
    }
}
