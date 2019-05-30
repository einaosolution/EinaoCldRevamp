using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Role
{
    public class ReceiveRoleArray
    {
        public List<int> AssignedRoles { get; set; }

        public int UserId { get; set; }

        public int CurrentRole { get; set; }
    }
}
