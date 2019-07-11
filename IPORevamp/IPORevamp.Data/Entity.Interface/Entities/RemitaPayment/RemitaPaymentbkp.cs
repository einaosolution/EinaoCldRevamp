using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.RemitaPayment
{
    public class RemitaPaymentModel
    {
        public string serviceTypeId { get; set; }
        public string amount { get; set; }
        public string orderId { get; set; }
        public string payerName { get; set; }
        public string payerEmail { get; set; }
        public string payerPhone { get; set; }
        public string description { get; set; }
    }

    public class RemitaPaymentResponseModel
    {
        public string statuscode { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }
    }

    public class RemitaQueryResponseModel
    {
        public string statusmessage { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }
        public string merchantId { get; set; }
        public string transactiontime { get; set; }
        public string orderId { get; set; }
    }

    public class RemitaPayNotificationResponseModel
    {
        public string channel { get; set; }
        public string rrr { get; set; }
        public string amount { get; set; }
        public string debitdate { get; set; }
        public string transactiondate { get; set; }
        public string bank { get; set; }
        public string branch { get; set; }
        public string serviceTypeId { get; set; }
        public string dateRequested { get; set; }
        public string orderRef { get; set; }
        public string payerName { get; set; }
        public string payerPhoneNumber { get; set; }
        public string payerEmail { get; set; }
        public string uniqueIdentifier { get; set; }
        public string orderID { get; set; }
    }
}
