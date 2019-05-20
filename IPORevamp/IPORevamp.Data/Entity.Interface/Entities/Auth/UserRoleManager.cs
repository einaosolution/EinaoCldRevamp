using IPORevamp.Data.Entities;
using IPORevamp.Data.Entity.Interface.Entities.Role;
using IPORevamp.Data.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Auth
{
    public class UserRoleManager : EntityBase
    {
        public UserRoleManager()
        {
            Admins = new HashSet<ApplicationUser>();
            LinkRolesMenus = new HashSet<LinkRolesMenus>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<ApplicationUser> Admins { get; set; }
        public ICollection<LinkRolesMenus> LinkRolesMenus { get; set; }
    }
}
