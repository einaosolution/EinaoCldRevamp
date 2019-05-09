using System.Collections.Generic;

namespace IPORevamp.Data.Entities.Menus
{
    
    public class Menu :EntityBase
    {

        public Menu()
        {
           
           
        }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Badge { get; set; }
        public string BadgeClass { get; set; }
        public bool IsExternalLink { get; set; }
        public  string Roles { get; set; }
        public virtual List<Menu> Submenu { get; set; }
      //  public int ParentId { get; set; }
        //public virtual ICollection<RoleMenu> Roles { get; set; }
        //public int? RoleId { get; set; }

        //public string Permission { get; set; }
        //public string ControllerName { get; set; }
        //public string ActionName { get; set; }

    }
}
