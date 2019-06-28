using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.FeeDetail
{
  public   class FeeDetail : EntityBase
    {

        [ForeignKey("Id")]
        public int feelistid { get; set; }

       
        public int twalletid { get; set; }

        public double initial_amount { get; set; }

        public double tech_amount { get; set; }

        public double total_amount { get; set; }


        public IPORevamp.Data.Entities.Fee.FeeList FeeList { get; set; }

       
    }
}
