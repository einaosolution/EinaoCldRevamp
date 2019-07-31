
using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.FileNewApplication
{
 public    interface InewApplication : IAutoDependencyRegister
    {
        Task<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> SaveApplication(IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application application);
        Task<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> UpdateApplication(IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application application);
        Task<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> GetApplication(int id );
        Task<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> GetMarkInfo(int id);
        Task<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> UpdateMarkInfo(IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation markinfo);
        Task<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> SaveAppHistory(IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory apphistory);
        Task<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> SaveMarkInfo(IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation markinfo);
        Task<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation>  UpdateMarkInfo(int id);
        Task<String> updateTransactionById(string transactionid, string paymentid);
    }
}
