using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Search_Unit
{
 public    interface Isearch : IAutoDependencyRegister
    {
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplication();
    }
}
