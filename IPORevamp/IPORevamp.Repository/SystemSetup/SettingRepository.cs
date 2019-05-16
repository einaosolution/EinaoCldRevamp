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

namespace IPORevamp.Repository.SystemSetup
{
    public class SettingRepository : ISettingRepository
    {
        private IRepository<Country> _countryrepository;
        private IRepository<EmailTemplate> _emailtemplaterepository;
        private IRepository<State> _staterepository;
        private IRepository<lga> _lgarepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;
        private IRepository<AccountType> _accounttyperepository;
        private IRepository<TMApplicationStatus> _tmApplicationStatusrepository;
        private IRepository<DSApplicationStatus> _dsApplicationStatusrepository;
        private IRepository<PTApplicationStatus> _ptApplicationStatusrepository;
        private IRepository<DSApplicationStatus> _dsApplicationStatus;
        private IRepository<Setting> _settingrepository;
        private IRepository<UserVerificationTemp> _userVerirepository;



        public SettingRepository(IRepository<Country> countryrepository,
            IRepository<State> staterepository,
            IRepository<lga> lgarepository, 
            IRepository<TMApplicationStatus> tmApplicationStatusrepository, 
            IAuditTrailManager<AuditTrail> auditTrailManager,
            IRepository<UserVerificationTemp> userVerirepository,
            IRepository<DSApplicationStatus> dsApplicationStatus, IRepository<Setting> settingrepository,
            IRepository<EmailTemplate> emailTemplaterepository, IRepository<PTApplicationStatus> ptApplicationStatusrepository)
        {
            _countryrepository = countryrepository;
            _userVerirepository = userVerirepository;
            _staterepository = staterepository;
            _tmApplicationStatusrepository = tmApplicationStatusrepository;
            _lgarepository = lgarepository;
            _auditTrailManager = auditTrailManager;
            _dsApplicationStatus = dsApplicationStatus;
            _settingrepository = settingrepository;
            _emailtemplaterepository = emailTemplaterepository;
            _ptApplicationStatusrepository = ptApplicationStatusrepository;


        }
        
        //public async Task<Country> SaveCountry(Country country)
        //{
           
        //    var saveContent = await _countryrepository.InsertOrUpdateAsync(country);
        //                        _countryrepository.SaveChanges();
        //    return saveContent.Entity;
        //}


        public async Task<UserVerificationTemp> SaveUserVerification(UserVerificationTemp userverificationTemp)
        {
            var saveContent = await _userVerirepository.InsertOrUpdateAsync(userverificationTemp);
                                    _userVerirepository.SaveChanges();

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

        public async Task<Setting> GetSettings(string SetSettingCode)
        {
            var setting  = await _settingrepository.GetAll().FirstOrDefaultAsync(a => a.SettingCode.ToLower()
                                                       == SetSettingCode.ToLower());
            return setting;
           
        }

        public async Task<State> SaveState(State state)
        {
            var saveContent = await _staterepository.InsertOrUpdateAsync(state);
            _countryrepository.SaveChanges();
            return saveContent.Entity;
        }

        public async Task<List<State>> GetStates()
        {
            var contents = await _staterepository.GetAllListAsync();
            return contents;
        }

        public async Task<List<State>> GetStatesByCountryId(int Id)
        {
            var contents =   _staterepository.GetAllList().Where(a => a.Country.Id == Id).ToList();
            return contents;
        }

        public Task<State> GetStatesById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<lga> SaveLocalGovtArea(lga lga)
        {
            var saveContent = await _lgarepository.InsertOrUpdateAsync(lga);
            _countryrepository.SaveChanges();
            return saveContent.Entity;
        }

        public Task<State> GetLgaById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<PTApplicationStatus> SavePatentApplicationStatus(PTApplicationStatus pTApplicationStatus)
        {
            var saveContent = await _ptApplicationStatusrepository.InsertOrUpdateAsync(pTApplicationStatus);
            _ptApplicationStatusrepository.SaveChanges();
            return saveContent.Entity;
        }

        public async Task<PTApplicationStatus> GetPatentApplicationStatusById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<TMApplicationStatus> SaveTradeMarkApplicationStatus(TMApplicationStatus tMApplicationStatus)
        {
            var saveContent = await _tmApplicationStatusrepository.InsertOrUpdateAsync(tMApplicationStatus);
            _tmApplicationStatusrepository.SaveChanges();
            return saveContent.Entity;
        }

        public Task<TMApplicationStatus> GetTradeMarkApplicationStatusById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<DSApplicationStatus> SaveDesignApplicationStatus(DSApplicationStatus dSApplicationStatus)
        {
            var saveContent = await _dsApplicationStatusrepository.InsertOrUpdateAsync(dSApplicationStatus);
            _tmApplicationStatusrepository.SaveChanges();
            return saveContent.Entity;
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

      

        public async Task<List<Setting>> GetSettingsByCode(string SetSettingCode)
        {
            var settings = await _settingrepository.GetAll().Where(x => x.SettingCode == SetSettingCode).ToListAsync();
            return settings;
        }

        public Task<UserVerificationTemp> EmailConfirmation(string Code)
        {
            var model = _userVerirepository.
                FirstOrDefaultAsync(a => a.Email.ToLower() 
                  == Code.ToLower() && a.ExpiringDate > DateTime.Now  &&  a.ConfirmationDate == null);

            return model;
        }

        public  Task<UserVerificationTemp> ValidateVerificationEmail(string Email)
        {
            var model =  _userVerirepository.
                FirstOrDefaultAsync(a => a.Email.ToLower()
                  == Email.ToLower() && a.expired != true);

            return model;
        }

        //public async Task<List<EmailTemplate>> GetEmailTemplateByCode(string EmailCode)
        //{
           
        //    var emailTemplates = await _emailtemplaterepository.GetAll().Where(x => x.EmailName == EmailCode).ToListAsync();
        //    return emailTemplates;
        //}

        public async Task<EmailTemplate> GetEmailTemplateByCode(string EmailCode)
        {           
            var emailTemplates = await _emailtemplaterepository.
                                                               FirstOrDefaultAsync(x => x.EmailName == EmailCode && x.IsActive==true);
            return emailTemplates;
        }

        public async Task<Country> SaveUpdateCountry(Country country)
        {
            var saveContent = await _countryrepository.InsertOrUpdateAsync(country);
            _countryrepository.SaveChanges();
            return saveContent.Entity;
        }

        public async Task<Country> GetCountryByName(string CountryName)
        {
            var country =  await _countryrepository.FirstOrDefaultAsync(x => x.Name == CountryName);
            return country;
        }

        public async Task<State> GetStatesByName(string StateName)
        {
            var country = await _staterepository.FirstOrDefaultAsync(x => x.StateName == StateName);
            return country;
        }

        public  async Task<EmailTemplate> SaveUpdateEmailTemplate(EmailTemplate emailTemplate)
        {
            var saveContent = await _emailtemplaterepository.InsertOrUpdateAsync(emailTemplate);
            _countryrepository.SaveChanges();
            return saveContent.Entity;
        }

       

        public async Task<EmailTemplate> GetEmailTemplateById(int Id)
        {

            var emailtemplate = await _emailtemplaterepository.FirstOrDefaultAsync(x => x.Id == Id);
            return emailtemplate;
        }
    }
}
