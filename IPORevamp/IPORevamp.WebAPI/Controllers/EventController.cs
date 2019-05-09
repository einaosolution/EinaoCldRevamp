using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EmailEngine.Base.Entities;
using EmailEngine.Base.Repository.EmailRepository;
using EmailEngine.Repository;
using EmailEngine.Repository.EmailRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IPORevamp.WebAPI.Models;
using IPORevamp.Data;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entities.Event;
using IPORevamp.Data.Entities.Modules;
using IPORevamp.Data.Entities.Payment;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.Event;
using IPORevamp.Repository.EventFeatures;


namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/events")]
    [ApiController]
    [Authorize]
    public class EventController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly IEventFeaturesRepository _eventFeatureRepo;
        private readonly IBilling<BillLog, PaymentLog, ApplicationUser, int> _billing;
        private readonly IFileHandler _fileHandler;
        private readonly IEventRepository _eventRepository;
        //private readonly 
        public EventController(UserManager<ApplicationUser> userManager,
           RoleManager<ApplicationRole> roleManager,
           SignInManager<ApplicationUser> signInManager,
           IConfiguration configuration,
           IMapper mapper,
           ILogger<EventController> logger,
       
           IEmailManager<EmailLog, EmailTemplate> emailManager,
           IAuditTrailManager<AuditTrail> auditTrailManager,
           IEventRepository eventRepository,
           IEventFeaturesRepository eventFeatureRepo,
           IFileHandler fileHandler,
           IBilling<BillLog, PaymentLog, ApplicationUser, int> billing
            ) : base(
               userManager,
               signInManager,
               roleManager,
               configuration,
               mapper,
               logger,
               auditTrailManager, eventRepository
               )
        {
            
            _emailManager = emailManager;
            _fileHandler = fileHandler;
            _eventFeatureRepo = eventFeatureRepo;
            _billing = billing;
          
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> FetchEvents(string eventName, string location, string organizer, string country, int? pageNum = null, int? pageSize = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            if (pageSize.HasValue && pageNum.HasValue)
            {
                var events = await _eventRepository.GetEvents(pageNum.Value, pageSize.Value, eventName, location, country,organizer, startDate, endDate);
                if (events.Items.Count() > 0)
                {
                    var respEvents = _mapper.Map<List<EventModel>>(events.Items.ToList());

                    var newPagedEvents = new PaginatedList<EventModel>()
                    {
                        Items = respEvents,
                        HasNextPage = events.HasNextPage,
                        HasPreviousPage = events.HasPreviousPage,
                        PageIndex = events.PageIndex,
                        TotalPages = events.TotalPages
                    };

                    return PrepareResponse(HttpStatusCode.OK, $"{events.Items.Count()} Events found", false, newPagedEvents);
                }
            }
            else
            {
                var events = await _eventRepository.GetEvents(eventName, location, organizer, country, startDate, endDate);
                var returnedEvents = _mapper.Map<List<EventModel>>(events);
                if (returnedEvents.Count > 0)
                {
                    return PrepareResponse(HttpStatusCode.OK, $"{returnedEvents.Count} events found", false, returnedEvents);
                }
            }

            return PrepareResponse(HttpStatusCode.NotFound, "No event found", false, null);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> FetchEvent(int id)
        {
            var eventInfo = await _eventRepository.GetEvent(id);
            if (eventInfo != null)
            {
                var returnedEvent = _mapper.Map<EventModel>(eventInfo);
                return PrepareResponse(HttpStatusCode.OK, $"Event found", false, returnedEvent);
            }
            return PrepareResponse(HttpStatusCode.NotFound, $"Event not found");
        }

        [AllowAnonymous]
        [HttpGet("{eventId}/attendees")]
        public async Task<IActionResult> FetchAttendees(int eventId, int? pageNum = null, int? pageSize = null)
        {
            if (!pageNum.HasValue && !pageSize.HasValue)
            {
                var attendees = await _eventRepository.GetAttendees(eventId);
                if (attendees.Count > 0)
                {
                    return PrepareResponse(HttpStatusCode.OK, $"{attendees.Count} Attendees found", false, attendees);
                }
            }
            else
            {
                var attendees = await _eventRepository.GetAttendees(eventId, pageNum.Value, pageSize.Value);
                if (attendees.Items.Count() > 0)
                {
                    return PrepareResponse(HttpStatusCode.OK, $"{attendees.Items.Count()} attendees found", false, attendees);
                }
            }

            return PrepareResponse(HttpStatusCode.NotFound, "Attendees not found", true, null);

        }

        [Authorize(Policy = "EventAttendees")]
        [HttpGet("{eventId}/session/{sessionId}/paticipants")]
        public async Task<IActionResult> FetchPaticipants(int eventId, int sessionId)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if(eventInfo != null)
            {
                var session = eventInfo.Sessions.FirstOrDefault(x => x.Id == sessionId);
                if(session != null)
                {
                    var sessionParticipants = await _eventRepository.FetchSessionParticipant(sessionId);
                    if(sessionParticipants.Count > 0)
                    {
                        return PrepareResponse(HttpStatusCode.OK,  $"{sessionParticipants.Count} Session participants retrieved", false, sessionParticipants);
                    }
                    return PrepareResponse(HttpStatusCode.NoContent, "No attendee is attending this session");
                }
                return PrepareResponse(HttpStatusCode.NotFound, "The Specified event can not be found");
            }
            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        }
        
        [AllowAnonymous]
        [HttpGet("{eventId}/sessions")]
        public async Task<IActionResult> FetchSessions(int eventId)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if(eventInfo != null)
            {
                var sessions = eventInfo.Sessions.ToList();
                return PrepareResponse(HttpStatusCode.OK, $"{sessions.Count} sessions found", false, _mapper.Map<List<SessionViewModel>>(sessions));
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        }

        [HttpGet("{eventId}/sessions/{sessionId}")]
        public async Task<IActionResult> FetchSession(int eventId, int sessionId)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if (eventInfo != null)
            {
                var session = eventInfo.Sessions.Find(x => x.Id == sessionId);
                if(session != null)
                {
                    return PrepareResponse(HttpStatusCode.OK, $"session  found", false, _mapper.Map<SessionViewModel>(session));
                }

                return PrepareResponse(HttpStatusCode.NotFound, "The specified session can not be found");
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        }


        [HttpPost("{eventId}/sessions")]
        public async Task<IActionResult> CreateSession(int eventId, SessionViewModel session)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if (eventInfo != null)
            {
                var newSession = _mapper.Map<EventSessions>(session);
                await _eventRepository.SaveSession(eventId, newSession);
                return PrepareResponse(HttpStatusCode.OK, "Session has been saved successfully", false, null);
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        } 

        [HttpPut("{eventId}/sessions/{sessionId}")]
        public async Task<IActionResult> UpdateSession(int eventId, SessionViewModel session)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if (eventInfo != null)
            {
                var newSession = _mapper.Map<EventSessions>(session);
                await _eventRepository.SaveSession(eventId, newSession);
                return PrepareResponse(HttpStatusCode.OK, "Session has been saved successfully", false, null);
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        }



        [Authorize(Policy = "EventOrganizers")]
        [HttpPost("{eventId}/attendeeType")]
        public async Task<IActionResult> SaveAttendeeType(int eventId, AttendeeTypeModel attendeeType)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if(eventInfo != null)
            {
                var type = _mapper.Map<AttendeeType>(attendeeType);
                type.EventId = eventId;

                await _eventRepository.SaveAttendeeType(type);

                return PrepareResponse(HttpStatusCode.OK, "Attendee type has been saved successfully", false);
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        }



        [Authorize]
        [HttpPut("{eventId}/attendeeType/{attendeeTypeId}")]
        public async Task<IActionResult> UpdateAttendeeType(int eventId, int attendeeTypeId, AttendeeTypeModel attendeeTypeModel)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if (eventInfo == null)
            {
                var attendeeType = await _eventRepository.GetAttendeeType(attendeeTypeId);
                if(attendeeType != null)
                {
                    attendeeType.RegistrationFee = Convert.ToDecimal(attendeeTypeModel.RegistrationFee);
                    attendeeType.Name = attendeeTypeModel.Name;
                    attendeeType.Description = attendeeTypeModel.Description;

                    await _eventRepository.SaveAttendeeType(attendeeType);
                    return PrepareResponse(HttpStatusCode.OK, "Attendee type has been saved successfully", false);
                }

                return PrepareResponse(HttpStatusCode.NotFound, "The specified attendee type not found");
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event is not found");
        }

        [Authorize]
        [HttpDelete("{eventId}/attendeeType/{attendeeType}")]
        public async Task<IActionResult> RemoveAttendeeType(int eventId, int attendeeType)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if(eventInfo != null)
            {
                _eventRepository.RemoveAttendeeType(attendeeType);
                return PrepareResponse(HttpStatusCode.OK, "AttendeeType has been deleted successfully", false);
            }
            return PrepareResponse(HttpStatusCode.NotFound, "The specified event is not found");
        }
        
        [HttpGet("{eventId}/features")]
        public async Task<IActionResult> FetchModules(int eventId)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if(eventInfo != null)
            {
                var modules = await _eventRepository.FetchFeatures(eventId);
                return PrepareResponse(HttpStatusCode.OK, "Features Found", false, modules);
            }
            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        }


        [HttpPost("{eventId}/feature")]
        public async Task<IActionResult> SelectFeatures(int eventId, FeaturesMultipleIdView model)
        {
            var featuresId = model.FeatureIds;
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if (eventInfo != null)
            {
                
                    var features = _eventFeatureRepo.FetchEventFeatures(featuresId);
                    if(features == null)
                    {
                        return PrepareResponse(HttpStatusCode.OK, "Features specified cannot be found");
                    }

                    var eventFeature = new List<EventEventFeature>();

                    foreach (var f  in features)
                    {
                        eventFeature.Add(new EventEventFeature() {
                            EventInfo = eventInfo,
                            Feature = f
                        });
                    }

                    eventInfo.Features = eventFeature;
                    await _eventRepository.SaveEvent(eventInfo);
                    return PrepareResponse(HttpStatusCode.OK, "Features have been saved successfully", false, null);                                
            }
            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        }



        [AllowAnonymous]
        [HttpGet("{eventId}/attendeeTypes")]
        public async Task<IActionResult> FetchAttendeeTypes(int eventId)
        {
            var attendeeTypes = await _eventRepository.FetchAttendeeTypes(eventId);

            
            if (attendeeTypes != null)
            {
                if(attendeeTypes.Count() > 0)
                {
                    return PrepareResponse(HttpStatusCode.OK, $"{attendeeTypes.Count} attendee types found", false, attendeeTypes);
                }                
            }

            return PrepareResponse(HttpStatusCode.NotFound, "Attendee types not found for this event");
        }

        [Authorize]
        [HttpPost("{eventId}/logo")]
        public async Task<IActionResult> UploadLogo(int eventId, IFormFile file)
        {
            var eventInfo = await  _eventRepository.GetEvent(eventId);
            if(eventInfo != null)
            {
                var uploadedImage = await _fileHandler.UploadFile(file, FileType.PICTURE);
                if(!string.IsNullOrEmpty(uploadedImage))
                {
                    eventInfo.EventLogo = uploadedImage;
                    await _eventRepository.SaveEvent(eventInfo);

                    return PrepareResponse(HttpStatusCode.OK, "Event logo has been uploaded successfully", false);
                }

                return PrepareResponse(HttpStatusCode.InternalServerError, "An error occured while uploading logo");
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        }

        


        [Authorize]
        [HttpPost("{eventId}/banner")]
        public async Task<IActionResult> UploadBanner(int eventId, IFormFile file)
        {            
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if (eventInfo != null)
            {
                var uploadedImage = await _fileHandler.UploadFile(file, FileType.PICTURE);
                if (!string.IsNullOrEmpty(uploadedImage))
                {
                    eventInfo.EventBanner = uploadedImage;
                    await _eventRepository.SaveEvent(eventInfo);

                    return PrepareResponse(HttpStatusCode.OK, "Event banner has been uploaded successfully", false);
                }

                return PrepareResponse(HttpStatusCode.InternalServerError, "An error occured while uploading event banner");
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        }
        
        [HttpPost("{userId}/register/{eventId}")]
        public async Task<IActionResult> AttendeeRegister(int userId, int eventId, AttendeeViewModel register)
        {
            var username = User?.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.NotFound, "User not found");
            }

            if (user.Id != userId)
            {
                return PrepareResponse(HttpStatusCode.Forbidden, "You are forbidden from registering for this event using another user's id");
            }            
            
            var eventInfo = await _eventRepository.GetEvent(eventId);

            


            if (eventInfo != null)
            {
                if (eventInfo.Attendees.Any(x => x.UserId == userId))
                {
                    return PrepareResponse(HttpStatusCode.Conflict, $"You have registered to the event {eventInfo.EventName} already. Please just join the event");
                }

                if (eventInfo.Attendees.Count(x=>x.IsPaid) >= eventInfo.NoOfAttendees)
                {
                    return PrepareResponse(HttpStatusCode.PreconditionFailed, $"You cannot register for the event {eventInfo.EventName}. Number of registered attendees has reached maximum bound");
                }

                var attendee = _mapper.Map<Attendee>(register);
                attendee.UserId = user.Id;
                attendee.EventId = eventId;

                if (eventInfo.AttendeeTypes.Count > 0 && !eventInfo.AttendeeTypes.Any(x => x.Id == register.AttendeeTypeId))
                {
                    return PrepareResponse(HttpStatusCode.NotFound, "The specified attendee type is not found");
                }

                
                if (register.UpdateIPORevampProfile)
                {
                    await _userManager.UpdateAsync(user);
                }

                var regAttendee = await _eventRepository.RegisterForEvent(attendee);

                if (regAttendee != null)
                {
                    if (!string.IsNullOrEmpty(regAttendee.InvitationCode))
                    {
                       
                        var attenRegEmailTemplate = _emailManager.GetEmailTemplate(IPOEmailTemplateType.AttendeeRegistration);
                        var emailTemplate = attenRegEmailTemplate.FirstOrDefault(x=>x.Id == eventId);
                        if(emailTemplate == null)
                        {
                            emailTemplate = attenRegEmailTemplate.FirstOrDefault();
                        }


                        if (emailTemplate == null)
                        {
                            return PrepareResponse(HttpStatusCode.NotFound, "Registration has been captured but not email template has been setup for event registration. Please contact event administrator");
                        }
                        var registrationBodyEmail = emailTemplate.EmailBody
                            .Replace("{INVCODE}", regAttendee.InvitationCode)
                            .Replace("{ATTENDEENAME}", regAttendee.FirstName)
                            .Replace("{EVENTNAME}", eventInfo.EventName);

                        if (eventInfo.IsPaid)
                        {
                            registrationBodyEmail += "<p>SP: Please note that your registration would not be complete without payment. Thank you.</p>";
                        }

                        await _emailManager.LogEmail(new EmailLog {
                            CC = user.Email,
                            DateCreated = DateTime.Now,
                            DateToSend = DateTime.Now,
                            Status = IPOEmailStatus.Fresh,
                            Receiver = regAttendee.EmailAddress,
                            MailBody = registrationBodyEmail,
                            Sender = emailTemplate.EmailSender,
                            Subject = emailTemplate.EmailSubject,
                            SendImmediately = true
                        });

                        var paymentResponse = string.Empty;
                        if (regAttendee.RegistrationBill != null)
                        {
                            paymentResponse = $"Please pay with this bill reference if you haven't {regAttendee.RegistrationBill.BillRefenceNo}";
                        }

                        await _auditTrailManager.AddAuditTrail(new AuditTrail {
                            ActionTaken = AuditAction.Create,
                            Description = $"Invitation Code {regAttendee.InvitationCode} to join event {eventInfo.EventName} has been generated for user {regAttendee.UserId}",
                            UserId = regAttendee.UserId,
                            Entity = "Attendee"                            
                        });

                        var repAttendee = _mapper.Map<AttendeeViewModel>(regAttendee);

                        return PrepareResponse(HttpStatusCode.OK, $"Your registration for the {eventInfo.EventName} " +
                            $"event has been captured successfully and an invitation code has been generated for you. {paymentResponse}", false, repAttendee);
                    }
                }

                return PrepareResponse(HttpStatusCode.InternalServerError, "An error occured while registration was been processed. Please contact your administrator");                
            }

            return PrepareResponse(HttpStatusCode.NotFound, "Specified event not found");                        
        }


        [AllowAnonymous]
        [HttpGet("{eventId}/sponsors")]
        public async Task<IActionResult> FetchSponsors(int eventId)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if(eventInfo != null)
            {
                var sponsors = eventInfo.Sponsors;
                if (sponsors != null)
                {
                    var retSponsor = _mapper.Map<SponsorViewModel>(sponsors);
                    return PrepareResponse(HttpStatusCode.OK, $"{sponsors.Count} Sponsors found", false, retSponsor);
                }

                return PrepareResponse(HttpStatusCode.NotFound, "Sponsors not found");
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        }


        [HttpPost("{eventId}/sponsors")]
        public async Task<IActionResult> CreateSponsors(int eventId, SponsorViewModel sponsor)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if (eventInfo != null)
            {
                var newSponsor = _mapper.Map<Sponsors>(sponsor);
                eventInfo.Sponsors.Add(newSponsor);
                await _eventRepository.SaveEvent(eventInfo);
                return PrepareResponse(HttpStatusCode.OK, "Sponsor has been created successfully", false);
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        }


        [HttpPut("{eventId}/sponsors/{sponsorId}/upload-banner")]
        public async Task<IActionResult> UploadSponsorImage(int eventId, int sponsorId, IFormFile image)
        {
            var username = User?.Identity?.Name;
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if (eventInfo != null)
            {
                if(username == eventInfo.Organizer.UserName)
                {
                    var sponsor = eventInfo.Sponsors.FirstOrDefault(x => x.Id == sponsorId);
                    if(sponsor != null)
                    {
                        var imageLoc = await _fileHandler.UploadFile(image, FileType.PICTURE);
                        sponsor.LogoImage = imageLoc;
                    }
                }

                return PrepareResponse(HttpStatusCode.Unauthorized, "You are not authorized to perform this action");
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event cannot be found");
        }

        [Authorize(Policy = "EventAttendees")]
        [HttpPost("{eventId}/personalizeagenda")]
        public async Task<IActionResult> PersonalizeAgenda(int eventId, PersonalizeViewModel personalize )
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if(eventInfo != null)
            {
                var sessionIds = personalize.SelectedSessions;
                var sessions = eventInfo.Sessions.Where(x => sessionIds.Contains(x.Id));
                if(sessions.Count() == sessionIds.Count())
                {
                   var saved = await _eventRepository.PersonalizeAgenda(personalize.AttendeeId, personalize.SelectedSessions);
                    if (saved)
                    {
                        return PrepareResponse(HttpStatusCode.OK, "Personalized agenda has been saved", false);
                    }

                    return PrepareResponse(HttpStatusCode.BadRequest, "Unknown error has occured");
                }
                return PrepareResponse(HttpStatusCode.BadRequest, "Some sessions do not belong this event");
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event cannot be found");            
        }


        [Authorize(Policy = "EventAttendees")]
        [HttpGet("{eventId}/personalizedagenda/{attendeeId}")]
        public async Task<IActionResult> FetchPersonalisedAgenda(int eventId, int attendeeId) 
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if (eventInfo != null)
            {
                var attendee = await _eventRepository.GetAttendee(attendeeId);
                if(attendee != null)
                {
                    var sessions = attendee.PersonalizedAgenda.Select(x => x.Session);
                    var returnedSessions = _mapper.Map<List<SessionViewModel>>(sessions);
                    return PrepareResponse(HttpStatusCode.OK, "Personalized agenda fetch successfully", false, returnedSessions);
                }
                return PrepareResponse(HttpStatusCode.NotFound, "The specified attendee can not be found");
            }
            return PrepareResponse(HttpStatusCode.NotFound, "The specified event cannot be found");
        }

        
        [HttpPut("{eventId}/sponsors/{sponsorId}")]
        public async Task<IActionResult> UpdateSponsor(int eventId, int sponsorId, SponsorViewModel sponsorViewModel)
        {

            var username = User?.Identity?.Name;
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if (eventInfo != null)
            {
                if(username == eventInfo.Organizer.UserName)
                {
                    var sponsor = eventInfo.Sponsors.FirstOrDefault(x => x.Id == sponsorId);
                    if (sponsor != null)
                    {
                        sponsor.Name = sponsorViewModel.Name;
                        sponsor.AdvertMessage = sponsorViewModel.AdvertMessage;

                        await _eventRepository.SaveSponsorAsync(sponsor);
                        return PrepareResponse(HttpStatusCode.OK, "Sponsor has been saved successfully", false);
                    }
                    return PrepareResponse(HttpStatusCode.NotFound, "The specified sponsor could not be found");
                }

                return PrepareResponse(HttpStatusCode.Unauthorized, "You are not authorized to perform this action");
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event could not be found");
        }


        

        [HttpPost("{eventId}/join/{userId}")]
        public async Task<IActionResult> JoinEvent(int eventId, int userId, string invCode)
        {
            if (!string.IsNullOrEmpty(invCode))
            {
                var eventInfo = await _eventRepository.GetEvent(eventId);
                if (eventInfo != null)
                {
                    var attendee = eventInfo.Attendees.FirstOrDefault(x => x.InvitationCode == invCode && x.UserId == userId);
                    if(attendee != null)
                    {

                        var repAttendee = GenerateAttendeeToken(attendee.EmailAddress, attendee);         
                        
                        return PrepareResponse(HttpStatusCode.OK, $"You have been granted access to join the event {eventInfo.EventName}", false, repAttendee);
                    }

                    return PrepareResponse(HttpStatusCode.Forbidden, "Please supply a valid invitation code.");
                }

                return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be fouond");
            }            

            return PrepareResponse(HttpStatusCode.NotFound, "Please supply your invitation code");
        }


        [Authorize]
        [HttpPost("{eventId}/complete-creation")]
        public async Task<IActionResult> CompleteEventCreation(int id)
        {
            var theEvent = await _eventRepository.GetEvent(id);
            if(theEvent != null)
            {
                theEvent.IsActive = true;
                await _eventRepository.SaveEvent(theEvent);
                return PrepareResponse(HttpStatusCode.OK, "Event has been created successfully", false, null);
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event doesn't exists", true, null);
        }

        [HttpPost]
        [Authorize]
        //[Authorize(Roles = "Organizers")]
        public async Task<IActionResult> CreateEvent(EventModel eventModel)
        {
            var organizer = await _userManager.FindByNameAsync(User.Identity.Name);
             var newEvent = _mapper.Map<EventInfo>(eventModel);
            if (newEvent != null)
            {
                newEvent.IsActive = false;
                newEvent.OrganizerId = organizer.Id;
                newEvent = await _eventRepository.SaveEvent(newEvent);

                await _auditTrailManager.AddAuditTrail(new AuditTrail {
                    ActionTaken = AuditAction.Create,
                    CreatedBy = User.Identity.Name,
                    DateCreated = DateTime.Now,
                    Description = $"Event {newEvent.EventName} was created",
                    Entity = typeof(EventInfo).Name,
                    UserId = organizer.Id,
                    UserName = organizer.UserName
                });
                
                return PrepareResponse(HttpStatusCode.OK, "Event creation process has been initiated successfully", false, _mapper.Map<EventModel>(newEvent));
            }

            return PrepareResponse(HttpStatusCode.BadGateway, "Error occured while processing you request");
        }

        [Authorize]
        [HttpPost("{eventId}/polls")]
        public async Task<IActionResult> CreatePoll(int eventId, CreatePollsViewModel createPolls)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if (eventInfo != null)
            {
                if(!eventInfo.Polls.Any(x=>x.Title == createPolls.Title))
                {
                    var newPoll = new Poll
                    {
                        Title = createPolls.Title
                    };
                    foreach(var option in createPolls.Options)
                    {
                        newPoll.Options.Add(new PollOption {
                            OptionText = option
                        });
                    }

                    await _eventRepository.SavePoll(eventId, newPoll);
                    return PrepareResponse(HttpStatusCode.OK, "Poll has been created successfully", false);
                }
                return PrepareResponse(HttpStatusCode.Conflict, "A poll exists in this event with that title already");
            }
            return PrepareResponse(HttpStatusCode.NotFound, "The soecified even can not be found");
        }


        [HttpGet("{eventId}/polls/{pollId}/result")]
        public async Task<IActionResult> PollResult(int eventId, int pollId)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if(eventInfo != null)
            {
                var poll = eventInfo.Polls.FirstOrDefault(x => x.Id == pollId);
                if (poll != null)
                {
                    var votes = poll.Votes.GroupBy(x => x.OptionId);
                }
                return PrepareResponse(HttpStatusCode.NotFound, "Specified poll can not be found");
            }
            return PrepareResponse(HttpStatusCode.OK, "The specified event can not be found");
        }
        

        [HttpPost("{eventId}/polls/{pollId}/vote")]
        public async Task<IActionResult> VotePoll(int eventId, int pollId,VoteViewModel vote)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if(eventInfo != null)
            {
                var poll = eventInfo.Polls.FirstOrDefault(x => x.Id == pollId);
                if(poll != null)
                {
                    if(!poll.Votes.Any(x=>x.AttendeeId == vote.AttendeeId))
                    {
                        if(await _eventRepository.AttendeeVote(pollId, vote.AttendeeId, vote.OptionId))
                        {
                            return PrepareResponse(HttpStatusCode.OK, "Your vote has been submitted successfully", false);
                        }

                        return PrepareResponse(HttpStatusCode.InternalServerError, "Your voting has failed due too some unkown reason");
                    }

                    return PrepareResponse(HttpStatusCode.Conflict, "You have participated in this poll already");
                }
                return PrepareResponse(HttpStatusCode.NotFound, "The specified poll can not be found");
            }

            return PrepareResponse(HttpStatusCode.NotFound, "The specified event can not be found");
        }

        [HttpPut("{eventId}/polls/{pollId}")]
        public async Task<IActionResult> CreatePoll(int eventId, int pollId, UpdatePollsViewModel updatePoll)
        {
            var eventInfo = await _eventRepository.GetEvent(eventId);
            if (eventInfo != null)
            {
                var poll = eventInfo.Polls.FirstOrDefault(x => x.Id == pollId);
                if(poll != null)
                {
                    poll.Title = updatePoll.Title;
                    foreach(var option in updatePoll.Options)
                    {
                        if (option.Id.HasValue)
                        {
                            poll.Options.ForEach(x => {
                                if (x.Id == option.Id) {
                                    x.OptionText = option.Option;
                                }
                            });
                        }
                        else
                        {
                            poll.Options.Add(new PollOption {
                                OptionText = option.Option
                            });
                        }
                    }

                    _eventRepository.SavePoll(eventId, poll);
                    return PrepareResponse(HttpStatusCode.OK, "Poll has been saved successfully", false);

                }
                return PrepareResponse(HttpStatusCode.Conflict, "A poll exists in this event with that title already");
            }
            return PrepareResponse(HttpStatusCode.NotFound, "The soecified even can not be found");
        }
    }


}
