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
        Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.NoticeOfOpposition> GetNoticeApplicationById(int applicationid);
        Task<IPORevamp.Data.Entity.Interface.Entities.Opposition.CounterOpposition> GetCounterOppostionApplicationById(int applicationid);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplication();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetNewJudgment();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetOppositionByUserId(String id);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetCounterOppositionByUserId(String id);

    }
}
