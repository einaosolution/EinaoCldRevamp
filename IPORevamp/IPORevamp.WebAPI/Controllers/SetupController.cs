
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

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/ParameterSetup")]
    [ApiController]
    public class SetupController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

  
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;







        public SetupController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,
    
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



        }

        // <summary>
        /// Retrieve the employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired Employee</param>
        /// <returns>A string status</returns>
        /// 






        #region Country
        // This metho      
        //[HttpPost("SaveUpdateCountry")]
        //public async Task<IActionResult> SaveUpdateCountry(CountryViewModel countryViewModel)
        //{



        //    // attempt to save or update 
        //    Data.Entities.Country.Country buildCountry = new Data.Entities.Country.Country();
        //        buildCountry.Code = countryViewModel.CountryCode;
        //        buildCountry.EnableForOtherCountry = countryViewModel.EnableForOtherCountry;
        //        buildCountry.DateCreated = DateTime.Now;
        //        buildCountry.CreatedBy = countryViewModel.CreatedBy.ToString();
        //        buildCountry.IsActive = true;
        //        buildCountry.Name = countryViewModel.CountryName;
        //        buildCountry.Id = countryViewModel.CountryId;

        //        var AddOrSaveCountry = await _settings.SaveUpdateCountry(buildCountry);

        //        // get User Information
        //        var user = await _userManager.FindByIdAsync(countryViewModel.CreatedBy.ToString());

        //    if (countryViewModel.CountryId == 0 && AddOrSaveCountry.Id > 0)
        //    {
        //        // Added A New Country 
        //        await _auditTrailManager.AddAuditTrail(new AuditTrail
        //        {
        //            ActionTaken = AuditAction.Create,
        //            DateCreated = DateTime.Now,
        //            Description = $"User {user.FirstName + ' ' + user.LastName} add a new {buildCountry.Name}  country  successfully",
        //            Entity = "CountryAdded",
        //            UserId = user.Id,
        //            UserName = user.UserName,
        //        });

        //        return PrepareResponse(HttpStatusCode.OK, "New Country Added Successfully", false, buildCountry.Name);
        //    }
        //    else if (countryViewModel.CountryId > 0 && AddOrSaveCountry.Id > 0)
        //    {
        //        // Update Country 
        //        await _auditTrailManager.AddAuditTrail(new AuditTrail
        //        {
        //            ActionTaken = AuditAction.Update,
        //            DateCreated = DateTime.Now,
        //            Description = $"User {user.FirstName + ' ' + user.LastName} updated  {buildCountry.Name}  country  successfully",
        //            Entity = "CountryUpdate",
        //            UserId = user.Id,
        //            UserName = user.UserName,
        //        });

        //        return PrepareResponse(HttpStatusCode.OK, "Update Country " + buildCountry.Name + " Successfully", false, buildCountry.Name);
        //    }
        //    else
        //    {

        //        return PrepareResponse(HttpStatusCode.BadRequest, "Bad Request", false, "");
        //    }
        //}
            

           
        


        //[Authorize]
        //[HttpPost("LoadAllCountries")]
        //public async Task<IActionResult> GetAllCountries(bool OnlyActive, int RequestById)
        //{

        //    var country = await _settings.GetCountries();

        //    if (country != null)
        //    {

        //        if (OnlyActive == true)
        //        {
        //            country = country.ToList().Where(a => a.IsActive == true).ToList();
        //        }
        //        else
        //        {
        //            country = country.ToList().Where(a => a.IsActive == false).ToList();
        //        }

        //        var countryList = _mapper.Map<List<Country>>(country);

        //        // get User Information
        //        var user = await _userManager.FindByIdAsync(RequestById.ToString());


        //        // check if country list is empty
        //        if (countryList.Count() > 0)
        //        {
        //            // Added A New Country 
        //            await _auditTrailManager.AddAuditTrail(new AuditTrail
        //            {
        //                ActionTaken = AuditAction.Create,
        //                DateCreated = DateTime.Now,
        //                Description = $"User {user.FirstName + ' ' + user.LastName}  loaded all countries successfully",
        //                Entity = "CountryAllCountry",
        //                UserId = user.Id,
        //                UserName = user.UserName,
        //            });

        //            return PrepareResponse(HttpStatusCode.OK, "Country Returned Successfully", false, countryList);
        //        }
        //        else
        //        {
        //            return PrepareResponse(HttpStatusCode.OK, "No Country Returned", false, false);
        //        }
        //    }
        //    else
        //    {
        //        return PrepareResponse(HttpStatusCode.BadRequest, "No country was found");
        //    }

        //}


        //[Authorize]
        //[HttpPost("GetCountryById")]
        //public async Task<IActionResult> GetSingleCountry(int CountryId, int RequestById)
        //{

        //    var country = await _settings.GetCountryById(CountryId);

        //    if (country != null)
        //    {



        //        var countryList = _mapper.Map<Country>(country);

        //        // get User Information
        //        var user = await _userManager.FindByIdAsync(RequestById.ToString());


        //        // check if country list is empty
        //        if (countryList != null)
        //        {
        //            // Added A New Country 
        //            await _auditTrailManager.AddAuditTrail(new AuditTrail
        //            {
        //                ActionTaken = AuditAction.Create,
        //                DateCreated = DateTime.Now,
        //                Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {countryList.Name} name  successfully",
        //                Entity = "CountrySingleCountry",
        //                UserId = user.Id,
        //                UserName = user.UserName,
        //            });

        //            return PrepareResponse(HttpStatusCode.OK, "Country Returned Successfully", false, countryList);
        //        }
        //        else
        //        {
        //            return PrepareResponse(HttpStatusCode.OK, "No Country Returned", false, false);
        //        }
        //    }
        //    else
        //    {
        //        return PrepareResponse(HttpStatusCode.BadRequest, "No country was found");
        //    }

        //}
        #endregion

      

       

    }
}