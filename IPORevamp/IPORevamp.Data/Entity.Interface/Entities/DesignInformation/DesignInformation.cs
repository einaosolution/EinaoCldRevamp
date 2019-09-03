using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.DesignInformation
{
   public  class DesignInformation : EntityBase
    {
        [ForeignKey("Id")]
        public int DesignApplicationID { get; set; }

        [ForeignKey("Id")]
        public int DesignTypeID { get; set; }

        [ForeignKey("Id")]
        public int NationClassID { get; set; }

        public string TitleOfDesign { get; set; }
        public string RegistrationNumber { get; set; }
        public string DesignDescription { get; set; }
        public string LetterOfAuthorization { get; set; }
        public string DeedOfAssignment { get; set; }
        public string PriorityDocument { get; set; }
        public string NoveltyStatement { get; set; }
        public string RepresentationOfDesign1 { get; set; }
        public string RepresentationOfDesign2 { get; set; }
        public string RepresentationOfDesign3 { get; set; }
        public string RepresentationOfDesign4 { get; set; }





        public IPORevamp.Data.Entity.Interface.Entities.DesignApplication.DesignApplication DesignApplication { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.DesignType.DesignType DesignType { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.National_Class.NationalClass NationalClass { get; set; }

        

    }
}
