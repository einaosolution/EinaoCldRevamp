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
using IPORevamp.Repository.Recordal;
using IPORevamp.Data.Entity.Interface.Entities.Recordal;
using Microsoft.EntityFrameworkCore;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Recordal")]
    [ApiController]
    public class RecordalController : BaseController
    {
        private readonly IRecordalRepository _recordalRepository;
        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly IPOContext _contex;


        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IFileHandler _fileUploadRespository;


        public RecordalController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,

    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
    IEmailManager<EmailLog, EmailTemplate> emailManager,
     IFileHandler fileUploadRespository,

  IRecordalRepository recordalRepository,
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
            _recordalRepository = recordalRepository;
            _fileUploadRespository = fileUploadRespository;


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


                var result = await _recordalRepository.GetApplicationByRegistrationId(ApplicationId);


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Recordal  By Id   successfully",
                    Entity = "GetRecordalById",
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


        [HttpGet("GetByRegistrationId")]
        public async Task<IActionResult> GetByRegistrationId([FromQuery] string RequestById, [FromQuery] string ApplicationId)
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


                var result = await _recordalRepository.GetApplicationByRegistrationId2(ApplicationId);


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Recordal  By Id   successfully",
                    Entity = "GetRecordalById",
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


        [HttpGet("GetApplicationByDocumentId")]
        public async Task<IActionResult> GetApplicationByDocumentId([FromQuery] string RequestById, [FromQuery] string ApplicationId)
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


                var result = await _recordalRepository.GetRenewalApplicationByDocumentId(Convert.ToInt32(ApplicationId));


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Recordal  By Id   successfully",
                    Entity = "GetRecordalById",
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


                var result = await _recordalRepository.GetIssuedCertificate();


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

        [HttpGet("GetRecordalRenewalCertificate")]
        public async Task<IActionResult> GetRecordalRenewalCertificate([FromQuery] string RequestById)
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


                var result = await _recordalRepository.GetRecordalRenewalCertificate();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Recordal renewal    successfully",
                    Entity = "GetRenewalAppliction",
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

        [HttpGet("GetRecordalRenewalDesignCertificate")]
        public async Task<IActionResult> GetRecordalRenewalDesignCertificate([FromQuery] string RequestById)
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


                var result = await _recordalRepository.GetRecordalRenewalDesignCertificate();


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Recordal renewal    successfully",
                    Entity = "GetRenewalAppliction",
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

        [HttpPost("SaveRenewalForm")]
        public async Task<IActionResult> SaveRenewalForm([FromForm] string userId,
[FromForm] string Name, [FromForm] string Address, [FromForm] string Comment, [FromForm] string Title, [FromForm] string Type, [FromForm] string NextRenewal, [FromForm] string AppID, [FromForm] string NoticeAppID)
        {
            string pwalletid2 = "";
            string filepath = "";
            string filepath2 = "";

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

                        if (file.Name == "FileUpload2")
                        {
                            FileUpload = true;
                            var postedFile = Request.Form.Files["FileUpload2"];

                            string msg = "";
                            try
                            {
                                String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                                String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                                int result1 = Convert.ToInt32(oneMegaByte[0]);
                                int result2 = Convert.ToInt32(fileMaxSize[0]);

                                msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload2"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                                  result2);

                                filepath2 = msg;

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
                    RecordalRenewal content = new RecordalRenewal();
                    content.applicationid = Convert.ToInt32(AppID);
                    content.DateCreated = DateTime.Now;
                    content.ApplicantName = Name;
                    content.ApplicantAddress = Address;
                    content.DetailOfRequest = Comment;
                    content.PowerOfAttorney = filepath;
                    content.CertificateOfTrademark = filepath2;
                    content.RenewalDueDate = Convert.ToDateTime(NextRenewal);
                    content.userid = userId;
                    content.Status = STATUS.Pending ;
                    content.RenewalType = Type;
                    //  content.transactionid = transactionid;

                    content.IsActive = true;

                    content.IsDeleted = false;

                    var save = _recordalRepository.Saveform(content);

                  

                    NoticeAppID = Convert.ToString(save.Result);


                }

                else
                {

                  //  var RenewalApplication = await _recordalRepository.GetRenewalApplicationById(Convert.ToInt32(NoticeAppID));

                    _recordalRepository.Updateform(Name, Address, Comment, filepath, filepath2, Type, Convert.ToInt32(NoticeAppID));

                   

                }

                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new  RecordalRenewal   successfully",
                    Entity = "RecordalRenewal",
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
                _logger.LogError(ex, "Save RecordalRenewal", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("SaveRenewalDesignForm")]
        public async Task<IActionResult> SaveRenewalDesignForm([FromForm] string userId,
[FromForm] string Name, [FromForm] string Address, [FromForm] string Comment, [FromForm] string Title, [FromForm] string Type, [FromForm] string NextRenewal, [FromForm] string AppID, [FromForm] string NoticeAppID)
        {
            string pwalletid2 = "";
            string filepath = "";
            string filepath2 = "";

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

                        if (file.Name == "FileUpload2")
                        {
                            FileUpload = true;
                            var postedFile = Request.Form.Files["FileUpload2"];

                            string msg = "";
                            try
                            {
                                String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                                String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                                int result1 = Convert.ToInt32(oneMegaByte[0]);
                                int result2 = Convert.ToInt32(fileMaxSize[0]);

                                msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload2"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                                  result2);

                                filepath2 = msg;

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
                    RecordalDesignRenewal content = new RecordalDesignRenewal();
                    content.applicationid = Convert.ToInt32(AppID);
                    content.DateCreated = DateTime.Now;
                    content.ApplicantName = Name;
                    content.ApplicantAddress = Address;
                    content.DetailOfRequest = Comment;
                    content.PowerOfAttorney = filepath;
                    content.CertificateOfTrademark = filepath2;
                   // content.RenewalDueDate = Convert.ToDateTime(NextRenewal);
                    content.RenewalDueDate = DateTime.Now.AddYears(4);
                    content.userid = userId;
                    content.Status = STATUS.Pending;
                    content.RenewalType = Type;
                    //  content.transactionid = transactionid;

                    content.IsActive = true;

                    content.IsDeleted = false;

                    var save = _recordalRepository.SaveDesignform(content);



                    NoticeAppID = Convert.ToString(save.Result);


                }

                else
                {

                    //  var RenewalApplication = await _recordalRepository.GetRenewalApplicationById(Convert.ToInt32(NoticeAppID));

                    _recordalRepository.UpdateDesignform(Name, Address, Comment, filepath, filepath2, Type, Convert.ToInt32(NoticeAppID));



                }

                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new  RecordalRenewal   successfully",
                    Entity = "RecordalRenewal",
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
                _logger.LogError(ex, "Save RecordalRenewal", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("SaveMergerForm")]
        public async Task<IActionResult> SaveMergerForm([FromForm] string userId,
[FromForm] string Name, [FromForm] string Address, [FromForm] string Name2, [FromForm] string Address2, [FromForm] string Comment, [FromForm] string Nationality, [FromForm] string MergerDate, [FromForm] string AppID, [FromForm] string NoticeAppID)
        {
            string pwalletid2 = "";
            string filepath = "";
            string filepath2 = "";
            string filepath3 = "";

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

                        if (file.Name == "FileUpload2")
                        {
                            FileUpload = true;
                            var postedFile = Request.Form.Files["FileUpload2"];

                            string msg = "";
                            try
                            {
                                String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                                String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                                int result1 = Convert.ToInt32(oneMegaByte[0]);
                                int result2 = Convert.ToInt32(fileMaxSize[0]);

                                msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload2"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                                  result2);

                                filepath2 = msg;

                            }

                            catch (Exception ee)
                            {
                                var kk = ee.Message;
                            }





                        }


                        if (file.Name == "FileUpload3")
                        {
                            FileUpload = true;
                            var postedFile = Request.Form.Files["FileUpload3"];

                            string msg = "";
                            try
                            {
                                String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                                String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                                int result1 = Convert.ToInt32(oneMegaByte[0]);
                                int result2 = Convert.ToInt32(fileMaxSize[0]);

                                msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload2"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                                  result2);

                                filepath3 = msg;

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
                    RecordalMerger content = new RecordalMerger();
                    content.applicationid = Convert.ToInt32(AppID);
                    content.DateCreated = DateTime.Now;
                    content.AssignorName = Name;
                    content.AssignorAddress = Address;
                    content.AssigneeName = Name2;
                    content.AssigneeAddress = Address2;
                    content.DetailOfRequest = Comment;
                    content.DateOfAssignment = MergerDate;
                  
                    content.AssigneeNationality = Convert.ToInt32(Nationality);
                    content.PowerOfAttorney = filepath;
                    content.Certificate = filepath3;
                    content.DeedOfAssigment = filepath2;
                  
                    content.userid = userId;
                    content.Status = "Pending";
                   
                    //  content.transactionid = transactionid;

                    content.IsActive = true;

                    content.IsDeleted = false;

                 var save =   _recordalRepository.Saveform(content);

                   

                    NoticeAppID = Convert.ToString(save.Result);


                }

                else
                {

                    _recordalRepository.Updateform(Name, Address, Name2, Address2, Comment, filepath, filepath2, filepath3, MergerDate, Convert.ToInt32(Nationality), Convert.ToInt32(NoticeAppID));


                }

                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new  RecordalMerger   successfully",
                    Entity = "RecordalMerger",
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
                _logger.LogError(ex, "Save RecordalRenewal", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }

        [HttpGet("UpdateRenewalFormById")]
        public async Task<IActionResult> UpdateRenewalFormById([FromQuery] string RequestById, [FromQuery] string ApplicationId, [FromQuery] string TransactionId)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                var roleid = user.RolesId;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

             var save=   _recordalRepository.UpdateRecord(Convert.ToString(roleid), TransactionId, Convert.ToInt32(ApplicationId), user.Id);

               
              

                //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for Renew Application By Id   successfully",
                    Entity = "GetRenewAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, save);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("UpdateRenewalDesignById")]
        public async Task<IActionResult> UpdateRenewalDesignById([FromQuery] string RequestById, [FromQuery] string ApplicationId, [FromQuery] string TransactionId)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                var roleid = user.RolesId;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var save = _recordalRepository.UpdateDesignRecord(Convert.ToString(roleid), TransactionId, Convert.ToInt32(ApplicationId), user.Id);




                //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for Renew Application By Id   successfully",
                    Entity = "GetRenewAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, save);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        

        [HttpGet("UpdateMergerFormById")]
        public async Task<IActionResult> UpdateMergerFormById([FromQuery] string RequestById, [FromQuery] string ApplicationId, [FromQuery] string TransactionId)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                var roleid = user.RolesId;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

              var save =  _recordalRepository.UpdateMergerRecord(Convert.ToString( roleid), TransactionId, Convert.ToInt32(ApplicationId), user.Id);
               


                //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for Renew Application By Id   successfully",
                    Entity = "GetRenewAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, save);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("UpdateRenewalFormStatus")]
        public async Task<IActionResult> UpdateRenewalFormStatus([FromQuery] string RequestById, [FromQuery] string ApplicationId)
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

               var save = _recordalRepository.UpdateRennewalRecord( Convert.ToInt32(ApplicationId), user.Id);

               
                //  SendOppositionOfficerEmail(Convert.ToString(result.ApplicationId));

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for Renew Application By Id   successfully",
                    Entity = "GetRenewAppliction",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Query Returned Successfully", false, save);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }
        [HttpGet("GetApplicationById2")]
        public async Task<IActionResult> GetApplicationById2([FromQuery] string RequestById, [FromQuery] string ApplicationId)
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


                var result = await _recordalRepository.GetApplicationById(Convert.ToInt32(ApplicationId));


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Recordal  By Id   successfully",
                    Entity = "GetRecordalById",
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

        [HttpGet("GetMergerApplicationById")]
        public async Task<IActionResult> GetMergerApplicationById([FromQuery] string RequestById, [FromQuery] string ApplicationId)
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


                var result = await _recordalRepository.GetMergerApplicationById(Convert.ToInt32(ApplicationId));


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Recordal  By Id   successfully",
                    Entity = "GetRecordalById",
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


        [HttpGet("GetMergerApplicationByAppId")]
        public async Task<IActionResult> GetMergerApplicationByAppId([FromQuery] string RequestById, [FromQuery] string ApplicationId)
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


                var result = await _recordalRepository.GetMergerApplicationByAppId(Convert.ToInt32(ApplicationId));


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Recordal  By Id   successfully",
                    Entity = "GetRecordalById",
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


        [HttpGet("GetRenewalApplicationById")]
        public async Task<IActionResult> GetRenewalApplicationById([FromQuery] string RequestById, [FromQuery] string ApplicationId)
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


                var result = await _recordalRepository.GetRenewalApplicationById(Convert.ToInt32(ApplicationId));


                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _contex.AddAsync(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Recordal  By Id   successfully",
                    Entity = "GetRecordalById",
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
