using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.DesignAssigment
{
   public  class DesignAssignment : EntityBase
    {
        [ForeignKey("Id")]
        public int DesignApplicationID { get; set; }

        public string AssigneeName { get; set; }
        public string AssigneeAddress { get; set; }




        public string AssignorName { get; set; }
        public string AssignorAddress { get; set; }


        public DateTime DateOfAssignment { get; set; }
        [ForeignKey("AssigneeNationality2"), Column(Order = 0)]
        public int AssigneeNationalityId { get; set; }
        [ForeignKey("AssignorNationality2"), Column(Order = 1)]
        public int AssignorNationalityId { get; set; }

       

        public virtual IPORevamp.Data.Entities.Country.Country AssigneeNationality2 { get; set; }

        public virtual IPORevamp.Data.Entities.Country.Country AssignorNationality2 { get; set; }


        public IPORevamp.Data.Entity.Interface.Entities.DesignApplication.DesignApplication DesignApplication { get; set; }


    }
}
