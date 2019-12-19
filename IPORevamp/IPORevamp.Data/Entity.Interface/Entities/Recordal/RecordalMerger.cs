using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.Recordal
{
  public   class RecordalMerger : EntityBase
    {
        public string AssignorName { get; set; }
        public string AssignorAddress { get; set; }
        public string AssigneeName { get; set; }
        public string AssigneeAddress { get; set; }

        [ForeignKey("Id")]
        public int applicationid { get; set; }

        [ForeignKey("CountryId")]

        public int AssigneeNationality { get; set; }

        public string DateOfAssignment { get; set; }
        public string DetailOfRequest { get; set; }
        public string PowerOfAttorney { get; set; }
        public string DeedOfAssigment { get; set; }
        public string Certificate { get; set; }
        public string userid { get; set; }
        public string Status { get; set; }
        public string PaymentReference { get; set; }
        public string PreviousApplicationStatus { get; set; }
        public string PreviousDataStatus { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime?  ApprovalDate { get; set; }



        public IPORevamp.Data.Entity.Interface.Entities.Pwallet.Application application { get; set; }

        public IPORevamp.Data.Entities.Country.Country Country { get; set; }

    }
}
