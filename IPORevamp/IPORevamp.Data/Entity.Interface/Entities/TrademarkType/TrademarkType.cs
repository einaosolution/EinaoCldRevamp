using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.TrademarkType
{
  public   class TrademarkType : EntityBase
    {
        public string Description { get; set; }

        public List<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> MarkInfo { get; set; }
    }
}
