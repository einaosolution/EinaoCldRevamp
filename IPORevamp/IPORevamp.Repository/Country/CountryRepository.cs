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
using IPORevamp.Data.Entities.Country;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.LGAs;

namespace IPORevamp.Repository.Country
{
    public class CountryRepository : ICountryRepository
    {
        private IRepository<Data.Entities.Country.Country> _countryrepository;
        private IRepository<State> _staterepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public CountryRepository(IRepository<Data.Entities.Country.Country> countryrepository,
            IRepository<State> staterepository,
            IRepository<Data.Entities.LGAs.LGA> lgarepository, 
            
            IAuditTrailManager<AuditTrail> auditTrailManager,
            IRepository<Data.Entities.Setting.Setting> settingrepository,
            IRepository<EmailTemplate> emailTemplaterepository)
        {
            _countryrepository = countryrepository;
            _staterepository = staterepository;
            _auditTrailManager = auditTrailManager;

        }
        
        

        // Get Country By Id
        public async Task<Data.Entities.Country.Country> GetCountryById(int CountryId, bool IncludeStates, bool IncludeStateLGAs)
        {
           

            Data.Entities.Country.Country country = new Data.Entities.Country.Country();
            if (IncludeStates == true && IncludeStateLGAs==false)
            {
                 country = await _countryrepository.GetAll().Include(a => a.States).FirstOrDefaultAsync(x => x.Id == CountryId);
            }
            else if (IncludeStateLGAs == true)
            {
                country = await _countryrepository.GetAll().Include(a => a.States).Include(x=>x.LGA).FirstOrDefaultAsync(x => x.Id == CountryId);
            }
            else
            {
                 country = await _countryrepository.GetAll().FirstOrDefaultAsync(x => x.Id == CountryId);
            }
          
            return country;
        }


        // Get All Countries 
        public async Task<List<Data.Entities.Country.Country>> GetCountries()
        {
            List<Data.Entities.Country.Country> country = new List<Data.Entities.Country.Country>();
                country = await _countryrepository.GetAllListAsync();
            return country;
        }


        

       

        public async Task<Data.Entities.Country.Country> UpdateCountry(Data.Entities.Country.Country country)
        {
            var saveContent = await _countryrepository.UpdateAsync(country);
            await _countryrepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<Data.Entities.Country.Country> DeleteCountry(Data.Entities.Country.Country country)
        {
            var saveContent = await _countryrepository.UpdateAsync(country);
            await _countryrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<Data.Entities.Country.Country> SaveCountry(Data.Entities.Country.Country country)
        {

                 var saveContent = await _countryrepository.InsertAsync(country);
                                   await _countryrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        // Get Country By Name

        public async Task<Data.Entities.Country.Country> GetCountryByName(string CountryName, bool IncludeStates, bool IncludeStateLGAs)
        {

            Data.Entities.Country.Country country = new Data.Entities.Country.Country();
            if (IncludeStates == true && IncludeStateLGAs==false)
            {
                country = await _countryrepository.GetAll().Include(a => a.States).FirstOrDefaultAsync(x => x.Name.ToUpper() == CountryName.ToUpper());
            }
            else if (IncludeStateLGAs == true)
            {
                country = await _countryrepository.GetAll().Include(a => a.States).Include(a => a.LGA).FirstOrDefaultAsync(x => x.Name.ToUpper() == CountryName.ToUpper());
            }

            else
            {
                country = await _countryrepository.GetAll().FirstOrDefaultAsync(x => x.Name.ToUpper() == CountryName.ToUpper());
            }

            return country;
        }

        public async Task<Data.Entities.Country.Country> CheckExistingCountry(string CountryName)
        {
            Data.Entities.Country.Country country = new Data.Entities.Country.Country();
           
                country = await _countryrepository.GetAll().FirstOrDefaultAsync(x => x.Name.ToUpper() == CountryName.ToUpper());
            

            return country;
        }

        public async Task<Data.Entities.Country.Country> GetCountryById(int CountryId)
        {
            Data.Entities.Country.Country country = new Data.Entities.Country.Country();
            country = await _countryrepository.GetAll().FirstOrDefaultAsync(x => x.Id == CountryId);

            return country;
        }
    }
}
