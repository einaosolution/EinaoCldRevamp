using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Recordal
{
  public   class ChangeOfName : EntityBase
    {
        public string OldApplicantFirstName { get; set; }
        public string OldApplicantSurname { get; set; }
        public string NewApplicantFirstname { get; set; }
        public string NewApplicantSurname { get; set; }

        public string userid { get; set; }
        public string Status { get; set; }

        public string PaymentReference { get; set; }
        public string PreviousApplicationStatus { get; set; }
        public string PreviousDataStatus { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string ApprovedBy { get; set; }

        [ForeignKey("Id")]
        public int applicationid { get; set; }


        public IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application application { get; set; }
    }
}
