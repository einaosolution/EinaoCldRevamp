using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.DesignType
{
  public   interface  IDesignTypeRepository : IAutoDependencyRegister
    {
        Task<List<IPORevamp.Data.Entity.Interface.Entities.DesignType.DesignType>> GetDesignType();
    }
}
