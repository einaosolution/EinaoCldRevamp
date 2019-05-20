using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.Menus;
using IPORevamp.Data.Entity.Interface.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Role 
{
    public class LinkRolesMenus :EntityBase 
    {
        public int Id { get; set; }
        public int RolesId { get; set; }
        public int MenusId { get; set; }

        public MenuManager Menus { get; set; }
        public RoleManager Roles { get; set; }
    }
}
