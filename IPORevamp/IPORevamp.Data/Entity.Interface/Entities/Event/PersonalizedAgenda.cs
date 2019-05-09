using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.Event
{
    public class PersonalizedAgenda:EntityBase
    {
        
        public int SessionId { get; set; }    
        [ForeignKey("SessionId")]
        public EventSessions Session { get; set; }
        public int AttendeeId { get; set; }
        [ForeignKey("AttendeeId")]
        public Attendee Attendee { get; set; }
    }
}
