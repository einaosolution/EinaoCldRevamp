
using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.FileNewApplication
{
 public    interface InewApplication : IAutoDependencyRegister
    {
        Task<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Pwallet> Savepwallet(IPORevamp.Data.Entity.Interface.Entities.Pwallet.Pwallet pwallet);
        Task<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> SaveAppHistory(IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory apphistory);
        Task<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.Mark_Info> SaveMarkInfo(IPORevamp.Data.Entity.Interface.Entities.MarkInfo.Mark_Info markinfo);
    }
}
