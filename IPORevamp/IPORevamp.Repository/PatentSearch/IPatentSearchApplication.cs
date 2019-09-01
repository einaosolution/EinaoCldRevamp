
using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;
using IPORevamp.Repository.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.PatentSearch
{
   public  interface IPatentSearchApplication : IAutoDependencyRegister
    {
        Task<List<PatentDataResult>> GetSubmittedApplication();
        Task<List<PatentInvention>> GetInventorById(int id);
        Task<List<PatentPriorityInformation>> GetPriorityById(int id);
        Task<List<AddressOfService>> GetAddressOfServiceById(int id);
        Task<List<PatentDataResult>> GetPatentSearchKiv();
        void SendExaminerEmail();
        void SendUserEmail(int userid, string comment);
        Task<List<PatentDataResult>> GetPatentFreshApplication();
        Task<List<PatentDataResult>> GetPatentApplicationByUserid(string userid);
        Task<List<PatentApplicationHistory>> GetSearchState(int id, int userid);
        void SendEmailForCerticate(int appid, string comment);
        void SaveApplicationStateHistory(int id, string userrole, HttpRequest request, string tostatus, string toDatastatus, string fromDatastatus, string fromstatus, string comment, string description, string userid, string filepath);
   void SaveApplicationHistory(int id, string userrole, HttpRequest request, string tostatus, string toDatastatus, string fromDatastatus, string fromstatus, string comment, string description, string userid,string filepath);

       

    }
}
