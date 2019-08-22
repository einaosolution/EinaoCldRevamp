using IPORevamp.Repository.Examiner;
using AutoMapper;
using EmailEngine.Base.Entities;
using EmailEngine.Base.Repository.EmailRepository;
using EmailEngine.Repository.EmailRepository;
using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Core.Utilities;
using IPORevamp.Data;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using IPORevamp.Data.Entity.Interface.PreliminarySearch;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.PreliminarySearch;
using IPORevamp.Repository.Search_Unit;
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
using IPORevamp.Repository.Email;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Examiner")]
    [ApiController]
    public class ExaminerController : BaseController
    {
        private readonly IExaminerRepository _examinerRepository;
        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly IPOContext _contex;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;


        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IFileHandler _fileUploadRespository;


        public ExaminerController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,
   
    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
      IEmailTemplateRepository EmailTemplateRepository,
    IEmailManager<EmailLog, EmailTemplate> emailManager,
     IFileHandler fileUploadRespository,

   IExaminerRepository examinerRepository,
  // IPOContext contex ,

    IEmailSender emailsender,
    IHttpContextAccessor httpContextAccessor,

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
            

         //   _contex = contex;

            _httpContextAccessor = httpContextAccessor;
            _examinerRepository = examinerRepository;
            _fileUploadRespository = fileUploadRespository;
            _EmailTemplateRepository = EmailTemplateRepository;


        }


        [HttpGet("GetFreshApplication")]
        public async Task<IActionResult> GetFreshApplication([FromQuery] string RequestById)
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


                var result = await _examinerRepository.GetFreshApplication();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Examiner  Fresh Application   successfully",
                    Entity = "GetFreshAppliction",
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

        [HttpGet("GetTreatedApplication")]
        public async Task<IActionResult> GetTreatedApplication([FromQuery] string RequestById)
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


                var result = await _examinerRepository.GetTreatedApplication();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Examiner  Fresh Application   successfully",
                    Entity = "GetFreshAppliction",
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

        [HttpGet("GetApplicationByUserid")]
        public async Task<IActionResult> GetApplicationByUserid([FromQuery] string RequestById)
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


                var result = await _examinerRepository.GetApplicationByUserid(RequestById);


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all  Application By Userid   successfully",
                    Entity = "GetAppliction",
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

        [HttpGet("GetUserKivApplication")]
        public async Task<IActionResult> GetUserKivApplication([FromQuery] string RequestById)
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


                var result = await _examinerRepository.GetUserKiv();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all User Kiv Application   successfully",
                    Entity = "GetUserKivAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Kiv  Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetExaminerReconductSearch")]
        public async Task<IActionResult> GetExaminerReconductSearch([FromQuery] string RequestById)
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


                var result = await _examinerRepository.GetExaminerReconductSearch();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all User Reconduct Application   successfully",
                    Entity = "GetUserReconductAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Reconduct  Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetExaminerKivApplication")]
        public async Task<IActionResult> GetExaminerKivApplication([FromQuery] string RequestById)
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


                var result = await _examinerRepository.GetExaminerKiv();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all User Kiv Application   successfully",
                    Entity = "GetUserKivAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Kiv  Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetRefuseApplicationById")]
        public async Task<IActionResult> GetRefuseApplicationById([FromQuery] string RequestById , [FromQuery] string ApplicationId)
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


                var result = await _examinerRepository.GetApplicationHistoryById(Convert.ToInt32(ApplicationId));


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for Refused  Application By Id   successfully",
                    Entity = "GetFreshAppliction",
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


        [HttpGet("SendUserEmail")]
        public async Task<IActionResult> SendUserEmail([FromQuery] string RequestById, [FromQuery] string userid, [FromQuery] string Comment)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                EmailTemplate emailTemplate;
                emailTemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.SentToKiv);

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                var user2 = await _userManager.FindByIdAsync(userid); ;

              //  var message = "Dear " + user2.FirstName + " " + user2.LastName + ",your application in examination unit hs been treated and sent to your kiv folder ,find below the comment from the officer . <br/> <br/>" + Comment;

                string mailContent = emailTemplate.EmailBody;

                var vname = user2.FirstName + " " + user2.LastName;
                mailContent = mailContent.Replace("#Name", vname);
                mailContent = mailContent.Replace("#comment", Comment);
                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                if (user2 != null)
                {
                    await _emailsender.SendEmailAsync(user2.Email, emailTemplate.EmailSubject, mailContent);

                   


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
                    var roleid = Convert.ToInt32(IPORoles.Search_Officer_Trade_Mark);
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


        [HttpGet("GetPreviousComment")]
        public async Task<IActionResult> GetPreviousComment([FromQuery] string RequestById , [FromQuery] string[] status , [FromQuery] string ID)
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


                var result = await _examinerRepository.GetPreviousComment(Convert.ToInt32(ID));


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
    }
}
