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
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using IPORevamp.Repository.PatentType;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;
using IPORevamp.Repository.PatentSearch;
using IPORevamp.Repository.PatentExaminer;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/PatentExaminer")]
    [ApiController]
    public class PatentExaminerController : BaseController
    {
        
        private readonly IPatentExaminerApplication _patentExaminerRepository;

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly Repository.Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        private readonly IPOContext _contex;
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PatentExaminerController(
       UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
      SignInManager<ApplicationUser> signInManager,
      IConfiguration configuration,
      IMapper mapper, ILogger<UserController> logger,
      IPOContext contex,
      IEmailManager<EmailLog, EmailTemplate> emailManager,
      Repository.Email.IEmailTemplateRepository EmailTemplateRepository,
     



IPatentExaminerApplication PatentExaminerRepository,


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
            _httpContextAccessor = httpContextAccessor;


            _contex = contex;
            //  _newApplicationRepository = newApplicationRepository;
            _patentExaminerRepository = PatentExaminerRepository;

            _fileUploadRespository = fileUploadRespository;
            _EmailTemplateRepository = EmailTemplateRepository;


        }

      





        [HttpGet("SendRegistraAppealEmail")]
        public async Task<IActionResult> SendRegistraAppealEmail([FromQuery] string RequestById, [FromQuery] string Appid)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var App = (from p in _contex.Application where p.Id == Convert.ToInt32(Appid) select p).FirstOrDefault();
                var roleid = Convert.ToInt32(IPORoles.RegistrarPatent);
                var departmentid = DEPARTMENT.Patent;
                var user2 = _userManager.Users.Where(x => x.RolesId == roleid && x.department == departmentid).ToList();
                EmailTemplate emailTemplate;
                emailTemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.AppealReply);

                foreach (var users in user2)
                {

                    string mailContent = emailTemplate.EmailBody;

                    var vname = users.FirstName + " " + users.LastName;
                    mailContent = mailContent.Replace("#Name", vname);
                    mailContent = mailContent.Replace("#transid", App.TransactionID);
                    mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                    await _emailsender.SendEmailAsync(users.Email, "User Replies Appeal", mailContent);

                }

                //   var result = await _publicationRepository.GetFreshApplication();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Publication  Fresh Application   successfully",
                    Entity = "User",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, "Success");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sent Email", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("SendExaminerEmail")]
        public async Task<IActionResult> SendExaminerEmail([FromQuery] string RequestById)
        {
            string ip = "";


            try
            {
                ip = Request.Headers["ip"];
                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                _patentExaminerRepository.SendExaminerEmail();



                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                try
                {
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all SendExaminerEmail  successfully",
                        Entity = "SendExaminerEmail",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                }
                catch (Exception ee)
                {

                }

                return PrepareResponse(HttpStatusCode.OK, "PatentInventor Returned Successfully", false, "success");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select PatentPriority", "");
                return PrepareResponse(HttpStatusCode.OK, "Mail Not Sent", false, "");
            }
        }

        [HttpGet("GetPreviousComment")]
        public async Task<IActionResult> GetPreviousComment([FromQuery] string RequestById,  [FromQuery] string ID)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var result = await _patentExaminerRepository.GetPreviousComment(Convert.ToInt32(ID));


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Examiner  Fresh Application   successfully",
                    Entity = "GetComment",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("GetRefusalComment")]
        public async Task<IActionResult> GetRefusalComment([FromQuery] string RequestById, [FromQuery] string ID)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var result = await _patentExaminerRepository.GetRefusalComment(Convert.ToInt32(ID));


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Examiner  Fresh Application   successfully",
                    Entity = "GetComment",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("SendRegistraEmail")]
        public async Task<IActionResult> SendRegistraEmail([FromQuery] string RequestById, [FromQuery] string ID)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                _patentExaminerRepository.SendRegistraEmail(Convert.ToInt32(ID));


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  send email   successful",
                    Entity = "Email",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Send Registra Email", false, "success");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Send Registra Email ", "");
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



            var details = _patentExaminerRepository.GetPatentFreshApplication();



            if (details != null)
            {

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all fresh patent successfully",
                    Entity = "GetAllFreshPatent",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Fresh Patent Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }


        [HttpGet("GetRefuseApplicationByUserid")]
        public async Task<IActionResult> GetRefuseApplicationByUserid([FromQuery] string RequestById)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var result = await _patentExaminerRepository.GetRefuseApplicationByUserid(RequestById);


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Patent Refuse Application   successfully",
                    Entity = "GetRefuseAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Refuse Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetPatentExaminerKiv")]
        public async Task<IActionResult> GetPatentExaminerKiv([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _patentExaminerRepository.GetPatentExaminerKiv();



            if (details != null)
            {

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Kiv patent successfully",
                    Entity = "GetAllPatentKiv",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Fresh Patent Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }


        [HttpGet("GetPatentAppealUnit")]
        public async Task<IActionResult> GetPatentAppealUnit([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _patentExaminerRepository.GetPatentAppealUnit();



            if (details != null)
            {

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all  patent appeal successfully",
                    Entity = "GetAllPatentAppeal",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Patent Appeal Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }
        [HttpGet("GetPatentAppeal")]
        public async Task<IActionResult> GetPatentAppeal([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _patentExaminerRepository.GetPatentAppeal();



            if (details != null)
            {

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all  patent successfully",
                    Entity = "GetAllPatentAppeal",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Appeal Patent Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }

        [HttpGet("GetApplicationTransactionAmount")]
        public async Task<IActionResult> GetApplicationTransactionAmountl([FromQuery] string RequestById , [FromQuery] string TransactionId)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _patentExaminerRepository.SendApplicationAmount(TransactionId);



            if (details != null)
            {

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all  patent successfully",
                    Entity = "GetAllPatentAppeal",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Appeal Patent Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }


        [HttpGet("GetPatentTreatedAppeal")]
        public async Task<IActionResult> GetPatentTreatedAppeal([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _patentExaminerRepository.GetPatentTreatedAppeal();



            if (details != null)
            {

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all  patent successfully",
                    Entity = "GetAllPatentAppeal",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Appeal Patent Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }

        [HttpGet("GetPatentExaminerReconductSearch")]
        public async Task<IActionResult> GetPatentExaminerReconductSearch([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _patentExaminerRepository.GetPatentExaminerReconductSearch();



            if (details != null)
            {

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Kiv patent successfully",
                    Entity = "GetAllPatentKiv",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Fresh Patent Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }


        [HttpGet("MailToReconductSearch")]
        public async Task<IActionResult> MailToReconductSearch([FromQuery] string RequestById, [FromQuery] string userid, [FromQuery] string Comment)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];
                EmailTemplate emailTemplate;

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                var user2 = await _userManager.FindByIdAsync(userid); ;
                emailTemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.ApplicationSentToOpposition);

                //  var message = "Dear " + user2.FirstName + " " + user2.LastName + ",Application has been sent to   Reconduct Search folder , Please login to treat the application  . ";
                string mailContent = emailTemplate.EmailBody;
                //  mailContent = mailContent.Replace("#Name", user2.FirstName + " " + user2.LastName);

                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                if (user2 != null)
                {
                    var roleid = Convert.ToInt32(IPORoles.Search_Officer_Patent);
                    var user3 = _userManager.Users.Where(x => x.RolesId == roleid).ToList();

                    foreach (var users in user3)
                    {
                        mailContent = mailContent.Replace("#Name", users.FirstName + " " + users.LastName);
                        await _emailsender.SendEmailAsync(users.Email, "Application Sent to Reconduct Search", mailContent);


                    }




                    // get User Information
                    //  user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  sent user email   successfully",
                        Entity = "Examiner",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Examiner Returned Successfully", false, user2);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Exminer", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

    }
}
