using EmailEngine.Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using IPORevamp.Data.Entity.Interface;
using System.ComponentModel.DataAnnotations;

namespace IPORevamp.Data.Entities.Email
{
    public class EmailTemplate: IPOEmailTemplate, IEntity, IAudit
    {
        [Key] // Primary key
        public int EmailTemplateID { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsTransient()
        {
            return EqualityComparer<int>.Default.Equals(Id, default(int));
        }
       
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string DeletedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
