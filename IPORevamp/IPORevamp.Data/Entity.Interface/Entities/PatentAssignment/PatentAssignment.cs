using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.PatentAssignment
{
  public   class PatentAssignment : EntityBase
    {
        [ForeignKey("Id")]
        public int PatentApplicationID { get; set; }

        public string AssigneeName { get; set; }
        public string AssigneeAddress { get; set; }

      
     

        public string AssignorName { get; set; }
        public string AssignorAddress { get; set; }
       

        public DateTime  DateOfAssignment { get; set; }
        [ForeignKey("AssigneeNationality"), Column(Order = 0)]
        public int AssigneeNationalityId { get; set; }
        [ForeignKey("AssignorNationality"), Column(Order = 1)]
        public int AssignorNationalityId { get; set; }

        public IPORevamp.Data.Entity.Interface.Entities.PatentApplication.PatentApplication PatentApplication { get; set; }
       
        public virtual IPORevamp.Data.Entities.Country.Country AssigneeNationality { get; set; }
      
        public virtual IPORevamp.Data.Entities.Country.Country AssignorNationality { get; set; }
       



    }
}
