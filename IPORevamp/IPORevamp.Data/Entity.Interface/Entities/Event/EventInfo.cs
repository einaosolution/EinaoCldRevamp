using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using IPORevamp.Data.Entities.Modules;
using IPORevamp.Data.UserManagement.Model;

namespace IPORevamp.Data.Entities.Event
{
    public class EventInfo : EntityBase
    {
        [Key] // Primary key
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventLogo { get; set; }
        public string EventBanner { get; set; }
        public string EventDescription { get; set; }
        public string OrganizerDescription { get; set; }
        public string EventSocialMediaHandle { get; set; }
        public string BrandColor { get; set; }
        public bool IsRegistrationOutside { get; set; }
        public string RegistrationURL { get; set; }
        public string EventRegistrationLink { get; set; }
        public bool IsPaid { get; set; }
        [ForeignKey("OrganizerId")]
        public ApplicationUser Organizer { get; set; }
        public decimal RegistrationFee { get; set; }
        public int OrganizerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Tags { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int NoOfAttendees { get; set; }
        public List<AttendeeType> AttendeeTypes { get; set; }
        public List<Attendee> Attendees { get; set; }
        public List<EventEventFeature> Features { get; set; }
        public List<Sponsors> Sponsors { get; set; }
        public List<EventSessions> Sessions { get; set; }
        public List<Poll> Polls { get; set; }
    }
}
