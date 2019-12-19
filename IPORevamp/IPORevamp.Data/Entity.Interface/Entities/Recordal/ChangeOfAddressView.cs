using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Recordal
{
  public  class ChangeOfAddressView
    {
        public string sn { get; set; }
        public DateTime FilingDate { get; set; }
        public string Filenumber { get; set; }
        public string ApplicantName { get; set; }
        public string NewApplicantAddress { get; set; }
        public string OldApplicantAddress { get; set; }
        public string DetailOfRequest { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
        public String Designtype { get; set; }
        public string classdescription { get; set; }
        public string ProductTitle { get; set; }
        public string Applicationclass { get; set; }
        public string status { get; set; }
        public string datastatus { get; set; }
        public string trademarktype { get; set; }
        

        public String NextrenewalDate { get; set; }


        public string logo_pic { get; set; }

        public string comment { get; set; }
        public string commentby { get; set; }

        public int pwalletid { get; set; }

        public string auth_doc { get; set; }
        public string sup_doc1 { get; set; }
        public string sup_doc2 { get; set; }
        public string renewalstatus { get; set; }

        public string renewalid { get; set; }

        public string attach_doc { get; set; }
        public string certificatePaymentReference { get; set; }

        public string BatCount { get; set; }


        public string Transactionid { get; set; }
        public string userid { get; set; }
    }
}
