using AutoMapper;
using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Core.Utilities;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.Ministry;
using IPORevamp.Data.UserManagement.Model;
using Microsoft.AspNetCore.Hosting;
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

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Ministry")]
    [ApiController]

    public class MinistryController : BaseController
    {
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPORevamp.Repository.Ministry.IministryRepository _ministryRepository;

        public MinistryController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager,
IConfiguration configuration, IMapper mapper, Microsoft.Extensions.Logging.ILogger<UnitController> logger, EmailEngine.Base.Repository.EmailRepository.IEmailManager<EmailLog, EmailTemplate> emailManager,
IPORevamp.Repository.Ministry.IministryRepository  ministryRepository, IEmailSender emailsender, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment, IAuditTrailManager<AuditTrail> auditTrailManager) : base(
 userManager,
 signInManager,
 roleManager,
 configuration,
 mapper,
 logger,
 auditTrailManager

 )

        {
            _ministryRepository = ministryRepository;

        }



        [HttpGet("DeleteMinistry")]
        public async Task<IActionResult> DeleteMinistry([FromQuery]String MinistryId, [FromQuery]String UserId)
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

                // Check if Ministry Exist 
                var record = await _ministryRepository.GetMinistryById(Convert.ToInt32(MinistryId));

                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the country

                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(MinistryId);


                var delete = await _ministryRepository.DeleteMinistry(record);
                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted Ministry {record}  Ministry  successfully",
                    Entity = "MinistryDelete",
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
                _logger.LogError(ex, "Delete Ministry", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }

        [HttpGet("GetAllMinistry")]
        public async Task<IActionResult> GetAllMinistry([FromQuery] string RequestById)
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


                var ministry = await _ministryRepository.GetMinistry();

                if (ministry != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all ministry  successfully",
                        Entity = "GetAllministry",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip 
                    });

                    return PrepareResponse(HttpStatusCode.OK, "ministry Returned Successfully", false, ministry);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select ministry", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpPost("UpdateMinistry")]
        public async Task<IActionResult> UpdateUnit(MinistryViewModel ministryview)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];
                var user = await _userManager.FindByIdAsync(ministryview.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if ministry Exist 

                var record = await _ministryRepository.GetMinistryById(ministryview.id);
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the country




                record.LastUpdateDate = DateTime.Now;
                record.UpdatedBy = ministryview.CreatedBy.ToString();
                record.IsActive = true;
                record.Description = ministryview.Description;
                record.Id = ministryview.id;


                var save = await _ministryRepository.UpdateUnit(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(ministryview.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update Ministry {record} Ministry successfully",
                    Entity = "MinistryUpdate",
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
                _logger.LogError(ex, "Update Ministry", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpPost("SaveMinistry")]
        public async Task<IActionResult> SaveProduct(MinistryViewModel ministryview)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(ministryview.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Country Exist 
                var checkCount = await _ministryRepository.CheckExistingMinistry(ministryview.Description);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, "Ministry  Exist", false, null);
                }

                // attempt to save
                Ministry content = new Ministry();

                content.Description = ministryview.Description;
                content.DateCreated = DateTime.Now;
                content.CreatedBy = ministryview.CreatedBy.ToString();
                content.IsActive = true;

                content.IsDeleted = false;


                var save = await _ministryRepository.SaveUnit(content);
                string json = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(ministryview.CreatedBy.ToString());


                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.Description}  Ministry  successfully",
                    Entity = "MinistryAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordAfter = json
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Ministry", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }




    }
}
