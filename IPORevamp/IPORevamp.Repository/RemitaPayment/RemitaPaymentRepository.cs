using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entity.Interface.Entities.RemitaPayment;
using IPORevamp.Repository.Interface;
using IPORevamp.Repository.RemittaPayment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static IPORevamp.Data.Entity.Interface.Entities.RemitaPayment.RemitaPayment;

namespace IPORevamp.Repository.RemitaPaymentRepository
{
    public class RemitaPaymentRepository : IRemitaPaymentRepository
    {
        private readonly IRepository<RemitaPayment> _remitapaymentrepository;
        private readonly IRepository<LineItem> _remitalineItemrepository;
        private readonly IRepository<CustomField> _remitacustomFieldrepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;
        private readonly IConfiguration _configuration;

        public RemitaPaymentRepository(IRepository<RemitaPayment> remitapaymentrepository, IAuditTrailManager<AuditTrail> auditTrailManager, IRepository<LineItem> remitalineItemrepository,
            IRepository<CustomField> remitacustomFieldrepository, IConfiguration configuration)
        {
            _remitapaymentrepository = remitapaymentrepository;
            _remitalineItemrepository = remitalineItemrepository;
            _remitacustomFieldrepository = remitacustomFieldrepository;
            _auditTrailManager = auditTrailManager;
            _configuration = configuration;
        }

        public async Task<List<RemitaPayment>> FetchByOrderId(string orderId)
        {
            var payments = await _remitapaymentrepository.GetAllListAsync(x => x.OrderId == orderId);
            return payments;
        }

        public async Task<List<RemitaPayment>> FetchByRRRCode(string RRR)
        {
            var payments = await _remitapaymentrepository.GetAllListAsync(x => x.RRR == RRR);
            return payments;
        }


        public void SaveRemitaCustomPayment(CustomField model)
        {
            throw new NotImplementedException();
        }

        public async void SaveRemitalineitemPayment(List<LineItem> model)
        {
            foreach (var item in model)
            {
                  _remitalineItemrepository.Insert(item);
            }               
              await _remitalineItemrepository.SaveChangesAsync();     
        }

        public void SaveRemitalineitemPayment(LineItem model)
        {
            throw new NotImplementedException();
        }

        public async Task<RemitaPayment> SaveRemitaPayment(RemitaPayment model)
        {
            var saveContent = await _remitapaymentrepository.InsertAsync(model);
            await _remitapaymentrepository.SaveChangesAsync();
            return saveContent.Entity;
        }

      

        public async Task<RemitaPayment> UpdateRemitaPayment(RemitaPayment model)
        {
            var saveContent = await _remitapaymentrepository.UpdateAsync(model);
            await _remitapaymentrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

    }
}
