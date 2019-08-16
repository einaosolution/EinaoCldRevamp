using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.PatentType
{
   public  interface IPatentTypeRepository : IAutoDependencyRegister
    {
        Task<List<IPORevamp.Data.Entity.Interface.Entities.PatentType.PatentType>> GetPatentType();
    }
}
