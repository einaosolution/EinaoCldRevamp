using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Core.Utilities;
using IPORevamp.Data;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Event;
using IPORevamp.Data.Entities.Modules;
using IPORevamp.Data.Entities.Payment;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.Interface;
using IPORevamp.Repository.Event;

namespace IPORevamp.Repository.Event
{
    public class EventRepository : IEventRepository
    {
        private IRepository<EventInfo> _repository;
        private IRepository<AttendeeType> _attendeeTypeRepo;
        private IRepository<Attendee> _attendeeRepo;
        private IRepository<EventFeature> _eventFeatureRepo;
        private IBilling<BillLog, PaymentLog, ApplicationUser, int> _billing;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;
        private IRepository<Sponsors> _sponsorRepository;
        private IRepository<EventSessions> _sessionRepository;
        private IRepository<PersonalizedAgenda> _agendaRepository;
        private IRepository<Poll> _pollRepository;
        public EventRepository(IRepository<EventInfo> repository, IRepository<PersonalizedAgenda> agendaRepository, IRepository<Poll> pollRepository, IRepository<EventSessions> sessionRepository, IRepository<AttendeeType> attendeeTypeRepo, IRepository<Attendee> attendeeRepo, IRepository<EventFeature> eventFeatureRepo, IBilling<BillLog, PaymentLog, ApplicationUser, int> billing, IAuditTrailManager<AuditTrail> auditTrailManager, IRepository<Sponsors> sponsorRepository)
        {
            _repository = repository;
            _attendeeTypeRepo = attendeeTypeRepo;
            _attendeeRepo = attendeeRepo;
            _eventFeatureRepo = eventFeatureRepo;
            _billing = billing;
            _auditTrailManager = auditTrailManager;
            _sponsorRepository = sponsorRepository;
            _sessionRepository = sessionRepository;
            _agendaRepository = agendaRepository;
            _pollRepository = pollRepository;
        }
        public async Task<EventInfo> SaveEvent(EventInfo eventInfo)
        {
            var savedEvent = await _repository.InsertOrUpdateAsync(eventInfo);
            _repository.SaveChanges();
            return savedEvent.Entity;
        }

        public async Task<List<AttendeeType>> FetchAttendeeTypes(int eventId)
        {
            var eventAttendeesTypes = await _repository.GetAll().Include(x => x.AttendeeTypes).FirstOrDefaultAsync(x => x.Id == eventId);
            return eventAttendeesTypes?.AttendeeTypes;
        }

        public async Task<List<Attendee>> FetchSessionParticipant(int sessionId)
        {
            var attendee = _agendaRepository.GetAll().Include(x => x.Attendee).Where(x => x.SessionId == sessionId).Select(x=>x.Attendee);
            return attendee.ToList();
        }

        public async Task<Attendee> GetAttendee(int attendeeId)
        {
            var attendee = await _attendeeRepo.GetAll().Include("PersonalizedAgenda.Session").FirstOrDefaultAsync(x=>x.Id == attendeeId);
            return attendee;
        }        

        public async Task<PaginatedQueryable<Attendee>> GetAttendees(int eventId, int pageNum, int pageSize)
        {
            var eventAttendees = _attendeeRepo.GetAll().Include(x => x.User).Where(x => x.EventId == eventId);            
            return await PaginatedQueryable<Attendee>.CreateAsync(eventAttendees, pageNum, pageSize);            
        }

        public async Task SaveSession(int eventId, EventSessions session)
        {
            session.EventId = eventId;
            await _sessionRepository.InsertOrUpdateAsync(session);
            await _sessionRepository.SaveChangesAsync();
        }

