using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory
{
  public   class TrademarkApplicationHistory : EntityBase
    {
       
        public string  trademarkcomment { get; set; }
        public string description{ get; set; }

        [ForeignKey("Id")]
        public int pwalletid { get; set; }

        public string transaction_id { get; set; }

        public string from_datastatus { get; set; }
        public string to_datastatus { get; set; }

        public string UploadsPath1 { get; set; }
        public int  userid { get; set; }
        public string UploadsPath2 { get; set; }


        public IPORevamp.Data.Entity.Interface.Entities.Pwallet.Pwallet pwallet { get; set; }
    }
}
