using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using IPORevamp.Data.Entities.Event;

namespace IPORevamp.Data.Entities.Modules
{
    public class EventEventFeature
    {
        [Key]
        public int EventId { get; set; }
        public EventInfo EventInfo { get; set; } 
        public int FeatureId { get; set; }
        public EventFeature Feature { get; set; }
    }
}
