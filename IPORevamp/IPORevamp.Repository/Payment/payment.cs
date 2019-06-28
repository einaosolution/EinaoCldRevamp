using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Payment
{
    class payment : Ipayment
    {
        private IRepository<IPORevamp.Data.Entity.Interface.Entities.Payment.Payment> _payment;
        private IRepository <IPORevamp.Data.Entity.Interface.Entities.Twallet.Twallet> _twallet;
        private IRepository<IPORevamp.Data.Entity.Interface.Entities.FeeDetail.FeeDetail> _feedetail;
        public payment(IRepository<IPORevamp.Data.Entity.Interface.Entities.Payment.Payment> payment, IRepository<IPORevamp.Data.Entity.Interface.Entities.Twallet.Twallet> twallet, IRepository<IPORevamp.Data.Entity.Interface.Entities.FeeDetail.FeeDetail> feedetail)
        {
            _payment = payment;
            _twallet = twallet;
            _feedetail = feedetail;


        }



        public async Task<IPORevamp.Data.Entity.Interface.Entities.Payment.Payment> SavePaymentUnit(IPORevamp.Data.Entity.Interface.Entities.Payment.Payment payment)
        {

            var saveContent = await _payment.InsertAsync(payment);
            await _payment.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<IPORevamp.Data.Entity.Interface.Entities.Twallet.Twallet> SaveTwalletUnit(IPORevamp.Data.Entity.Interface.Entities.Twallet.Twallet twallet)
        {

            var saveContent = await _twallet.InsertAsync(twallet);
            await _twallet.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<IPORevamp.Data.Entity.Interface.Entities.FeeDetail.FeeDetail> SaveFeeDetailUnit(IPORevamp.Data.Entity.Interface.Entities.FeeDetail.FeeDetail feedetail)
        {

            var saveContent = await _feedetail.InsertAsync(feedetail);
            await _feedetail.SaveChangesAsync();

            return saveContent.Entity;
        }
    }
}
