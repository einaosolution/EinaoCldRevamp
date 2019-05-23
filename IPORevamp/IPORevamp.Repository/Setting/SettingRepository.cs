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


using IPORevamp.Data.Entities.Payment;
using IPORevamp.Data.UserManagement.Model;

using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using System.Data.SqlClient;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entities.Country;
using IPORevamp.Data.Entities.LGAs;
using IPORevamp.Data.Entities;

namespace IPORevamp.Repository.Setting
{
    public class SettingRepository : ISettingRepository
    {
        private IRepository<IPORevamp.Data.Entities.Setting.Setting> _settingrepository;

       
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public SettingRepository(IRepository<IPORevamp.Data.Entities.Setting.Setting> settingrepository,
                                  IAuditTrailManager<AuditTrail> auditTrailManager)
        {
            _settingrepository = settingrepository;
           
            _auditTrailManager = auditTrailManager;

        }
        
        



        // Get All States 
        public async Task<List<IPORevamp.Data.Entities.Setting.Setting>> GetSettings()
        {
           var  entity = await _settingrepository.GetAllListAsync();
           return entity;
        }


        

     
        public async Task<IPORevamp.Data.Entities.Setting.Setting> GetSettingById(int settingid)
        {
            IPORevamp.Data.Entities.Setting.Setting country = new IPORevamp.Data.Entities.Setting.Setting();
            country = await _settingrepository.GetAll().FirstOrDefaultAsync(x => x.Id == settingid);

            return country;
        }
        public async Task<IPORevamp.Data.Entities.Setting.Setting> UpdateSetting(IPORevamp.Data.Entities.Setting.Setting setting)
        {
            var entity = await _settingrepository.UpdateAsync(setting);
            await _settingrepository.SaveChangesAsync();

            return entity.Entity;
        }


        public async Task<IPORevamp.Data.Entities.Setting.Setting> DeleteSetting(IPORevamp.Data.Entities.Setting.Setting entity)
        {
            var saveContent = await _settingrepository.UpdateAsync(entity);
            await _settingrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<IPORevamp.Data.Entities.Setting.Setting> SaveSetting(IPORevamp.Data.Entities.Setting.Setting entity)
        {

                 var saveContent = await _settingrepository.InsertAsync(entity);
                                   await _settingrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        // Get Country By Name

        public async Task<List<IPORevamp.Data.Entities.Setting.Setting>> GetSettingByCode(string SettingCode)
        {
            var content = await _settingrepository.GetAll().Where(a => a.ItemValue == SettingCode).ToListAsync();          
            return content;
        }

       
    }
}
