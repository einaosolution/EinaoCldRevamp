using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Payment
{
    public interface IpaymentRepository : IAutoDependencyRegister
    {
        Task<IPORevamp.Data.Entity.Interface.Entities.Payment.Payment> SavePaymentUnit(IPORevamp.Data.Entity.Interface.Entities.Payment.Payment  payment);
        Task<IPORevamp.Data.Entity.Interface.Entities.Twallet.Twallet> SaveTwalletUnit(IPORevamp.Data.Entity.Interface.Entities.Twallet.Twallet twallet);
        Task<IPORevamp.Data.Entity.Interface.Entities.FeeDetail.FeeDetail> SaveFeeDetailUnit(IPORevamp.Data.Entity.Interface.Entities.FeeDetail.FeeDetail feedetail);

    }
}
