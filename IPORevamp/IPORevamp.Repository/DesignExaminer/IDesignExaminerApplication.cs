using IPORevamp.Repository.Interface;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.DesignInvention;
using IPORevamp.Data.Entity.Interface.Entities.DesignPriority;
using Microsoft.AspNetCore.Http;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;

namespace IPORevamp.Repository.DesignExaminer
{
  public   interface IDesignExaminerApplication : IAutoDependencyRegister
    {

        Task<List<DesignDataResult>> GetDesignFreshApplication();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.PreviousComments>> GetPreviousComment(int id);
        void SendRegistraEmail(int applicationId);
        Task<DesignApplicationHistory> GetRefusalComment(int id);


    }
}
