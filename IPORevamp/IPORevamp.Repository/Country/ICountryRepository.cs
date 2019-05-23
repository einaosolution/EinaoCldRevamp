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

namespace IPORevamp.Repository.Country
{
    public interface ICountryRepository : IAutoDependencyRegister
    {
        #region Country Respository

        Task<Data.Entities.Country.Country> SaveCountry(Data.Entities.Country.Country country);
        Task<Data.Entities.Country.Country> GetCountryById(int CountryId, bool IncludeStates, bool IncludeLGAs);
        Task<Data.Entities.Country.Country> GetCountryByName(string CountryName, bool IncludeStates, bool IncludeLGAs);
        Task<List<Data.Entities.Country.Country>> GetCountries();
        Task<Data.Entities.Country.Country> CheckExistingCountry(string CountryName);
        Task<Data.Entities.Country.Country> GetCountryById(int CountryId);
        Task<Data.Entities.Country.Country> UpdateCountry(Data.Entities.Country.Country country);
         Task<Data.Entities.Country.Country> DeleteCountry(Data.Entities.Country.Country country);
        #endregion


    }
}
