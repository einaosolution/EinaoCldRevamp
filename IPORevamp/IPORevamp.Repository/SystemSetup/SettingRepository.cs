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

namespace IPORevamp.Repository.SystemSetup
{
    public class SettingRepository : ISettingRepository
    {
        private IRepository<Country> _countryrepository;

        private IRepository<State> _staterepository;
        private IRepository<lga> _lgarepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;
        private IRepository<AccountType> _accounttyperepository;
        private IRepository<TMApplicationStatus> _tmApplicationStatusrepository;
        private IRepository<DSApplicationStatus> _dsApplicationStatusrepository;
        private IRepository<PTApplicationStatus> _ptApplicationStatusrepository;
        private IRepository<DSApplicationStatus> _dsApplicationStatus;
        private IRepository<Setting> _settingrepository;
     

        public SettingRepository(IRepository<Country> countryrepository,
            IRepository<State> staterepository,
            IRepository<lga> lgarepository, 
            IRepository<TMApplicationStatus> tmApplicationStatusrepository, 
            IAuditTrailManager<AuditTrail> auditTrailManager,
            IRepository<DSApplicationStatus> dsApplicationStatus, IRepository<Setting> settingrepository)
        {
            _countryrepository = countryrepository;
            _staterepository = staterepository;
            _tmApplicationStatusrepository = tmApplicationStatusrepository;
            _lgarepository = lgarepository;
            _auditTrailManager = auditTrailManager;
            _dsApplicationStatus = dsApplicationStatus;
            _settingrepository = settingrepository;


        }
        
        public async Task<Country> SaveCountry(Country country)
        {
           
            var saveContent = await _countryrepository.InsertOrUpdateAsync(country);
            _countryrepository.SaveChanges();
            return saveContent.Entity;
        }

       
      
    
        public async Task<Country> GetCountryById(int CountryId)
        {
            var country = await _countryrepository.GetAll().FirstOrDefaultAsync(x => x.Id == CountryId);
            return country;
        }

        public async Task<List<Country>> GetCountries()
        {

          
            var getCountries = await _countryrepository.GetAll().ToListAsync();
            return getCountries;
        }

        /// <summary>
        ///  This a call with a store procedure 
        /// </summary>
        /// <returns></returns>
       


        public async Task<AccountType> AccountType(string AccountTypeCode)
        {
            var accountType = await _accounttyperepository.GetAll().FirstOrDefaultAsync(a => a.AccountTypeCode.ToLower()
                                                       == AccountTypeCode.ToLower());
            return accountType;

        }

        public async Task<Setting> SaveSetting(Setting setting)
        {
            var saveContent = await _settingrepository.InsertOrUpdateAsync(setting);
            _countryrepository.SaveChanges();
            return saveContent.Entity;
        }

        public async Task<List<Setting>> GetSettings()
        {
            var settings = await _settingrepository.GetAllListAsync();
            return settings;
        }

        public async Task<List<Setting>> GetSettings(string SetSettingCode)
        {
            throw new NotImplementedException();
        }

        public Task<State> SaveState(State state)
        {
            throw new NotImplementedException();
        }

        public Task<List<State>> GetStates()
        {
            throw new NotImplementedException();
        }

        public Task<State> GetStatesById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<lga> SaveLocalGovtArea(lga state)
        {
            throw new NotImplementedException();
        }

        public Task<State> GetLgaById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<PTApplicationStatus> SavePatentApplicationStatus(PTApplicationStatus pTApplicationStatus)
        {
            throw new NotImplementedException();
        }

        public Task<PTApplicationStatus> GetPatentApplicationStatusById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<TMApplicationStatus> SaveTradeMarkApplicationStatus(TMApplicationStatus tMApplicationStatus)
        {
            throw new NotImplementedException();
        }

        public Task<TMApplicationStatus> GetTradeMarkApplicationStatusById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<DSApplicationStatus> SaveDesignApplicationStatus(DSApplicationStatus dSApplicationStatus)
        {
            throw new NotImplementedException();
        }

        public Task<DSApplicationStatus> GetDesignApplicationStatusById(int Id)
        {
            throw new NotImplementedException();
        }

        public  Task<List<CountryStateViewModel>> GetCountryStateByCode(string CountryCode)
        {
           // var model = _countryrepository.ExecuteStoreprocedure<CountryStateViewModel>("GetspGetCountrySataes   @CountryCode", new SqlParameter("CountryCode", CountryCode));
            //var model = await _countryrepository.ExecuteStoreprocedure<CountryStateViewModel>("SpGetAssignmentByUserId @CountryCode", new SqlParameter("CountryCode", GetCurrentUserId())).ToListAsync();
            //return View(model);
            return null;
        }
    }
}
