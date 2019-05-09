using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using IPORevamp.Data.Entities.Event;

namespace IPORevamp.Data.Entities.Modules
{
    public class EventFeature:EntityBase
    {
        [Key] // Primary key
        public int Featureid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<EventEventFeature> Events { get; set; }
    }
}
