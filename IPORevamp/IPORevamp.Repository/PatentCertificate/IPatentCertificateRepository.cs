using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
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
    }
}
