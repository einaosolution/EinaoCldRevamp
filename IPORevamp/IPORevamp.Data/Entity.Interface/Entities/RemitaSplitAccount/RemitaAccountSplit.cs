using IPORevamp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entities
{
    public class RemitaAccountSplit : EntityBase
    {
        public string BeneficiaryAccount { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryBank { get; set; }
        public string  DeductFee { get; set; }
    }
}
