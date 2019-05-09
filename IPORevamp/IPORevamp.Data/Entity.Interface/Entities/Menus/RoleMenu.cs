using IPORevamp.Data.UserManagement.Model;

namespace IPORevamp.Data.Entities.Menus
{
    public class RoleMenu:EntityBase
    {
        public int? MenuId { get; set; }
        public int? RoleId { get; set; }
        public ApplicationRole Role { get; set; }
        public Menu Menu { get; set; }
    }
}