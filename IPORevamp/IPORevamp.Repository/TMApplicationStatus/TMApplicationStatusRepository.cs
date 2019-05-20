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

using IPORevamp.Repository.Event;
using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using System.Data.SqlClient;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entities.TMApplicationStatus;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.LGAs;

namespace IPORevamp.Repository.TMApplicationStatus
{
    public class TMApplicationStatusRepository : ITMApplicationStatusRepository
	{
        private IRepository<Data.Entities.TMApplicationStatus.TMApplicationStatus> _tmApplicationStatusrepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public TMApplicationStatusRepository(IRepository<Data.Entities.TMApplicationStatus.TMApplicationStatus> tmApplicationStatusrepository,
            IRepository<State> staterepository,
            IRepository<Data.Entities.LGAs.LGA> lgarepository, 
            
            IAuditTrailManager<AuditTrail> auditTrailManager,
           
            IRepository<EmailTemplate> emailTemplaterepository)
        {
			_tmApplicationStatusrepository = tmApplicationStatusrepository;
             _auditTrailManager = auditTrailManager;

        }
        
        

        // Get TMApplicationStatus By Id
        public async Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> GetTMApplicationStatusById(int TMApplicationStatusId, bool IncludeStates, bool IncludeStateLGAs)
        {

            Data.Entities.TMApplicationStatus.TMApplicationStatus TMApplicationStatus = new Data.Entities.TMApplicationStatus.TMApplicationStatus();
           
                 TMApplicationStatus = await _tmApplicationStatusrepository.GetAll().FirstOrDefaultAsync(x => x.Id == TMApplicationStatusId);
          
          
            return TMApplicationStatus;
        }


        // Get All Countries 
        public async Task<List<Data.Entities.TMApplicationStatus.TMApplicationStatus>> GetTMApplicationStatus()
        {
            List<Data.Entities.TMApplicationStatus.TMApplicationStatus> TMApplicationStatus = new List<Data.Entities.TMApplicationStatus.TMApplicationStatus>();
                TMApplicationStatus = await _tmApplicationStatusrepository.GetAllListAsync();
            return TMApplicationStatus;
        }
			   

       

        public async Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> UpdateTMApplicationStatus(Data.Entities.TMApplicationStatus.TMApplicationStatus TMApplicationStatus)
        {
            var saveContent = await _tmApplicationStatusrepository.UpdateAsync(TMApplicationStatus);
            await _tmApplicationStatusrepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> DeleteTMApplicationStatus(Data.Entities.TMApplicationStatus.TMApplicationStatus TMApplicationStatus)
        {
            var saveContent = await _tmApplicationStatusrepository.UpdateAsync(TMApplicationStatus);
            await _tmApplicationStatusrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> SaveTMApplicationStatus(Data.Entities.TMApplicationStatus.TMApplicationStatus TMApplicationStatus)
        {

                 var saveContent = await _tmApplicationStatusrepository.InsertAsync(TMApplicationStatus);
                                   await _tmApplicationStatusrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        // Get TMApplicationStatus By Name

        public async Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> GetTMApplicationStatusByName(string TMApplicationStatusName, bool IncludeStates, bool IncludeStateLGAs)
        {

            Data.Entities.TMApplicationStatus.TMApplicationStatus TMApplicationStatus = new Data.Entities.TMApplicationStatus.TMApplicationStatus();
          
                TMApplicationStatus = await _tmApplicationStatusrepository.GetAll().FirstOrDefaultAsync(x => x.StatusDescription.ToUpper() == TMApplicationStatusName.ToUpper());
            

            return TMApplicationStatus;
        }

        public async Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> CheckExistingTMApplicationStatus(string TMApplicationStatusName)
        {
            Data.Entities.TMApplicationStatus.TMApplicationStatus TMApplicationStatus = new Data.Entities.TMApplicationStatus.TMApplicationStatus();
           
                TMApplicationStatus = await _tmApplicationStatusrepository.GetAll().FirstOrDefaultAsync(x => x.StatusDescription.ToUpper() == TMApplicationStatusName.ToUpper());
            

            return TMApplicationStatus;
        }

        public async Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> GetTMApplicationStatusById(int TMApplicationStatusId)
        {
            Data.Entities.TMApplicationStatus.TMApplicationStatus TMApplicationStatus = new Data.Entities.TMApplicationStatus.TMApplicationStatus();
            TMApplicationStatus = await _tmApplicationStatusrepository.GetAll().FirstOrDefaultAsync(x => x.Id == TMApplicationStatusId);

            return TMApplicationStatus;
        }

        public Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> GetTMApplicationStatusByName(string TMApplicationStatusName)
        {
            throw new NotImplementedException();
        }

        public Task GetTMApplicationStatusById(string statusDescription)
        {
            throw new NotImplementedException();
        }
    }
}
