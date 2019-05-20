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
using IPORevamp.Data.Entities.PTApplicationStatus;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.LGAs;

namespace IPORevamp.Repository.PTApplicationStatus
{
    public class PTApplicationStatusRepository : IPTApplicationStatusRepository
	{
        private IRepository<Data.Entities.PTApplicationStatus.PTApplicationStatus> _ptApplicationStatusrepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public PTApplicationStatusRepository(IRepository<Data.Entities.PTApplicationStatus.PTApplicationStatus> ptApplicationStatusrepository,
            
            IAuditTrailManager<AuditTrail> auditTrailManager,
         
            IRepository<EmailTemplate> emailTemplaterepository)
        {
			_ptApplicationStatusrepository = ptApplicationStatusrepository;
             _auditTrailManager = auditTrailManager;

        }
        
        

        // Get PTApplicationStatus By Id
        public async Task<Data.Entities.PTApplicationStatus.PTApplicationStatus> GetPTApplicationStatusById(int PTApplicationStatusId)
        {
			 Data.Entities.PTApplicationStatus.PTApplicationStatus PTApplicationStatus = new Data.Entities.PTApplicationStatus.PTApplicationStatus();
             PTApplicationStatus = await _ptApplicationStatusrepository.GetAll().FirstOrDefaultAsync(x => x.Id == PTApplicationStatusId);
                   
             return PTApplicationStatus;
        }

		// Get All PTApplications 
        public async Task<List<Data.Entities.PTApplicationStatus.PTApplicationStatus>> GetPTApplicationStatus()
        {
            List<Data.Entities.PTApplicationStatus.PTApplicationStatus> PTApplicationStatus = new List<Data.Entities.PTApplicationStatus.PTApplicationStatus>();
                PTApplicationStatus = await _ptApplicationStatusrepository.GetAllListAsync();
            return PTApplicationStatus;
        }     

        public async Task<Data.Entities.PTApplicationStatus.PTApplicationStatus> UpdatePTApplicationStatus(Data.Entities.PTApplicationStatus.PTApplicationStatus PTApplicationStatus)
        {
            var saveContent = await _ptApplicationStatusrepository.UpdateAsync(PTApplicationStatus);
            await _ptApplicationStatusrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

		// Delete PTApplicationStatus
		public async Task<Data.Entities.PTApplicationStatus.PTApplicationStatus> DeletePTApplicationStatus(Data.Entities.PTApplicationStatus.PTApplicationStatus PTApplicationStatus)
        {
            var saveContent = await _ptApplicationStatusrepository.UpdateAsync(PTApplicationStatus);
            await _ptApplicationStatusrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

		//Save PTApplicationStatus
		public async Task<Data.Entities.PTApplicationStatus.PTApplicationStatus> SavePTApplicationStatus(Data.Entities.PTApplicationStatus.PTApplicationStatus PTApplicationStatus)
        {
			var saveContent = await _ptApplicationStatusrepository.InsertAsync(PTApplicationStatus);
                                   await _ptApplicationStatusrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

       // Get PTApplicationStatus By Name
		public async Task<Data.Entities.PTApplicationStatus.PTApplicationStatus> GetPTApplicationStatusByName(string PTApplicationStatusName)
        {
			 Data.Entities.PTApplicationStatus.PTApplicationStatus PTApplicationStatus = new Data.Entities.PTApplicationStatus.PTApplicationStatus();
             PTApplicationStatus = await _ptApplicationStatusrepository.GetAll().FirstOrDefaultAsync(x => x.StatusDescription.ToUpper() == PTApplicationStatusName.ToUpper());
            
             return PTApplicationStatus;
        }


		// Check Existing PTApplicationStatus
		public async Task<Data.Entities.PTApplicationStatus.PTApplicationStatus> CheckExistingPTApplicationStatus(string PTApplicationStatusName)
        {
            Data.Entities.PTApplicationStatus.PTApplicationStatus PTApplicationStatus = new Data.Entities.PTApplicationStatus.PTApplicationStatus();
            PTApplicationStatus = await _ptApplicationStatusrepository.GetAll().FirstOrDefaultAsync(x => x.StatusDescription.ToUpper() == PTApplicationStatusName.ToUpper());
            
            return PTApplicationStatus;
        }
	}
}
