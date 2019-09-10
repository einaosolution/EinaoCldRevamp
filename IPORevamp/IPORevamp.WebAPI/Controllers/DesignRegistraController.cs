using AutoMapper;
using EmailEngine.Base.Entities;
using EmailEngine.Base.Repository.EmailRepository;
using EmailEngine.Repository.EmailRepository;

using IPORevamp.Core.Utilities;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.MarkInfo;
using IPORevamp.Data.Entity.Interface.Entities.Pwallet;
using IPORevamp.Data.Entity.Interface.PreliminarySearch;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.FileNewApplication;
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
using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data;
using System.IO;

using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using IPORevamp.Repository.DesignNewpplication;
using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.DesignAssigment;
using IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignInformation;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;
using IPORevamp.Repository.DesignRegistra;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/DesignRegistra")]
    [ApiController]
    public class DesignRegistraController : BaseController
    {
        private readonly IDesignRegistraApplication _designRegistraRepository;
        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly Repository.Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        private readonly IPOContext _contex;
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DesignRegistraController(
  UserManager<ApplicationUser> userManager,
  RoleManager<ApplicationRole> roleManager,
  SignInManager<ApplicationUser> signInManager,
  IConfiguration configuration,
  IMapper mapper, ILogger<UserController> logger,
  IPOContext contex,
  IEmailManager<EmailLog, EmailTemplate> emailManager,
    Repository.Email.IEmailTemplateRepository EmailTemplateRepository,
     IDesignRegistraApplication designRegistraRepository,




  IEmailSender emailsender,
  IHttpContextAccessor httpContextAccessor,
  IFileHandler fileUploadRespository,

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
            //  _httpContextAccessor = httpContextAccessor;
            //_patentTypeRepository = patentTypeRepository;

            _contex = contex;
            _designRegistraRepository = designRegistraRepository;
            _fileUploadRespository = fileUploadRespository;
            _EmailTemplateRepository = EmailTemplateRepository;


        }


        [HttpGet("GetDesignFreshApplication")]
        public async Task<IActionResult> GetDesignFreshApplication([FromQuery] string RequestById)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }



            var details = _designRegistraRepository.GetDesignFreshApplication();



            if (details != null)
            {

                // get User Information
                user = await _userManager.FindByIdAsync(RequestById.ToString());

                // Added A New Country 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all fresh design successfully",
                    Entity = "GetAllFreshDesign",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip
                });

                return PrepareResponse(HttpStatusCode.OK, "Fresh Design Returned Successfully", false, details.Result);

            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }









        }

    }
}
