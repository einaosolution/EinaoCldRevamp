using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EmailEngine.Base.Repository.EmailRepository;
using EmailEngine.Repository.EmailRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IPORevamp.WebAPI.Controllers;
using IPORevamp.WebAPI.Models;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.Event;


namespace NACC.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/audit")]
    [Authorize]
    public class AuditController : BaseController
    {        
        public AuditController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper,
            ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,
            IAuditTrailManager<AuditTrail> auditTrailManager,
            IEventRepository eventRepository
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
            
        }


        [HttpGet("all")]
        public async Task<IActionResult> All()
        {
            var all = await _auditTrailManager.GetAuditTrails();
            var allvm=_mapper.Map<List<AuditVm>>(all);
            
            if(allvm.Count > 0)
            {
                return PrepareResponse(HttpStatusCode.OK, "Records Found", false, allvm);
            }
            return PrepareResponse(HttpStatusCode.NotFound, "No records Found", true, null);
        }
    }
}