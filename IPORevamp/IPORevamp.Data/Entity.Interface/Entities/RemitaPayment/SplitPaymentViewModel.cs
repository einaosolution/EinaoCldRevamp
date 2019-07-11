using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static IPORevamp.Data.Entity.Interface.Entities.RemitaPayment.RemitaPayment;

namespace IPORevamp.Data.Entity.Interface.Entities.RemitaPayment
{
    public class SplitRemitaPaymentViewModel
    {
        public string serviceTypeId { get; set; }
        public string amount { get; set; }
        public string orderId { get; set; }
        public string payerName { get; set; }
        public string payerEmail { get; set; }
        public string payerPhone { get; set; }
        public string description { get; set; }
        public LineItemViewModel lineItems { get; set; }
    }

    public class SplitRemitaPaymenWithCustomFieldViewModel
    {
        public string serviceTypeId { get; set; }
        public string amount { get; set; }
        public string orderId { get; set; }
        public string payerName { get; set; }
        public string payerEmail { get; set; }
        public string payerPhone { get; set; }
        public string description { get; set; }
        public LineItemViewModel lineItems { get; set; }
        public CustomFieldViewModel customFields { get; set; }
    }


    public class LineItemViewModel
    {
        public string lineItemsId { get; set; }
        public string beneficiaryName { get; set; }
        public string beneficiaryAccount { get; set; }
        public string bankCode { get; set; }
        public string beneficiaryAmount { get; set; }
        public string deductFeeFrom { get; set; }
    }

    public class CustomFieldViewModel
    {
        public string name { get; set; }
        public string value { get; set; }
        public string type { get; set; }
    }

    public class RequeryRemitaModel
    {
     ///   public string OrderId { get; set; }
        public string RRR { get; set; }
    }
    public class SplitResponseVO
    {
        public string orderId { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }
        public string statuscode { get; set; }
    }

    public class RemitaPaymentResponseModel
    {
        public string statuscode { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }
        public string Hash { get; set; }
        public string MerchantId { get; set; }
    }

    public class RemitaPaymentPostModel
    {
        public string merchantId { get; set; }
        public string hash { get; set; }
        public string rrr { get; set; }
        public string responseurl { get; set; }
      
    }

    public class RemitaQueryResponseModel
    {
        public string message { get; set; }
        public string RRR { get; set; }
        public string status { get; set; }
        public string merchantId { get; set; }
        public string transactiontime { get; set; }
        public string orderId { get; set; }
        public string amount { get; set; }
        public string paymentDate { get; set; }



    }


    public class RemitaPaymentResponsCodeModel
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



      public class RemitaPaymentModelPost
    {
        public string ServiceTypeId { get; set; }
        public decimal? Amount { get; set; }
        public string OrderId { get; set; }
        public string PayerName { get; set; }
        public string PayerEmail { get; set; }
        public string PayerPhone { get; set; }
        public string Description { get; set; }
        public List<LineItemModel> lineItems { get; set; }


    }

    public class RemitaPaymentModel
    {
        [Required(ErrorMessage="Please supply user Id")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please supply fee ids")]
        public int[] FeeIds { get; set; }
    }


    public class RemitaMakePaymentViewModel
    {
        public string rrr { get; set; }
        public int UserId { get; set; }
    }

    public class RemitaPaymentPayLoad
    {
        public string serviceTypeId { get; set; }
        public decimal? amount { get; set; }
        public string orderId { get; set; }
        public string payerName { get; set; }
        public string payerEmail { get; set; }
        public string payerPhone { get; set; }
        public string description { get; set; }

        
        public List<RemitaLineItem> lineItems { get; set; }

    }

}
