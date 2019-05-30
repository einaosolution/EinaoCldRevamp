
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

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Country")]
    [ApiController]
    public class CountryController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

      
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICountryRepository _countryRepository;






        public CountryController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,
         
            ICountryRepository countryRepository,
           
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
            _countryRepository = countryRepository;


        }



        //[Authorize]
        /// <summary>
        /// This method will get country and related States
        /// </summary>
        /// <param name="CountryId"></param>
        /// <param name="IncludeStates"></param>
        /// <param name="RequestById"></param>
        /// <returns></returns>
       
        [HttpPost("GetCountryById/{CountryId}")]
        public async Task<IActionResult> GetSingleCountryById(int CountryId, bool IncludeStates,string RequestById,bool IncludeLGAs)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var country = await _countryRepository.GetCountryById(CountryId, IncludeStates, IncludeLGAs);

            if (country != null)
            {

                // get User Information
                 user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {country.Name} name  successfully",
                        Entity = "GetCountryById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Country Returned Successfully", false, country);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Country", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }

     



        [HttpPost("GetCountryByName/{CountryName}")]
        public async Task<IActionResult> GetSingleCountryByName(string  CountryName, bool IncludeStates, string RequestById, bool IncludeLGAs)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }



                var country = await _countryRepository.GetCountryByName(CountryName, IncludeStates, IncludeLGAs);

                if (country != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {country.Name} name  successfully",
                        Entity = "GetCountryById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Country Returned Successfully", false, country);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Country", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }



        [HttpGet("GetAllCountries")]
        public async Task<IActionResult> GetAllCountries([FromQuery] string RequestById)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var country = await _countryRepository.GetCountries();

                if (country != null)
                {

                    // get User Information
                      user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all countries  successfully",
                        Entity = "GetAllCountries",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Country Returned Successfully", false, country);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Country", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }



        // Save Country

        [HttpPost("SaveCountry")]
        public async Task<IActionResult> SaveCountry(CountryViewModel countryViewModel)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(countryViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Country Exist 
                var checkCount = await _countryRepository.CheckExistingCountry(countryViewModel.CountryName);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to save
                Country content = new Country();
                content.Code = countryViewModel.CountryCode;
                content.EnableForOtherCountry = countryViewModel.EnableForOtherCountry;
                content.DateCreated = DateTime.Now;
                content.CreatedBy = countryViewModel.CreatedBy.ToString();
                content.IsActive = true;
                content.Name = countryViewModel.CountryName;
                content.IsDeleted = false;


                var save = await _countryRepository.SaveCountry(content);

                // get User Information
                 user = await _userManager.FindByIdAsync(countryViewModel.CreatedBy.ToString());


                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.Name}  country  successfully",
                    Entity = "CountryAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Country", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateCountry")]
        public async Task<IActionResult> UpdateCountry(CountryViewModel countryViewModel)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(countryViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Country Exist 

                var record = await _countryRepository.GetCountryById(countryViewModel.CountryId);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the country


                record.Code = countryViewModel.CountryCode;
                record.EnableForOtherCountry = countryViewModel.EnableForOtherCountry;
                record.LastUpdateDate = DateTime.Now;
                record.UpdatedBy = countryViewModel.CreatedBy.ToString();
                record.IsActive = true;
                record.Name = countryViewModel.CountryName;
                record.Id = countryViewModel.CountryId;
               

                var save = await _countryRepository.UpdateCountry(record);

                // get User Information
                 user = await _userManager.FindByIdAsync(countryViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update country {record}  country  successfully",
                    Entity = "CountryUpdate",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.UpdateRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Country", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpGet("DeleteCountry")]
        public async Task<IActionResult> DeleteCountry([FromQuery]String  CountryId, [FromQuery]String  UserId)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(UserId.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Country Exist 
                var record = await _countryRepository.GetCountryById(Convert.ToInt32(CountryId));

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the country
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(CountryId);


                var delete = await _countryRepository.DeleteCountry(record);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted country {record}  country  successfully",
                    Entity = "CountryDelete",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.DeleteRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete Country", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}