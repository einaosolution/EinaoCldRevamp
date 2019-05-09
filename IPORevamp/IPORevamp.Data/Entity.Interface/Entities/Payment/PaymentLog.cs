using EmailEngine.Base.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.UserManagement.Model;

namespace IPORevamp.Data.Entities.Payment
{
    public class PaymentLog: VatPaymentLog<ApplicationUser, int>, IEntity, IAudit
    {

    }
}
