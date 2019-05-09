using System;
using System.Collections.Generic;
using System.Text;

namespace EmailEngine.Base.Entities.Models
{
    public class PaymentDetails
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PaymentDescription { get; set; }
        public decimal Amount { get; set; } = 0.00M;
        public decimal TechFee { get; set; } = 0.00M;
        public decimal Total { get; set; } = 0.00M;
        public string Email { get; set; }
        public string Phone { get; set; }
        public int BillType { get; set; }
        public string TransactionRef { get; set; }
        public int UserId { get; set; }
        public string billRefenceNo { get; set; }
        public DateTime GeneratedDate { get; set; } = DateTime.Now;
        public int BillStatus { get; set; }
        public string MembershipNo { get; set; }
        public decimal GateWayAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public int PaymentStatus { get; set; }
        public string GatewayMessage { get; set; }
        public string PaymentMethod { get; set; }
        public int? EntityId { get; set; }
    }
}
