using EmailEngine.Base.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using IPORevamp.Data.Entity.Interface;
using System.ComponentModel.DataAnnotations;

namespace IPORevamp.Data.Entities.AuditTrail
{
    public class AuditTrail : IPOActivityLog, IAudit
    {
        [Key] // Primary key
        public int AuditID { get; set; }
        public string CreatedBy { get; set; }
        public string DeletedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string IpAddress { get; set; }
        public string RecordBefore { get; set; }
        public string RecordAfter { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
