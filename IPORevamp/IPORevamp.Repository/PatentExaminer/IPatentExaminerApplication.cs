
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplicationHistory;
using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.PatentExaminer
{
  public   interface IPatentExaminerApplication : IAutoDependencyRegister
    {
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.PreviousComments>> GetPreviousComment(int id);
        void SendRegistraEmail(int applicationId);
        void SendExaminerEmail();
        Task<List<PatentDataResult>> GetPatentFreshApplication();
        Task<PatentApplicationHistory> GetRefusalComment(int id);
        Task<List<PatentDataResult>> GetPatentExaminerKiv();
        Task<List<PatentDataResult>> GetPatentExaminerReconductSearch();
        Task<List<PatentDataResult>> GetRefuseApplicationByUserid(string userid);
        Task<List<PatentDataResult>> GetPatentAppeal();
        Task<String> SendApplicationAmount(string transid);
        Task<List<PatentDataResult>> GetPatentAppealUnit();
        Task<List<PatentDataResult>> GetPatentTreatedAppeal();


    }
}
