using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Examiner
{
  public   interface IExaminerRepository : IAutoDependencyRegister
    {
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplication();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetUserKiv();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetExaminerKiv();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetTreatedApplication();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetExaminerReconductSearch();
        Task<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> GetApplicationHistoryById(int id);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.PreviousComments>> GetPreviousComment(int id);
    }
}
