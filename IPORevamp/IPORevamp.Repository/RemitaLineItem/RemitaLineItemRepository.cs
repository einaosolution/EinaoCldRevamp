using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Core.Utilities;
using IPORevamp.Data;
using IPORevamp.Data.Entities.AuditTrail;



using IPORevamp.Data.UserManagement.Model;

using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using System.Data.SqlClient;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entities;
using IPORevamp.Repository.RemitaAccountSplit;
using IPORevamp.Data.Entity.Interface.Entities.RemitaPayment;

namespace IPORevamp.Repository.RemitaLineItem
{
    public class RemitaLineItemRepository : IRemitaLineItemRepository
    {
   
        private IRepository<LineItem> _RemitaLineItemRepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public RemitaLineItemRepository(IRepository<LineItem> RemitaLineItemRepository, IAuditTrailManager<AuditTrail> auditTrailManager
           )
        {
            _RemitaLineItemRepository = RemitaLineItemRepository;
            _auditTrailManager = auditTrailManager;

        }


    

        public async Task<List<LineItem>> GetAllTransactionLineItems(string orderId)
        {
            return _RemitaLineItemRepository.GetAllList(a => a.OrderId == orderId);
        }

        public async Task<LineItem> SaveLineItem(LineItem lineItem)
        {
            var saveContent = await _RemitaLineItemRepository.InsertAsync(lineItem);
            await _RemitaLineItemRepository.SaveChangesAsync();

            return saveContent.Entity;
        }
    }
}
