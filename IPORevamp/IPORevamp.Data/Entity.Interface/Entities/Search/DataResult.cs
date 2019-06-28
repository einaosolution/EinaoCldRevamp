using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Search
{
  public   class DataResult
    {
        public string sn { get; set; }
        public DateTime FilingDate { get; set; }
        public string Filenumber { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantAddress { get; set; }
        public string phonenumber { get; set; }
        public string email { get; set; }
        public string trademarktype { get; set; }
        public string classdescription { get; set; }
        public string ProductTitle { get; set; }
        public string Applicationclass { get; set; }
        public string status { get; set; }


        public string logo_pic { get; set; }

        public int  pwalletid { get; set; }

        public string auth_doc { get; set; }
        public string sup_doc1 { get; set; }
        public string sup_doc2 { get; set; }

        public string Transactionid { get; set; }
    }
}
