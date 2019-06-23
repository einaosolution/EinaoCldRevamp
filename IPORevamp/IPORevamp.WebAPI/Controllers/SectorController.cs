
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
using IPORevamp.Repository.Sector;
using IPORevamp.Core.Utilities;
using IPORevamp.Data.Entity.Interface.Entities.Sector;
using IPORevamp.Data.Entities;
using Newtonsoft.Json;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Sector")]
    [ApiController]
    public class SectorController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

      
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISectorRepository _sectorRepository;






        public SectorController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,

            ISectorRepository SectorRepository,
           
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
            _sectorRepository = SectorRepository;


        }



        //[Authorize]
        /// <summary>
        /// This method will get Sector and related States
        /// </summary>
        /// <param name="SectorId"></param>
        /// <param name="IncludeStates"></param>
        /// <param name="RequestById"></param>
        /// <returns></returns>
       
        [HttpPost("GetSectorById/{SectorId}")]
        public async Task<IActionResult> GetSingleSectorById(int SectorId,string RequestById)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var Sector = await _sectorRepository.GetSectorById(SectorId);

            if (Sector != null)
            {

                // get User Information
                 user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Sector 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {Sector.Description} name  successfully",
                        Entity = "GetSectorById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, Sector);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Sector", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }

     



        [HttpPost("GetSectorByName/{SectorName}")]
        public async Task<IActionResult> GetSingleSectorByName(string  SectorName, bool IncludeStates, string RequestById, bool IncludeLGAs)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var Sector = await _sectorRepository.GetSectorByName(SectorName);

                if (Sector != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Sector 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {Sector.Description} name  successfully",
                        Entity = "GetSectorById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Sector Returned Successfully", false, Sector);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Sector", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }



        [HttpGet("GetAllSectors")]
        public async Task<IActionResult> GetAllSector([FromQuery] string RequestById)
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

                var Sector = await _sectorRepository.GetSectors();

                if (Sector != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Sector 
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

                    return PrepareResponse(HttpStatusCode.OK, "Sector Returned Successfully", false, Sector);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Sector", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }



        // Save Sector

        [HttpPost("SaveSector")]
        public async Task<IActionResult> SaveSector(SectorViewModel SectorViewModel)
        {
            try
            {

                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(SectorViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                // Check if Sector Exist 
                var checkCount = await _sectorRepository.CheckExistingSector(SectorViewModel.Description);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to save
                Sector content = new Sector();
                content.Description = SectorViewModel.Description;
                content.DateCreated = DateTime.Now;
                content.Type = SectorViewModel.Type;
                content.CreatedBy = SectorViewModel.CreatedBy.ToString();
                content.IsActive = true;
               
                content.IsDeleted = false;


                var save = await _sectorRepository.SaveSector(content);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(SectorViewModel.CreatedBy.ToString());


                // Added A New Sector 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.Description}  Sector  successfully",
                    Entity = "SectorAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Sector", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateSector")]
        public async Task<IActionResult> UpdateSector(SectorViewModel SectorViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];
                // Check if Sector Exist 


                var user = await _userManager.FindByIdAsync(SectorViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var record = await _sectorRepository.GetSectorById(SectorViewModel.SectorId);

                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the Sector


                record.Description = SectorViewModel.Description;
                record.Type = SectorViewModel.Type;
                record.LastUpdateDate = DateTime.Now;
                record.UpdatedBy = SectorViewModel.CreatedBy.ToString();
                record.IsActive = true;
                record.Id = SectorViewModel.SectorId;
                

                 var save = await _sectorRepository.UpdateSector(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(SectorViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update Sector {record}  Sector  successfully",
                    Entity = "SectorUpdate",
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
                _logger.LogError(ex, "Update Sector", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpGet("DeleteSector")]
        public async Task<IActionResult> DeleteSector([FromQuery]String  SectorId, [FromQuery] String UserId)
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

                // Check if Sector Exist 
                var record = await _sectorRepository.GetSectorById(Convert.ToInt32(SectorId));
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the Sector
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(SectorId);


                var delete = await _sectorRepository.DeleteSector(record);
                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted Sector {record}  Sector  successfully",
                    Entity = "SectorDelete",
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
                _logger.LogError(ex, "Delete Sector", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}