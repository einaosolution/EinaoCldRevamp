using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace IPORevamp.Repository.PatentCertificate
{
   public  interface IPatentCertificateRepository : IAutoDependencyRegister
    {

        Task<List<PatentDataResult>> GetPatentCertificate(string userid);
        Task<IPORevamp.Data.Entity.Interface.Entities.Certificate.PayCertificate> ProcessCertificatePayment(int NoticeAppID, string TransactionId);
        Task<List<PatentDataResult>> GetPatentPaidCertificate();
        Task<List<PatentDataResult>> GetPatentConfirmedCertificate();
        Task<List<PatentDataResult>> GetDelegateCertificate(string userid);
        Task<List<PatentDataResult>> GetPatentPayCertificate(string userid);
        Task<ApplicationUser> GetUserByApplicationId(int AppID);
    }
}
