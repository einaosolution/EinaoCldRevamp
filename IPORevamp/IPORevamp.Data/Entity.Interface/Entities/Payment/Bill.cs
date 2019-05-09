using EmailEngine.Base.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.UserManagement.Model;

namespace IPORevamp.Data.Entities.Payment
{
    public class BillLog:VatBill<ApplicationUser, int, PaymentLog >, IEntity, IAudit
    {

    }

    public enum BillStatus
    {
        Initiated = 1,
        Paid,
        PaymentFailed
    }

    public enum BillType
    {
        EventCreation = 1,
        EventRegistration
    }
}
