using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.NationalClass
{
  public   interface INationalClassRepository : IAutoDependencyRegister
    {
        Task<List<IPORevamp.Data.Entity.Interface.Entities.National_Class.NationalClass>> GetNationalClass();
    }
}
