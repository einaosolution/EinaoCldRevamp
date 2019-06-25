using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IPORevamp.Data.Entities.Fee
{
    public class FeeList : EntityBase
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public string ItemCode { get; set; }
        [Required]
        public string QTCode { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal init_amt { get; set; }
        [Required]
        public decimal TechnologyFee { get; set; }
        [Required]
        public string Category { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.Twallet.Twallet> twallet { get; set; }
        public List<IPORevamp.Data.Entity.Interface.Entities.FeeDetail.FeeDetail> feedetail { get; set; }




    }
}
