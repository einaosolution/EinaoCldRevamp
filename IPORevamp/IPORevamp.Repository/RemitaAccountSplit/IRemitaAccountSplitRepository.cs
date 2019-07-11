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

namespace IPORevamp.Repository.RemitaAccountSplit
{
    public interface IRemitaAccountSplitRepository : IAutoDependencyRegister
    {
        #region RemitaAccountSplit Respository

        Task<Data.Entities.RemitaAccountSplit> SaveRemitaAccountSplit(Data.Entities.RemitaAccountSplit RemitaAccountSplit);
        Task<Data.Entities.RemitaAccountSplit> GetRemitaAccountSplitByName(string RemitaAccountSplitName);
        Task<List<Data.Entities.RemitaAccountSplit>> GetRemitaAccountSplits();
        Task<Data.Entities.RemitaAccountSplit> CheckExistingRemitaAccountSplit(string RemitaAccountSplitName);
        Task<Data.Entities.RemitaAccountSplit> GetRemitaAccountSplitById(int RemitaAccountSplitId);
        Task<Data.Entities.RemitaAccountSplit> UpdateRemitaAccountSplit(Data.Entities.RemitaAccountSplit RemitaAccountSplit);
         Task<Data.Entities.RemitaAccountSplit> DeleteRemitaAccountSplit(Data.Entities.RemitaAccountSplit RemitaAccountSplit);
        #endregion


    }
}
