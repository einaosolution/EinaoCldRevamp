using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.Event
{
    public class AttendeeType:EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("EventId")]
        public EventInfo Event { get; set; }
        public decimal RegistrationFee { get; set; }
        public int EventId { get; set; }
    }
}
