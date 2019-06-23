
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
using IPORevamp.Repository.TMApplicationStatus;
using IPORevamp.Core.Utilities;

using IPORevamp.Data.Entity.Interface.Entities.TMApplicationStatus;
using Newtonsoft.Json;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/TMApplicationStatus")]
    [ApiController]
    public class TMApplicationStatusController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

       
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ITMApplicationStatusRepository _tmApplicationStatusRepository;







        public TMApplicationStatusController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,

			ITMApplicationStatusRepository tmApplicationStatusRepository,
        
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
			_tmApplicationStatusRepository = tmApplicationStatusRepository;


        }



        //[Authorize]
        /// <summary>
        /// This method will get TMApplicationStatus and related States
        /// </summary>
        /// <param name="TMApplicationStatusId"></param>
        /// <param name="IncludeStates"></param>
        /// <param name="ActionBy"></param>
        /// <returns></returns>
       
        [HttpPost("GetTMApplicationStatusById/{TMApplicationStatusId}")]
        public async Task<IActionResult> GetSingleTMApplicationStatusById(int TMApplicationStatusId,string RequestById)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                var TMApplicationStatus = await _tmApplicationStatusRepository.GetTMApplicationStatusById(TMApplicationStatusId);

            if (TMApplicationStatus != null)
            {

                // get User Information
                 user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New TMApplicationStatus 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {TMApplicationStatus.StatusDescription} name  successfully",
                        Entity = "GetTMApplicationStatusById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "TMApplicationStatus Returned Successfully", false, TMApplicationStatus);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select TMApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }

     



        [HttpPost("GetTMApplicationStatusByName/{TMApplicationStatusName}")]
        public async Task<IActionResult> GetSingleTMApplicationStatusByName(string  TMApplicationStatusName, string RequestById)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var TMApplicationStatus = await _tmApplicationStatusRepository.GetTMApplicationStatusByName(TMApplicationStatusName);

                if (TMApplicationStatus != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New TMApplicationStatus 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {TMApplicationStatus.StatusDescription} name  successfully",
                        Entity = "GetTMApplicationStatusById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "TMApplicationStatus Returned Successfully", false, TMApplicationStatus);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select TMApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }



        [HttpGet("GetAllTMApplicationStatus")]
        public async Task<IActionResult> GetAllTMApplicationStatus([FromQuery] string RequestById)
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

                var TMApplicationStatus = await _tmApplicationStatusRepository.GetTMApplicationStatus();

                if (TMApplicationStatus != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New TMApplicationStatus 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all TM Application Status  successfully",
                        Entity = "GetAllTMApplicationStatus",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip 
                    });

                    return PrepareResponse(HttpStatusCode.OK, "TMApplicationStatus Returned Successfully", false, TMApplicationStatus);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select TMApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }



        // Save TMApplicationStatus

        [HttpPost("SaveTMApplicationStatus")]
        public async Task<IActionResult> SaveTMApplicationStatus(TMApplicationStatusViewModel TMApplicationStatusViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];
                var user = await _userManager.FindByIdAsync(TMApplicationStatusViewModel.CreatedBy.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if TMApplicationStatus Exist 
                var checkCount = await _tmApplicationStatusRepository.CheckExistingTMApplicationStatus(TMApplicationStatusViewModel.StatusDescription);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

				// attempt to save
				Data.Entities.TMApplicationStatus.TMApplicationStatus content = new Data.Entities.TMApplicationStatus.TMApplicationStatus();
                content.StatusDescription = TMApplicationStatusViewModel.StatusDescription;
                content.DateCreated = DateTime.Now;
                content.CreatedBy = TMApplicationStatusViewModel.CreatedBy.ToString();
                content.IsActive = true;
                content.IsDeleted = false;
                content.RoleId = TMApplicationStatusViewModel.RoleId;


                var save = await _tmApplicationStatusRepository.SaveTMApplicationStatus(content);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(TMApplicationStatusViewModel.CreatedBy.ToString());


                // Added A New TMApplicationStatus 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.StatusDescription}  TMApplicationStatus  successfully",
                    Entity = "TMApplicationStatusAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save TMApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateTMApplicationStatus")]
        public async Task<IActionResult> UpdateTMApplicationStatus(TMApplicationStatusViewModel TMApplicationStatusViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];
                var user = await _userManager.FindByIdAsync(TMApplicationStatusViewModel.CreatedBy.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if TMApplicationStatus Exist 

                var record = await _tmApplicationStatusRepository.GetTMApplicationStatusById(TMApplicationStatusViewModel.Id);
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the TMApplicationStatus


                record.StatusDescription = TMApplicationStatusViewModel.StatusDescription;
                record.LastUpdateDate = DateTime.Now;
                record.UpdatedBy = TMApplicationStatusViewModel.CreatedBy.ToString();
                record.IsActive = true;
                record.RoleId = TMApplicationStatusViewModel.RoleId;

                record.Id = TMApplicationStatusViewModel.Id;
               

                var save = await _tmApplicationStatusRepository.UpdateTMApplicationStatus(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(TMApplicationStatusViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update TMApplicationStatus {record}  TMApplicationStatus  successfully",
                    Entity = "TMApplicationStatusUpdate",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress =ip,
                    RecordBefore =json ,
                    RecordAfter = json 
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.UpdateRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Update TMApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpGet("DeleteTMApplicationStatus")]
        public async Task<IActionResult> DeleteTMApplicationStatus([FromQuery]String  TMApplicationStatusId, [FromQuery] String  UserId)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                // Check if TMApplicationStatus Exist 
                var record = await _tmApplicationStatusRepository.GetTMApplicationStatusById(Convert.ToInt32(TMApplicationStatusId));
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the TMApplicationStatus
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(TMApplicationStatusId);


                var delete = await _tmApplicationStatusRepository.DeleteTMApplicationStatus(record);
                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                var user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted TMApplicationStatus {record}  TMApplicationStatus  successfully",
                    Entity = "TMApplicationStatusDelete",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress= ip ,
                    RecordBefore = json ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.DeleteRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete TMApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}