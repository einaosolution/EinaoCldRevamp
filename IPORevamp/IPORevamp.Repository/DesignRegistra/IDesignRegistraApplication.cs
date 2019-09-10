using IPORevamp.Repository.Interface;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.DesignInvention;
using IPORevamp.Data.Entity.Interface.Entities.DesignPriority;
using Microsoft.AspNetCore.Http;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;

namespace IPORevamp.Repository.DesignRegistra
{
   public  interface IDesignRegistraApplication : IAutoDependencyRegister
    {
        Task<List<DesignDataResult>> GetDesignFreshApplication();
    }
}
