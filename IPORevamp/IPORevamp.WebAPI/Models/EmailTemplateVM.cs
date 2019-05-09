using EmailEngine.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPORevamp.WebAPI.Models
{
    public class EmailTemplateVM
    {
        public int Id { get; set; }
        public string EmailName { get; set; }
        public string EmailBody { get; set; }
        public string EmailSubject { get; set; }
        public IPOEmailTemplateType? EmailTemplateType { get; set; }
        public string EmailSender { get; set; }
        public DateTime DateCreated { get; set; }        
        public string EventName{ get; set; }
        public int? EventId { get; set; }
    }

    public class EMailCampaignViewModel
    {
        public int[] AttendeeIds { get; set;}         
        public string EmailBody { get; set; }
        public string EmailSubject { get; set; }
        public DateTime SendTime { get; set; }
        public string Sender { get; set; }
        public bool HasAttachment { get; set; }

    }

    public class VATEMailCampaignViewModel
    {
        public int[] UserIds { get; set; }
        public string EmailBody { get; set; }
        public string EmailSubject { get; set; }
        public DateTime SendTime { get; set; } 
        public string Sender { get; set; }
        public bool HasAttachment { get; set; }

    }
}
