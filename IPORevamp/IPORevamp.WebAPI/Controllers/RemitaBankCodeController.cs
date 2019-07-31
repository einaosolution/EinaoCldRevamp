
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IPORevamp.WebAPI.Models;
using IPORevamp.Data.UserManagement.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EmailEngine.Base.Repository.EmailRepository;
using IPORevamp.Data.Entities.Email;
using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;

using IPORevamp.Data;
using IPORevamp.Data.TempModel;
using IPORevamp.Repository.Interface;

using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entities.Setting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Authorization;

using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data.SetupViewModel;
using IPORevamp.Repository.RemitaBankCode;
using IPORevamp.Core.Utilities;

using IPORevamp.Data.Entities;
using Newtonsoft.Json;
using IPORevamp.Data.Entity.Interface.Entities.RemitaBankCode;
using IPORevamp.Repository.Email;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/RemitaBankCode")]
    [ApiController]
    public class RemitaBankCodeController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

      
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRemitaBankCodeRepository _RemitaBankCodeRepository;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;

        public RemitaBankCodeController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,

            IRemitaBankCodeRepository RemitaBankCodeRepository,
           
            IEmailSender emailsender,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment,
              IEmailTemplateRepository EmailTemplateRepository,
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
            _RemitaBankCodeRepository = RemitaBankCodeRepository;
            _EmailTemplateRepository = EmailTemplateRepository;

        }



        //[Authorize]
        /// <summary>
        /// This method will get RemitaBankCode and related States
        /// </summary>
        /// <param name="RemitaBankCodeId"></param>
        /// <param name="IncludeStates"></param>
        /// <param name="RequestById"></param>
        /// <returns></returns>
       
        [HttpPost("GetRemitaBankCodeById/{RemitaBankCodeId}")]
        public async Task<IActionResult> GetSingleRemitaBankCodeById(int RemitaBankCodeId,string RequestById)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var RemitaBankCode = await _RemitaBankCodeRepository.GetRemitaBankCodeById(RemitaBankCodeId);

            if (RemitaBankCode != null)
            {

                // get User Information
                 user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New RemitaBankCode 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {RemitaBankCode.BankName} name  successfully",
                        Entity = "GetRemitaBankCodeById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, RemitaBankCode);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select RemitaBankCode", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }

     

       
        



        [HttpPost("GetRemitaBankCodeByName/{RemitaBankCodeName}")]
        public async Task<IActionResult> GetSingleRemitaBankCodeByName(string  RemitaBankCodeName, bool IncludeStates, string RequestById, bool IncludeLGAs)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var RemitaBankCode = await _RemitaBankCodeRepository.GetRemitaBankCodeByName(RemitaBankCodeName);

                if (RemitaBankCode != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New RemitaBankCode 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {RemitaBankCode.BankName} name  successfully",
                        Entity = "GetRemitaBankCodeById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "RemitaBankCode Returned Successfully", false, RemitaBankCode);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select RemitaBankCode", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }



        [HttpGet("GetAllRemitaBankCodes")]
        public async Task<IActionResult> GetAllRemitaBankCode([FromQuery] string RequestById)
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

                var RemitaBankCode = await _RemitaBankCodeRepository.GetRemitaBankCodes();

                if (RemitaBankCode != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New RemitaBankCode 
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

                    return PrepareResponse(HttpStatusCode.OK, "RemitaBankCode Returned Successfully", false, RemitaBankCode);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select RemitaBankCode", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }



        // Save RemitaBankCode

        [HttpPost("SaveRemitaBankCode")]
        public async Task<IActionResult> SaveRemitaBankCode(RemitaBankCodeViewModel RemitaBankCodeViewModel)
        {
            try
            {

                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RemitaBankCodeViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                // Check if RemitaBankCode Exist 
                var checkCount = await _RemitaBankCodeRepository.CheckExistingRemitaBankCode((RemitaBankCodeViewModel.BankId).ToString());

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to save
                RemitaBankCode content = new RemitaBankCode();
                content.BankCode = RemitaBankCodeViewModel.BankCode;
                content.DateCreated = DateTime.Now;
                content.BankName = RemitaBankCodeViewModel.BankName;
                content.CreatedBy = RemitaBankCodeViewModel.CreatedBy.ToString();
                content.IsActive = true;
               
                content.IsDeleted = false;


                var save = await _RemitaBankCodeRepository.SaveRemitaBankCode(content);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(RemitaBankCodeViewModel.CreatedBy.ToString());


                // Added A New RemitaBankCode 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.BankName}  RemitaBankCode  successfully",
                    Entity = "RemitaBankCodeAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save RemitaBankCode", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateRemitaBankCode")]
        public async Task<IActionResult> UpdateRemitaBankCode(RemitaBankCodeViewModel RemitaBankCodeViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];
                // Check if RemitaBankCode Exist 


                var user = await _userManager.FindByIdAsync(RemitaBankCodeViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var record = await _RemitaBankCodeRepository.GetRemitaBankCodeById(RemitaBankCodeViewModel.BankId);

                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the RemitaBankCode


                record.BankCode = RemitaBankCodeViewModel.BankCode;
                record.BankName = RemitaBankCodeViewModel.BankName;
                record.LastUpdateDate = DateTime.Now;
                record.UpdatedBy = RemitaBankCodeViewModel.CreatedBy.ToString();
                record.IsActive = true;
                record.Id = RemitaBankCodeViewModel.BankId;
                

                 var save = await _RemitaBankCodeRepository.UpdateRemitaBankCode(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(RemitaBankCodeViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update RemitaBankCode {record}  RemitaBankCode  successfully",
                    Entity = "RemitaBankCodeUpdate",
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
                _logger.LogError(ex, "Update RemitaBankCode", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpGet("DeleteRemitaBankCode")]
        public async Task<IActionResult> DeleteRemitaBankCode([FromQuery]String  RemitaBankCodeId, [FromQuery] String UserId)
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

                // Check if RemitaBankCode Exist 
                var record = await _RemitaBankCodeRepository.GetRemitaBankCodeById(Convert.ToInt32(RemitaBankCodeId));
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the RemitaBankCode
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(RemitaBankCodeId);


                var delete = await _RemitaBankCodeRepository.DeleteRemitaBankCode(record);
                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted RemitaBankCode {record}  RemitaBankCode  successfully",
                    Entity = "RemitaBankCodeDelete",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip,
                    RecordBefore = json ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.DeleteRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete RemitaBankCode", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}