        public async Task<PaginatedQueryable<EventInfo>> GetEvents(int pageNum, int pageSize, string eventName = null, string location=null, string country =null ,string organizer = null,  DateTime? startDate = null, DateTime? endDate = null)
        {
            var events = _repository.GetAll();//.Include(x=>x.Organizer);
            var predicate = PredicateBuilder.True<EventInfo>();
            if (!string.IsNullOrEmpty(organizer))
            {
                predicate = predicate.And(x => x.Organizer.EmployerName.Contains(organizer) || (x.Organizer.FirstName + " " + x.Organizer.LastName).Contains(organizer));                
            }

            if (!string.IsNullOrEmpty(eventName))
            {
                predicate = predicate.And(x => x.EventName == eventName);
            }

            if (!string.IsNullOrEmpty(country))
            {
                predicate = predicate.And(x => x.Country.Contains(country));
            }

            if (!string.IsNullOrEmpty(location))
            {
                predicate = predicate.And(x => x.Location.Contains(location));
            }

            if(startDate != null && endDate != null)
            {
                predicate = predicate.And(x => x.StartDate >= startDate && x.StartDate <= x.EndDate);
            }

            events = events.Include(x=>x.Organizer).Where(predicate);
            return await PaginatedQueryable<EventInfo>.CreateAsync(events, pageNum,pageSize);
           
        }
        public async Task<AttendeeType> GetAttendeeType(int typeId)
        {
            try
            {
                var attendeetype = await _attendeeTypeRepo.GetAsync(typeId);
                return attendeetype;
            }
            catch (Exception)
            {
                return null;
            }
           
        }

        private string GenerateInvitationCode()
        {
            var randomString = Utilities.GenerateRandomString(7);            
            while (_attendeeRepo.GetAllList(x => x.InvitationCode == randomString).Any())
            {
                randomString  = Utilities.GenerateRandomString();                
            }

            return randomString;
        }

        public async Task SaveSponsorAsync(Sponsors sponsors)
        {
            await _sponsorRepository.UpdateAsync(sponsors);
            await _sponsorRepository.SaveChangesAsync();
        }

        public async Task<Attendee> RegisterForEvent(Attendee attendee)
        {
            //bill attendee for Registration
            var eventInfo = await GetEvent(attendee.EventId);
            attendee.RegistrationBill = null;
            if (eventInfo.IsPaid)
            {
                
                var registrationBill = new BillLog()
                {
                    //Amount = attendee.AttendeeType.RegistrationFee,
                    BillStatus = (int)BillStatus.Initiated,
                    BillType = (int)BillType.EventRegistration,
                    DateCreated = DateTime.Now,
                    UserId = attendee.UserId,

                };

                var attendeeType = _attendeeTypeRepo.GetAll().FirstOrDefault(x=>x.Id == attendee.AttendeeTypeId);
                if(attendeeType == null)
                {
                    attendee.AttendeeType = null;
                    attendee.AttendeeTypeId = null;
                    registrationBill.Amount = eventInfo.RegistrationFee;
                }
                else
                {
                    registrationBill.Amount = attendeeType.RegistrationFee;
                }
                
                var bill = await _billing.GenerateBillLog(registrationBill);
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    Description = $"System generated a bill: {bill.BillRefenceNo} for attendee registration against user #{attendee.UserId}",
                    UserId = attendee.UserId                  
                });

                attendee.RegistrationBillId = bill.Id;
            }

            var invCode = await SaveAttendeeAsync(attendee);

