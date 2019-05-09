using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPORevamp.WebAPI.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Event name is required")]
        public string EventName { get; set; }
        public string EventLogo { get; set; }
        public string EventBanner { get; set; }
        [Required(ErrorMessage = "Event Description is required")]
        [StringLength(250, ErrorMessage = "Event Description can not exceed 250 characters")]
        public string EventDescription { get; set; }
        //[Required(ErrorMessage = "Organizer Description is required")]
        //[StringLength(250, ErrorMessage = "Organizer Description can not exceed 250 characters")]
        public string OrganizerDescription { get; set; }
        public string EventSocialMediaHashtags { get; set; }                
        public string BrandColor { get; set; }
        public bool IsRegistrationOutside { get; set; }       
        public string RegistrationURL { get; set; }
        [Required(ErrorMessage = "Event start date is required")]
        [DataType(DataType.DateTime, ErrorMessage = "Please provide a valid Start date")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage="Event end date is required")]
        [DataType(DataType.DateTime, ErrorMessage = "Please provide a valid End date")]
        public DateTime? EndDate { get; set; }
        public int RegisteredAttendees { get; set; }
        [Required(ErrorMessage = "Please provide the number of attendees")]
        public int? NoOfAttendees { get; set; }              
        public bool IsPaid { get; set; }
        public string Organizer { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }

        public List<AttendeeTypeModel> AttendeeTypes { get; set; }
    }

    public class PersonalizeViewModel
    {
        [Required(ErrorMessage = "Please choose an attendeeId")]
        public int AttendeeId { get; set; }
        [Required(ErrorMessage = "Please choosee a session")]
        public int[] SelectedSessions { get; set; }
    }
    public class AttendeeTypeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please specify the name of the attendee type")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please specify registration fee for this attendee type")]
        [DataType(DataType.Currency,ErrorMessage = "Please provide a valid monetary value")]
        public string RegistrationFee { get; set; }
        public string Description { get; set; }
    }

    public class SessionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CreatePollsViewModel
    {
        [Required(ErrorMessage = "Please supply poll title/question")]
        public string Title{get;set;}    
        [Required(ErrorMessage = "you can not create a poll without options")]
        public string[] Options { get; set; }
    }

    public class UpdatePollsViewModel
    {        
        public int Id { get; set; }
        [Required(ErrorMessage = "Please supply poll title/question")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You can not have a poll without opotions")]
        public UpdatePollsOption[] Options { get; set; }

    }

    public class UpdatePollsOption
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Please supply the text in the option")]
        public string Option { get; set; }
    }

    public class VoteViewModel
    {
        public int OptionId { get; set; }
        public int AttendeeId { get; set; }
    }
}
