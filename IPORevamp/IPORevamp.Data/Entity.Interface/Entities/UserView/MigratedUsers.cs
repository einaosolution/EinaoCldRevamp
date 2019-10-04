using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.UserView
{
   public  class MigratedUsers
    {
        [Key]
        public string sn { get; set; }
        public Int64 ?  migrateduserid { get; set; }
        public string  migratedagentcode { get; set; }

        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string roles { get; set; }



    }
}
