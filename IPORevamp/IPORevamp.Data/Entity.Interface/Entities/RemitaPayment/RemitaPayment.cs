using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.RemitaPayment
{
    public class RemitaPayment : EntityBase
    {
        public string ServiceTypeId { get; set; }
        public decimal? Amount { get; set; }
        public string OrderId { get; set; }
        public string PayerName { get; set; }
        public string PayerEmail { get; set; }
        public string PayerPhone { get; set; }
        public string Description { get; set; }
        public string RRRCode { get; set; }
        public string Statuscode { get; set; }
        public string RRR { get; set; }
        public string Status { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? TechFee { get; set; }
        public string Channel { get; set; }
        public string TotalAmount { get; set; }
        public int PaymentPurposeId { get; set; }
        public int PaymentStatus { get; set; }
        public string RemitaPostPayLoad { get; set; }
        public string RemitaResponsePayLoad { get; set; }


        public string RemitaPostVerifyPayLoad { get; set; }
        public string RemitaResponseVerifyPayLoad { get; set; }

        public int FeeId { get; set; }
        public string FeeItemName { get; set; }
        public DateTime TransactionInitiatedDate { get; set; } 
        public DateTime TransactionCompletedDate { get; set; }

        [NotMapped]
        public List<RemitaLineItem> RemitaPaymentLinkItems { get; set; }

    }


   


    public class LineItem : EntityBase
       {
            public string LineItemsId { get; set; }
            public string BeneficiaryName { get; set; }
            public string BeneficiaryAccount { get; set; }
            public string BankCode { get; set; }
            public string BeneficiaryAmount { get; set; }
            public string DeductFeeFrom { get; set; }

            [ForeignKey("orderId")]
            public string OrderId { get; set; }

        }

    public class RemitaLineItem
    {
        public string lineItemsId { get; set; }
        public string beneficiaryName { get; set; }
        public string beneficiaryAccount { get; set; }
        public string bankCode { get; set; }
        public string beneficiaryAmount { get; set; }
        public string deductFeeFrom { get; set; }
      
        public string orderId { get; set; }

    }

    public class LineItemModel
        {
            public string lineItemsId { get; set; }
            public string beneficiaryName { get; set; }
            public string beneficiaryAccount { get; set; }
            public string bankCode { get; set; }
            public string beneficiaryAmount { get; set; }
            public string deductFeeFrom { get; set; }


        }

        public class CustomField : EntityBase
        {
            public string Name { get; set; }
            public string value { get; set; }
            public string type { get; set; }
            [ForeignKey("orderId")]
            public string orderId { get; set; }
        }

}