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
using IPORevamp.Data.Entities.Event;
using IPORevamp.Data.Entities.Modules;
using IPORevamp.Data.Entities.Payment;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.Interface;
using IPORevamp.Repository.Event;
using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using System.Data.SqlClient;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.lg;

namespace IPORevamp.Repository.SystemSetup
{
    public class CountryRepository 
    {
        private IRepository<Data.Entities.Country.Country> _countryrepository;
        private IRepository<EmailTemplate> _emailtemplaterepository;
        private IRepository<State> _staterepository;
        private IRepository<lga> _lgarepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;
        private IRepository<AccountType> _accounttyperepository;
        private IRepository<TMApplicationStatus> _tmApplicationStatusrepository;
        private IRepository<DSApplicationStatus> _dsApplicationStatusrepository;
        private IRepository<PTApplicationStatus> _ptApplicationStatusrepository;
        private IRepository<DSApplicationStatus> _dsApplicationStatus;
        //private IRepository<Setting> _settingrepository;
        private IRepository<UserVerificationTemp> _userVerirepository;



        public CountryRepository(IRepository<Data.Entities.Country.Country> countryrepository,
            IRepository<State> staterepository,
            IRepository<lga> lgarepository, 
            IRepository<TMApplicationStatus> tmApplicationStatusrepository, 
            IAuditTrailManager<AuditTrail> auditTrailManager,
            IRepository<UserVerificationTemp> userVerirepository,
            IRepository<DSApplicationStatus> dsApplicationStatus,
            IRepository<EmailTemplate> emailTemplaterepository, IRepository<PTApplicationStatus> ptApplicationStatusrepository)
        {
            _countryrepository = countryrepository;
            _userVerirepository = userVerirepository;
            _staterepository = staterepository;
            _tmApplicationStatusrepository = tmApplicationStatusrepository;
            _lgarepository = lgarepository;
            _auditTrailManager = auditTrailManager;
            _dsApplicationStatus = dsApplicationStatus;
            //_settingrepository = settingrepository;
            _emailtemplaterepository = emailTemplaterepository;
            _ptApplicationStatusrepository = ptApplicationStatusrepository;


        }
      





        /// <summary>
        ///  This a call with a store procedure 
        /// </summary>
        /// <returns></returns>
       


       

        //public async Task<Setting> SaveSetting(Setting setting)
        //{
        //    var saveContent = await _settingrepository.InsertOrUpdateAsync(setting);
        //    _countryrepository.SaveChanges();
        //    return saveContent.Entity;
        //}

        //public async Task<List<Setting>> GetSettings()
        //{
        //    var settings = await _settingrepository.GetAllListAsync();
        //    return settings;
        //}

        //public async Task<Setting> GetSettings(string SetSettingCode)
        //{
        //    var setting  = await _settingrepository.GetAll().FirstOrDefaultAsync(a => a.SettingCode.ToLower()
        //                                               == SetSettingCode.ToLower());
        //    return setting;
           
        //}

       
       

        


      

      
      

     
    }
}
