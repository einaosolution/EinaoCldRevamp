using System;
using System.Collections.Generic;
using System.Text;

namespace EmailEngine.Base.Entities
{
    public abstract class IPOEmailTemplate
    {
        public int Id { get; set; }
        public string EmailName { get; set; }
        public string EmailBody { get; set; }
        public string EmailSubject { get; set; }
        public IPOEmailTemplateType? EmailTemplateType { get; set; }
        public string EmailSender { get; set; }  

    }
}
