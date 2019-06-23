
using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IPORevamp.Data.UserManagement.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EmailEngine.Base.Repository.EmailRepository;
using IPORevamp.Data.Entities.Email;
using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;

using IPORevamp.Data.Entity.Interface;
using Microsoft.AspNetCore.Hosting;
using IPORevamp.Data.SetupViewModel;
using IPORevamp.Data.Entities.Setting;
using IPORevamp.Repository.Setting;

using IPORevamp.Core.Utilities;
using Newtonsoft.Json;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/SystemSetup")]
    [ApiController]
    public class SettingController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

        private readonly ISettingRepository _settings;
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISettingRepository _SettingRepository;






        public SettingController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
    
         

            ISettingRepository settingrepository,

            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment,
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
            
            _settings = settingrepository;
            _httpContextAccessor = httpContextAccessor;
    


        }



        //[Authorize]
        /// <summary>
        /// This method will get Setting and related States
        /// </summary>
        /// <param name="SettingId"></param>
        /// <param name="IncludeStates"></param>
        /// <param name="RequestById"></param>
        /// <returns></returns>
       
        [HttpPost("GetSettingById/{SettingId}")]
        public async Task<IActionResult> GetSingleSettingById(int SettingId,string RequestById)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }



                var Setting = await _settings.GetSettingById(SettingId);

            if (Setting != null)
            {

                // get User Information
                 user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Setting 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {Setting.ItemName} name  successfully",
                        Entity = "GetSettingById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Setting Returned Successfully", false, Setting);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound,true);
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Setting", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }

     



        [HttpPost("GetSettingByName/{SettingName}")]
        public async Task<IActionResult> GetSingleSettingByName(string  SettingName, string RequestById)
        {
            try
            {
                



                var Setting = await _settings.GetSettingByCode(SettingName);

                if (Setting != null)
                {

                    // get User Information
                    var user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Setting 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {Setting} name  successfully",
                        Entity = "GetSettingById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Setting Returned Successfully", false, Setting);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Setting", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }



        [HttpGet("GetAllSettings")]
        public async Task<IActionResult> GetAllSettings([FromQuery] string RequestById)
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


                var Setting = await _settings.GetSettings();

                if (Setting != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Setting 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all countries  successfully",
                        Entity = "GetAllCountries",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip 
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Setting Returned Successfully", false, Setting);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound,true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Setting", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound,true);
            }
        }



        // Save Setting

        [HttpPost("SaveSetting")]
        public async Task<IActionResult> SaveSetting(SettingViewModel SettingViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];
                var user = await _userManager.FindByIdAsync(SettingViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                // Check if Setting Exist 
                var checkCount = await _settings.GetSettingByCode(SettingViewModel.SettingCode);

                if (checkCount.Count > 0 )
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to save
                Setting content = new Setting();
                content.SettingCode = SettingViewModel.SettingCode;
                content.ItemValue = SettingViewModel.ItemValue;
                content.DateCreated = DateTime.Now;
                content.CreatedBy = SettingViewModel.CreatedBy.ToString();
                content.IsActive = true;
                content.ItemName = SettingViewModel.ItemName;
                content.IsDeleted = false;


               // var save = await _SettingRepository.SaveSetting(content);
                var save = await _settings.SaveSetting(content);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);


                // get User Information
                user = await _userManager.FindByIdAsync(SettingViewModel.CreatedBy.ToString());


                // Added A New Setting 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.ItemName}  Setting  successfully",
                    Entity = "SettingAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Setting", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateSetting")]
        public async Task<IActionResult> UpdateSetting(SettingViewModel SettingViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                // Check if Setting Exist 

                var user = await _userManager.FindByIdAsync(SettingViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var record = await _settings.GetSettingById(SettingViewModel.SettingId);
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the Setting


                record.ItemName = SettingViewModel.ItemName;
                record.ItemValue = SettingViewModel.ItemValue;
                record.DateCreated = DateTime.Now;
                record.CreatedBy = SettingViewModel.CreatedBy.ToString();
                record.IsActive = true;
                record.SettingCode = SettingViewModel.SettingCode;
                record.Id = SettingViewModel.SettingId;
               

                var save = await _settings.UpdateSetting(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(SettingViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update Setting {record}  Setting  successfully",
                    Entity = "SettingUpdate",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordBefore = json ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.UpdateRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Setting", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpGet("DeleteSetting")]
        public async Task<IActionResult> DeleteSetting([FromQuery] String  SettingId, [FromQuery] String UserId)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(UserId.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Setting Exist 
                var record = await _settings.GetSettingById(Convert.ToInt32(SettingId));
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the Setting
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(SettingId);


                var delete = await _settings.DeleteSetting(record);
                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted Setting {record}  Setting  successfully",
                    Entity = "SettingDelete",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordBefore = json ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.DeleteRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete Setting", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}