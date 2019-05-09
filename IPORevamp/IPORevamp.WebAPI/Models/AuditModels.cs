using EmailEngine.Base.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPORevamp.WebAPI.Models
{

    public class AuditVm
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public AuditAction ActionTaken { get; set; }
        public string Description { get; set; }
        public string Entity { get; set; }
        public string CreatedBy { get; set; }        
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public Actions Actions { get; set; }
    }

    public class Actions
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class AuditModels
    {
    }
}
