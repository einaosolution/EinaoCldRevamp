using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Setting
{
   public  class UserView
    {
        [Key]
        public string sn { get; set; }
        public String username { get; set; }
        public String firstname { get; set; }
        public String lastname { get; set; }
        public String mobileNumber { get; set; }
        public String description { get; set; }
        public DateTime datecreated { get; set; }



    }
}
