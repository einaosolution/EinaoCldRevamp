using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.PatentInformation
{
   public  class PatentInformation : EntityBase
    {
        [ForeignKey("Id")]
        public int PatentApplicationID { get; set; }

        [ForeignKey("Id")]
        public int PatentTypeID { get; set; }

        public string TitleOfInvention { get; set; }
        public string RegistrationNumber{ get; set; }
        public string InventionDescription { get; set; }
        public string LetterOfAuthorization { get; set; }
        public string Claims { get; set; }
        public string PctDocument { get; set; }
        public string DeedOfAssignment { get; set; }
        public string CompleteSpecificationForm { get; set; }


        public IPORevamp.Data.Entity.Interface.Entities.PatentApplication.PatentApplication PatentApplication { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.PatentType.PatentType PatentType { get; set; }
    }
}
