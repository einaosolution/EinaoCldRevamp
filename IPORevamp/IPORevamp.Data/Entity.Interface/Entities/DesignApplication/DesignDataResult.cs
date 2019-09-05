using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.DesignApplication
{
  public  class DesignDataResult
    {
        [Key]
        public string sn { get; set; }
        public DateTime FilingDate { get; set; }
        public string Status { get; set; }
        public int  NationClassID { get; set; }
        public string datastatus { get; set; }
        public string ClassDescription { get; set; }
        public string Transactionid { get; set; }
        public string Filenumber { get; set; }
        public string TitleOfInvention { get; set; }
        public string DesignDescription { get; set; }
        public string LetterOfAuthorization { get; set; }
        public string DeedOfAssignment { get; set; }
        public string PriorityDocument { get; set; }
        public string NoveltyStatement { get; set; }
        public string RepresentationOfDesign1 { get; set; }
        public string RepresentationOfDesign2 { get; set; }
        public string RepresentationOfDesign3 { get; set; }
        public string RepresentationOfDesign4 { get; set; }
        public string DesignType { get; set; }
        public int ApplicationId { get; set; }
        public string AssigneeName { get; set; }
        public string AssigneeAddress { get; set; }
        public string AssigneeCountry { get; set; }
        public string AssignorName { get; set; }
        public string AssignorAddress { get; set; }
        public string AssignorCountry { get; set; }
    }
}
