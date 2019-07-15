using AutoMapper;
using EmailEngine.Base.Entities;
using EmailEngine.Base.Repository.EmailRepository;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Core.Utilities;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.PreliminarySearch;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.PreliminarySearch;
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
    [Route("api/preliminary")]
    [ApiController]
    public class PrelimSearchController : BaseController
    {
        private readonly IpreliminarySearchRepository _prilimsearchRepository;

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;


        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PrelimSearchController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
    IEmailManager<EmailLog, EmailTemplate> emailManager,

    IpreliminarySearchRepository prelimsearchRepository,

    IEmailSender emailsender,
    IHttpContextAccessor httpContextAccessor,
 
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
            _prilimsearchRepository = prelimsearchRepository;


        }


        [HttpGet("GetPrelimSearch")]
        public async Task<IActionResult> GetPrelimSearch([FromQuery] string RequestById)
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


                var prelimsearch = await _prilimsearchRepository.GetPreliminarySearch();

                if (prelimsearch != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all ministry  successfully",
                        Entity = "GetAllprelim",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "prelim Returned Successfully", false, prelimsearch);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select prelimsearch", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }



        [HttpPost("SendUserEmail")]
        public async Task<IActionResult> SendUserEmail([FromForm] string RequestById , [FromForm] string userid, [FromForm] string EmailContent , [FromForm] string ID)
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
              //  var user2 = await _userManager.FindByIdAsync(userid); ;

               

              
                    await _emailsender.SendEmailAsync(userid, "Preliminary Search", EmailContent);

                    var save = await _prilimsearchRepository.GetPrelimById(Convert.ToInt32(ID));
                    save.status = "Treated";
                    await _prilimsearchRepository.UpdatePreliminary(save);


                    // get User Information
                    //  user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  processed preliminary search   successfully",
                        Entity = "prelimSearch",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "prelim Returned Successfully", false, "Success");

            
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select prelimsearch", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }



        [HttpPost("SavePrelimSearch")]
        public async Task<IActionResult> SavePrelimSearch(IPORevamp.Data.Entity.Interface.PreliminarySearch.PreliminarySearchView preliminarySearch)
        {
            try
            {

                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(preliminarySearch.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                // attempt to save
                PreliminarySearch content = new PreliminarySearch();
                try
                {
                    content.product = preliminarySearch.description ;
                }

                catch(Exception ee)
                {

                }
               
                content.DateCreated = DateTime.Now;
                content.first_name = preliminarySearch.first_name;
                content.CreatedBy = user.FirstName;
                content.last_name = preliminarySearch.last_name;
                content.payment_reference = preliminarySearch.payment_reference;
                content.sectorid = Convert.ToInt32(preliminarySearch.type);
                content.userid = preliminarySearch.userid;
                content.Email = preliminarySearch.UserEmail;
                content.status = "Submitted";
                content.IsActive = true;

                content.IsDeleted = false;


                var save = await _prilimsearchRepository.SavePreliminary(content);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
              //  user = await _userManager.FindByIdAsync(SectorViewModel.CreatedBy.ToString());


                // Added A New Sector 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new   Prelim Search  successfully",
                    Entity = "PreliminaryAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Prelim", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


    }
}
