using AutoMapper;
using EmailEngine.Base.Entities;
using EmailEngine.Base.Repository.EmailRepository;
using EmailEngine.Repository.EmailRepository;

using IPORevamp.Core.Utilities;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.MarkInfo;
using IPORevamp.Data.Entity.Interface.Entities.Pwallet;
using IPORevamp.Data.Entity.Interface.PreliminarySearch;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.FileNewApplication;
using IPORevamp.Repository.PreliminarySearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data;
using System.IO;

using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using IPORevamp.Repository.DesignNewpplication;
using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.DesignAssigment;
using IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignInformation;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;
using IPORevamp.Repository.DesignCertificate;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/DesignCertificate")]
    [ApiController]
    public class DesignCertificateController : BaseController
    {
        private readonly IDesignCertificateApplication _designCertificateRepository;
        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly Repository.Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        private readonly IPOContext _contex;
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public DesignCertificateController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
    IPOContext contex,
    IEmailManager<EmailLog, EmailTemplate> emailManager,
      Repository.Email.IEmailTemplateRepository EmailTemplateRepository,
      IDesignCertificateApplication designCertificateRepository,
     




    IEmailSender emailsender,
    IHttpContextAccessor httpContextAccessor,
    IFileHandler fileUploadRespository,

    IAuditTrailManager<AuditTrail> auditTrailManager




    ) : base(
        userManager,
        signInManager,
        roleManager,
        configuration,
        mapper,
        logger,
        auditTrailManager

        )
        {
            _emailManager = emailManager;

            _emailsender = emailsender;
            //  _httpContextAccessor = httpContextAccessor;
            //_patentTypeRepository = patentTypeRepository;

            _contex = contex;
            _designCertificateRepository = designCertificateRepository;
            _fileUploadRespository = fileUploadRespository;
            _EmailTemplateRepository = EmailTemplateRepository;


        }


        [HttpGet("SendUserEmail")]
        public async Task<IActionResult> SendUserEmail([FromQuery] string RequestById, [FromQuery] string applicationId)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                EmailTemplate emailTemplate;


                emailTemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.PatentCertificateCollection && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;

                var result = (from p in _contex.DesignApplication where p.Id == Convert.ToInt32(applicationId) select p).FirstOrDefault();

                var users = (from p in _contex.Users where p.Id == Convert.ToInt32(result.userid) select p).FirstOrDefault();
                var user2 = await _userManager.FindByIdAsync(Convert.ToString(users.Id)); ;

                //  var message = "Dear " + user2.FirstName + " " + user2.LastName + ",your application in examination unit hs been treated and sent to your kiv folder ,find below the comment from the officer . <br/> <br/>" + Comment;

                string mailContent = emailTemplate.EmailBody;

                var vname = user2.FirstName + " " + user2.LastName;
                mailContent = mailContent.Replace("#Name", vname);

                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);


                await _emailsender.SendEmailAsync(user2.Email, emailTemplate.EmailSubject, mailContent);




                // get User Information
                //  user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  sent user email   successfully",
                    Entity = "Certificate",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Certificate Returned Successfully", false, user2);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Certificate", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("GetFreshApplication")]
        public async Task<IActionResult> GetFreshApplication([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _designCertificateRepository.GetDesignFreshApplication();



            if (details != null)
            {

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all fresh design successfully",
                    Entity = "GetAllFreshDesign",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Fresh Design Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }


        [HttpGet("GetConfirmCertificate")]
        public async Task<IActionResult> GetConfirmCertificate([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _designCertificateRepository.GetDesignConfirmCertificate();



            if (details != null)
            {

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all fresh design successfully",
                    Entity = "GetAllConfirmDesign",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Confirm Design Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }

        [HttpGet("GetApplicationById")]
        public async Task<IActionResult> GetApplicationById([FromQuery] string RequestById , [FromQuery] string ApplicationID)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _designCertificateRepository.GetApplicationByRegistrationId(ApplicationID);



            if (details != null)
            {

                // get User Information
               

                return PrepareResponse(HttpStatusCode.OK, "Design By Id Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }


        [HttpGet("GetPatentApplicationById")]
        public async Task<IActionResult> GetPatentApplicationById([FromQuery] string RequestById, [FromQuery] string ApplicationID)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _designCertificateRepository.GetPatentApplicationByRegistrationId(ApplicationID);



            if (details != null)
            {

                // get User Information


                return PrepareResponse(HttpStatusCode.OK, "Design By Id Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }


        [HttpGet("GetPatentApplicationById2")]
        public async Task<IActionResult> GetPatentApplicationById2([FromQuery] string RequestById, [FromQuery] string ApplicationID)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _designCertificateRepository.GetPatentApplicationById(Convert.ToInt32(ApplicationID));



            if (details != null)
            {

                // get User Information


                return PrepareResponse(HttpStatusCode.OK, "Design By Id Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }

        [HttpGet("GetApplicationByAppid")]
        public async Task<IActionResult> GetApplicationByAppid([FromQuery] string RequestById, [FromQuery] string ApplicationID)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _designCertificateRepository.GetApplicationByAppId(ApplicationID);



            if (details != null)
            {

                // get User Information


                return PrepareResponse(HttpStatusCode.OK, "Design By Id Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }




        [HttpGet("GetDateFormat")]
        public async Task<IActionResult> GetDateFormat([FromQuery] string ApplicationId)
        {
            string ip = "";

            ip = Request.Headers["ip"];

            //var result = (from p in _contex.DesignApplication where p.Id == Convert.ToInt32(ApplicationId) select p).FirstOrDefault();
            var result2 = (from p in _contex.DesignApplicationHistory where p.DesignApplicationID == Convert.ToInt32(ApplicationId)  && p.ToStatus ==STATUS.Confirm && p.ToDataStatus == DATASTATUS.Certificate select p).FirstOrDefault();
            DateTime dd2 = result2.DateCreated;
            DesignDateFormat DesignDate = new DesignDateFormat();
            DesignDate.registrationdate = dd2.Day.ToString() + getDayFormat(dd2.Day.ToString()) + " " +   dd2.ToString("MMM") + " " + dd2.Year.ToString(); ;

            DateTime Todaydate = DateTime.Now;
            DateTime Expires = result2.DateCreated.AddYears(5);
            DesignDate.todaysdate = Todaydate.Day.ToString() + getDayFormat(Todaydate.Day.ToString()) + " " + Todaydate.ToString("MMM") + " " + Todaydate.Year.ToString();
            DesignDate.expiredate = Expires.Day.ToString() + getDayFormat(Expires.Day.ToString()) + " " + Expires.ToString("MMM") + " " + Expires.Year.ToString();

          



         

                // Added A New Country 
            

                return PrepareResponse(HttpStatusCode.OK, "Confirm  Returned Successfully", false, DesignDate);

           









        }

        [HttpGet("ApplicationCount")]
        public async Task<IActionResult> ApplicationCount([FromQuery] string ApplicationId)
        {
            string ip = "";

            ip = Request.Headers["ip"];

            //var result = (from p in _contex.DesignApplication where p.Id == Convert.ToInt32(ApplicationId) select p).FirstOrDefault();
            var result2 = (from p in _contex.DesignApplication join c in _contex.DesignInformation on p.Id equals c.DesignApplicationID join d in _contex.DesignApplicationHistory on p.Id equals d.DesignApplicationID where c.RegistrationNumber == ApplicationId && d.ToStatus == STATUS.Paid && d.ToDataStatus == DATASTATUS.Certificate select p).Count();
           




            // Added A New Country 


            return PrepareResponse(HttpStatusCode.OK, "Confirm  Returned Successfully", false, result2);











        }


        protected string getDayFormat(string day)
        {
            //string new_day = "";
            //if(day.EndsWith("1")){ new_day="st";}
            //else if (day.EndsWith("2")) { new_day = "nd"; }
            //else if (day.EndsWith("3")) { new_day = "rd"; }
            //else  { new_day = "th"; }
            //return new_day;


            switch (day)
            {
                case "1":
                case "21":
                case "31":
                    return "st";
                case "2":
                case "22":
                    return "nd";
                case "3":
                case "23":
                    return "rd";
                default:
                    return "th";
            }
        }
    }
}
