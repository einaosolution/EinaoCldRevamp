﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Core.Utilities
{
   public class WebApiMessage
    {
        public const string  RecordEixst = "An existing record was found.";
        public const string SaveRequest = "Your request was saved successfully";
        public const string UpdateRequest = "Your request was updated successfully";
        public const string RecordNotFound = "No record was found";
        public const string FailSaveRequest = "An error occurred when saving your request";
        public const string DeleteRequest = "Your request status was changed to  successfully";
        public const string FailUpdateRequest = "An error occurred when saving your request";
        public const string FailDeletedRequest = "An error occurred when saving your request";
        public const string MissingUserInformation = "User Information not found";

        public const string ApprovedPaymentStatus = "Transaction Approved";

        public const string ApprovedPaymentStatusRemita= "Approved";
        public const string TransactionPending = "Transaction Pending";
        public const string RRRNotFound = "No RRR Information was found, please try again";
        public const string FailedTransactionCodeStatus = "xxx";
        public const int  SuccessfullyPayment = 1;
    }
}
