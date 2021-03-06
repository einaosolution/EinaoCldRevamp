﻿
using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;
using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.FileNewApplication
{
 public    interface InewApplication : IAutoDependencyRegister
    {
        Task<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> SaveApplication(IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application application);
        Task<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> UpdateApplication(IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application application);
        Task<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> GetApplication(int id );
        Task<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> GetMarkInfo(int id);
        Task<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> UpdateMarkInfo(IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation markinfo);
        Task<IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory> SaveAppHistory(IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory.TrademarkApplicationHistory apphistory);
        Task<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation> SaveMarkInfo(IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation markinfo);
        Task<IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInformation>  UpdateMarkInfo(int id);
        Task<String> updateTransactionById(string transactionid, string paymentid);
        Task<Int32> ApplicationUserCount(int ApplicationId, int userid);
        Task<PatentApplication> SavePatentApplication(PatentApplication application);
        Task<PatentInformation> SavePatentInformation(PatentInformation ptinfo);
        Task<PatentInformation> GetPatentApplication(int id);
        Task<PatentAssignment> SavePatentAssignment(PatentAssignment patentAssignment);
        Task<PatentInformation> UpdatePatentInformation(PatentInformation patentinfo);
        Task<PatentInventionView[]> SavePatentInvention(PatentInventionView[] PatentInventionView, int ApplicationId);
        Task<PatentPriorityInformationView[]> SavePriorityInformation(PatentPriorityInformationView[] PatentPriorityInformationView, int ApplicationId);
        Task<PatentApplication> GetPatentApplicationById(int id);
        Task<AppCount> AllApplicationUserCount(string userid);
        Task<PatentApplication> GetPatentApplicationByUserId(string userid);
        Task<AddressOfService> SaveAddressOfService(AddressOfService addressOfService);
        Task<String> updatePatentTransactionById(string transactionid, string paymentid);
        Task<PatentAssignment> UpdatePatentAssignment(int id, PatentAssignment patentassignment);
        Task<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> UpdateApplication2(IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application application);
        Task<IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application> SaveApplication2(IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application application);
        Task<DashBoardCount> AllSearchApplicationCount();
        Task<PatentInformation> SavePatentInformation2(PatentInformation ptinfo);
        Task<PatentApplication> UpdatePatentApplication(PatentApplication patentinfo);
        Task<DashBoardPatentCount> AllSearchPatentCount();
        Task<DashBoardDesignCount> AllSearchDesignCount();
    }
}
