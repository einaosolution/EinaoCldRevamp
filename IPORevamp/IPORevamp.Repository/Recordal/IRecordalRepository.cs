
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
        Task<Int32> Savechangeofname(IPORevamp.Data.Entity.Interface.Entities.Recordal.ChangeOfName Recordalchangeofname);
        Task<Int32> Updateform(string Name, string Address, string Comment, string filepath, string filepath2, string Type, int NoticeAppID);
        Task<Int32> Updateform(string Name, string Address, string Name2, string Address2, string Comment, string filepath, string filepath2, string filepath3, string MergerDate, int Nationality, int NoticeAppID);
        Task<Int32> Updatechangeofname(string newfirstname, string newlastname, int NoticeAppID);
        Task<Int32> UpdateRecord(string roleid, string TransactionId, int NoticeAppID, int userid);
        Task<Int32> UpdateMergerRecord(string roleid, string TransactionId, int NoticeAppID, int userid);
        Task<Int32> UpdateRennewalRecord(int NoticeAppID, int userid);
        Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.ChangeOfName> GetChangeOfNameApplicationById(int applicationid);
        Task<Int32> UpdateDesignRecord(string roleid, string TransactionId, int NoticeAppID, int userid);
        Task<Int32> SaveDesignform(IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalDesignRenewal RecordalRenewal);
        Task<Int32> UpdateDesignform(string Name, string Address, string Comment, string filepath, string filepath2, string Type, int NoticeAppID);
        Task<String> UpdateRennewalRecord(int NoticeAppID);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalPatentCertificate2();
        Task<Int32> UpdateRennewalDesignRecord(int NoticeAppID, int userid);
        Task<Int32> UpdateRennewalPatentRecord(int NoticeAppID, int userid);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalDesignCertificate2();
        Task<Int32> UpdatePatentform(string Name, string Address, string Comment, string filepath, string filepath2, string Type, int NoticeAppID);
        Task<Int32> UpdateRennewalRecord2(int NoticeAppID, int userid,string Status, string requestuser);
        Task<Int32> Savechangeofaddress(IPORevamp.Data.Entity.Interface.Entities.Recordal.ChangeOfAddress Recordalchangeofname);
        Task<Int32> Updatechangeofaddress(string newaddress, int NoticeAppID);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalCertificate2();
        Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalPatentRenewal> GetRenewalPatentApplicationById(int applicationid);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalPatentCertificate();

        Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalDesignRenewal> GetRenewalDesignApplicationById(int applicationid);
        Task<Int32> UpdateRennewalRecord4(int NoticeAppID, int userid, string Status, string requestuser);
        Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalMerger> GetMergerApplicationByAppId2(int applicationid);
        Task<IPORevamp.Data.Entity.Interface.Entities.Recordal.ChangeOfAddress> GetChangeOfAddressApplicationById(int applicationid);
        Task<Int32> UpdateChangeOfAddressRecord(string roleid, string TransactionId, int NoticeAppID, int userid);
        Task<List<ChangeOfAddressView>> GetRecordalChangeOfAddress(string status);
        Task<Int32> UpdatePatentRecord(string roleid, string TransactionId, int NoticeAppID, int userid);
        Task<Int32> SavePatentform(IPORevamp.Data.Entity.Interface.Entities.Recordal.RecordalPatentRenewal RecordalRenewal);
        Task<Int32> UpdateRennewalRecord3(int NoticeAppID, int userid, string Status, string requestuser);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalChangeOfName(string status);
        Task<Int32> UpdateChangeOfNameRecord(string roleid, string TransactionId, int NoticeAppID, int userid);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult3>> GetRecordalMerger(string status);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRecordalRenewalDesignCertificate();

    }
}
