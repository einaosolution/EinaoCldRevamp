using IPORevamp.Data.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Pwallet
{
  public   class TrademarkView
    {
        public Application  application { get; set; }
        public MarkInfo.MarkInformation  markinfo  { get; set; }
        public ApplicationUser applicationUser { get; set; }

        public List<TrademarkLogo.TrademarkLogo> TrademarkLogo { get; set; }
    }
}
