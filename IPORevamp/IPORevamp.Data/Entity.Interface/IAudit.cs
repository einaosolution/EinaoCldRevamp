using System;

namespace IPORevamp.Data.Entity.Interface
{
    public interface IAudit :IEntity
    {
         

        /// <summary>
        /// 
        /// </summary>
        string CreatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string DeletedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string UpdatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
     
        /// <summary>
        /// 
        /// </summary>
        DateTime? LastUpdateDate { get; set; }

        /// <summary>
        /// to manage versioning
        /// </summary>
        byte[] RowVersion { get; set; }
    

         
    }
}
