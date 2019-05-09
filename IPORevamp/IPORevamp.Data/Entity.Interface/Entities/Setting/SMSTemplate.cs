using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entities.Setting
{
     public class SMSTemplate : EntityBase
    {
        public string SMSCode { get; set; }
        public string SMSSubject { get; set; }
        public string SMSContent{ get; set; }
    }
}
