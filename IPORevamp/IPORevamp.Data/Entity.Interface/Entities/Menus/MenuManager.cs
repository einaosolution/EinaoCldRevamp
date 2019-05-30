using IPORevamp.Data.Entities;
using IPORevamp.Data.Entity.Interface.Entities.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.Menus
{
   public  class MenuManager : EntityBase
    {
        public MenuManager()
        {
          //  LinkRolesMenus = new HashSet<LinkRolesMenus>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int ParentId { get; set; }
        [NotMapped]
        public Boolean Selected { get; set; }
        public ICollection<LinkRolesMenus> LinkRolesMenus { get; set; }
    }
}
