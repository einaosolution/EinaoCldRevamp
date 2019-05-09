using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.Event
{
    public class PollOption:EntityBase
    {
        public string OptionText { get; set; }
        public int PollId { get; set; }
        [ForeignKey("PollId")]
        public Poll Poll { get; set; }        
    }
}
