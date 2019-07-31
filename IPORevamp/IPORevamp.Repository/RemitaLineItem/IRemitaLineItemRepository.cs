using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Data;


using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface.Entities.RemitaPayment;

namespace IPORevamp.Repository.RemitaLineItem
{
    public interface IRemitaLineItemRepository : IAutoDependencyRegister
    {
        #region RemitaLineItem Respository

        Task<List<LineItem>> GetAllTransactionLineItems(string orderId);
        Task<LineItem> SaveLineItem(LineItem lineItem);


        #endregion


    }
}
