using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmailEngine.Base.Entities
{
    public abstract class VatPaymentLog<TUser,TUserKey>:IGenericEntity where TUser:IdentityUser<TUserKey> where TUserKey: IEquatable<TUserKey>
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

        public string MembershipNo { get; set; }
        [ForeignKey("UserId")]
        public TUser User { get; set; }
        public TUserKey UserId { get; set; }
        public int PaymenType { get; set; }
        public int BillId { get; set; }
        public string PaymentReferenceNo { get; set; }
        public decimal Amount { get; set; } = 0.00M;
        public decimal FinalAmount { get; set; } = 0.00M;
        public DateTime? PaymentDate { get; set; }
        public decimal PaymentCharges { get; set; } = 0.00M;
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public decimal GateWayAmount { get; set; } = 0.00M;
        public decimal TechFee { get; set; } = 0.00M;
        public int PaymentStatus { get; set; }
        public string ResponsePayload { get; set; }       
        public string CardNumber { get; set; }
        public int? EntityId { get; set; }
    }
}
