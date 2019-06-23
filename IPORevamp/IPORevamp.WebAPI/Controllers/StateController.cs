
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
using IPORevamp.Data.Entities.Country;
using IPORevamp.Repository.Country;
using IPORevamp.Core.Utilities;
using IPORevamp.Repository.state;
using IPORevamp.Data.Entities;
using Newtonsoft.Json;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/State")]
    [ApiController]
    public class StateController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

       
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStateRepository _StateRepository;





        public StateController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,

            IStateRepository StateRepository,
        
            IStateRepository stateRepository,
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
            _StateRepository = StateRepository;


        }



        //[Authorize]
        /// <summary>
        /// This method will get State and related States
        /// </summary>
        /// <param name="StateId"></param>
        /// <param name="IncludeStates"></param>
        /// <param name="RequestById"></param>
        /// <returns></returns>
       
        [HttpPost("GetStateById/{StateId}")]
        public async Task<IActionResult> GetSingleStateById(int StateId, bool IncludeStates,string RequestById)
        {
            try
            {
               // check for user information before processing the request
               var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.OK, WebApiMessage.MissingUserInformation, true, null); ;
                }

                

            var State = await _StateRepository.GetStateById(StateId, IncludeStates);

            if (State != null)
            {

                // get User Information
              

                    // Added A New State 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {State.StateName} name  successfully",
                        Entity = "GetStateById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "State Returned Successfully", false, State);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select State", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

     



        [HttpPost("GetStateByName/{StateName}")]
        public async Task<IActionResult> GetSingleStateByName(string  StateName, bool IncludeStates, string RequestById)
        {
            try
            {
                // check for user information before processing the request
                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }




                var State = await _StateRepository.GetStateByName(StateName, IncludeStates);

                if (State != null)
                {

                    // get User Information
                      await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New State 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {State.StateName} name  successfully",
                        Entity = "GetStateById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "State Returned Successfully", false, State);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest,WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select State", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetStateByCountry")]
        public async Task<IActionResult> GetStateByCountry([FromQuery] string Countryid )
        {
            try
            {
                // check for user information before processing the request
              


                var State = await _StateRepository.GetStateByCountryId(Convert.ToInt32(Countryid));

                if (State != null)
                {

                    // get User Information
                  

                    return PrepareResponse(HttpStatusCode.OK, "State Returned Successfully", false, State);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.OK, "State Returned Successfully", false, State);
                  //  return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select State", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }



        [HttpGet("GetAllStates")]
        public async Task<IActionResult> GetAllStates([FromQuery] string RequestById)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];


                // check for user information before processing the request
                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, false, null); ;
                }

                var State = await _StateRepository.GetStates();

                if (State != null)
                {

                    // get User Information
                       await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New State 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all states  successfully",
                        Entity = "GetAllState",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "State Returned Successfully", false, State);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select State", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }



        // Save State

        [HttpPost("SaveState")]
        public async Task<IActionResult> SaveState(StateCountryViewModel StateViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];


                // check for user information before processing the request
                var user = await _userManager.FindByIdAsync(StateViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                // Check if State Exist 

                var checkCount = await _StateRepository.CheckExistingState(StateViewModel.StateName);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound, true, null);
                }

                // attempt to save
                State content = new State();
                content.StateName = StateViewModel.StateName;          
                content.DateCreated = DateTime.Now;
                content.CreatedBy = StateViewModel.CreatedBy.ToString();
                content.IsActive = true;       
                content.IsDeleted = false;
                content.CountryId = StateViewModel.CountryID;

                 var save = await _StateRepository.SaveState(content);

                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(StateViewModel.CreatedBy.ToString());


                // Added A New State 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Read,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.StateName}  State  successfully",
                    Entity = "StateAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save State", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateState")]
        public async Task<IActionResult> UpdateState(StateCountryViewModel StateViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                // Check if State Exist 
                // check for user information before processing the request
                var user = await _userManager.FindByIdAsync(StateViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, false, null); ;
                }


                var record = await _StateRepository.GetStateById(StateViewModel.StateId);
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, true, null);
                }

                // attempt to update the State
                record.DateCreated = DateTime.Now;
                record.CreatedBy = StateViewModel.CreatedBy.ToString();
                record.IsActive = true;
                record.StateName = StateViewModel.StateName;
                record.Id = StateViewModel.StateId;
                record.CountryId = StateViewModel.CountryID;

                var save = await _StateRepository.UpdateState(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(StateViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Update,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update State {record}  State  successfully",
                    Entity = "StateUpdate",
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
                _logger.LogError(ex, "Update State", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest,true);
            }
        }



        [HttpGet("DeleteState")]
        public async Task<IActionResult> DeleteState([FromQuery]String  StateId, [FromQuery] String UserId)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                // check for user information before processing the request
                var user = await _userManager.FindByIdAsync(UserId.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if State Exist 
                var record = await _StateRepository.GetStateById(Convert.ToInt32(StateId));
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, true, null);
                }

                // attempt to delete the State
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(StateId);


                var delete = await _StateRepository.DeleteState(record);
                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Delete,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted State {record}  State  successfully",
                    Entity = "StateDelete",
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
                _logger.LogError(ex, "Delete State", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}