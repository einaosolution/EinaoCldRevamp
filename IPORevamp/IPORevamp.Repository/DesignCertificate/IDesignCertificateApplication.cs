using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.DesignCertificate
{
  public   interface IDesignCertificateApplication : IAutoDependencyRegister
    {
        Task<List<DesignDataResult>> GetDesignFreshApplication();
        Task<List<DesignDataResult>> GetDesignConfirmCertificate();
    }
}