            return attendee;
        }

        public async Task<string> SaveAttendeeAsync(Attendee attendee)
        {
            if (string.IsNullOrEmpty(attendee.InvitationCode))
            {
                attendee.InvitationCode = GenerateInvitationCode();
            }            

            var attende = await _attendeeRepo.InsertOrUpdateAsync(attendee);
            await _attendeeRepo.SaveChangesAsync();


            return attendee.InvitationCode;
        }
        
        public async Task<EventInfo> GetEvent(int id)
        {
            try
            {
                var eventInfo = await _repository.GetAll().Include(x=>x.Attendees).Include(x=>x.Organizer).Include(x=>x.AttendeeTypes).Include(x=>x.Sponsors).Include(x=>x.Sessions).Include("Polls.Options").Include("Polls.Votes").FirstOrDefaultAsync(x=>x.Id == id);
                return eventInfo;
            }catch(Exception ex)
            {
                
                return null;
            }
            
        }

        public bool RemoveAttendeeType(int attendeeTypeId)
        {
            try
            {
                _attendeeTypeRepo.Remove(attendeeTypeId);
                _attendeeTypeRepo.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
            
        }


        public async Task SaveAttendeeType(AttendeeType attendeeType)
        {
            _attendeeTypeRepo.InsertOrUpdate(attendeeType);
            _attendeeTypeRepo.SaveChanges();
        }

        public async Task<List<EventInfo>> GetEvents()
        {
            var events = _repository.GetAll().Include(x => x.Attendees).Include(x => x.AttendeeTypes);
            return await events.ToListAsync();
        }

        public async Task<bool> PersonalizeAgenda(int attendeeId, int[] selectedSessions)
        {
            var attendeeInfo = await _attendeeRepo.GetAll().Include(x=>x.PersonalizedAgenda).FirstOrDefaultAsync(x=>x.Id == attendeeId);
            if(attendeeInfo != null)
            {
                attendeeInfo.PersonalizedAgenda.RemoveAll(x => true);
                foreach(var sessionId in selectedSessions)
                {
                    attendeeInfo.PersonalizedAgenda.Add(new PersonalizedAgenda {
                        AttendeeId = attendeeInfo.Id,
                        SessionId = sessionId
                    });
                }

                await _attendeeRepo.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<Attendee>> GetAttendees(int eventId)
        {
            var eventAttendees = _attendeeRepo.GetAll().Include(x => x.User).Where(c => c.EventId == eventId);
            return await eventAttendees.ToListAsync();
        }

        public async Task<List<EventInfo>> GetEvents(string eventName, string location, string country ,string organizer,DateTime? startDate = null, DateTime? endDate = null)
        {
            var events = _repository.GetAll();//.Include(x=>x.Organizer);
            var predicate = PredicateBuilder.True<EventInfo>();
            if (!string.IsNullOrEmpty(organizer))
            {
                predicate = predicate.And(x => x.Organizer.EmployerName.Contains(organizer) || (x.Organizer.FirstName + " " + x.Organizer.LastName).Contains(organizer));
            }

            if (!string.IsNullOrEmpty(eventName))
            {
                predicate = predicate.And(x => x.EventName == eventName);
            }

            if (!string.IsNullOrEmpty(country))
            {
                predicate = predicate.And(x => x.Country.Contains(country));
            }

            if (!string.IsNullOrEmpty(location))
            {
                predicate = predicate.And(x => x.Location.Contains(location));
            }

            if (startDate != null && endDate != null)
            {
                predicate = predicate.And(x => x.StartDate >= startDate && x.StartDate <= x.EndDate);
            }

            events = events.Include(x => x.Organizer).Where(predicate);
            return await events.ToListAsync();
        }

        public async Task<List<EventInfo>> FetchOrganizedEvents(int userId)
        {
            var eventInfo = await _repository.GetAllListAsync(x => x.OrganizerId == userId);
            return eventInfo;
        }

        public async Task<List<FeaturesModel>> FetchFeatures(int eventId)
        {
            var eventInfo = await _repository.GetAll().Include(c=>c.Features).FirstOrDefaultAsync(x=>x.Id == eventId);
            var eventFeatures = eventInfo.Features.Select(x => x.Feature.Id);
            var allEventFeature = await _eventFeatureRepo.GetAllListAsync();

            var eventFeatureSelection = allEventFeature.Select(x => new FeaturesModel {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                IsSelected = eventFeatures.Contains(x.Id),
                Price = x.Price.ToString()
            });

            return eventFeatureSelection.ToList();
        }

        public async Task SavePoll(int eventId, Poll poll)
        {
            poll.EventId = eventId;
            _pollRepository.InsertOrUpdate(poll);
            _pollRepository.SaveChanges();
        }

        public async Task<bool> AttendeeVote(int pollId, int attendeeId, int optionId)
        {
            var poll = await _pollRepository.GetAll().Include(x => x.Votes).FirstOrDefaultAsync(x => x.Id == pollId);
            if (poll != null)
            {
                poll.Votes.Add(new Vote {
                    AttendeeId = attendeeId,
                    OptionId = optionId,
                });

                _pollRepository.Update(poll);
                _pollRepository.SaveChanges();
                return true;
            }

            return false;
        }
        
    }
}
