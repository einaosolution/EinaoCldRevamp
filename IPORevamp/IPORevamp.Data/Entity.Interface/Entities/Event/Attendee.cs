using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using IPORevamp.Data.Entities.Payment;
using IPORevamp.Data.UserManagement.Model;

namespace IPORevamp.Data.Entities.Event
{
    public class Attendee:EntityBase
    {
        
        public string InvitationCode { get; set; }
        [ForeignKey("EventId")]
        public EventInfo Event { get; set; }
        public int EventId { get; set; }
        [ForeignKey("AttendeeTypeId")]
        public AttendeeType AttendeeType { get; set; }        
        public int? AttendeeTypeId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }

        public ProfileTitle Title { get; set; }
        public string Bio { get; set; }
        public string Nationality { get; set; }
        public string EmployerName { get; set; }
        public string Occupation { get; set; }
        public string ResidentialAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DoB { get; set; }
        public string MobileNumber { get; set; }  
        public string EmailAddress { get; set; }
        public bool IsPaid { get; set; }
        public BillLog RegistrationBill { get; set; }
        [ForeignKey("RegistrationBill")]
        public int? RegistrationBillId { get; set; }
        public List<PersonalizedAgenda> PersonalizedAgenda { get; set; }

    }
}
