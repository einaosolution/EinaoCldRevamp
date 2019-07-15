using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Opposition
{
   public  class NoticeOfOpposition : EntityBase
    {
        public string opponentName { get; set; }
        public string opponentAddress { get; set; }
        public string Comment { get; set; }
        public string PaymentReference { get; set; }
        public string Upload { get; set; }
        public string Status { get; set; }
        public string userid { get; set; }

        [ForeignKey("ApplicationId")]

        public int ApplicationId { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application Application { get; set; }
    }
}
