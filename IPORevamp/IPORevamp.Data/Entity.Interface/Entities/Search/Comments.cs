using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Search
{
   public  class PreviousComments
    {
        [Key]
        public string Sn { get; set; }
        public  string users { get; set; }

       public  string Comments { get; set; }
        public string Role { get; set; }
        public string UploadPath { get; set; }
    }
}
