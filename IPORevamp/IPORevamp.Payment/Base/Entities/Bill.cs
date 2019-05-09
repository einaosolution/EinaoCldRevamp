using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Payment.Base.Entities
{
    public abstract class Bill<TUser, TUserKey, TPaymentLog> : IGenericEntity where TUser:IdentityUser<TUserKey>  where TPaymentLog:PaymentLog<TUser, TUserKey> where TUserKey:IEquatable<TUserKey>
    {
        
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

        [ForeignKey("UserId")]
        public TUser User{ get; set; }
        public TUserKey UserId { get; set; }
        public decimal Amount { get; set; } = 0.00M;
        public int BillStatus { get; set; }
        public string BillRefenceNo { get; set; }
        public DateTime? DatePaid { get; set; }
        public int BillType { get; set; }
        public int Year { get; set; }

        public List<TPaymentLog> PaymentDetails { get; set; }
        public int PaymentMethod { get; set; }
        public int? EntityId { get; set; }
    }
}
