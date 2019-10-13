
using IPORevamp.Data.Entity.Interface.Entities.Recordal;
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
        Task<Int32> Saveform(IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalRenewal RecordalRenewal);
        Task<Int32> Saveform(IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger RecordalMerger);
        Task<Int32> Updateform(string Name, string Address, string Comment, string filepath, string filepath2, string Type, int NoticeAppID);
        Task<Int32> Updateform(string Name, string Address, string Name2, string Address2, string Comment, string filepath, string filepath2, string filepath3, string MergerDate, int Nationality, int NoticeAppID);
        Task<Int32> UpdateRecord(string roleid, string TransactionId, int NoticeAppID, int userid);
        Task<Int32> UpdateMergerRecord(string roleid, string TransactionId, int NoticeAppID, int userid);
        Task<Int32> UpdateRennewalRecord(int NoticeAppID, int userid);
        Task<Int32> UpdateDesignRecord(string roleid, string TransactionId, int NoticeAppID, int userid);
        Task<Int32> SaveDesignform(IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalDesignRenewal RecordalRenewal);
        Task<Int32> UpdateDesignform(string Name, string Address, string Comment, string filepath, string filepath2, string Type, int NoticeAppID);
        Task<String> UpdateRennewalRecord(int NoticeAppID);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalDesignCertificate();

    }
}
