using EmailEngine.Base.Entities;
using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Repository.Email;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using IPORevamp.Data.UserManagement.Model;

namespace IPORevamp.Repository.PatentCertificate
{
  public  class PatentCertificateRepository : IPatentCertificateRepository
    {
        private readonly IPOContext _contex;
              
        private IFileHandler _fileUploadRespository;
        protected readonly IConfiguration _configuration;
        private readonly IEmailSender _emailsender;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;



        public PatentCertificateRepository(IPOContext contex, IFileHandler fileUploadRespository, IConfiguration configuration, IEmailTemplateRepository EmailTemplateRepository, IEmailSender emailsender)
        {

            _contex = contex;

            _fileUploadRespository = fileUploadRespository;
            _configuration = configuration;
            _emailsender = emailsender;
            _EmailTemplateRepository = EmailTemplateRepository;



        }

        public async System.Threading.Tasks.Task<IPORevamp.Data.Entity.Interface.Entities.Certificate.PayCertificate> ProcessCertificatePayment(int NoticeAppID, string TransactionId)
        {

            var details = await (from p in _contex.PayCertificate
                                                                                                
                                 where p.Id == NoticeAppID

                                 select p).FirstOrDefaultAsync();



            var appid = details.ApplicationId.Split(',');
            details.PaymentReference = TransactionId;
            details.Status = "Paid";

            _contex.SaveChanges();

            foreach (var kk in appid)
            {
                var App = (from p in _contex.PatentApplication where p.Id == Convert.ToInt32(kk) select p).FirstOrDefault();

                App.CertificatePayReference = TransactionId;
               // App.RtNumber = Convert.ToString(getMaxRtNo() + 1);
                _contex.SaveChanges();
            }




            return details;
            // return null;
        }

        public async System.Threading.Tasks.Task<ApplicationUser> GetUserByApplicationId(int AppID)
        {

            var Application = await (from p in _contex.PatentApplication

                                 where p.Id == AppID

                                 select p).FirstOrDefaultAsync();

            int userid = Convert.ToInt32(Application.userid);


            var Result = await (from p in _contex.Users

                                     where p.Id == userid

                                select p).FirstOrDefaultAsync();



          




            return Result;
            // return null;
        }
        public async Task<List<PatentDataResult>> GetPatentPaidCertificate()
        {



            var details = _contex.PatentDataResult
            .FromSql($"GePatentPaidCertificate   @p0, @p1", parameters: new[] { DATASTATUS.Certificate, STATUS.Paid })
           .ToList();



            return details;
        }


        public async Task<List<PatentDataResult>> GetPatentConfirmedCertificate()
        {



            var details = _contex.PatentDataResult
            .FromSql($"GetPatentConfirmedCertificate   @p0, @p1", parameters: new[] { DATASTATUS.Certificate, STATUS.Confirm })
           .ToList();



            return details;
        }


        public async Task<List<PatentDataResult>> GetPatentCertificate(string userid)
        {



            var details = _contex.PatentDataResult
            .FromSql($"GetPatentCertificate   @p0, @p1, @p2", parameters: new[] { DATASTATUS.Acceptance, STATUS.Fresh, userid })
           .ToList();



            return details;
        }


        public async Task<List<PatentDataResult>> GetDelegateCertificate(string userid)
        {



            var details = _contex.PatentDataResult
            .FromSql($"GetPatentCertificateById   @p0, @p1, @p2 ", parameters: new[] { DATASTATUS.Examiner, STATUS.Delegate, userid })
           .ToList();



            return details;
        }

        public async Task<List<PatentDataResult>> GetPatentPayCertificate(string userid)
        {



            var details = _contex.PatentDataResult
            .FromSql($"GetPatentCertificate   @p0, @p1, @p2", parameters: new[] { DATASTATUS.Acceptance, STATUS.Approved, userid })
           .ToList();



            return details;
        }
    }
}
