using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPORevamp.WebAPI.Models
{
    public class AttendeeViewModel
    {
        public int Id { get; set; }
        public string InvitationCode { get; set; }
        public string EventName { get; set; }
        public int EventId { get; set; }
        
        public int? AttendeeTypeId { get; set; }
        public string AttendeeTypeName { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please provide your title")]
        public string Title { get; set; }
        public string Bio { get; set; }
        [Required(ErrorMessage = "Please choose Nationality")]
        public string Nationality { get; set; }
        public string EmployerName { get; set; }
        public string Occupation { get; set; }
        [Required(ErrorMessage = "Please specify your residential address")]
        public string ResidentialAddress { get; set; }
        [Required(ErrorMessage = "Please provide your phone number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please provide yoour Country Code")]
        public string CountryCode { get; set; }
        [Required(ErrorMessage = "Please provide your firstname")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please provide your middlename")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Please provide your lastname")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please provide your gender")]
        public string Gender { get; set; }
        public DateTime DoB { get; set; }
        [Required(ErrorMessage = "Please provide your email address")]
        public string EmailAddress { get; set; }
        public bool IsPaid { get; set; }        
        public string RegistrationBillRefNo { get; set; }
        public bool UpdateIPORevampProfile { get; set; }
        public string AccessCode { get; set; }
    }
}
