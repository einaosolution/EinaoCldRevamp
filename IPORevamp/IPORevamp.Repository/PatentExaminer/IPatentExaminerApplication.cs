using EmailEngine.Repository.Interface;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.PatentExaminer
{
  public   interface IPatentExaminerApplication : IAutoDependencyRegister
    {
        Task<List<PatentDataResult>> GetPatentFreshApplication();
    }
}
