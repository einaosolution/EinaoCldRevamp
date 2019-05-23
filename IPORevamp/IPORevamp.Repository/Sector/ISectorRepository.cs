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

namespace IPORevamp.Repository.Sector
{
    public interface ISectorRepository : IAutoDependencyRegister
    {
        #region Sector Respository

        Task<Data.Entities.Sector> SaveSector(Data.Entities.Sector Sector);
        Task<Data.Entities.Sector> GetSectorByName(string SectorName);
        Task<List<Data.Entities.Sector>> GetSectors();
        Task<Data.Entities.Sector> CheckExistingSector(string SectorName);
        Task<Data.Entities.Sector> GetSectorById(int SectorId);
        Task<Data.Entities.Sector> UpdateSector(Data.Entities.Sector Sector);
         Task<Data.Entities.Sector> DeleteSector(Data.Entities.Sector Sector);
        #endregion


    }
}
