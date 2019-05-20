using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Menus
{
   public class MenuViewModel
    {

        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int ParentId { get; set; }

        public string CreatedBy { get; set; }
    }
}
