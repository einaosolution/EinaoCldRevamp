using IPORevamp.Data.Entities.Country;
using IPORevamp.Data.Entities.Setting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IPORevamp.Data.Entities
{
    public class FeeListViewModel
    {
        public int FeeId { get; set; }
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
        public int IsActive { get; set; }

        public int CreatedBy { get; set; }


    }
}
