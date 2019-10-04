using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Data;
using System.Linq;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.MarkInfo;
using IPORevamp.Data.Entity.Interface.Entities.Pwallet;
using IPORevamp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using EmailEngine.Base.Entities;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;
using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;
using EmailEngine.Repository.FileUploadRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplicationHistory;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Repository.Email;
using IPORevamp.Data.Entities.Email;

namespace IPORevamp.Repository.PatentExaminer
{
  public   class PatentExaminerApplication : IPatentExaminerApplication
    {
        private readonly IPOContext _contex;
        private IFileHandler _fileUploadRespository;
        protected readonly IConfiguration _configuration;
        private readonly IEmailSender _emailsender;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;

        public PatentExaminerApplication(IPOContext contex, IFileHandler fileUploadRespository, IConfiguration configuration, IEmailTemplateRepository EmailTemplateRepository, IEmailSender emailsender)
        {

            _contex = contex;

            _fileUploadRespository = fileUploadRespository;
            _configuration = configuration;
            _emailsender = emailsender;
            _EmailTemplateRepository = EmailTemplateRepository;



        }



        public async Task<String> SendApplicationAmount(string transid)
        {



            var Transval = (from c in _contex.RemitaPayments where c.OrderId == transid select c).FirstOrDefault();
            // ApplicationUser[] currentUser = _contex.Users.FirstOrDefault(x => x.RolesId == roleid);

            if (Transval != null)
            {
                return Transval.TotalAmount;
            }

            else
            {
                return "0";
            }
           


         


        }

        public async void SendExaminerEmail()
        {


            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.SendPatentExaminerEmail && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();

            var roleid = Convert.ToInt32(IPORoles.Patent_Examiner);

            var ApplicationUser = (from c in _contex.Users where c.RolesId == roleid select c).ToList();
            // ApplicationUser[] currentUser = _contex.Users.FirstOrDefault(x => x.RolesId == roleid);

            foreach (var users in ApplicationUser)
            {
                try
                {
                    var vname = users.FirstName + " " + users.LastName;

                    string mailContent = emailtemplate.EmailBody;

                    mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                    mailContent = mailContent.Replace("#Name", vname);

                    await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);

                }

                catch(Exception ee)
                {
                    var err = ee.Message;
                }



            }


        }

        public async void SendRegistraEmail(int applicationId)
        {

            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.ApplicationAccepted && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();


            


            var roleid = Convert.ToInt32(IPORoles.RegistrarPatent);

            var ApplicationUser = (from c in _contex.Users where c.RolesId == roleid && c.department ==DEPARTMENT.Patent select c).ToList();
            var Result = (from c in _contex.PatentInformation where c.PatentApplicationID == applicationId select c).FirstOrDefault();
            // ApplicationUser[] currentUser = _contex.Users.FirstOrDefault(x => x.RolesId == roleid);

            foreach (var users in ApplicationUser)
            {
                var vname = users.FirstName + " " + users.LastName;

                string mailContent = emailtemplate.EmailBody;

                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                mailContent = mailContent.Replace("#Name", vname);
                mailContent = mailContent.Replace("#num", Result.RegistrationNumber);
                mailContent = mailContent.Replace("#num2", Result.TitleOfInvention);

                await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);



            }


        }

        public async Task<PatentApplicationHistory> GetRefusalComment(int id)
        {


            var Result = (from c in _contex.PatentApplicationHistory where c.PatentApplicationID == id && c.FromStatus ==STATUS.Fresh && c.ToStatus == STATUS.Refused && c.ToDataStatus==DATASTATUS.Examiner select c).FirstOrDefault();


            return Result;
        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.PreviousComments>> GetPreviousComment(int id)
        {
            string applicationid = Convert.ToString(id);


            var details = _contex.PreviousComments
                .FromSql($"GetPatentPreviousComment   @p0", parameters: new[] { applicationid })
               .ToList();


            return details;
            // return null;
        }

        public async Task<List<PatentDataResult>> GetPatentFreshApplication()
        {



            var details = _contex.PatentDataResult
            .FromSql($"PatentFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Examiner, STATUS.Fresh })
           .ToList();



            return details;
        }

        public int GetPatentAppealCount()
        {
            var model = (from c in _contex.PatentApplication
                         join p in _contex.PatentApplicationHistory on new { a = c.Id } equals new { a = p.PatentApplicationID }


                         where c.ApplicationStatus == STATUS.Registra && c.DataStatus == DATASTATUS.Examiner && p.ToStatus == STATUS.Registra && p.FromStatus == STATUS.Refused
                         select c).Count();


            return model;
        }



        public async Task<List<PatentDataResult>> GetPatentAppeal()
        {



            var details = _contex.PatentDataResult
            .FromSql($"GetPatentAppeal   @p0, @p1, @p2", parameters: new[] { DATASTATUS.Examiner, STATUS.Registra,  STATUS.Refused })
           .ToList();



            return details;
        }

        public int GetPatentTreatedAppealCount()
        {
            var model = (from c in _contex.PatentApplication
                         join p in _contex.PatentApplicationHistory on new { a = c.Id } equals new { a = p.PatentApplicationID }


                         where c.ApplicationStatus == STATUS.Registra && c.DataStatus == DATASTATUS.Examiner && p.ToStatus == STATUS.ReceiveAppeal && p.FromStatus == STATUS.Appeal
                         select c).Count();


            return model;
        }
        public async Task<List<PatentDataResult>> GetPatentTreatedAppeal()
        {



            var details = _contex.PatentDataResult
            .FromSql($"GetPatentTreatedAppeal   @p0, @p1 , @p2", parameters: new[] { DATASTATUS.Examiner, STATUS.ReceiveAppeal, STATUS.Appeal })
           .ToList();



            return details;
        }



        public async System.Threading.Tasks.Task<List<PatentDataResult>> GetRefuseApplicationByUserid(string userid)
        {
            //  var BatchCount = (from p in _contex.PublicationBatch select p).Count() + 1;
            var details = _contex.PatentDataResult
            .FromSql($"GetPatentRefuseApplicationById    @p0, @p1 ", parameters: new[] { STATUS.Refused, userid })
           .ToList();

            return details;
            // return null;
        }

        public async Task<List<PatentDataResult>> GetPatentExaminerKiv()
        {



            var details = _contex.PatentDataResult
            .FromSql($"GetPatentExminerKiv   @p0, @p1", parameters: new[] { DATASTATUS.ApplicantKiv, STATUS.ApplicantKiv })
           .ToList();



            return details;
        }


        public async Task<List<PatentDataResult>> GetPatentAppealUnit()
        {



            var details = _contex.PatentDataResult
            .FromSql($"GetPatentAppealUnit   @p0, @p1", parameters: new[] { DATASTATUS.Examiner, STATUS.Appeal })
           .ToList();



            return details;
        }
        public async Task<List<PatentDataResult>> GetPatentExaminerReconductSearch()
        {



            var details = _contex.PatentDataResult
            .FromSql($"PatentReconductSearch   @p0, @p1", parameters: new[] { DATASTATUS.ReconductSearch, STATUS.ReconductSearch })
           .ToList();



            return details;
        }
    }
}
