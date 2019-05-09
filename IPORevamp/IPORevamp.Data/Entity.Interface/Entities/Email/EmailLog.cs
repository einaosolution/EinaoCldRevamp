﻿using EmailEngine.Base.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using IPORevamp.Data.Entity.Interface;

namespace IPORevamp.Data.Entities.Email
{
    public class EmailLog : IPOEmailLog, IEntity, IAudit
    {
        [Key] // Primary key
        public int EmailLogID { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string DeletedBy { get; set; }
        public string UpdatedBy { get;set; }
        public DateTime? LastUpdateDate { get; set; }
        public byte[] RowVersion { get; set ; }
        public DateTime DateCreated { get ; set ; }                     

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }
    }
}
