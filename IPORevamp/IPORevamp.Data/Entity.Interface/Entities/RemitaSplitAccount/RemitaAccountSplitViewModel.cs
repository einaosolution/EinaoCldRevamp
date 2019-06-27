using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.RemitaSplitAccount
{
    public class RemitaAccountSplitViewModel
    {
        public int RemitaAccountSplitId { get; set; }
        public string BeneficiaryAccount { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryBank { get; set; }
        public string DeductFee { get; set; }
        public int CreatedBy { get; set; }
        public int IsActive { get; set; }

    }
}
