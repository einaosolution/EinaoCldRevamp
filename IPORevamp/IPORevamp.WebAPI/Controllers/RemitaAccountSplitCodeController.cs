
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
using IPORevamp.Repository.RemitaAccountSplit;
using IPORevamp.Core.Utilities;

using IPORevamp.Data.Entities;
using Newtonsoft.Json;
using IPORevamp.Data.Entity.Interface.Entities.RemitaSplitAccount;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/RemitaAccountSplit")]
    [ApiController]
    public class RemitaAccountSplitController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

      
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRemitaAccountSplitRepository _RemitaAccountSplitRepository;






        public RemitaAccountSplitController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,

            IRemitaAccountSplitRepository RemitaAccountSplitRepository,
           
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
            _RemitaAccountSplitRepository = RemitaAccountSplitRepository;


        }



        //[Authorize]
        /// <summary>
        /// This method will get RemitaAccountSplit and related States
        /// </summary>
        /// <param name="RemitaAccountSplitId"></param>
        /// <param name="IncludeStates"></param>
        /// <param name="RequestById"></param>
        /// <returns></returns>
       
        [HttpPost("GetRemitaAccountSplitById/{RemitaAccountSplitId}")]
        public async Task<IActionResult> GetSingleRemitaAccountSplitById(int RemitaAccountSplitId,string RequestById)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var RemitaAccountSplit = await _RemitaAccountSplitRepository.GetRemitaAccountSplitById(RemitaAccountSplitId);

            if (RemitaAccountSplit != null)
            {

                // get User Information
                 user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New RemitaAccountSplit 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {RemitaAccountSplit.BeneficiaryName} name  successfully",
                        Entity = "GetRemitaAccountSplitById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, RemitaAccountSplit);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select RemitaAccountSplit", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }

     



        [HttpPost("GetRemitaAccountSplitByName/{RemitaAccountSplitName}")]
        public async Task<IActionResult> GetSingleRemitaAccountSplitByName(string  RemitaAccountSplitName, bool IncludeStates, string RequestById, bool IncludeLGAs)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var RemitaAccountSplit = await _RemitaAccountSplitRepository.GetRemitaAccountSplitByName(RemitaAccountSplitName);

                if (RemitaAccountSplit != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New RemitaAccountSplit 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {RemitaAccountSplit.BeneficiaryName} name  successfully",
                        Entity = "GetRemitaAccountSplitById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "RemitaAccountSplit Returned Successfully", false, RemitaAccountSplit);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Remita Account Split", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }



        [HttpGet("GetAllRemitaAccountSplits")]
        public async Task<IActionResult> GetAllRemitaAccountSplit([FromQuery] string RequestById)
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

                var RemitaAccountSplit = await _RemitaAccountSplitRepository.GetRemitaAccountSplits();

                if (RemitaAccountSplit != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New RemitaAccountSplit 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all Remita Account Split  successfully",
                        Entity = "GetAllRemitaAccountSplit",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Remita Account Split Returned Successfully", false, RemitaAccountSplit);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select RemitaAccountSplit", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }



        // Save RemitaAccountSplit

        [HttpPost("SaveRemitaAccountSplit")]
        public async Task<IActionResult> SaveRemitaAccountSplit(RemitaAccountSplitViewModel RemitaAccountSplitViewModel)
        {
            try
            {

                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RemitaAccountSplitViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                // Check if RemitaAccountSplit Exist 
                var checkCount = await _RemitaAccountSplitRepository.CheckExistingRemitaAccountSplit((RemitaAccountSplitViewModel.RemitaAccountSplitId).ToString());

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to save
                RemitaAccountSplit content = new RemitaAccountSplit();
                content.BeneficiaryAccount = RemitaAccountSplitViewModel.BeneficiaryAccount;
                content.DateCreated = DateTime.Now;
                content.BeneficiaryBank = RemitaAccountSplitViewModel.BeneficiaryBank;
                content.BeneficiaryName = RemitaAccountSplitViewModel.BeneficiaryName;
                content.DeductFee = RemitaAccountSplitViewModel.DeductFee;
                content.CreatedBy = RemitaAccountSplitViewModel.CreatedBy.ToString();
                content.IsActive = true;
               
                content.IsDeleted = false;


                var save = await _RemitaAccountSplitRepository.SaveRemitaAccountSplit(content);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(RemitaAccountSplitViewModel.CreatedBy.ToString());


                // Added A New RemitaAccountSplit 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.BeneficiaryName}  Remita Account Split  successfully",
                    Entity = "RemitaAccountSplitAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Remita Account Split", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateRemitaAccountSplit")]
        public async Task<IActionResult> UpdateRemitaAccountSplit(RemitaAccountSplitViewModel RemitaAccountSplitViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];
                // Check if RemitaAccountSplit Exist 


                var user = await _userManager.FindByIdAsync(RemitaAccountSplitViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var record = await _RemitaAccountSplitRepository.GetRemitaAccountSplitById(RemitaAccountSplitViewModel.RemitaAccountSplitId);

                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the RemitaAccountSplit


                record.BeneficiaryAccount = RemitaAccountSplitViewModel.BeneficiaryAccount;

                record.BeneficiaryBank = RemitaAccountSplitViewModel.BeneficiaryBank;
                record.BeneficiaryName = RemitaAccountSplitViewModel.BeneficiaryName;
                record.DeductFee = RemitaAccountSplitViewModel.DeductFee;
                record.LastUpdateDate = DateTime.Now;
                record.UpdatedBy = RemitaAccountSplitViewModel.CreatedBy.ToString();
                record.IsActive = true;
                record.Id = RemitaAccountSplitViewModel.RemitaAccountSplitId;
                

                 var save = await _RemitaAccountSplitRepository.UpdateRemitaAccountSplit(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(RemitaAccountSplitViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update Remita Account Split {record}  Remita Account Split  successfully",
                    Entity = "RemitaAccountSplitUpdate",
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
                _logger.LogError(ex, "Update Remita Account Split", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpGet("DeleteRemitaAccountSplit")]
        public async Task<IActionResult> DeleteRemitaAccountSplit([FromQuery]String  RemitaAccountSplitId, [FromQuery] String UserId)
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

                // Check if RemitaAccountSplit Exist 
                var record = await _RemitaAccountSplitRepository.GetRemitaAccountSplitById(Convert.ToInt32(RemitaAccountSplitId));
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the RemitaAccountSplit
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(RemitaAccountSplitId);


                var delete = await _RemitaAccountSplitRepository.DeleteRemitaAccountSplit(record);
                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted Remita Account Split {record}  Remita Account Split  successfully",
                    Entity = "RemitaAccountSplitDelete",
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
                _logger.LogError(ex, "Delete Remita Account Split", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}