using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.SetupViewModel
{
   public class EmailTemplateViewModel
    {
        public int EmailTemplateId { get; set; }
        public string EmailName { get; set; }
        public string EmailBody { get; set; }
        public string EmailSubject { get; set; }
        public int EmailTemplateType { get; set; }
        public string EmailSender { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string  DeletedBy { get; set; }
        public string EmailCode { get; set; }
        public string UpdatedBy { get; set; }

    }
}
