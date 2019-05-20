
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
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entities.Setting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Authorization;

using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data.SetupViewModel;

using IPORevamp.Core.Utilities;
using IPORevamp.Repository.Email;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/EmailTemplate")]
    [ApiController]
    public class EmailTemplateController : BaseController
    {


       
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;






        public EmailTemplateController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,

            IEmailTemplateRepository EmailTemplateRepository,

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

            _emailsender = emailsender;
            _httpContextAccessor = httpContextAccessor;
            _EmailTemplateRepository = EmailTemplateRepository;


        }



        //[Authorize]
        /// <summary>
        /// This method will get Email Template and related States
        /// </summary>
        /// <param name="Email TemplateId"></param>
        /// <param name="IncludeStates"></param>
        /// <param name="RequestById"></param>
        /// <returns></returns>






        [HttpPost("GetEmailTemplateByName/{EmailTemplateCode}")]
        public async Task<IActionResult> GetSingleEmailTemplateByName(string EmailTemplateName, string RequestById)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var EmailTemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(EmailTemplateName);

                if (EmailTemplate != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Email Template 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {EmailTemplate.EmailSubject} name  successfully",
                        Entity = "GetEmail TemplateById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Email Template Returned Successfully", false, EmailTemplate);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select EmailTemplate", "");
                return PrepareResponse(HttpStatusCode.BadRequest, "No EmailTemplate was found");
            }
        }



        [HttpPost("GetAllEmailTemplates")]
        public async Task<IActionResult> GetAllEmailTemplates(string RequestById)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var EmailTemplate = await _EmailTemplateRepository.GetEmailTemplates();

                if (EmailTemplate != null)
                {

                    // get User Information
                  user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Email Template 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all email templates  successfully",
                        Entity = "GetAllEmailTemplates",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Email Template Returned Successfully", false, EmailTemplate);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Email Template", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }



        // Save Email Template

        [HttpPost("SaveEmailTemplate")]
        public async Task<IActionResult> SaveEmailTemplate(EmailTemplateViewModel EmailTemplateViewModel)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(EmailTemplateViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Email Template Exist 
                var checkCount = await _EmailTemplateRepository.CheckExistingEmailTemplate(EmailTemplateViewModel.EmailName);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to save
                EmailTemplate content = new EmailTemplate();
                content.CreatedBy = EmailTemplateViewModel.CreatedBy;
                content.DateCreated = DateTime.Now;
                content.EmailBody = EmailTemplateViewModel.EmailBody;
                content.EmailName = EmailTemplateViewModel.EmailName;
                content.EmailSender = EmailTemplateViewModel.EmailSender;
                content.EmailSubject = EmailTemplateViewModel.EmailSubject;

                content.IsActive = true;
                content.IsDeleted = false;


                var save = await _EmailTemplateRepository.SaveEmailTemplate(content);

                // get User Information
                user = await _userManager.FindByIdAsync(EmailTemplateViewModel.CreatedBy.ToString());


                // Added A New Email Template 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.EmailSubject}  Email Template  successfully",
                    Entity = "EmailTemplateAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content.EmailSubject);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save EmailTemplate", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateEmail Template/{EmailTemplate}")]
        public async Task<IActionResult> UpdateEmailTemplate(EmailTemplateViewModel EmailTemplateViewModel)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(EmailTemplateViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Email Template Exist 

                var record = await _EmailTemplateRepository.CheckExistingEmailTemplate(EmailTemplateViewModel.EmailCode);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the Email Template


                record.UpdatedBy = EmailTemplateViewModel.CreatedBy;
                record.LastUpdateDate = DateTime.Now;
                record.EmailBody = EmailTemplateViewModel.EmailBody;
                record.EmailName = EmailTemplateViewModel.EmailName;
                record.EmailSender = EmailTemplateViewModel.EmailSender;
                record.EmailSubject = EmailTemplateViewModel.EmailSubject;
                record.Id = EmailTemplateViewModel.EmailTemplateId;


                var save = await _EmailTemplateRepository.UpdateEmailTemplate(record);

                // get User Information
                 user = await _userManager.FindByIdAsync(EmailTemplateViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update Email Template {record}  Email Template  successfully",
                    Entity = "EmailTemplateUpdate",
                    UserId = user.Id,
                    UserName = user.UserName,
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.UpdateRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Update EmailTemplate", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }
    }
}