using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.Event
{
    public class Sponsors:EntityBase
    {
        [Key]
        public string Name { get; set; }
        public string AdvertMessage { get; set; }
        public string BannerImage { get; set; }
        public string LogoImage { get; set; }
        [ForeignKey("EventId")]
        public EventInfo Event { get; set; }
        public string URL { get; set; }
        public int EventId { get; set; }
    }
}
