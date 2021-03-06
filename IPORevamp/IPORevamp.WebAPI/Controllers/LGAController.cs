﻿
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
using IPORevamp.Repository.LGA;
using IPORevamp.Data.Entities;
using IPORevamp.Repository.state;
using IPORevamp.Data.Entities.LGAs;
using Newtonsoft.Json;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/LGA")]
    [ApiController]
    public class LGAController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

   
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
      
      
        private readonly IStateRepository _stateRepository;
        private readonly ILGARepository _lgarepository;

        public LGAController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,
  
            ILGARepository lgarepository,
          
            IStateRepository StateRepository,
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
            _stateRepository = StateRepository;
            _lgarepository = lgarepository;

        }



        //[Authorize]
        /// <summary>
        /// This method will get LGA and related LGAs
        /// </summary>
        /// <param name="LGAId"></param>
        /// <param name="IncludeLGAs"></param>
        /// <param name="RequestById"></param>
        /// <returns></returns>
       
        [HttpPost("GetLGAById/{LGAId}")]
        public async Task<IActionResult> GetSingleLGAById(int LGAId, bool IncludeStates, string RequestById)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(RequestById.ToString()); 

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

       

                var LGA = await _lgarepository.GetLGAById(LGAId, IncludeStates);

            if (LGA != null)
            {

                // get User Information
                 user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New LGA 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {LGA.LGAName} name  successfully",
                        Entity = "GetLGAById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "LGA Returned Successfully", false, LGA);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select LGA", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetLGAByState")]
        public async Task<IActionResult> GetLGAByState([FromQuery] string State2)
        {
            try
            {
                // check for user information before processing the request



                var Lga2= await _lgarepository.GetLGAByState(Convert.ToInt32(State2));

                if (Lga2 != null)
                {

                    // get User Information


                    return PrepareResponse(HttpStatusCode.OK, "State Returned Successfully", false, Lga2);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.OK, "State Returned Successfully", false, Lga2);
                    //  return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select State", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }



        [HttpPost("GetLGAByName/{LGAName}")]
        public async Task<IActionResult> GetSingleLGAByName(string  LGAName, bool IncludeLGAs, string RequestById)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                var LGA = await _lgarepository.GetLGAByName(LGAName, IncludeLGAs);

                if (LGA != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New LGA 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {LGA.LGAName} name  successfully",
                        Entity = "GetLGAById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "LGA Returned Successfully", false, LGA);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select LGA", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }



        [HttpGet("GetAllLGAs")]
        public async Task<IActionResult> GetAllLGAs([FromQuery] string RequestById)
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

                var LGA = await _lgarepository.GetLGAs();

                if (LGA != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New LGA 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all countries  successfully",
                        Entity = "GetAllLGA",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "LGA Returned Successfully", false, LGA);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select LGA", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }



        // Save LGA

        [HttpPost("SaveLGA")]
        public async Task<IActionResult> SaveLGA(LGAStateViewModel lGAStateViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];


                var user = await _userManager.FindByIdAsync(lGAStateViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                // Check if LGA Exist 

              //  var checkCount = await _lgarepository.CheckExistingLGA(lGAStateViewModel.LGAName);

             //   if (checkCount != null)
             //   {
                  //  return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
              //  }

                // attempt to save
                Data.Entities.LGAs.LGA content = new Data.Entities.LGAs.LGA();
                content.LGAName = lGAStateViewModel.LGAName;          
                content.DateCreated = DateTime.Now;
                content.CreatedBy = lGAStateViewModel.CreatedBy.ToString();
                content.IsActive = true;       
                content.IsDeleted = false;
                content.StateId = lGAStateViewModel.StateId;
                content.CountryId = lGAStateViewModel.CountryId;

                                var save = await _lgarepository.SaveLGA(content);
                string json = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(lGAStateViewModel.CreatedBy.ToString());

                // Added A New LGA 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Read,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.LGAName}  LGA  successfully",
                    Entity = "LGAAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress= ip ,
                    RecordAfter = json 
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save LGA", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateLGA")]
        public async Task<IActionResult> UpdateLGA(LGAStateViewModel LGAViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(LGAViewModel.CreatedBy.ToString()); 

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if LGA Exist 

                var record = await _lgarepository.GetLGAById(LGAViewModel.LGAId);
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the LGA
                record.DateCreated = DateTime.Now;
                record.CreatedBy = LGAViewModel.CreatedBy.ToString();
                record.IsActive = true;
                record.LGAName = LGAViewModel.LGAName;
                record.Id = LGAViewModel.LGAId;
                record.StateId = LGAViewModel.StateId;
                record.CountryId = LGAViewModel.CountryId;

                var save = await _lgarepository.UpdateLGA(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(LGAViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Update,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update LGA {record}  LGA  successfully",
                    Entity = "LGAUpdate",
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
                _logger.LogError(ex, "Update LGA", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpGet("DeleteLGA")]
        public async Task<IActionResult> DeleteLGA([FromQuery]String  LGAId, [FromQuery]String UserId)
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

                // Check if LGA Exist 
                var record = await _lgarepository.GetLGAById(Convert.ToInt32(LGAId));
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the LGA
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(LGAId);


                var delete = await _lgarepository.DeleteLGA(record);

                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Delete,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted LGA {record}  LGA  successfully",
                    Entity = "LGADelete",
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
                _logger.LogError(ex, "Delete LGA", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}