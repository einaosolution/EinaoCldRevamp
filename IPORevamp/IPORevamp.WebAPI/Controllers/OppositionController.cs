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
using IPORevamp.Repository.Email;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Opposition")]
    [ApiController]
    public class OppositionController : BaseController
    {
        private readonly IOppositionRepository _oppositionRepository;
        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly IPOContext _contex;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;


        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IFileHandler _fileUploadRespository;


        public OppositionController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,

    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
    IEmailManager<EmailLog, EmailTemplate> emailManager,
     IFileHandler fileUploadRespository,
     IEmailTemplateRepository EmailTemplateRepository,

        IOppositionRepository oppositionRepository,
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
            _oppositionRepository = oppositionRepository;
            _fileUploadRespository = fileUploadRespository;
            _EmailTemplateRepository = EmailTemplateRepository;


        }


        [HttpGet("GetApplicationById")]
        public async Task<IActionResult> GetApplicationById([FromQuery] string RequestById, [FromQuery] string ApplicationId)
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


                var result = await _oppositionRepository.GetApplicationById(Convert.ToInt32(ApplicationId));
                int userid = Convert.ToInt32(result.userid);
                var user2 = _userManager.Users.Where(x => x.Id == userid).FirstOrDefault();


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

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, user2);


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


                var result = await _oppositionRepository.GetFreshApplication();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 

                try
                {
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

                }

                catch(Exception ee)
                {

                }

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetNewJudgment")]
        public async Task<IActionResult> GetNewJudgment([FromQuery] string RequestById)
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


                var result = await _oppositionRepository.GetNewJudgment();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                try
                {
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Judgement on  Application   successfully",
                        Entity = "GetOppositionAppliction",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                }
                catch(Exception ee)
                {

                }

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetOpposeFormById")]
        public async Task<IActionResult> GetOpposeFormById([FromQuery] string RequestById, [FromQuery] string ApplicationId)
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


                var result = await _oppositionRepository.GetNoticeApplicationById(Convert.ToInt32(ApplicationId));
               


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                try
                {
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for Refused  Application By Id   successfully",
                        Entity = "GetOpposeAppliction",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                }
                catch(Exception ee)
                {

                }

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("GetCounterOpposeFormById")]
        public async Task<IActionResult> GetCounterOpposeFormById([FromQuery] string RequestById, [FromQuery] string ApplicationId)
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


                var result = await _oppositionRepository.GetCounterOppostionApplicationById(Convert.ToInt32(ApplicationId));



                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                try
                {
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for Refused  Application By Id   successfully",
                        Entity = "GetOpposeAppliction",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                }

                catch(Exception ee)
                {

                }

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("GetOppositionByUserid")]
        public async Task<IActionResult> GetOppositionByUserid([FromQuery] string RequestById, [FromQuery] string userId)
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


                var result = await _oppositionRepository.GetOppositionByUserId(userId);



                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                try
                {


                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for oppose  Application By Id   successfully",
                        Entity = "GetOpposeAppliction",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                }

                catch(Exception ee)
                {

                }

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select opposition  Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetCounterOppositionByUserid")]
        public async Task<IActionResult> GetCounterOppositionByUserid([FromQuery] string RequestById, [FromQuery] string userId)
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


                var result = await _oppositionRepository.GetCounterOppositionByUserId(userId);



                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                try
                {
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for oppose  Application By Id   successfully",
                        Entity = "GetOpposeAppliction",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                }

                catch(Exception ee)
                {

                }

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select opposition  Application", "");
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

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                var user2 = await _userManager.FindByIdAsync(userid); ;

                EmailTemplate emailTemplate;
                emailTemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.NotifyUserOfOpposition);
                string mailContent = emailTemplate.EmailBody;

                var vname = user2.FirstName + " " + user2.LastName;
                mailContent = mailContent.Replace("#Name", vname);
               
                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

               // var message = "Dear " + user2.FirstName + " " + user2.LastName + ",Your Application was opposed,please login to counter the opposition. ";



                if (user2 != null)
                {

                  

                  
                        await _emailsender.SendEmailAsync(user2.Email, "Notice Of Opposition", mailContent);







                    // get User Information
                    //  user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    try
                    {
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

                    }
                    catch(Exception ee)
                    {

                    }

                    return PrepareResponse(HttpStatusCode.OK, "Examiner Returned Successfully", false, user2);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Examiner", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("UpdateOpposeFormById")]
        public async Task<IActionResult> UpdateOpposeFormById([FromQuery] string RequestById, [FromQuery] string ApplicationId, [FromQuery] string TransactionId)
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


                var result = await _oppositionRepository.GetNoticeApplicationByTransactionid(Convert.ToInt32(ApplicationId) , TransactionId);

              

                SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                try
                {
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for Opposed  Application By Id   successfully",
                        Entity = "GetOpposeAppliction",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                }

                catch(Exception ee)
                {

                }

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("UpdateCounterOpposeFormById")]
        public async Task<IActionResult> UpdateCounterOpposeFormById([FromQuery] string RequestById, [FromQuery] string ApplicationId, [FromQuery] string TransactionId)
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


                var result = await _oppositionRepository.GetCounterOppostionApplicationByTransactionid(Convert.ToInt32(ApplicationId), TransactionId);

             
              //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                try
                {
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for Opposed  Application By Id   successfully",
                        Entity = "GetOpposeAppliction",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                }

                catch(Exception ee)
                {

                }

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, result);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        public async void SendOppositionOfficerEmail(string Appid)
        {
            EmailTemplate emailTemplate;
            try
            {

                var App = (from p in _contex.Application where p.Id == Convert.ToInt32(Appid) select p).FirstOrDefault();
                var roleid = Convert.ToInt32(IPORoles.Opposition_Officer_Trade_Mark);
                var user2 = _userManager.Users.Where(x => x.RolesId == roleid).ToList();
                emailTemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.NoticeOfOpposition);
                foreach (var users in user2)
                {
                    string mailContent = emailTemplate.EmailBody;
                    var username = users.FirstName + " " + users.LastName;
                    mailContent = mailContent.Replace("#Name", username);
                    mailContent = mailContent.Replace("#transid", App.TransactionID);
                    mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);
                  
                   
                   // var mailContent = "Dear " + vname + "<br> a user opposed an  application with transactionID   " + App.TransactionID;
                    _emailsender.SendEmailAsync(users.Email, "Notice Of Opposition", mailContent);

                }




            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sent Email", "");
                // return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpPost("SaveOppositionForm")]
        public async Task<IActionResult> SaveOppositionForm([FromForm] string userId,
      [FromForm] string opponentName, [FromForm] string opponentAddress, [FromForm] string Comment, [FromForm] string AppID, [FromForm] string NoticeAppID)
        {
            string pwalletid2 = "";
            string filepath = "";

            Boolean FileUpload = false;
            Boolean FileUpload1 = false;
            Boolean FileUpload2 = false;
            Boolean FileUpload3 = false;

            try
            {

     









                if (Request.Form.Files.Count > 0)
                {
                    foreach (var file in Request.Form.Files)
                    {

                        var files = new List<string>();
                        if (file.Name == "FileUpload")
                        {
                            FileUpload = true;
                            var postedFile = Request.Form.Files["FileUpload"];

                            string msg = "";
                            try
                            {
                                String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                                String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                                int result1 = Convert.ToInt32(oneMegaByte[0]);
                                int result2 = Convert.ToInt32(fileMaxSize[0]);

                                msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                                  result2);

                                filepath = msg;

                            }

                            catch (Exception ee)
                            {
                                var kk = ee.Message;
                            }





                        }

                   
                    }

                }

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
                    NoticeOfOpposition content = new NoticeOfOpposition();
                    content.ApplicationId = Convert.ToInt32(AppID);
                    content.DateCreated= DateTime.Now;
                    content.opponentName = opponentName;
                    content.opponentAddress = opponentAddress;
                    content.Comment = Comment;
                    content.Upload = filepath;
                    content.userid = userId;
                    content.Status =STATUS.Pending ;
                    //  content.transactionid = transactionid;

                    content.IsActive = true;

                    content.IsDeleted = false;

                 var Noticeid =  _oppositionRepository.SaveForm(content);

                 

                    NoticeAppID = Convert.ToString(Noticeid.Result);


                }

                else
                {

                   

                    _oppositionRepository.UpdateForm(opponentName, opponentAddress, Comment, filepath, Convert.ToInt32(NoticeAppID));

                   

                }

                try
                {
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName} add a new  Opposition   Application   successfully",
                        Entity = "Opposition",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip,
                        RecordAfter = json2
                    });

                }

                catch (Exception ee)
                {

                }


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

        [HttpPost("SaveCounterOppositionForm")]
        public async Task<IActionResult> SaveCounterOppositionForm([FromForm] string userId,
     [FromForm] string opponentName, [FromForm] string opponentAddress, [FromForm] string Comment, [FromForm] string AppID, [FromForm] string NoticeAppID)
        {
            string pwalletid2 = "";
            string filepath = "";

            Boolean FileUpload = false;
            Boolean FileUpload1 = false;
            Boolean FileUpload2 = false;
            Boolean FileUpload3 = false;

            try
            {


                if (Request.Form.Files.Count > 0)
                {
                    foreach (var file in Request.Form.Files)
                    {

                        var files = new List<string>();
                        if (file.Name == "FileUpload")
                        {
                            FileUpload = true;
                            var postedFile = Request.Form.Files["FileUpload"];

                            string msg = "";
                            try
                            {
                                String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                                String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                                int result1 = Convert.ToInt32(oneMegaByte[0]);
                                int result2 = Convert.ToInt32(fileMaxSize[0]);

                                msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                                  result2);

                                filepath = msg;

                            }

                            catch (Exception ee)
                            {
                                var kk = ee.Message;
                            }





                        }


                    }

                }

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
                  CounterOpposition  content = new CounterOpposition();
                    content.ApplicationId = Convert.ToInt32(AppID);
                    content.DateCreated = DateTime.Now;
                    content.ApplicantName= opponentName;
                    content.ApplicantAddress = opponentAddress;
                    content.Comment = Comment;
                    content.Upload = filepath;
                    content.userid = userId;
                    content.Status =STATUS.Pending ;
                    //  content.transactionid = transactionid;

                    content.IsActive = true;

                    content.IsDeleted = false;

                 var save = _oppositionRepository.SaveForm(content);

                   

                    NoticeAppID = Convert.ToString(save.Result);


                }

                else
                {


                    _oppositionRepository.UpdateCounterForm(opponentName, opponentAddress, Comment, filepath, (Convert.ToInt32(NoticeAppID))) ;




                  

                }
                try
                {
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName} add a new  Opposition   Application   successfully",
                        Entity = "Opposition",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip,
                        RecordAfter = json2
                    });

                }

                catch(Exception ee)
                {

                }


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
    }
}
