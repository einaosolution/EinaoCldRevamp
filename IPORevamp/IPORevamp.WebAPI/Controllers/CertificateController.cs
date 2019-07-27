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
using IPORevamp.Repository.Opposition;
using IPORevamp.Data.Entity.Interface.Entities.Opposition;
using IPORevamp.Repository.Certificate;
using IPORevamp.Data.Entity.Interface.Entities.Certificate;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Certificate")]
    [ApiController]
    public class CertificateController : BaseController
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly IPOContext _contex;
        private static readonly object _syncRoot = new object();


        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IFileHandler _fileUploadRespository;


        public CertificateController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,

    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
    IEmailManager<EmailLog, EmailTemplate> emailManager,
     IFileHandler fileUploadRespository,

   ICertificateRepository certificateRepository,
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
            _certificateRepository = certificateRepository;
            _fileUploadRespository = fileUploadRespository;


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


                var result = await _certificateRepository.GetFreshApplicationByUserid(RequestById);


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

        [HttpGet("GetApplicationById")]
        public async Task<IActionResult> GetApplicationById([FromQuery] string RequestById , [FromQuery] string ApplicationId)
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

                var appArray = ApplicationId.Split(',');

                int applicationid = Convert.ToInt32(appArray[0]);
                var result = await _certificateRepository.GetApplicationById(applicationid);


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Publication  Fresh Application by id   successfully",
                    Entity = "GetFreshApplictionNy Id",
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


        [HttpGet("GetUpdateApplicationById")]
        public async Task<IActionResult> GetUpdateApplicationById([FromQuery] string RequestById, [FromQuery] string ApplicationId)
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

              //  var appArray = ApplicationId.Split(',');

              //  int applicationid = Convert.ToInt32(appArray[0]);
                var result = await _certificateRepository.UpdateApplication(Convert.ToInt32(ApplicationId));


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Publication  Fresh Application by id   successfully",
                    Entity = "GetFreshApplictionNy Id",
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


        [HttpGet("GetPaidCertificate")]
        public async Task<IActionResult> GetPaidCertificate([FromQuery] string RequestById)
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


                var result = await _certificateRepository.GetPaidCertificate();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Certificate  Fresh Application   successfully",
                    Entity = "GetPaidAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Paid Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("GetIssuedCertificate")]
        public async Task<IActionResult> GetIssuedCertificate([FromQuery] string RequestById)
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


                var result = await _certificateRepository.GetIssuedCertificate();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Certificate  Fresh Application   successfully",
                    Entity = "GetPaidAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Paid Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }



        [HttpPost("SaveCertificatePayment")]
        public async Task<IActionResult> SaveCertificatePayment([FromForm] string userId,
     [FromForm] string opponentName, [FromForm] string opponentAddress, [FromForm] string Comment, [FromForm] string AppID, [FromForm] string NoticeAppID, [FromForm] string quantity)
        {
            string pwalletid2 = "";
            string filepath = "";

       

            try
            {











             

                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(userId); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                string json2 = "";
                if (NoticeAppID == null)
                {
                    // attempt to save
                    PayCertificate content = new PayCertificate();
                    content.ApplicationId = AppID;
                    content.DateCreated = DateTime.Now;
                    content.ApplicantName = opponentName;
                    content.ApplicantAddress = opponentAddress;
                   
                    content.userid = userId;
                    content.Status = "Pending";
                    content.quantity = Convert.ToInt32(quantity);
                    //  content.transactionid = transactionid;

                    content.IsActive = true;

                    content.IsDeleted = false;

                    _contex.PayCertificate.Add(content);
                    _contex.SaveChanges();

                    NoticeAppID = Convert.ToString(content.Id);


                }

                else
                {

                    var CertPayment = await _certificateRepository.GetCertificatePaymentById(Convert.ToInt32(NoticeAppID));



                    CertPayment.ApplicantName = opponentName;
                    CertPayment.ApplicantAddress = opponentAddress;
                 

                    _contex.SaveChanges();

                }

                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new  Certificate Payment  successfully",
                    Entity = "Certificate",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip,
                    RecordAfter = json2
                });




                // get User Information
                //  user = await _userManager.FindByIdAsync(SectorViewModel.CreatedBy.ToString());


                // Added A New Sector 


                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, NoticeAppID);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Opposition  Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }

        [HttpGet("UpdateCertPaymentById")]
        public async Task<IActionResult> UpdateCertPaymentById([FromQuery] string RequestById, [FromQuery] string ApplicationId, [FromQuery] string TransactionId)
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


                var result = await _certificateRepository.GetCertificatePaymentById(Convert.ToInt32(ApplicationId));
                var appid = result.ApplicationId.Split(',');
                result.PaymentReference = TransactionId;
                result.Status = "Paid";

                _contex.SaveChanges();

                foreach(var kk in appid)
                {
                    var App = (from p in _contex.Application where p.Id == Convert.ToInt32(kk) select p).FirstOrDefault();

                     App.CertificatePayReference = TransactionId;
                    App.RtNumber = Convert.ToString(getMaxRtNo() + 1);
                    _contex.SaveChanges();
                }

                    //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for Certificate   Application By Id   successfully",
                    Entity = "GetCertificateAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Cerificate Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        public long  getMaxRtNo()
        {
            long  MaxRt;
        lock (_syncRoot)
        {
                return  MaxRt = _contex.Application.ToList().Max(e => Convert.ToInt64(e.RtNumber));
                //Body function
            }


}

        [HttpGet("GetApplicationByAppId")]
        public async Task<IActionResult> GetApplicationByAppId([FromQuery] string RequestById, [FromQuery] string ApplicationId)
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


              //  var result = await _oppositionRepository.GetApplicationById(Convert.ToInt32(ApplicationId));
                int userid = Convert.ToInt32(RequestById);
                var user2 = _userManager.Users.Where(x => x.Id == userid).FirstOrDefault();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for Refused  Application By Id   successfully",
                    Entity = "GetFreshAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, user2);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

    }
}
