using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.RemitaBankCode
{
    public class RemitaBankCodeViewModel
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public int CreatedBy { get; set; }
        public int IsActive { get; set; }

    }
}
