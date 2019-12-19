﻿using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace IPORevamp.Data.Entity.Interface.Entities.Recordal
{
  public   class RecordalPatentRenewal : EntityBase
    {
        public string TrademarkTitle { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantAddress { get; set; }
        public DateTime? RenewalDueDate { get; set; }
        public string RenewalType { get; set; }
        public string DetailOfRequest { get; set; }
        public string PowerOfAttorney { get; set; }
        public string CertificateOfTrademark { get; set; }
        public string PaymentReference { get; set; }
        public string userid { get; set; }
        public string Status { get; set; }
        public string PreviousApplicationStatus { get; set; }
        public string PreviousDataStatus { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? DateApproved { get; set; }

        [ForeignKey("Id")]
        public int applicationid { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.PatentApplication.PatentApplication Patentapplication { get; set; }
    }
}
