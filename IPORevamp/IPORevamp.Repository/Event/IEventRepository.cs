using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Data;
using IPORevamp.Data.Entities.Event;
using IPORevamp.Data.Entities.Modules;

namespace IPORevamp.Repository.Event
{
    public interface IEventRepository : IAutoDependencyRegister
    {
        Task<List<EventInfo>> GetEvents();
        Task<PaginatedQueryable<EventInfo>> GetEvents(int pageSize, int pageNum ,string eventName, string location, string country, string organizer, DateTime? startDate = null, DateTime? endDate = null);
        Task<List<EventInfo>> GetEvents(string eventName, string location, string country,string organizer, DateTime? startDate = null, DateTime? endDate = null);
        Task<EventInfo> SaveEvent(EventInfo eventInfo);
        Task<EventInfo> GetEvent(int id);
        Task<List<AttendeeType>> FetchAttendeeTypes(int eventId);
        Task<AttendeeType> GetAttendeeType(int typeId);
        Task<List<Attendee>> GetAttendees(int eventId);
        Task<Attendee> GetAttendee(int attendeeId);
        Task<PaginatedQueryable<Attendee>> GetAttendees(int eventId, int pageNum, int pageSize);
        Task SaveAttendeeType(AttendeeType attendeeType);
        bool RemoveAttendeeType(int attendeeTypeId);
        Task<List<FeaturesModel>> FetchFeatures(int eventId);
        Task<List<EventInfo>> FetchOrganizedEvents(int organizerId);
        Task<string> SaveAttendeeAsync(Attendee attendee);
        Task SaveSession(int eventId, EventSessions session);
        Task<Attendee> RegisterForEvent(Attendee attendee);
        Task SaveSponsorAsync(Sponsors sponsors);
        Task<bool> PersonalizeAgenda(int attendeeId, int[] selectedSessions);
        Task<List<Attendee>> FetchSessionParticipant(int sessionId);
        Task SavePoll(int eventId, Poll poll);
        Task<bool> AttendeeVote(int pollId, int attendeeId, int optionId);

    }
}
