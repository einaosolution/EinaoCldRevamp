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
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.Fee;

namespace IPORevamp.Repository.Fee
{
    public interface IFeeListRepository : IAutoDependencyRegister
    {
        #region Fee Respository

        Task<FeeList> SaveFeeList(FeeList FeeList);
        Task<FeeList> GetFeeListById(int FeeListId);
        Task<FeeList> GetFeeListByName(string FeeListName);
        Task<List<FeeList>> GetFeeLists();
        Task<FeeList> CheckExistingFeeList(string FeeListName);
        Task<List<FeeList>> GetFeeListsByCategory(string Category);
        Task<FeeList> UpdateFeeList(FeeList FeeList);
        Task<FeeList> DeleteFeeList(FeeList FeeList);
        #endregion


    }
}
