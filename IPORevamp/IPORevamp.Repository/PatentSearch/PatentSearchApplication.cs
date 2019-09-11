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
using IPORevamp.Data.Entity.Interface.Entities.DelegateJob;

namespace IPORevamp.Repository.PatentSearch
{
    class PatentSearchApplication : IPatentSearchApplication
    {
        private readonly IPOContext _contex;
        private IFileHandler _fileUploadRespository;
        protected readonly IConfiguration _configuration;
        private readonly IEmailSender _emailsender;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;



        public PatentSearchApplication(IPOContext contex , IFileHandler fileUploadRespository, IConfiguration configuration, IEmailTemplateRepository EmailTemplateRepository, IEmailSender emailsender)
        {
            
            _contex = contex;

            _fileUploadRespository = fileUploadRespository;
            _configuration = configuration;
            _emailsender = emailsender;
            _EmailTemplateRepository = EmailTemplateRepository;
 


        }



        public async Task<List<PatentDataResult>> GetSubmittedApplication()
        {
            var details = _contex.PatentDataResult
             .FromSql($"PatentApplicationSubmitted   @p0", parameters: new[] { STATUS.Pending })
            .ToList();



            return details;
        }

        public async void SendUserEmail(int  appid,string comment)
        {
           
            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.SentToKiv && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();
            // var roleid = Convert.ToInt32(IPORoles.Patent_Examiner);
            var vpwallet = (from c in _contex.PatentApplication where c.Id == appid select c).FirstOrDefault();
            var users = (from c in _contex.Users where c.Id == Convert.ToInt32(vpwallet.userid)  select c).FirstOrDefault();
            // ApplicationUser[] currentUser = _contex.Users.FirstOrDefault(x => x.RolesId == roleid);

          
                var vname = users.FirstName + " " + users.LastName;

                string mailContent = emailtemplate.EmailBody;

                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                mailContent = mailContent.Replace("#comment", comment);

            mailContent = mailContent.Replace("#Name", vname);



            await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);



           


        }


        public async void SendEmailForCerticate(int appid, string comment)
        {

            EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.CertificatePayment && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();
            // var roleid = Convert.ToInt32(IPORoles.Patent_Examiner);
            var vpwallet = (from c in _contex.PatentApplication where c.Id == appid select c).FirstOrDefault();
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

           
             EmailTemplate emailtemplate =  (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.SendPatentExaminerEmail && c.IsActive == true && c.IsDeleted == false  select c).FirstOrDefault();
            
            var roleid = Convert.ToInt32(IPORoles.Patent_Examiner);

            var ApplicationUser =  (from c in _contex.Users where c.RolesId == roleid select c).ToList();
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


        public async void DelegateExaminerEmail(string userid ,int applicationid)
        {

            DelegateJob delegates = new DelegateJob();
            delegates.PatentApplicationID = applicationid;
            delegates.userid = userid;
            delegates.DateCreated = DateTime.Now;
            delegates.IsActive = true;
            delegates.IsDeleted = false;
            delegates.applicationstage = "patent";
            _contex.Add(delegates);
            _contex.SaveChanges();






          EmailTemplate emailtemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.DelegateEmail && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();

            var roleid = Convert.ToInt32(IPORoles.Patent_Examiner);

            // var ApplicationUser = (from c in _contex.Users where c.RolesId == roleid select c).ToList();
            var  ApplicationUser = _contex.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));

           
                var vname = ApplicationUser.FirstName + " " + ApplicationUser.LastName;

                string mailContent = emailtemplate.EmailBody;

                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                mailContent = mailContent.Replace("#Name", vname);

                await _emailsender.SendEmailAsync(ApplicationUser.Email, emailtemplate.EmailSubject, mailContent);



          


        }



        public async void SaveApplicationHistory(int id, string userrole, HttpRequest request, string tostatus, string toDatastatus, string fromDatastatus, string fromstatus, string comment, string description, string userid,string filepath)
        {

            var vpwallet = (from c in _contex.PatentApplication where c.Id == id select c).FirstOrDefault();

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
      


           

                _contex.Add(new PatentApplicationHistory
                {
                    PatentApplicationID = id,
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


        public async void SaveApplicationStateHistory(int id, string userrole, HttpRequest request, string tostatus, string toDatastatus, string fromDatastatus, string fromstatus, string comment, string description, string userid, string filepath)
        {

            var vpwallet = (from c in _contex.PatentApplication where c.Id == id select c).FirstOrDefault();

            string transactionid = vpwallet.TransactionID;
          



         



            // file upload





            _contex.Add(new PatentApplicationHistory
            {
                PatentApplicationID = id,
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
        public async Task<List<PatentDataResult>> GetPatentSearchKiv()
        {



            var details = _contex.PatentDataResult
            .FromSql($"GetPatentExminerKiv   @p0, @p1", parameters: new[] { DATASTATUS.Search, STATUS.Kiv })
           .ToList();



            return details;
        }

        public async Task<List<PatentDataResult>> GetPatentFreshApplication()
        {
          


            var details = _contex.PatentDataResult
            .FromSql($"PatentFreshApplication   @p0, @p1", parameters: new[] { DATASTATUS.Search, STATUS.Fresh })
           .ToList();



            return details;
        }


        public async Task<List<PatentDataResult>> GetPatentApplicationByUserid(string userid)
        {



            var details = _contex.PatentDataResult
            .FromSql($"GetPatentApplicationByUserid   @p0", parameters: new[] { userid })
           .ToList();



            return details;
        }


        public async Task<List<PatentApplicationHistory>> GetSearchState(int id,int  userid)
        {


            var patentsearchstater = (from c in _contex.PatentApplicationHistory where c.PatentApplicationID == id  && c.userid == userid && c.ToStatus == STATUS.SaveMode select c).ToList();


            return patentsearchstater;
        }

        public async Task<List<PatentInvention>> GetInventorById(int id )
        {


            var patentinventor = (from c in _contex.PatentInvention where c.PatentApplicationID == id select c).Include(a => a.Country).ToList();


            return patentinventor;
        }


        public async Task<List<AddressOfService>> GetAddressOfServiceById(int id)
        {


            var AddressOfService = (from c in _contex.AddressOfService where c.PatentApplicationID == id select c).Include(a => a.State).ToList();


            return AddressOfService;
        }



        public async Task<List<PatentPriorityInformation>> GetPriorityById(int id)
        {


            var patentinventor = (from c in _contex.PatentPriorityInformation where c.PatentApplicationID == id select c).Include(a => a.Country).ToList();


            return patentinventor;
        }


    }
}
