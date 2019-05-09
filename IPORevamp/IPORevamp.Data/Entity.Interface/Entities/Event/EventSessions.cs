using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.Event
{
    public class EventSessions:EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("EventId")]
        public EventInfo Event { get; set; }
        public int EventId { get; set; }
    }
}
