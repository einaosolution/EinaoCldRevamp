using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using IPORevamp.Data.Entities.Menus;
using IPORevamp.Data.Entity.Interface;

namespace IPORevamp.Data.UserManagement.Model
{

    public class ApplicationRole : IdentityRole<int>, IEntity
    {
        public ApplicationRole()
        {
            DateCreated = DateTime.Now;
            Menus= new List<RoleMenu>();
        }

        public bool IsDefault { get; set; }
        public DateTime DateCreated { get; set; }

        public bool IsTransient()
        {
            return EqualityComparer<int>.Default.Equals(Id, default(int));
        }

        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        
        public virtual ICollection<RoleMenu> Menus { get; set; }
        [NotMapped]
        public virtual ICollection<ApplicationUserRole> Users { get; } = new List<ApplicationUserRole>();
        public string Description { get; set; }
       
    }
}
