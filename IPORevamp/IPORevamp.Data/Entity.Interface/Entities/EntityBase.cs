using System;
using System.Collections.Generic;
using IPORevamp.Data.Entity.Interface;

namespace IPORevamp.Data.Entities
{
    public class EntityBase: IAudit
    {
        public EntityBase()
        {
            IsActive = true;
            DateCreated=DateTime.Now;
            
        }
        public int Id { get; set; }
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