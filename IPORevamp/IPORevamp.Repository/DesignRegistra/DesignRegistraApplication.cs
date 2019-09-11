using EmailEngine.Base.Entities;
using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.DelegateJob;
using IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.DesignInvention;
using IPORevamp.Data.Entity.Interface.Entities.DesignPriority;
using IPORevamp.Repository.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.DesignRegistra
{
    class DesignRegistraApplication : IDesignRegistraApplication
    {
        private readonly IPOContext _contex;
        private readonly Data.Entity.Interface.IEmailSender _emailsender;
        private readonly Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        protected readonly IConfiguration _configuration;

        public DesignRegistraApplication(IPOContext contex, IConfiguration configuration, IEmailTemplateRepository EmailTemplateRepository, IEmailSender emailsender, IFileHandler fileUploadRespository)
        {

            _contex = contex;
            _emailsender = emailsender;
            _EmailTemplateRepository = EmailTemplateRepository;
            _fileUploadRespository = fileUploadRespository;
            _configuration = configuration;

        }


        public async Task<List<DesignDataResult>> GetDesignPayCertificate(string userid)
        {



            var details = _contex.DesignDataResult
            .FromSql($"GetDesignCertificate   @p0, @p1, @p2", parameters: new[] { DATASTATUS.Acceptance, STATUS.Approved, userid })
           .ToList();



            return details;
        }
        public async Task<List<DesignDataResult>> GetDelegateCertificate(string userid)
        {



            var details = _contex.DesignDataResult
            .FromSql($"GetDesignCertificateById   @p0, @p1, @p2 ", parameters: new[] { DATASTATUS.Examiner, STATUS.Delegate, userid })
           .ToList();



            return details;
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
                var App = (from p in _contex.DesignApplication where p.Id == Convert.ToInt32(kk) select p).FirstOrDefault();

                App.CertificatePayReference = TransactionId;
                // App.RtNumber = Convert.ToString(getMaxRtNo() + 1);
                _contex.SaveChanges();
            }




            return details;
            // return null;
        }

        public async void SendEmailForCerticate(int appid, string comment)
        {

            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.CertificatePayment && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();
            // var roleid = Convert.ToInt32(IPORoles.Patent_Examiner);
            var vpwallet = (from c in _contex.DesignApplication where c.Id == appid select c).FirstOrDefault();
            var users = (from c in _contex.Users where c.Id == Convert.ToInt32(vpwallet.userid) select c).FirstOrDefault();
            // ApplicationUser[] currentUser = _contex.Users.FirstOrDefault(x => x.RolesId == roleid);


            var vname = users.FirstName + " " + users.LastName;

            string mailContent = emailtemplate.EmailBody;

            mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

            mailContent = mailContent.Replace("#comment", comment);

            mailContent = mailContent.Replace("#Name", vname);



            await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);






        }


        public async void DelegateExaminerEmail(string userid, int applicationid)
        {

            DelegateDesignJob delegates = new DelegateDesignJob();
            delegates.DesignApplicationID = applicationid;
            delegates.userid = userid;
            delegates.DateCreated = DateTime.Now;
            delegates.IsActive = true;
            delegates.IsDeleted = false;
            delegates.applicationstage = "design";
            _contex.Add(delegates);
            _contex.SaveChanges();






            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.DelegateEmail && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();

            var roleid = Convert.ToInt32(IPORoles.Design_Examiner);

            // var ApplicationUser = (from c in _contex.Users where c.RolesId == roleid select c).ToList();
            var ApplicationUser = _contex.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));


            var vname = ApplicationUser.FirstName + " " + ApplicationUser.LastName;

            string mailContent = emailtemplate.EmailBody;

            mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

            mailContent = mailContent.Replace("#Name", vname);

            await _emailsender.SendEmailAsync(ApplicationUser.Email, emailtemplate.EmailSubject, mailContent);






        }

        public async Task<List<DesignDataResult>> GetDesignFreshApplication()
        {



            var details = _contex.DesignDataResult
            .FromSql($"DesignFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Acceptance, STATUS.Fresh })
           .ToList();



            return details;
        }

    }
}
