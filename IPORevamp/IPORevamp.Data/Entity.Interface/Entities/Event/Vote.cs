using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.Event
{
    public class Vote:EntityBase
    {
        public int OptionId { get; set; }
        [ForeignKey("OptionId")]        
        public PollOption Option { get; set; }        
        public int PollId { get; set; }
        [ForeignKey("PollId")]
        public Poll Poll { get; set; }
        public int AttendeeId { get; set; }
        [ForeignKey("AttendeeId")]
        public Attendee Attendee { get; set; }
    }
}
