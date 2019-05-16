
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
using IPORevamp.Repository.Event;
using IPORevamp.Data;
using IPORevamp.Data.TempModel;
using IPORevamp.Repository.Interface;
using IPORevamp.Repository.SystemSetup;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entities.Setting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Authorization;

using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data.SetupViewModel;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/ParameterSetup")]
    [ApiController]
    public class SetupController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

        private readonly ISettingRepository _settings;
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;







        public SetupController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,
            ISettingRepository settingrepository,
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
            _settings = settingrepository;
            _emailsender = emailsender;
            _httpContextAccessor = httpContextAccessor;



        }

        // <summary>
        /// Retrieve the employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired Employee</param>
        /// <returns>A string status</returns>
        /// 






        #region Country
        // This method is used for verification of account 
        [Authorize]
        [HttpPost("SaveUpdateCountry")]
        public async Task<IActionResult> SaveUpdateCountry(CountryViewModel countryViewModel)
        {
            var country = await _settings.GetCountryByName(countryViewModel.CountryName.ToUpper());

            if (country != null)
            {
                // attempt to save or update 
                Country buildCountry = new Country();
                buildCountry.Code = countryViewModel.CountryCode;
                buildCountry.EnableForOtherCountry = countryViewModel.EnableForOtherCountry;
                buildCountry.DateCreated = DateTime.Now;
                buildCountry.CreatedBy = "";
                buildCountry.IsActive = true;
                buildCountry.Name = countryViewModel.CountryName;
                buildCountry.Id = countryViewModel.CountryId;

                var AddOrSaveCountry = await _settings.SaveUpdateCountry(buildCountry);

                // get User Information
                var user = await _userManager.FindByIdAsync(countryViewModel.CreatedBy.ToString());

                if (countryViewModel.CountryId == 0 && AddOrSaveCountry.Id > 0)
                {
                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName} add a new {buildCountry.Name}  country  successfully",
                        Entity = "CountryAdded",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "New Country Added Successfully", false, buildCountry.Name);
                }
                else if (countryViewModel.CountryId > 0 && AddOrSaveCountry.Id > 0)
                {
                    // Update Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Update,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName} updated  {buildCountry.Name}  country  successfully",
                        Entity = "CountryUpdate",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Update Country " + buildCountry.Name + " Successfully", false, buildCountry.Name);
                }
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, "The country information was not saved");
            }

            return PrepareResponse(HttpStatusCode.OK, "d", false, "");
        }


        [Authorize]
        [HttpPost("LoadAllCountries")]
        public async Task<IActionResult> GetAllCountries(bool OnlyActive, int ActionBy)
        {

            var country = await _settings.GetCountries();

            if (country != null)
            {

                if (OnlyActive == true)
                {
                    country = country.ToList().Where(a => a.IsActive == true).ToList();
                }
                else
                {
                    country = country.ToList().Where(a => a.IsActive == false).ToList();
                }

                var countryList = _mapper.Map<List<Country>>(country);

                // get User Information
                var user = await _userManager.FindByIdAsync(ActionBy.ToString());


                // check if country list is empty
                if (countryList.Count() > 0)
                {
                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  loaded all countries successfully",
                        Entity = "CountryAllCountry",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Country Returned Successfully", false, countryList);
                }
                else
                {
                    return PrepareResponse(HttpStatusCode.OK, "No Country Returned", false, false);
                }
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, "No country was found");
            }

        }


        [Authorize]
        [HttpPost("GetCountryById")]
        public async Task<IActionResult> GetSingleCountry(int CountryId, int ActionBy)
        {

            var country = await _settings.GetCountryById(CountryId);

            if (country != null)
            {



                var countryList = _mapper.Map<Country>(country);

                // get User Information
                var user = await _userManager.FindByIdAsync(ActionBy.ToString());


                // check if country list is empty
                if (countryList != null)
                {
                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {countryList.Name} name  successfully",
                        Entity = "CountrySingleCountry",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Country Returned Successfully", false, countryList);
                }
                else
                {
                    return PrepareResponse(HttpStatusCode.OK, "No Country Returned", false, false);
                }
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, "No country was found");
            }

        }
        #endregion

        #region States
        // This method is used save and update state
        [Authorize]
        [HttpPost("SaveUpdateState")]
        public async Task<IActionResult> SaveUpdateState(StateCountryViewModel stateCountryViewModel)
        {
            var state = await _settings.GetStatesByName(stateCountryViewModel.StateName);

            if (state != null)
            {
                // attempt to save or update 
                State buildList = new State();
                buildList.Country.Id = stateCountryViewModel.CountryID;
                buildList.StateName = stateCountryViewModel.StateName;


                buildList.DateCreated = DateTime.Now;
                buildList.CreatedBy = stateCountryViewModel.CreatedBy.ToString();
                buildList.IsActive = true;
                buildList.Id = stateCountryViewModel.StateId;

                var AddOrSaveState = await _settings.SaveState(buildList);

                // get User Information
                var user = await _userManager.FindByIdAsync(stateCountryViewModel.CreatedBy.ToString());

                if (stateCountryViewModel.StateId == 0 && AddOrSaveState.Id > 0)
                {
                    // Added A New State 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName} add a new {buildList.StateName}  state  successfully",
                        Entity = "StateAdded",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "New State Added Successfully", false, buildList.StateName);
                }
                else if (stateCountryViewModel.StateId > 0 && AddOrSaveState.Id > 0)
                {
                    // Update Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Update,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName} updated  {buildList.StateName}  state  successfully",
                        Entity = "StateUpdate",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Update state " + buildList.StateName + " Successfully", false, buildList.StateName);
                }
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, "The state information was not saved");
            }

            return PrepareResponse(HttpStatusCode.BadRequest, "Bad Request", false, "");
        }


        [Authorize]
        [HttpPost("LoadAllStates")]
        public async Task<IActionResult> GetAllStates(bool OnlyActive, int ActionBy, int CountryId)
        {

            var states = await _settings.GetStatesByCountryId(CountryId);

            
            if (states != null)
            {

                if (OnlyActive == true)
                {
                    states = states.ToList().Where(a => a.IsActive == true).ToList();
                }
                else
                {
                    states = states.ToList().Where(a => a.IsActive == false).ToList();
                }

                var stateList = _mapper.Map<List<State>>(states);

                // get User Information
                var user = await _userManager.FindByIdAsync(ActionBy.ToString());


                // check if state list is empty
                if (stateList.Count() > 0)
                {
                    // Added A New State 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  loaded all states successfully",
                        Entity = "CountryAllState",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "State Returned Successfully", false, stateList);
                }
                else
                {
                    return PrepareResponse(HttpStatusCode.OK, "No State Returned", false, false);
                }
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, "No State was found");
            }

        }


        [Authorize]
        [HttpPost("LoadSingleState")]
        public async Task<IActionResult> GetSingleState(int StateId, int ActionBy)
        {

            var state = await _settings.GetStatesById(StateId);

            if (state != null)
            {

                var stateInfo = _mapper.Map<State>(state);
                // get User Information
                var user = await _userManager.FindByIdAsync(ActionBy.ToString());


                // check if country list is empty
                if (stateInfo != null)
                {
                    // Added 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {stateInfo.StateName}   successfully",
                        Entity = "GetSingleState",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "State Returned Successfully", false, stateInfo);
                }
                else
                {
                    return PrepareResponse(HttpStatusCode.OK, "No State Returned", false, false);
                }
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, "No State was found");
            }

        }
        #endregion

        // Email Template 
        #region Email Template
        // This method is used save and update email template
        [Authorize]
        [HttpPost("SaveUpdateEmailTemplate")]
        public async Task<IActionResult> SaveUpdateEmailTemplate(EmailTemplateViewModel emailTemplateViewModel)
        {
            var state = await _settings.GetEmailTemplateByCode(emailTemplateViewModel.EmailCode);

            if (state != null)
            {
                // attempt to save or update 


                EmailTemplate buildList = new EmailTemplate();
                buildList.Id = emailTemplateViewModel.EmailTemplateId;
                buildList.CreatedBy = emailTemplateViewModel.CreatedBy;
                buildList.EmailBody = emailTemplateViewModel.EmailBody;
                buildList.CreatedBy = emailTemplateViewModel.CreatedBy;
                buildList.Id = emailTemplateViewModel.EmailTemplateId;
                buildList.EmailName = emailTemplateViewModel.EmailName;
                buildList.EmailSender = emailTemplateViewModel.EmailSender;
                buildList.EmailSubject = emailTemplateViewModel.EmailSubject;
                buildList.EmailTemplateType = 0;
                buildList.IsActive = emailTemplateViewModel.IsActive;
                buildList.IsDeleted = emailTemplateViewModel.IsDeleted;
                buildList.UpdatedBy = emailTemplateViewModel.UpdatedBy;
              
                buildList.DateCreated = DateTime.Now;
                buildList.CreatedBy = emailTemplateViewModel.CreatedBy.ToString();
                buildList.IsActive = true;
                buildList.Id = emailTemplateViewModel.EmailTemplateId;

                var AddOrSave= await _settings.SaveUpdateEmailTemplate(buildList);

                // get User Information
                var user = await _userManager.FindByIdAsync(emailTemplateViewModel.CreatedBy.ToString());

                if (emailTemplateViewModel.EmailTemplateId == 0 && AddOrSave.Id > 0)
                {
                    // Added A New Email Template 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName} add a new  template {buildList.EmailSubject}  state  successfully",
                        Entity = "EmaiTemplateAdded",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "New Email Template Added Successfully", false, buildList.EmailSubject);
                }
                else if (emailTemplateViewModel.EmailTemplateId > 0 && AddOrSave.Id > 0)
                {
                    // Update Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Update,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName} updated  {buildList.EmailSubject}  template  successfully",
                        Entity = "TemplateUpdate",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Update Email Template " + buildList.EmailSubject + " Successfully", false, buildList.EmailSubject);
                }
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, "The Email Template information was not saved");
            }

            return PrepareResponse(HttpStatusCode.BadRequest, "Bad Request", false, "");
        }


     

       
        #endregion

    }
}