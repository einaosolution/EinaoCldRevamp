using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.Event
{
    public class Poll:EntityBase
    {
        public string Title { get; set; }      
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public EventInfo Event { get; set; }
        public List<PollOption> Options { get; set; }
        public List<Vote> Votes { get; set; }
        public bool IsCompulsory { get; set; }
    }
}
