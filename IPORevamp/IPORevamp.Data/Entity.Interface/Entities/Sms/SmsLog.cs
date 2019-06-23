using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Sms
{
   public  class SmsLog : EntityBase
    {
        public string username { get; set; }
        public string message { get; set; }
        public string sender { get; set; }
        public string mobilenumber  { get; set; }

        public string useremail { get; set; }
        public string status { get; set; }
    }
}
