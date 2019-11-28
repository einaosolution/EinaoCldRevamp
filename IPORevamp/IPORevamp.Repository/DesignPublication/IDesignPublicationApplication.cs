using System;
using System.Collections.Generic;
using System.Text;
using IPORevamp.Repository.Interface;
using System.Threading.Tasks;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;

namespace IPORevamp.Repository.DesignPublication
{
   public  interface IDesignPublicationApplication : IAutoDependencyRegister
    {
        Task<List<DesignDataResult>> GetDesignFreshApplication();
        void UpdateApplicationBatch(int BatchNo, string userole, string userid);
        Task<List<Int32>> GetDesignApplicationBatch();
        Task<List<DesignDataResult>> GetPublicationById(String id);
        Task<List<DesignDataResult>> GetDesignUnplishedApplication();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.DesignApplication.DesignPublication>> GetDesignPublication();

    }
}
