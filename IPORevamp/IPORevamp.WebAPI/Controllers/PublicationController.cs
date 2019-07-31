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
using IPORevamp.Repository.Publication;
using IPORevamp.Repository.Email;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Publication")]
    [ApiController]
    public class PublicationController : BaseController
    {
        private readonly IpublicationRepository _publicationRepository;
        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;
        private readonly IPOContext _contex;


        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IFileHandler _fileUploadRespository;


        public PublicationController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,
      IEmailTemplateRepository EmailTemplateRepository,

    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
    IEmailManager<EmailLog, EmailTemplate> emailManager,
     IFileHandler fileUploadRespository,

   IpublicationRepository publicationRepository,
   IPOContext contex,

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


            _contex = contex;

            _httpContextAccessor = httpContextAccessor;
            _publicationRepository = publicationRepository;
            _fileUploadRespository = fileUploadRespository;
            _EmailTemplateRepository = EmailTemplateRepository;


        }


        [HttpGet("UpdateBatch")]
        public async Task<IActionResult> UpdateBatch([FromQuery] string RequestById, [FromQuery] string[] BatchData)
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


                var result = await _publicationRepository.UpdateBatch(BatchData);


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
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


                var result = await _publicationRepository.GetFreshApplication();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Publication  Fresh Application   successfully",
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


        [HttpGet("SendRegistraEmail")]
        public async Task<IActionResult> SendRegistraEmail([FromQuery] string RequestById , [FromQuery] string Appid)
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

                var App = (from p in _contex.Application where p.Id == Convert.ToInt32(Appid)  select p).FirstOrDefault();
                var roleid = Convert.ToInt32(IPORoles.Registrar);
                var departmentid = Convert.ToString(IPODepartment.Trademark);
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
                await _contex.AddAsync(new AuditTrail
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


                var result = await _publicationRepository.GetRefuseApplicationByUserid(RequestById);


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Publication  Fresh Application   successfully",
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


        [HttpGet("GetBatches")]
        public async Task<IActionResult> GetBatches([FromQuery] string RequestById)
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


                var result = await _publicationRepository.SelectBatches();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Publication  Batch   successfully",
                    Entity = "GetBatchAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Batch Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetPublicationById")]
        public async Task<IActionResult> GetPublicationById([FromQuery] string RequestById , [FromQuery] string Id)
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


                var result = await _publicationRepository.GetPublicationById(Id);


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Publication  By Id   successfully",
                    Entity = "GetPublicationById",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Publication by Id Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetPublicationByRegistrationId")]
        public async Task<IActionResult> GetPublicationByRegistrationId([FromQuery] string RequestById, [FromQuery] string Id)
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


                var result = await _publicationRepository.GetPublicationByRegistrationId(Id);


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Publication  By Id   successfully",
                    Entity = "GetPublicationById",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Publication by Id Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

    }
}
