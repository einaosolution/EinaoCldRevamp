using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Data;
using IPORevamp.Data.Entities.Event;
using IPORevamp.Data.Entities.Modules;
using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entities;
 
namespace IPORevamp.Repository.Setting
{
    public interface ISettingRepository : IAutoDependencyRegister
    {
        #region Setting Respository

        Task<IPORevamp.Data.Entities.Setting.Setting> SaveSetting(IPORevamp.Data.Entities.Setting.Setting setting);
        Task<IPORevamp.Data.Entities.Setting.Setting> GetSettingById(int SettingId);
        Task<List<IPORevamp.Data.Entities.Setting.Setting>> GetSettingByCode(string Code);
        Task<List<IPORevamp.Data.Entities.Setting.Setting>> GetSettings();


        Task<IPORevamp.Data.Entities.Setting.Setting> UpdateSetting(IPORevamp.Data.Entities.Setting.Setting setting);
        Task<IPORevamp.Data.Entities.Setting.Setting> DeleteSetting(IPORevamp.Data.Entities.Setting.Setting setting);
        #endregion


    }
}
