using IPORevamp.Data.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Role
{
   public class RoleViewModel
    {
       
        public int CreatedBy { get; set; }
        public int RoleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

      
    }
}
