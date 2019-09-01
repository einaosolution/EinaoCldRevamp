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

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/PatentSearch")]
    [ApiController]
    public class PatentSearchController : BaseController
    {

        private readonly IPatentTypeRepository _patentTypeRepository;
        private readonly InewApplication _newApplicationRepository;
        private readonly IPatentSearchApplication _patentSearchRepository;

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly Repository.Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        private readonly IPOContext _contex;



        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;




        public PatentSearchController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
    IPOContext contex,
    IEmailManager<EmailLog, EmailTemplate> emailManager,
      Repository.Email.IEmailTemplateRepository EmailTemplateRepository,
       InewApplication newApplicationRepository,
     

   
    IPatentSearchApplication PatentSearchRepository ,


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
            _patentSearchRepository = PatentSearchRepository;

          _fileUploadRespository = fileUploadRespository;
            _EmailTemplateRepository = EmailTemplateRepository;


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


            _patentSearchRepository.SendExaminerEmail();

              

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
                catch(Exception ee)
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


        [HttpGet("GetPatentSearchKiv")]
        public async Task<IActionResult> GetPatentSearchKiv([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _patentSearchRepository.GetPatentSearchKiv();



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


                _patentSearchRepository.SendUserEmail(Convert.ToInt32(appid),comment);



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

        [HttpGet("SendEmailForCertificate")]
        public async Task<IActionResult> SendEmailForCertificate([FromQuery] string RequestById, [FromQuery] string appid, [FromQuery] string comment)
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


                _patentSearchRepository.SendEmailForCerticate(Convert.ToInt32(appid), comment);



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


        [HttpGet("GetPatentInventorById")]
        public async Task<IActionResult> GetPatentInventorById([FromQuery] string RequestById , [FromQuery] string Id)
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


                var patentinventor = await _patentSearchRepository.GetInventorById(Convert.ToInt32(Id));

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
                        Entity = "GetAllPatentInventor",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "PatentInventor Returned Successfully", false, patentinventor);

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


                var AddressOfService = await _patentSearchRepository.GetAddressOfServiceById(Convert.ToInt32(Id));

                if (AddressOfService != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all patent Address Of Service  successfully",
                        Entity = "GetAllAddressOfService",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Patent Address Of Service  Returned Successfully", false, AddressOfService);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select PatentAddressOfService", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpPost("SavePatentStateAppHistory")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> SavePatentStateAppHistory([FromForm] string pwalletid,
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


            _patentSearchRepository.SaveApplicationStateHistory(id, userrole, Request, tostatus, toDatastatus, fromDatastatus, fromstatus, comment, description, userid, msg);










            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);

        }

        [HttpPost("SavePatentFreshAppHistory")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> SavePatentFreshAppHistory([FromForm] string pwalletid,
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

           
                _patentSearchRepository.SaveApplicationHistory(id, userrole, Request, tostatus, toDatastatus, fromDatastatus, fromstatus, comment, description, userid, msg );

         


           

          



            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);

        }

        [HttpGet("GetPatentPriority")]
        public async Task<IActionResult> GetPatentPriority([FromQuery] string RequestById, [FromQuery] string Id)
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


                var patentpriority = await _patentSearchRepository.GetPriorityById(Convert.ToInt32(Id));

                if (patentpriority != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all patentInventor  successfully",
                        Entity = "GetAllPatentPriority",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "PatentPriority Returned Successfully", false, patentpriority);

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


        [HttpGet("GetPatentSubmittedApplication")]
        public async Task<IActionResult> GetPatentSubmittedApplication()
        {

            var details = _patentSearchRepository.GetSubmittedApplication();



      

          


            return Ok(details.Result);
        }


        [HttpGet("GetPatentByUserId")]
        public async Task<IActionResult> GetPatentByUserId([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _patentSearchRepository.GetPatentApplicationByUserid(RequestById.ToString());



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
                    Entity = "GetAllPatentByUserid",
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


        [HttpGet("GetPatentSearchState")]
        public async Task<IActionResult> GetPatentSearchState([FromQuery] string RequestById, [FromQuery] string ApplicationId)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _patentSearchRepository.GetSearchState(Convert.ToInt32(ApplicationId) , Convert.ToInt32(RequestById));



   

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all  patent successfully",
                    Entity = "GetAllPatentByUserid",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Fresh Patent Returned Successfully", false, details.Result);

           









        }

        [HttpGet("GetPatentFreshApplication")]
        public async Task<IActionResult> GetPatentFreshApplication([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _patentSearchRepository.GetPatentFreshApplication();



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
    }
}
