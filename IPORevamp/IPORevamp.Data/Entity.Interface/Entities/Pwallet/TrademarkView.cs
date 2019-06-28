using IPORevamp.Data.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Pwallet
{
  public   class TrademarkView
    {
        public Pwallet pwallet { get; set; }
        public MarkInfo.Mark_Info  markinfo  { get; set; }
        public ApplicationUser applicationUser { get; set; }
    }
}
