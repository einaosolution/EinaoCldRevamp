using IPORevamp.Repository.Interface;

using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.DesignAssigment;
using IPORevamp.Data.Entity.Interface.Entities.DesignInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.DesignInvention;
using IPORevamp.Data.Entity.Interface.Entities.DesignPriority;
using Microsoft.AspNetCore.Http;

namespace IPORevamp.Repository.DesignSearch
{
  public   interface IDesignSearchApplication : IAutoDependencyRegister
    {
        Task<List<DesignDataResult>> GetDesignFreshApplication();
        Task<List<DesignAddressOfService>> GetAddressOfServiceById(int id);
        Task<List<DesignApplicationHistory>> GetSearchState(int id, int userid);
        void SendUserEmail(int appid, string comment);
        void SaveApplicationStateHistory(int id, string userrole, HttpRequest request, string tostatus, string toDatastatus, string fromDatastatus, string fromstatus, string comment, string description, string userid, string filepath);
        Task<List<DesignInvention>> GetInventorById(int id);
        Task<List<DesignPriority>> GetPriorityById(int id);
        void SaveApplicationHistory(int id, string userrole, HttpRequest request, string tostatus, string toDatastatus, string fromDatastatus, string fromstatus, string comment, string description, string userid, string filepath);
        void SendExaminerEmail();
        Task<List<DesignDataResult>> GetDesignReconductSearch();
        Task<List<DesignDataResult>> GetDesignKivSearch();
        Task<List<DesignDataResult>> GetDesignListing(string userid);



    }
}
