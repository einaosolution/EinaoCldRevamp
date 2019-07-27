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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Search")]
    [ApiController]
    public class SearchController : BaseController
    {

        private readonly  IsearchRepository _searchRepository;

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly  IPOContext _contex;


        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IFileHandler _fileUploadRespository;


        public SearchController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,
    IPOContext contex,
    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
    IEmailManager<EmailLog, EmailTemplate> emailManager,
     IFileHandler fileUploadRespository,

    IsearchRepository  searchRepository,

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
            _searchRepository = searchRepository;
            _fileUploadRespository = fileUploadRespository;


        }

        [HttpPost("SaveFreshAppHistory")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> SaveFreshAppHistory([FromForm] string pwalletid,
         [FromForm] string comment, [FromForm] string description, [FromForm] string fromstatus, [FromForm] string tostatus, [FromForm] string fromDatastatus, [FromForm] string toDatastatus, [FromForm] string userid)
        {
            string ip = "";

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

            var vpwallet = (from c in _contex.Application where c.Id == id select c).FirstOrDefault();

            string transactionid = vpwallet.TransactionID;



            if (vpwallet != null)
            {
              
                vpwallet.ApplicationStatus = tostatus;
                vpwallet.DataStatus= toDatastatus;

               

                // get User Information




            }



            // file upload
            string msg = "";

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

            await _contex.AddAsync(new TrademarkApplicationHistory
            {
                ApplicationID = id,
                DateCreated = DateTime.Now,
                TransactionID = transactionid,
                FromDataStatus = fromDatastatus,
                trademarkcomment = comment,
                description = description ,

                ToDataStatus = toDatastatus,
                FromStatus = fromstatus ,
                ToStatus = tostatus, 
                UploadsPath1 = msg,
                userid = Convert.ToInt32(userid) ,
                Role= userrole
            });

            _contex.SaveChanges();

            var user3 = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));
                   
                    await _contex.AddAsync(new AuditTrail
                    {
                        ActionTaken = AuditAction.Update,
                        DateCreated = DateTime.Now,
                        Description = $"Application  has been Updated  successfully",
                        Entity = "Pwallet",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip,
                        RecordBefore = fromstatus,
                        RecordAfter = tostatus
                    });

            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);

        }




        [HttpGet("GetExactSearch")]
        public async Task<IActionResult> GetExactSearch([FromQuery] string title)
        {

            var name = "\" " + title + "\"";
            var results = _contex.MarkInformation
                      .FromSql($"uspGetMarkByTitle {name}")
                      .ToList();


            return Ok(results);
        }

        [HttpGet("GetMeaningSearch")]
        public async Task<IActionResult> GetMeaningSearch([FromQuery] string title)
        {

            var name = "\" " + title + "\"";
            var results = _contex.MarkInformation
                      .FromSql($"uspGetMarkByTitle2 {name}")
                      .ToList();


            return Ok(results);
        }

        [HttpPost("SaveFreshAppHistory2")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> SaveFreshAppHistory2([FromForm] string pwalletid,
 [FromForm] string comment, [FromForm] string description, [FromForm] string fromstatus, [FromForm] string tostatus, [FromForm] string fromDatastatus, [FromForm] string toDatastatus, [FromForm] string userid, [FromForm] string uploadpath)
        {
            string ip = "";

            ip = Request.Headers["ip"];


            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));
            var userrole = Convert.ToString(user.RolesId);

            string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);

            if (user == null)
            {

                return PrepareResponse(HttpStatusCode.Found, "Member record don't exist, please try again", false);

            }

            // check for user information before processing the request
            int id = Convert.ToInt32(pwalletid);

            var vpwallet = (from c in _contex.Application where c.Id == id select c).FirstOrDefault();

            string transactionid = vpwallet.TransactionID;
            string prevappstatus = vpwallet.ApplicationStatus;
            string prevDatastatus = vpwallet.DataStatus;



            if (vpwallet != null)
            {

                vpwallet.ApplicationStatus = tostatus;
                vpwallet.DataStatus = toDatastatus;



                // get User Information




            }



            // file upload
            string msg = "";

      

            await _contex.AddAsync(new TrademarkApplicationHistory
            {
                ApplicationID = id,
                DateCreated = DateTime.Now,
                TransactionID = transactionid,
                FromDataStatus = prevDatastatus,
                trademarkcomment = comment,
                description = description,

                ToDataStatus = toDatastatus,
                FromStatus = prevappstatus,
                ToStatus = tostatus,
                UploadsPath1 = uploadpath,
                userid = Convert.ToInt32(userid) ,
                Role = userrole
            });

            _contex.SaveChanges();

            var user3 = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));

            await _contex.AddAsync(new AuditTrail
            {
                ActionTaken = AuditAction.Update,
                DateCreated = DateTime.Now,
                Description = $"Application  has been Updated  successfully",
                Entity = "Pwallet",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip,
                RecordBefore = fromstatus,
                RecordAfter = tostatus
            });

            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);

        }


        [HttpPost("SaveFreshAppHistory3")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SaveFreshAppHistory3([FromForm] string pwalletid,
         [FromForm] string comment, [FromForm] string description, [FromForm] string fromstatus, [FromForm] string tostatus, [FromForm] string fromDatastatus, [FromForm] string toDatastatus, [FromForm] string userid, [FromForm] string uploadpath , [FromForm] string Batch)
        {
            string ip = "";

            ip = Request.Headers["ip"];


            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));
            var userrole = Convert.ToString(user.RolesId);
            string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);

            if (user == null)
            {

                return PrepareResponse(HttpStatusCode.Found, "Member record don't exist, please try again", false);

            }

            // check for user information before processing the request
           // int id = Convert.ToInt32(pwalletid);

            var vpwallet = (from c in _contex.Application where c.Batchno == Batch select c).ToList();

            foreach (var Application in vpwallet)
            {

                string transactionid = Application.TransactionID;
                string prevappstatus = Application.ApplicationStatus;
                string prevDatastatus = Application.DataStatus;



                if (Application != null)
                {

                    Application.ApplicationStatus = tostatus;
                    Application.DataStatus = toDatastatus;



                    // get User Information




                }



                // file upload
                string msg = "";



                await _contex.AddAsync(new TrademarkApplicationHistory
                {
                    ApplicationID = Application.Id,
                    DateCreated = DateTime.Now,
                    TransactionID = transactionid,
                    FromDataStatus = prevDatastatus,
                    trademarkcomment = comment,
                    description = description,

                    ToDataStatus = toDatastatus,
                    FromStatus = prevappstatus,
                    ToStatus = tostatus,
                    UploadsPath1 = uploadpath,
                    userid = Convert.ToInt32(userid) ,
                    Role = userrole
                });

                _contex.SaveChanges();

            }

            var user3 = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));

            await _contex.AddAsync(new AuditTrail
            {
                ActionTaken = AuditAction.Update,
                DateCreated = DateTime.Now,
                Description = $"Application  has been Updated  successfully",
                Entity = "Pwallet",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip,
                RecordBefore = fromstatus,
                RecordAfter = tostatus
            });

            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);

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


                var result= await _searchRepository.GetFreshApplication();


                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _contex.AddAsync(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Search Fresh Application   successfully",
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

        [HttpGet("GetKivApplication")]
        public async Task<IActionResult> GetKivApplication([FromQuery] string RequestById)
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


                var result = await _searchRepository.GetKivApplication();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Search Fresh Application   successfully",
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


                var result = await _searchRepository.GetTreatedApplication();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Search Fresh Application   successfully",
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


        [HttpGet("GetTradeMarkType")]
        public async Task<IActionResult> GetTradeMarkType([FromQuery] string RequestById)
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


                var result = await _searchRepository.GetTradeMarkType();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Trademark Type    successfully",
                    Entity = "GetTrademarkType",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select TrademarkType", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }




    }
}
