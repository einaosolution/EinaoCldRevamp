
using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.DesignAssigment;
using IPORevamp.Data.Entity.Interface.Entities.DesignCoApplicant;
using IPORevamp.Data.Entity.Interface.Entities.DesignInformation;
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

namespace IPORevamp.Repository.DesignNewpplication
{
  public   interface IDesignNewpplicationRepository : IAutoDependencyRegister
    {

        Task<DesignInformation> UpdatedesignInformation(DesignInformation patentinfo);

        Task<DesignApplication> SaveDesignApplication(DesignApplication application);
        Task<DesignAddressOfService> SaveAddressOfService(DesignAddressOfService addressOfService);

        Task<PatentInventionView[]> SaveDesignInvention(PatentInventionView[] PatentInventionView, int ApplicationId);

        Task<DesignApplication> GetDesignApplicationById(int id);

        Task<DesignApplication> GetDesignApplicationByUserId(string userid);

        Task<String> updateDesignTransactionById(string transactionid, string paymentid);
        void GetCancelApplicationById(int id);
        void GetCancelApplication2ById(int id);


        Task<DesignInformation> SaveDesignInformation(DesignInformation ptinfo);
        Task<PatentPriorityInformationView[]> SavePriorityInformation(PatentPriorityInformationView[] PatentPriorityInformationView, int ApplicationId);
        Task<DesignAssignment> SaveDesignAssignment(DesignAssignment designAssignment);
       
        Task<DesignInformation> GetDesignApplication(int id);
        Task<DesignAssignment> UpdateDesignAssignment(int id, DesignAssignment designassignment);
        Task<CoApplicantView[]> SaveCoApplicantInformation(CoApplicantView[] CoApplicantView, int ApplicationId);





    }
}
