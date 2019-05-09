using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entities.Setting
{
    public class PTApplicationStatus : EntityBase
    {
        public int RoleId { get; set; }
        public string StatusDescription { get; set; }
    }
}
