using IPORevamp.Data.Entity.Interface.Entities.Payment;
using IPORevamp.Data.Entity.Interface.Entities.RemitaPayment;
using IPORevamp.Repository.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using static IPORevamp.Data.Entity.Interface.Entities.RemitaPayment.RemitaPayment;

namespace IPORevamp.Repository.RemittaPayment
{
    public interface IRemitaPaymentRepository : IAutoDependencyRegister
    {
        #region RemittaPayment Respository

        Task<RemitaPayment> SaveRemitaPayment(RemitaPayment model);
        void SaveRemitalineitemPayment(LineItem model);
        void SaveRemitaCustomPayment(CustomField model);
        Task<List<RemitaPayment>> FetchByOrderId(string orderId);
        Task<List<RemitaPayment>> FetchByRRRCode(string RRR);
        Task<List<PaymentReport>> FetchPayments(string startdate, string enddate);
        Task<List<PaymentReport>> FetchPaymentsByFee(string startdate, string enddate, string fee);
        Task<RemitaPayment> UpdateRemitaPayment(RemitaPayment model);

        



        #endregion


    }
}
