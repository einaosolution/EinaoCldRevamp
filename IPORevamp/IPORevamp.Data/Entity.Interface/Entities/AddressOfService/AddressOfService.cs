using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.AddressOfService
{
   public  class AddressOfService : EntityBase
    {

        [ForeignKey("Id")]
        public int PatentApplicationID { get; set; }

        [ForeignKey("Id")]
        public int StateID { get; set; }

        public string AttorneyCode { get; set; }
        public string AttorneyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }


        public IPORevamp.Data.Entity.Interface.Entities.PatentApplication.PatentApplication PatentApplication { get; set; }

        public IPORevamp.Data.Entities.State State { get; set; }

    }
}
