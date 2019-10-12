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

            _searchRepository.SaveApplicationHistory(id, userrole, Request, tostatus, toDatastatus, fromDatastatus, fromstatus, comment, description, userid, msg);

 



           



            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);

        }




        [HttpGet("GetExactSearch")]
        public async Task<IActionResult> GetExactSearch([FromQuery] string title)
        {

            var producttitle = "\" " + title + "\"";
            var results = _contex.MarkInformation
                      .FromSql($"uspGetMarkByTitle {producttitle}")
                      .ToList();


            return Ok(results);
        }


        [HttpGet("GetMeaningSearch")]
        public async Task<IActionResult> GetMeaningSearch([FromQuery] string title)
        {

            var producttitle = "\" " + title + "\"";
            var results = _contex.MarkInformation
                      .FromSql($"uspGetMarkByTitle2 {producttitle}")
                      .ToList();


            return Ok(results);
        }


       

        [HttpPost("SaveFreshAppHistoryAttachment")]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> SaveFreshAppHistoryAttachment([FromForm] string pwalletid,
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

            _searchRepository.SaveApplicationHistory(id, userrole, Request, tostatus, toDatastatus, fromDatastatus, fromstatus, comment, description, userid, msg);

           

            var user3 = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));

            await _auditTrailManager.AddAuditTrail(new AuditTrail
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


        [HttpPost("SaveFreshAppHistoryMultiple")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SaveFreshAppHistoryMultiple([FromForm] string pwalletid,
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

            _searchRepository.SaveApplicationHistoryMultiple( userrole, Request, tostatus, toDatastatus, fromDatastatus, fromstatus, comment, description, userid, Batch);

          

            var user3 = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));

            await _auditTrailManager.AddAuditTrail(new AuditTrail
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
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
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
                await _auditTrailManager.AddAuditTrail(new AuditTrail
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



        [HttpGet("TestApp")]
        public async Task<IActionResult> TestApp()
        {

            var settings = (from c in _contex.Settings where c.SettingCode == IPOCONSTANT.PublicationMaxDay select c).FirstOrDefault();




            //  settings.ItemValue
            var result = (from p in _contex.Application
                          join f in _contex.TrademarkApplicationHistory
                               on p.Id equals f.ApplicationID

                          where f.ToDataStatus ==DATASTATUS.Publication && f.ToStatus ==STATUS.Batch  && p.DataStatus == DATASTATUS.Publication && p.ApplicationStatus == STATUS.Batch
                          select f).ToList();

            // System.Console.Write("result count = " + result.Count);
            foreach (var results in result)
            {
                if ((results.DateCreated.AddDays(Convert.ToInt32(settings.ItemValue))) < DateTime.Now)
                {
                    string ss = "";

                }


                }

                return Ok();
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
                await _auditTrailManager.AddAuditTrail(new AuditTrail
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
                await _auditTrailManager.AddAuditTrail(new AuditTrail
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
