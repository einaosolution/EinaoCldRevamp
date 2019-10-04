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
        void DelegateExaminerEmail(string userid, int applicationid);
        Task<List<DesignDataResult>> GetDelegateCertificate(string userid);
        void SendEmailForCerticate(int appid, string comment);
        Task<List<DesignDataResult>> GetDesignPayCertificate(string userid);
        Task<IPORevamp.Data.Entity.Interface.Entities.Certificate.PayCertificate> ProcessCertificatePayment(int NoticeAppID, string TransactionId);
        Task<List<DesignDataResult>> GetDesignAppealApplication();
        Task<List<DesignDataResult>> GetDelegateAppeal(string userid);
        Task<List<DesignDataResult>> GetDesignReceiveAppealApplication();
        int GetDesignAppealCount();
        int GetReceiveAppealCount();
    }
}
