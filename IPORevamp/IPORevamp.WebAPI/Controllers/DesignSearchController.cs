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
using IPORevamp.Repository.DesignSearch;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/DesignSearch")]
    [ApiController]
    public class DesignSearchController : BaseController
    {
        private readonly IDesignSearchApplication _designSearchRepository;
        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly Repository.Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        private readonly IPOContext _contex;
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public DesignSearchController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
    IPOContext contex,
    IEmailManager<EmailLog, EmailTemplate> emailManager,
      Repository.Email.IEmailTemplateRepository EmailTemplateRepository,
       IDesignSearchApplication designSearchRepository,




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
            _designSearchRepository = designSearchRepository;
            _fileUploadRespository = fileUploadRespository;
            _EmailTemplateRepository = EmailTemplateRepository;


        }


        [HttpGet("GetDesignPriority")]
        public async Task<IActionResult> GetDesignPriority([FromQuery] string RequestById, [FromQuery] string Id)
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


                var patentpriority = await _designSearchRepository.GetPriorityById(Convert.ToInt32(Id));

              

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Design   successfully",
                        Entity = "GetAllDesignPriority",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "DesignPriority Returned Successfully", false, patentpriority);

               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select PatentPriority", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("GetDesignInventorById")]
        public async Task<IActionResult> GetDesignInventorById([FromQuery] string RequestById, [FromQuery] string Id)
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


                var patentinventor = await _designSearchRepository.GetInventorById(Convert.ToInt32(Id));

                if (patentinventor != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all patentInventor  successfully",
                        Entity = "GetAllDesignInventor",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "DesignInventor Returned Successfully", false, patentinventor);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select PatentPriority", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpPost("SaveDesignStateAppHistory")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> SaveDesignStateAppHistory([FromForm] string pwalletid,
        [FromForm] string comment, [FromForm] string description, [FromForm] string fromstatus, [FromForm] string tostatus, [FromForm] string fromDatastatus, [FromForm] string toDatastatus, [FromForm] string userid)
        {
            string ip = "";
            string msg = "";

            ip = Request.Headers["ip"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));



            string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);

            if (user == null)
            {

                return PrepareResponse(HttpStatusCode.Found, "Member record don't exist, please try again", false);

            }

            // check for user information before processing the request
            int id = Convert.ToInt32(pwalletid);
            var userrole = Convert.ToString(user.RolesId);


            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                    String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                    int result1 = Convert.ToInt32(oneMegaByte[0]);
                    int result2 = Convert.ToInt32(fileMaxSize[0]);

                    msg = await _fileUploadRespository.UploadFile(Request.Form.Files[0], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                      result2);

                }

                catch (Exception ee)
                {
                    var kk = ee.Message;
                }


            }


            _designSearchRepository.SaveApplicationStateHistory(id, userrole, Request, tostatus, toDatastatus, fromDatastatus, fromstatus, comment, description, userid, msg);










            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);

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


                _designSearchRepository.SendExaminerEmail();



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

        [HttpPost("SaveDesignFreshAppHistory")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> SaveDesignFreshAppHistory([FromForm] string pwalletid,
        [FromForm] string comment, [FromForm] string description, [FromForm] string fromstatus, [FromForm] string tostatus, [FromForm] string fromDatastatus, [FromForm] string toDatastatus, [FromForm] string userid, [FromForm] string Uploads)
        {
            string ip = "";
            //  string msg = "";
            string msg = Uploads;
            Boolean Fileupload = false;

            ip = Request.Headers["ip"];
            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));



            string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);

            if (user == null)
            {

                return PrepareResponse(HttpStatusCode.Found, "Member record don't exist, please try again", false);

            }

            // check for user information before processing the request
            int id = Convert.ToInt32(pwalletid);
            var userrole = Convert.ToString(user.RolesId);


            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                    String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                    int result1 = Convert.ToInt32(oneMegaByte[0]);
                    int result2 = Convert.ToInt32(fileMaxSize[0]);

                    msg = await _fileUploadRespository.UploadFile(Request.Form.Files[0], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                      result2);



                }

                catch (Exception ee)
                {
                    var kk = ee.Message;
                }


            }


            _designSearchRepository.SaveApplicationHistory(id, userrole, Request, tostatus, toDatastatus, fromDatastatus, fromstatus, comment, description, userid, msg);










            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);

        }

        [HttpGet("SendUserEmail")]
        public async Task<IActionResult> SendUserEmail([FromQuery] string RequestById, [FromQuery] string appid, [FromQuery] string comment)
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


                _designSearchRepository.SendUserEmail(Convert.ToInt32(appid), comment);



                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
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

                return PrepareResponse(HttpStatusCode.OK, "PatentInventor Returned Successfully", false, "success");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select PatentPriority", "");


                return PrepareResponse(HttpStatusCode.OK, "Mail Not Sent", false, "");
            }
        }

        [HttpGet("GetDesignSearchState")]
        public async Task<IActionResult> GetDesignSearchState([FromQuery] string RequestById, [FromQuery] string ApplicationId)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _designSearchRepository.GetSearchState(Convert.ToInt32(ApplicationId), Convert.ToInt32(RequestById));





            // get User Information
            user = await _userManager.FindByIdAsync(RequestById.ToString());

            // Added A New Country 
            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Create,
                DateCreated = DateTime.Now,
                Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all  design successfully",
                Entity = "GetAllDesignByUserid",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip
            });

            return PrepareResponse(HttpStatusCode.OK, "Fresh Patent Returned Successfully", false, details.Result);











        }

        [HttpGet("GetAddressOfServiceById")]
        public async Task<IActionResult> GetAddressOfServiceById([FromQuery] string RequestById, [FromQuery] string Id)
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


                var AddressOfService = await _designSearchRepository.GetAddressOfServiceById(Convert.ToInt32(Id));

              
                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all design Address Of Service  successfully",
                        Entity = "GetAllAddressOfService",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Design  Address Of Service  Returned Successfully", false, AddressOfService);

              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select DesignAddressOfService", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("GetDesignReconductSearchApplication")]
        public async Task<IActionResult> GetDesignReconductSearchApplication([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _designSearchRepository.GetDesignReconductSearch();



            if (details != null)
            {

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all ReconductSearch design successfully",
                    Entity = "GetAllReconductSearchDesign",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "ReconductSearch Design Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }

        [HttpGet("GetDesignKivSearchApplication")]
        public async Task<IActionResult> GetDesignKivSearchApplication([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _designSearchRepository.GetDesignKivSearch();



            if (details != null)
            {

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all KivSearch design successfully",
                    Entity = "GetAllKivSearchDesign",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "KivSearch Design Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }


        [HttpGet("GetDesignFreshApplication")]
        public async Task<IActionResult> GetDesignFreshApplication([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _designSearchRepository.GetDesignFreshApplication();



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


        [HttpGet("GetDesignListing")]
        public async Task<IActionResult> GetDesignListing([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _designSearchRepository.GetDesignListing(RequestById);



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
    }
}
