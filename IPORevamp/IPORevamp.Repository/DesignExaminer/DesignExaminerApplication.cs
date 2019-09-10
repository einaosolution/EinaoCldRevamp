using System;
using System.Collections.Generic;
using System.Text;

using EmailEngine.Base.Entities;
using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.DesignInvention;
using IPORevamp.Data.Entity.Interface.Entities.DesignPriority;
using IPORevamp.Repository.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System.Linq;

using System.Threading.Tasks;

namespace IPORevamp.Repository.DesignExaminer
{
    class DesignExaminerApplication : IDesignExaminerApplication
    {

        private readonly IPOContext _contex;
        private readonly Data.Entity.Interface.IEmailSender _emailsender;
        private readonly Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        protected readonly IConfiguration _configuration;

        public DesignExaminerApplication(IPOContext contex, IConfiguration configuration, IEmailTemplateRepository EmailTemplateRepository, IEmailSender emailsender, IFileHandler fileUploadRespository)
        {

            _contex = contex;
            _emailsender = emailsender;
            _EmailTemplateRepository = EmailTemplateRepository;
            _fileUploadRespository = fileUploadRespository;
            _configuration = configuration;

        }


        public async Task<DesignApplicationHistory> GetRefusalComment(int id)
        {


            var Result = (from c in _contex.DesignApplicationHistory where c.DesignApplicationID == id && c.FromStatus == STATUS.Fresh && c.ToStatus == STATUS.Refused && c.ToDataStatus == DATASTATUS.Examiner select c).FirstOrDefault();


            return Result;
        }

        public async void SendRegistraEmail(int applicationId)
        {

            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.ApplicationAccepted && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();





            var roleid = Convert.ToInt32(IPORoles.Registrar);

            var ApplicationUser = (from c in _contex.Users where c.RolesId == roleid && c.department == DEPARTMENT.Design select c).ToList();
            var Result = (from c in _contex.DesignInformation where c.DesignApplicationID == applicationId select c).FirstOrDefault();
            // ApplicationUser[] currentUser = _contex.Users.FirstOrDefault(x => x.RolesId == roleid);

            foreach (var users in ApplicationUser)
            {
                var vname = users.FirstName + " " + users.LastName;

                string mailContent = emailtemplate.EmailBody;

                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                mailContent = mailContent.Replace("#Name", vname);
                mailContent = mailContent.Replace("#num", Result.RegistrationNumber);
                mailContent = mailContent.Replace("#num2", Result.TitleOfDesign);

                await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);



            }


        }

        public async System.Threading.Tasks.Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.PreviousComments>> GetPreviousComment(int id)
        {
            string applicationid = Convert.ToString(id);


            var details = _contex.PreviousComments
                .FromSql($"GetDesignPreviousComment   @p0", parameters: new[] { applicationid })
               .ToList();


            return details;
            // return null;
        }
        public async Task<List<DesignDataResult>> GetDesignFreshApplication()
        {



            var details = _contex.DesignDataResult
            .FromSql($"DesignFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Examiner, STATUS.Fresh })
           .ToList();



            return details;
        }
    }
}
