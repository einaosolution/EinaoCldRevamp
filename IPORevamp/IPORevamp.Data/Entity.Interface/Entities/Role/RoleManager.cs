using IPORevamp.Data.Entities;
using IPORevamp.Data.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Role
{
    public partial class RoleManager : EntityBase
    {
        public RoleManager()
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
