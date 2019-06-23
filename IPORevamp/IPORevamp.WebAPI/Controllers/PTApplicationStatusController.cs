
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
using IPORevamp.Data.Entities.TMApplicationStatus;
using IPORevamp.Repository.PTApplicationStatus;
using IPORevamp.Core.Utilities;
using IPORevamp.Data.Entity.Interface.Entities.PTApplicationStatus;
using Newtonsoft.Json;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/PTApplicationStatus")]
    [ApiController]
    public class PTApplicationStatusController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;


        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IPTApplicationStatusRepository _ptApplicationStatusRepository;
	






        public PTApplicationStatusController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,

			IPTApplicationStatusRepository ptApplicationStatusRepository,
 
            IEmailSender emailsender,
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
            _emailManager = emailManager;
            
            _emailsender = emailsender;
            _httpContextAccessor = httpContextAccessor;
			_ptApplicationStatusRepository = ptApplicationStatusRepository;


        }



        //[Authorize]
        /// <summary>
        /// This method will get PTApplicationStatus and related States
        /// </summary>
        /// <param name="PTApplicationStatusId"></param>
        /// <param name="IncludeStates"></param>
        /// <param name="ActionBy"></param>
        /// <returns></returns>
       
        [HttpPost("GetPTApplicationStatusById/{PTApplicationStatusId}")]
        public async Task<IActionResult> GetSinglePTApplicationStatusById(int PTApplicationStatusId,string RequestById)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var PTApplicationStatus = await _ptApplicationStatusRepository.GetPTApplicationStatusById(PTApplicationStatusId);

            if (PTApplicationStatus != null)
            {

                // get User Information
                 user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New PTApplicationStatus 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {PTApplicationStatus.StatusDescription} name  successfully",
                        Entity = "GetPTApplicationStatusById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "PTApplicationStatus Returned Successfully", false, PTApplicationStatus);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select PTApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }

     



        [HttpPost("GetPTApplicationStatusByName/{PTApplicationStatusName}")]
        public async Task<IActionResult> GetSinglePTApplicationStatusByName(string  PTApplicationStatusName, string RequestById)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var PTApplicationStatus = await _ptApplicationStatusRepository.GetPTApplicationStatusByName(PTApplicationStatusName);

                if (PTApplicationStatus != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New PTApplicationStatus 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {PTApplicationStatus.StatusDescription} name  successfully",
                        Entity = "GetPTApplicationStatusById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "PTApplicationStatus Returned Successfully", false, PTApplicationStatus);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select PTApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }



        [HttpGet("GetAllPTApplicationStatus")]
        public async Task<IActionResult> GetAllPTApplicationStatus( [FromQuery] string ActionBy)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(ActionBy.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var PTApplicationStatus = await _ptApplicationStatusRepository.GetPTApplicationStatus();

                if (PTApplicationStatus != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(ActionBy.ToString());


                    // Added A New PTApplicationStatus 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all PT Application Status  successfully",
                        Entity = "GetAllPTApplicationStatus",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "PTApplicationStatus Returned Successfully", false, PTApplicationStatus);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select PTApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }



        // Save PTApplicationStatus

        [HttpPost("SavePTApplicationStatus")]
        public async Task<IActionResult> SavePTApplicationStatus(PTApplicationStatusViewModel PTApplicationStatusViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(PTApplicationStatusViewModel.CreatedBy.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if PTApplicationStatus Exist 
                var checkCount = await _ptApplicationStatusRepository.CheckExistingPTApplicationStatus(PTApplicationStatusViewModel.StatusDescription);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

				// attempt to save
				Data.Entities.PTApplicationStatus.PTApplicationStatus content = new Data.Entities.PTApplicationStatus.PTApplicationStatus();
                content.StatusDescription = PTApplicationStatusViewModel.StatusDescription;
                content.DateCreated = DateTime.Now;
                content.CreatedBy = PTApplicationStatusViewModel.CreatedBy.ToString();
                content.IsActive = true;
                content.RoleId = PTApplicationStatusViewModel.RoleId;
                content.IsDeleted = false;


                var save = await _ptApplicationStatusRepository.SavePTApplicationStatus(content);

                string json = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);


                // get User Information
                user = await _userManager.FindByIdAsync(PTApplicationStatusViewModel.CreatedBy.ToString());


                // Added A New PTApplicationStatus 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.StatusDescription}  PTApplicationStatus  successfully",
                    Entity = "PTApplicationStatusAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordAfter = json
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save PTApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdatePTApplicationStatus")]
        public async Task<IActionResult> UpdatePTApplicationStatus(PTApplicationStatusViewModel PTApplicationStatusViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];
                var user = await _userManager.FindByIdAsync(PTApplicationStatusViewModel.CreatedBy.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                // Check if PTApplicationStatus Exist 

                var record = await _ptApplicationStatusRepository.GetPTApplicationStatusById(PTApplicationStatusViewModel.Id);
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the PTApplicationStatus


                record.StatusDescription = PTApplicationStatusViewModel.StatusDescription;
                record.LastUpdateDate = DateTime.Now;
                record.UpdatedBy = PTApplicationStatusViewModel.CreatedBy.ToString();
                record.IsActive = true;

                 record.Id = PTApplicationStatusViewModel.Id;
                record.RoleId = PTApplicationStatusViewModel.RoleId;


                var save = await _ptApplicationStatusRepository.UpdatePTApplicationStatus(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(PTApplicationStatusViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update PTApplicationStatus {record}  PTApplicationStatus  successfully",
                    Entity = "PTApplicationStatusUpdate",
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
                _logger.LogError(ex, "Update PTApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpGet("DeletePTApplicationStatus")]
        public async Task<IActionResult> DeletePTApplicationStatus([FromQuery]String PTApplicationStatusId, [FromQuery] String  UserId)
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
                // Check if PTApplicationStatus Exist 
                var record = await _ptApplicationStatusRepository.GetPTApplicationStatusById(Convert.ToInt32(PTApplicationStatusId));
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the PTApplicationStatus
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(PTApplicationStatusId);


                var delete = await _ptApplicationStatusRepository.DeletePTApplicationStatus(record);
                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);


                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted PTApplicationStatus {record}  PTApplicationStatus  successfully",
                    Entity = "PTApplicationStatusDelete",
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
                _logger.LogError(ex, "Delete PTApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}