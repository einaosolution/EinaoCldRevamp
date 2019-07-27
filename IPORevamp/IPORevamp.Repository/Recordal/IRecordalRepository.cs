
using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Recordal
{
  public   interface IRecordalRepository : IAutoDependencyRegister
    {
        Task<List<Data.Entity.Interface.Entities.Search.DataResult>> GetApplicationByRegistrationId(String id);
        Task<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult> GetApplicationById(int applicationid);
        Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalRenewal> GetRenewalApplicationById(int applicationid);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalRenewal>> GetRenewalApplicationByDocumentId(int applicationid);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetIssuedCertificate();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetApplicationByRegistrationId2(String id);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalCertificate();
        Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger> GetMergerApplicationById(int applicationid);
        Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger> GetMergerApplicationByAppId(int applicationid);
    }
}
