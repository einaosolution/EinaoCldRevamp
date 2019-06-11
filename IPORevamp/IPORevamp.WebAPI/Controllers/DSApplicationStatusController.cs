
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
using IPORevamp.Data.Entities.DSApplicationStatus;
using IPORevamp.Repository.DSApplicationStatus;
using IPORevamp.Core.Utilities;
using IPORevamp.Repository.DSApplicationStatus;
using IPORevamp.Data.Entity.Interface.Entities.DSApplicationStatus;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/DSApplicationStatus")]
    [ApiController]
    public class DSApplicationStatusController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

       
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IDSApplicationStatusRepository _dsApplicationStatusRepository;







        public DSApplicationStatusController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,

			IDSApplicationStatusRepository dsApplicationStatusRepository,
            
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
			_dsApplicationStatusRepository = dsApplicationStatusRepository;


        }



        //[Authorize]
        /// <summary>
        /// This method will get DSApplicationStatus and related States
        /// </summary>
        /// <param name="DSApplicationStatusId"></param>
        /// <param name="IncludeStates"></param>
        /// <param name="ActionBy"></param>
        /// <returns></returns>
       
        [HttpPost("GetDSApplicationStatusById/{DSApplicationStatusId}")]
        public async Task<IActionResult> GetSingleDSApplicationStatusById(int DSApplicationStatusId,string CreatedBy)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(CreatedBy.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }



                var DSApplicationStatus = await _dsApplicationStatusRepository.GetDSApplicationStatusById(DSApplicationStatusId);

            if (DSApplicationStatus != null)
            {

                // get User Information
                 user = await _userManager.FindByIdAsync(CreatedBy.ToString());


                    // Added A New DSApplicationStatus 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {DSApplicationStatus.StatusDescription} name  successfully",
                        Entity = "GetDSApplicationStatusById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "DSApplicationStatus Returned Successfully", false, DSApplicationStatus);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select DSApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

     



        [HttpPost("GetDSApplicationStatusByName/{DSApplicationStatusName}")]
        public async Task<IActionResult> GetSingleDSApplicationStatusByName(string  DSApplicationStatusName, string CreatedBy)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(CreatedBy.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }



                var DSApplicationStatus = await _dsApplicationStatusRepository.GetDSApplicationStatusByName(DSApplicationStatusName);

                if (DSApplicationStatus != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(CreatedBy.ToString());


                    // Added A New DSApplicationStatus 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {DSApplicationStatus.StatusDescription} name  successfully",
                        Entity = "GetDSApplicationStatusById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "DSApplicationStatus Returned Successfully", false, DSApplicationStatus);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select DSApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }



        [HttpGet("GetAllDSApplicationStatus")]
        public async Task<IActionResult> GetAllDSApplicationStatus([FromQuery] string ActionBy)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(ActionBy.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var DSApplicationStatus = await _dsApplicationStatusRepository.GetDSApplicationStatus();

                if (DSApplicationStatus != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(ActionBy.ToString());


                    // Added A New DSApplicationStatus 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all PT Application Status  successfully",
                        Entity = "GetAllDSApplicationStatus",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "DSApplicationStatus Returned Successfully", false, DSApplicationStatus);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select DSApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }



        // Save DSApplicationStatus

        [HttpPost("SaveDSApplicationStatus")]
        public async Task<IActionResult> SaveDSApplicationStatus(DSApplicationStatusViewModel DSApplicationStatusViewModel)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(DSApplicationStatusViewModel.CreatedBy.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if DSApplicationStatus Exist 
                var checkCount = await _dsApplicationStatusRepository.CheckExistingDSApplicationStatus(DSApplicationStatusViewModel.StatusDescription);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

				// attempt to save
				Data.Entities.DSApplicationStatus.DSApplicationStatus content = new Data.Entities.DSApplicationStatus.DSApplicationStatus();
                content.StatusDescription = DSApplicationStatusViewModel.StatusDescription;
                content.DateCreated = DateTime.Now;
                content.CreatedBy = DSApplicationStatusViewModel.CreatedBy.ToString();
                content.IsActive = true;
                content.IsDeleted = false;


                var save = await _dsApplicationStatusRepository.SaveDSApplicationStatus(content);

                // get User Information
                 user = await _userManager.FindByIdAsync(DSApplicationStatusViewModel.CreatedBy.ToString());


                // Added A New DSApplicationStatus 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.StatusDescription}  DSApplicationStatus  successfully",
                    Entity = "DSApplicationStatusAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save DSApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateDSApplicationStatus")]
        public async Task<IActionResult> UpdateDSApplicationStatus(DSApplicationStatusViewModel DSApplicationStatusViewModel)
        {
			try
			{

                // Check if DSApplicationStatus Exist 
                var user = await _userManager.FindByIdAsync(DSApplicationStatusViewModel.CreatedBy.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var record = await _dsApplicationStatusRepository.GetDSApplicationStatusById(DSApplicationStatusViewModel.RoleId);

				if (record == null)
				{
					return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
				}

				// attempt to update the DSApplicationStatus


				record.StatusDescription = DSApplicationStatusViewModel.StatusDescription;
				record.LastUpdateDate = DateTime.Now;
				record.UpdatedBy = DSApplicationStatusViewModel.CreatedBy.ToString();
				record.IsActive = true;

				record.Id = DSApplicationStatusViewModel.RoleId;


				var save = await _dsApplicationStatusRepository.UpdateDSApplicationStatus(record);

				// get User Information
				 user = await _userManager.FindByIdAsync(DSApplicationStatusViewModel.CreatedBy.ToString());


				// log action
				await _auditTrailManager.AddAuditTrail(new AuditTrail
				{
					ActionTaken = AuditAction.Create,
					DateCreated = DateTime.Now,
					Description = $"User {user.FirstName + ' ' + user.LastName} update DSApplicationStatus {record}  DSApplicationStatus  successfully",
					Entity = "DSApplicationStatusUpdate",
					UserId = user.Id,
					UserName = user.UserName,
				});

				return PrepareResponse(HttpStatusCode.OK, WebApiMessage.UpdateRequest, false, record);
			}

			catch (Exception ex)
			{
				_logger.LogError(ex, "Update TMApplicationStatus", "");
				return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
			}
		}



        [HttpGet("DeleteDSApplicationStatus")]
        public async Task<IActionResult> DeleteDSApplicationStatus([FromQuery]string  DSApplicationStatusId, [FromQuery] string  UserId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(UserId.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                // Check if DSApplicationStatus Exist 
                var record = await _dsApplicationStatusRepository.GetDSApplicationStatusById(Convert.ToInt32(DSApplicationStatusId));

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the DSApplicationStatus
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(DSApplicationStatusId);


                var delete = await _dsApplicationStatusRepository.DeleteDSApplicationStatus(record);

                // get User Information
                 user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted DSApplicationStatus {record}  DSApplicationStatus  successfully",
                    Entity = "DSApplicationStatusDelete",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.DeleteRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete DSApplicationStatus", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}