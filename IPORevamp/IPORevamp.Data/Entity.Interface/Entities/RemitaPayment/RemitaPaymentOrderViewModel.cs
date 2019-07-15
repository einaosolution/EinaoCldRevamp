using IPORevamp.Data.UserManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPORevamp.Data.Entity.Interface.Entities.RemitaPayment
{
    public class RemitaPaymentOrderViewModel
    {
        public virtual RemitaPayment RemitaPayment { get; set; }

        public virtual  List<RemitaLineItem> RemitaLineItem { get; set; }
        public virtual  ApplicationUser ApplicationUser { get; set; }
    }
}
