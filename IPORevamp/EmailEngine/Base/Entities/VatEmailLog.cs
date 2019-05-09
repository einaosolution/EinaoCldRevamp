using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmailEngine.Base.Entities
{    
    public abstract class IPOEmailLog
    {
        public int Id { get; set; }
        public string Sender { get; set ; }
        public string Receiver { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string MailBody { get; set; }
        public IPOEmailStatus Status { get; set; }
        public DateTime? DateSent { get; set; }
        public DateTime DateToSend { get; set; }
        public bool HasAttachements { get; set; }
        public string AttachmentLoc { get; set; }

        public bool SendImmediately { get; set; }
    }
}
