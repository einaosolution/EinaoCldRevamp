using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Certificate
{
    public interface ICertificateRepository : IAutoDependencyRegister
    {
        Task<List<Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplicationByUserid(string userid);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetPaidCertificate();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetIssuedCertificate();
        Task<IPORevamp.Data.Entity.Interface.Entities.Certificate.PayCertificate> GetCertificatePaymentById(int applicationid);
        Task<String> UpdateApplication(int applicationid);
        Task<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult> GetApplicationById(int id);
    }
}
