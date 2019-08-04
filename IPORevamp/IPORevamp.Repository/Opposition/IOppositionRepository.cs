using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Opposition
{
   public  interface IOppositionRepository : IAutoDependencyRegister
    {
        Task<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> GetApplicationById(int applicationid);
        Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.NoticeOfOpposition> GetNoticeApplicationById(int applicationid );
        Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.NoticeOfOpposition> GetNoticeApplicationByTransactionid(int applicationid,string transactionid);
        Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.CounterOpposition> GetCounterOppostionApplicationById(int applicationid);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplication();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetNewJudgment();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetOppositionByUserId(String id);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetCounterOppositionByUserId(String id);
        Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.CounterOpposition> GetCounterOppostionApplicationByTransactionid(int applicationid, string transactionid);
        Task<Int32> SaveForm(IPORevamp.Data.Entity.Interface.Entities.Opposition.NoticeOfOpposition NoticeOfOpposition);
        Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.NoticeOfOpposition> UpdateForm(string opponentName, string opponentAddress, string Comment, string filepath, int NoticeAppID);
        Task<Int32> SaveForm(IPORevamp.Data.Entity.Interface.Entities.Opposition.CounterOpposition CounterOpposition);
        Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.CounterOpposition> UpdateCounterForm(string opponentName, string opponentAddress, string Comment, string filepath, int NoticeAppID);

    }
}
