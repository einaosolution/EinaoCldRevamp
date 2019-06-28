using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Comments
{
   public  class TrademarkComments : EntityBase
    {
        [ForeignKey("Id")]
        public int pwalletid { get; set; }

        public string userid { get; set; }

        public string Comment { get; set; }


        public IPORevamp.Data.Entity.Interface.Entities.Pwallet.Pwallet pwallet { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> TrademarkApplicationHistory { get; set; }
    }
}
