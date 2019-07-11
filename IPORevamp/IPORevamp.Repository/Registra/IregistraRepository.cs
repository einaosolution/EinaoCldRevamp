using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Registra
{
  public   interface IregistraRepository : IAutoDependencyRegister
    {
        Task<List<Data.Entity.Interface.Entities.Search.DataResult>> GetUserAppeal();
        Task<List<Data.Entity.Interface.Entities.Search.DataResult>> GetAppeal();
        Task<List<Data.Entity.Interface.Entities.Search.DataResult>> TreatUserAppeal();
    }
}
