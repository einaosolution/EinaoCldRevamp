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
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.LGAs;

namespace IPORevamp.Repository.Sector
{
    public class SectorRepository : ISectorRepository
    {
        private IRepository<Data.Entities.Sector> _sectorrepository;
       
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public SectorRepository(IRepository<Data.Entities.Sector> sectorrepository, IAuditTrailManager<AuditTrail> auditTrailManager
           )
        {
            _sectorrepository = sectorrepository;
            _auditTrailManager = auditTrailManager;

        }
        
        

        // Get Sector By Id
        public async Task<Data.Entities.Sector> GetSectorById(int SectorId, bool IncludeStates, bool IncludeStateLGAs)
        {

            var content = await _sectorrepository.GetAll().FirstOrDefaultAsync(x => x.Id == SectorId);

            return content;
        }


        // Get All Countries 
        public async Task<List<Data.Entities.Sector>> GetSectors()
        {
            List<Data.Entities.Sector> Sector = new List<Data.Entities.Sector>();
                Sector = await _sectorrepository.GetAllListAsync();
            return Sector;
        }
       

        public async Task<Data.Entities.Sector> UpdateSector(Data.Entities.Sector Sector)
        {
            var saveContent = await _sectorrepository.UpdateAsync(Sector);
            await _sectorrepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<Data.Entities.Sector> DeleteSector(Data.Entities.Sector Sector)
        {
            var saveContent = await _sectorrepository.UpdateAsync(Sector);
            await _sectorrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<Data.Entities.Sector> SaveSector(Data.Entities.Sector Sector)
        {

                 var saveContent = await _sectorrepository.InsertAsync(Sector);
                                   await _sectorrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        // Get Sector By Name

        public async Task<Data.Entities.Sector> GetSectorByName(string Description)
        {

            var Sector = await _sectorrepository.GetAll().FirstOrDefaultAsync(x => x.Description.ToUpper() == Description.ToUpper());
            return Sector;
        }

        public async Task<Data.Entities.Sector> CheckExistingSector(string Description)
        {
            Data.Entities.Sector Sector = new Data.Entities.Sector();
           
                Sector = await _sectorrepository.GetAll().FirstOrDefaultAsync(x => x.Description.ToUpper() == Description.ToUpper());
            

            return Sector;
        }

        public async Task<Data.Entities.Sector> GetSectorById(int SectorId)
        {
            Data.Entities.Sector Sector = new Data.Entities.Sector();
            Sector = await _sectorrepository.GetAll().FirstOrDefaultAsync(x => x.Id == SectorId);

            return Sector;
        }

      
    }
}
