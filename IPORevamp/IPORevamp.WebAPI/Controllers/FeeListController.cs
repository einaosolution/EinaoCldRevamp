
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
using IPORevamp.Data.Entities;
using IPORevamp.Repository.state;
using IPORevamp.Repository.Fee;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/FeeList")]
    [ApiController]
    public class FeeListController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFeeListRepository _FeeListrepository;

        public FeeListController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,
            IFeeListRepository FeeListrepository,
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
            _FeeListrepository = FeeListrepository;

        }



        //[Authorize]
        /// <summary>
        /// This method will get FeeList and related FeeLists
        /// </summary>
        /// <param name="FeeListId"></param>
        /// <param name="IncludeFeeLists"></param>
        /// <param name="RequestById"></param>
        /// <returns></returns>
       
        [HttpPost("GetFeeListById/{FeeListId}")]
        public async Task<IActionResult> GetSingleFeeListById(int FeeListId, string RequestById)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(RequestById.ToString()); 

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

       

                var FeeList = await _FeeListrepository.GetFeeListById(FeeListId);

            if (FeeList != null)
            {

                // get User Information
                 user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New FeeList 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {FeeList.ItemName} name  successfully",
                        Entity = "GetFeeListById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "FeeList Returned Successfully", false, FeeList);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select FeeList", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }

     



        [HttpPost("GetFeeListByName/{FeeListName}")]
        public async Task<IActionResult> GetSingleFeeListByName(string  FeeListName, string RequestById)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                var FeeList = await _FeeListrepository.GetFeeListByName(FeeListName);

                if (FeeList != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New FeeList 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {FeeList.ItemName} name  successfully",
                        Entity = "GetFeeListById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Fee List Returned Successfully", false, FeeList);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select FeeList", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }



        [HttpGet("GetAllFeeLists")]
        public async Task<IActionResult> GetAllFeeLists([FromQuery] string RequestById)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var FeeList = await _FeeListrepository.GetFeeLists();

                if (FeeList != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New FeeList 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all fee list  successfully",
                        Entity = "GetAllFeeList",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Fee List Returned Successfully", false, FeeList);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Fee List", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }


        [HttpPost("GetFeeListsByCategory")]
        public async Task<IActionResult> GetFeeListsByCategory( string RequestById, string category)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var FeeList = await _FeeListrepository.GetFeeListsByCategory(category);

                if (FeeList != null)
                {

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New FeeList 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all fee list  successfully",
                        Entity = "GetAllFeeList",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Fee List Returned Successfully", false, FeeList);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select FeeList", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }

        // Save FeeList

        [HttpPost("SaveFeeList")]
        public async Task<IActionResult> SaveFeeList(FeeListViewModel feeListViewModel)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(feeListViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }
                // Check if FeeList Exist 

                var checkCount = await _FeeListrepository.CheckExistingFeeList(feeListViewModel.ItemName);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to save
                Data.Entities.Fee.FeeList content = new Data.Entities.Fee.FeeList();
                content.ItemCode = feeListViewModel.ItemCode;
                content.ItemName = feeListViewModel.ItemName;
                content.QTCode = feeListViewModel.QTCode;
                content.TechnologyFee = feeListViewModel.TechnologyFee;
                content.Category = feeListViewModel.Category;
                content.Description = feeListViewModel.Description;
                content.init_amt = feeListViewModel.init_amt;
                content.DateCreated = DateTime.Now;
                content.CreatedBy = feeListViewModel.CreatedBy.ToString();
                content.IsActive = true;       
                content.IsDeleted = false;
      

                                var save = await _FeeListrepository.SaveFeeList(content);

                // get User Information
                 user = await _userManager.FindByIdAsync(feeListViewModel.CreatedBy.ToString());

                // Added A New FeeList 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Read,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.Description}  FeeList  successfully",
                    Entity = "FeeListAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save FeeList", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateFeeList")]
        public async Task<IActionResult> UpdateFeeList(FeeListViewModel feeListViewModel)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(feeListViewModel.CreatedBy.ToString()); 

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if FeeList Exist 

                var record = await _FeeListrepository.GetFeeListById(feeListViewModel.FeeId);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the FeeList
                record.ItemCode = feeListViewModel.ItemCode;
                record.ItemName = feeListViewModel.ItemName;
                record.QTCode = feeListViewModel.QTCode;
                record.TechnologyFee = feeListViewModel.TechnologyFee;
                record.Category = feeListViewModel.Category;
                record.Description = feeListViewModel.Description;
                record.init_amt = feeListViewModel.init_amt;
                record.LastUpdateDate = DateTime.Now;
                record.UpdatedBy = feeListViewModel.CreatedBy.ToString();
               


                var save = await _FeeListrepository.UpdateFeeList(record);

                // get User Information
                 user = await _userManager.FindByIdAsync(feeListViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Update,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update FeeList {record}  FeeList  successfully",
                    Entity = "FeeListUpdate",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.UpdateRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Update FeeList", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpGet("DeleteFeeList")]
        public async Task<IActionResult> DeleteFeeList([FromQuery] String  FeeListId, [FromQuery] String  UserId)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(UserId.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if FeeList Exist 
                var record = await _FeeListrepository.GetFeeListById(Convert.ToInt32(FeeListId));

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the FeeList
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(FeeListId);


                var delete = await _FeeListrepository.DeleteFeeList(record);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Delete,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted Fee List {record}  FeeList  successfully",
                    Entity = "FeeListDelete",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.DeleteRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete FeeList", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}