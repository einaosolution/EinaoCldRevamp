
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

using System.Net;
using AutoMapper;
using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Core.Utilities;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.Unit;

using IPORevamp.Data.UserManagement.Model;
using Newtonsoft.Json;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Unit")]
    [ApiController]
  
  
    public class UnitController : BaseController
    {
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPORevamp.Repository.Units.IunitRepository   _unitRepository;

        public UnitController(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager,SignInManager<ApplicationUser> signInManager,
IConfiguration configuration,IMapper mapper, ILogger<UnitController> logger,EmailEngine.Base.Repository.EmailRepository.IEmailManager<EmailLog, EmailTemplate> emailManager,
IPORevamp.Repository.Units.IunitRepository unitRepository,
IEmailSender emailsender,IHttpContextAccessor httpContextAccessor,IHostingEnvironment hostingEnvironment,IAuditTrailManager<AuditTrail> auditTrailManager) : base(
 userManager,
 signInManager,
 roleManager,
 configuration,
 mapper,
 logger,
 auditTrailManager

 )

        {
            _unitRepository = unitRepository;

        }



        [HttpGet("DeletUnit")]
        public async Task<IActionResult> DeletUnit([FromQuery]String ProductId, [FromQuery]String UserId)
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

                // Check if Country Exist 
                var record = await _unitRepository.GetUnitById(Convert.ToInt32(ProductId));
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the country

                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(ProductId);


                var delete = await _unitRepository.DeleteUnit(record);
                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted Unit {record}  unit  successfully",
                    Entity = "UnitDelete",
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
                _logger.LogError(ex, "Delete Unit", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }

        [HttpGet("GetAllUnit")]
        public async Task<IActionResult> GetAllUnit([FromQuery] string RequestById)
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


                var department = await _unitRepository.GetUnits();

                if (department != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all unit  successfully",
                        Entity = "GetAllunit",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "unit Returned Successfully", false, department);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select unit", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpPost("UpdateUnit")]
        public async Task<IActionResult> UpdateUnit(UnitViewModel unitview)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(unitview.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Country Exist 

                var record = await _unitRepository.GetUnitById(unitview.id);
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the country


             

                record.LastUpdateDate = DateTime.Now;
                record.UpdatedBy = unitview.CreatedBy.ToString();
                record.IsActive = true;
                record.Description = unitview.Description;
                record.Id = unitview.id;
                record.DepartmentId = unitview.department;


                var save = await _unitRepository.UpdateUnit(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(unitview.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update Unit {record} unit successfully",
                    Entity = "UnitUpdate",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress =ip ,
                    RecordBefore = json ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.UpdateRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Unit", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpPost("SaveUnit")]
        public async Task<IActionResult> SaveUnit(UnitViewModel unitview)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];


                var user = await _userManager.FindByIdAsync(unitview.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Country Exist 
                var checkCount = await _unitRepository.CheckExistingUnit(unitview.Description);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, "Unit  Exist", false, null);
                }

                // attempt to save
                Units content = new Units();
            
                content.Description= unitview.Description;
                content.DepartmentId = unitview.department;
                content.DateCreated = DateTime.Now;
                content.CreatedBy = unitview.CreatedBy.ToString();
                content.IsActive = true;

                content.IsDeleted = false;


                var save = await _unitRepository.SaveUnit(content);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(unitview.CreatedBy.ToString());


                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.Description}  Unit  successfully",
                    Entity = "UnitAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Unit", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


    }
}
