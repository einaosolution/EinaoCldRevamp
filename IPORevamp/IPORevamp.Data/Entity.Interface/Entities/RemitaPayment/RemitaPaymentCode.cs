using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.RemitaPayment
{
    public class RemitaPaymentCode
    {
        public static string RemitaPaymentCodeFeedback(string p)
        {
            string desc = "";
            if (p == "00")
                desc = "Transaction Completed Successfully";
            else if (p == "01")
                desc = "Transaction Approved";
            else if (p == "02")
                desc = "Transaction Failed";
            else if (p == "012")
                desc = "User Aborted Transaction";
            else if (p == "020")
                desc = "Invalid User Authentication";
            else if (p == "021")
                desc = "Transaction Pending";
            else if (p == "022")
                desc = "Invalid Request";
            else if (p == "023")
                desc = "Service Type or Merchant Does not Existn";
            else if (p == "025")
                desc = "Payment Reference Generatedn";
            else if (p == "029")
                desc = "Invalid Bank Code";
            else if (p == "30")
                desc = "Insufficient Balance";
            else if (p == "31")
                desc = "No Funding Account";
            else if (p == "32")
                desc = " Invalid Date Format";
            else if (p == "40")
                desc = "Initial Request OK";
            else if (p == "999")
                desc = "Unknown Error";
           
            return desc;
        }


        
    }
}
