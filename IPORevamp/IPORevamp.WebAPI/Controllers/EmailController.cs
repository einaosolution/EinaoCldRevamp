using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EmailEngine.Base.Entities;
using EmailEngine.Base.Repository.EmailRepository;
using EmailEngine.Repository.EmailRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IPORevamp.WebAPI.Models;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.Event;

namespace IPORevamp.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : BaseController
    {
        IEmailManager<EmailLog, EmailTemplate> _emailManager;
        
        public EmailController(UserManager<ApplicationUser> userManager,
          RoleManager<ApplicationRole> roleManager,
          SignInManager<ApplicationUser> signInManager,
          IConfiguration configuration,
          IMapper mapper,
          ILogger<EventController> logger,
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
              auditTrailManager,
               eventRepository
              )
        {
            _emailManager = emailManager;
        }



        [Authorize(Roles ="Admin")]
        [HttpGet("templates")]
        public IActionResult FetchEmailTemplates()
        {

            var templatesWithoutEvents = _emailManager.GetEmailTemplates(x => x.EmailBody == null).ToList();
            var respEmailTemplate = _mapper.Map<List<EmailTemplateVM>>(templatesWithoutEvents);
            if (respEmailTemplate.Count > 0)
            {
                return PrepareResponse(HttpStatusCode.OK, $"{respEmailTemplate.Count} templates found", false, respEmailTemplate);
            }

            return PrepareResponse(HttpStatusCode.NotFound, "No Email Template Found");
        }


        [HttpGet("templates/{eventId}/event")]
        public IActionResult FetchEmailTemplates(int eventId)
        {

            var emailTemplate = _emailManager.GetEmailTemplates(x => x.Id == eventId).ToList();
            var respEmailTemplate = _mapper.Map<List<EmailTemplateVM>>(emailTemplate);
            if (respEmailTemplate.Count > 0)
            {
                return PrepareResponse(HttpStatusCode.OK, $"{respEmailTemplate.Count} templates found", false, respEmailTemplate);
            }

            return PrepareResponse(HttpStatusCode.NotFound, "No Email Template Found");
        }

        [HttpGet("template/{templateId}")]
        public IActionResult FetchEmailTemplate(int templateId)
        {
            var eventEmailTemplate = _emailManager.GetEmailTemplates(x => x.Id == templateId).FirstOrDefault();
            if (eventEmailTemplate != null)
            {
                return PrepareResponse(HttpStatusCode.OK, "template found", false, _mapper.Map<EmailTemplateVM>(eventEmailTemplate));
            }

            return PrepareResponse(HttpStatusCode.NotFound, "Template not found");
        }

        [HttpPost("template")]
        public async Task<IActionResult> CreateEmailTemplate(EmailTemplateVM model)
        {

            var user = await _userManager.FindByNameAsync(User?.Identity.Name);
            var emailTemplate = _mapper.Map<EmailTemplate>(model);
            var createdTemplate = await _emailManager.CreateEmailTempalteAsync(emailTemplate);
            await _auditTrailManager.AddAuditTrail(new AuditTrail() {
                ActionTaken = EmailEngine.Base.Entities.AuditAction.Create,
                Description = $"Email template {createdTemplate.EmailName} was created by user #{User?.Identity?.Name}",
                CreatedBy = User?.Identity?.Name,
                DateCreated = DateTime.Now,
                UserId = user.Id,
                UserName = user.UserName,
                Entity = "EmailTemplate"
            });
            return PrepareResponse(HttpStatusCode.OK, "Template has been created", false, _mapper.Map<EmailTemplate>(createdTemplate));
        }


        [HttpPut("template/{templateId}")]
        public async Task<IActionResult> EditEmailTemplate(EmailTemplateVM model)
        {

            var user = await _userManager.FindByNameAsync(User?.Identity.Name);
            var emailTemplate = _mapper.Map<EmailTemplate>(model);
            var template = await _emailManager.CreateEmailTempalteAsync(emailTemplate);

            await _auditTrailManager.AddAuditTrail(new AuditTrail()
            {
                ActionTaken = EmailEngine.Base.Entities.AuditAction.Update,
                Description = $"Email template #{template.Id} was updated by user #{User?.Identity?.Name}",
                CreatedBy = User?.Identity?.Name,
                DateCreated = DateTime.Now,
                UserId = user.Id,
                UserName = user.UserName,
                Entity = "EmailTemplate"
            });

            return PrepareResponse(HttpStatusCode.OK, "Template has been created", false, _mapper.Map<EmailTemplateVM>(template));
        }

       [HttpPost]
       public async Task<IActionResult> PrepareEmailCampaign()
       {
            return PrepareResponse(HttpStatusCode.OK, string.Empty);
       }



        [HttpPost("campaigns")]
        public async Task<IActionResult> SendEmailCampaign(VATEMailCampaignViewModel model)
        {
            var emailLogs = new List<EmailLog>();            
            
                var users = _userManager.Users.Where(x => model.UserIds.Contains(x.Id));
                foreach (var u in users)
                {
                    emailLogs.Add(new EmailLog
                    {
                        MailBody = model.EmailBody,
                        DateToSend = model.SendTime,
                        Sender = model.Sender,
                        Subject = model.EmailSubject,
                        Status = IPOEmailStatus.Fresh,
                        Receiver = u.Email,
                        SendImmediately = false,
                    });
                }

                await _emailManager.LogBatchEmail(emailLogs);
                return PrepareResponse(HttpStatusCode.OK, "Campaigns have been sucessfully sent", false, null);
            
        }

        [HttpPost("campaigns/{eventId}")]
        public async Task<IActionResult> SendEmailCampaign(int eventId, EMailCampaignViewModel model)
        {
            var emailLogs = new List<EmailLog>();
            var eventInfo = await _eventRepository.GetEvent(eventId);           
            if(eventInfo != null)
            {
                var attendees = eventInfo.Attendees.Where(x => model.AttendeeIds.Contains(x.Id));
                foreach(var attendee in attendees)
                {
                    emailLogs.Add(new EmailLog {
                        MailBody = model.EmailBody,
                        DateToSend = model.SendTime,
                        Sender = model.Sender,
                        Subject = model.EmailSubject,                        
                        Status = IPOEmailStatus.Fresh,
                        Receiver = attendee.EmailAddress,
                        SendImmediately = false,                        
                    });
                }

                await _emailManager.LogBatchEmail(emailLogs);
                return PrepareResponse(HttpStatusCode.OK, "Campaigns have been sucessfully sent", false, null);
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event not found");

        }        
    }
}