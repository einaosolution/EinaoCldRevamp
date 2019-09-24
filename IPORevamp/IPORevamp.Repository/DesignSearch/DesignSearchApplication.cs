using EmailEngine.Base.Entities;
using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;
using IPORevamp.Data.Entity.Interface.Entities.DesignCoApplicant;
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

namespace IPORevamp.Repository.DesignSearch
{
    class DesignSearchApplication: IDesignSearchApplication
    {
        private readonly IPOContext _contex;
        private readonly Data.Entity.Interface.IEmailSender _emailsender;
        private readonly Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        protected readonly IConfiguration _configuration;

        public DesignSearchApplication(IPOContext contex , IConfiguration configuration, IEmailTemplateRepository EmailTemplateRepository, IEmailSender emailsender , IFileHandler fileUploadRespository)
        {

            _contex = contex;
            _emailsender = emailsender;
            _EmailTemplateRepository = EmailTemplateRepository;
            _fileUploadRespository = fileUploadRespository;
            _configuration = configuration;

        }

        public async void SaveApplicationStateHistory(int id, string userrole, HttpRequest request, string tostatus, string toDatastatus, string fromDatastatus, string fromstatus, string comment, string description, string userid, string filepath)
        {

            var vpwallet = (from c in _contex.DesignApplication where c.Id == id select c).FirstOrDefault();

            string transactionid = vpwallet.TransactionID;








            // file upload





            _contex.Add(new DesignApplicationHistory
            {
                DesignApplicationID = id,
                DateCreated = DateTime.Now,
                TransactionID = transactionid,
                FromDataStatus = toDatastatus,
                patentcomment = comment,
                description = description,

                ToDataStatus = toDatastatus,
                FromStatus = fromstatus,
                ToStatus = tostatus,
                UploadsPath1 = filepath,
                userid = Convert.ToInt32(userid),
                Role = userrole
            });



            _contex.SaveChanges();






            // return null;
        }

        public async void SendUserEmail(int appid, string comment)
        {

            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.SentToKiv && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();
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

        public async void SendExaminerEmail()
        {


            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.SendPatentExaminerEmail && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();

            var roleid = Convert.ToInt32(IPORoles.Design_Examiner);

            var ApplicationUser = (from c in _contex.Users where c.RolesId == roleid select c).ToList();
            // ApplicationUser[] currentUser = _contex.Users.FirstOrDefault(x => x.RolesId == roleid);

            foreach (var users in ApplicationUser)
            {
                var vname = users.FirstName + " " + users.LastName;

                string mailContent = emailtemplate.EmailBody;

                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                mailContent = mailContent.Replace("#Name", vname);

                await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);



            }


        }

        public async void SaveApplicationHistory(int id, string userrole, HttpRequest request, string tostatus, string toDatastatus, string fromDatastatus, string fromstatus, string comment, string description, string userid, string filepath)
        {

            var vpwallet = (from c in _contex.DesignApplication where c.Id == id select c).FirstOrDefault();

            string transactionid = vpwallet.TransactionID;
            string prevappstatus = vpwallet.ApplicationStatus;
            string prevDatastatus = vpwallet.DataStatus;



            if (vpwallet != null)
            {

                vpwallet.ApplicationStatus = tostatus;
                vpwallet.DataStatus = toDatastatus;


                _contex.SaveChanges();
                // get User Information




            }



            // file upload





            _contex.Add(new DesignApplicationHistory
            {
                DesignApplicationID = id,
                DateCreated = DateTime.Now,
                TransactionID = transactionid,
                FromDataStatus = prevDatastatus,
                patentcomment = comment,
                description = description,

                ToDataStatus = toDatastatus,
                FromStatus = prevappstatus,
                ToStatus = tostatus,
                UploadsPath1 = filepath,
                userid = Convert.ToInt32(userid),
                Role = userrole
            });



            _contex.SaveChanges();






            // return null;
        }

        public async Task<List<DesignPriority>> GetPriorityById(int id)
        {


            var patentinventor = (from c in _contex.DesignPriority where c.DesignApplicationID == id select c).Include(a => a.Country).ToList();


            return patentinventor;
        }
        public async Task<List<DesignInvention>> GetInventorById(int id)
        {


            var patentinventor = (from c in _contex.DesignInvention where c.DesignApplicationID == id select c).Include(a => a.Country).ToList();


            return patentinventor;
        }


        public async Task<List<DesignCoApplicant>> GetCoApplicantById(int id)
        {


            var DesignCoApp = (from c in _contex.DesignCoApplicant where c.DesignApplicationID == id select c).ToList();


            return DesignCoApp;
        }


        public async Task<List<DesignApplicationHistory>> GetSearchState(int id, int userid)
        {


            var patentsearchstater = (from c in _contex.DesignApplicationHistory where c.DesignApplicationID == id && c.userid == userid && c.ToStatus == STATUS.SaveMode select c).ToList();


            return patentsearchstater;
        }

        public async Task<List<DesignAddressOfService>> GetAddressOfServiceById(int id)
        {


            var AddressOfService = (from c in _contex.DesignAddressOfService where c.DesignApplicationID == id select c).Include(a => a.State).ToList();


            return AddressOfService;
        }
        public async Task<List<DesignDataResult>> GetDesignFreshApplication()
        {



            var details = _contex.DesignDataResult
            .FromSql($"DesignFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Search, STATUS.Fresh })
           .ToList();



            return details;
        }


        public async Task<List<DesignDataResult>> GetDesignReconductSearch()
        {



            var details = _contex.DesignDataResult
            .FromSql($"DesignFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.ReconductSearch, STATUS.ReconductSearch })
           .ToList();



            return details;
        }

        public async Task<List<DesignDataResult>> GetDesignKivSearch()
        {



            var details = _contex.DesignDataResult
            .FromSql($"DesignFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Search, STATUS.Kiv })
           .ToList();



            return details;
        }


        public async Task<List<DesignDataResult>> GetDesignListing(string userid)
        {



            var details = _contex.DesignDataResult
            .FromSql($"GetDesignListing   @p0", parameters: new[] { userid })
           .ToList();



            return details;
        }


    }
}
