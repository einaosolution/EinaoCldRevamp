using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.PatentApplication
{
  public   class PatentDataResult
    {
        [Key]
        public string sn { get; set; }
        public DateTime FilingDate { get; set; }
        public string Status { get; set; }
        public string datastatus { get; set; }
        public string Transactionid { get; set; }
        public string Filenumber { get; set; }
        public string TitleOfInvention { get; set; }
        public string InventionDescription { get; set; }
        public string LetterOfAuthorization { get; set; }
        public string Claims { get; set; }
        public string PctDocument { get; set; }
        public string DeedOfAssignment { get; set; }
        public string CompleteSpecificationForm { get; set; }
        public string PatentType { get; set; }
        public int ApplicationId { get; set; }
        public string AssigneeName { get; set; }
        public string AssigneeAddress { get; set; }
        public string AssigneeCountry { get; set; }
        public string AssignorName { get; set; }
        public string AssignorAddress { get; set; }
        public string AssignorCountry { get; set; }



    }
}
