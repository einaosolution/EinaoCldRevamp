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
using IPORevamp.Data.Entities.DSApplicationStatus;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.LGAs;

namespace IPORevamp.Repository.DSApplicationStatus
{
    public class DSApplicationStatusRepository : IDSApplicationStatusRepository
	{
        private IRepository<Data.Entities.DSApplicationStatus.DSApplicationStatus> _dsApplicationStatusrepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public DSApplicationStatusRepository(IRepository<Data.Entities.DSApplicationStatus.DSApplicationStatus> dsApplicationStatusrepository,
            
            IAuditTrailManager<AuditTrail> auditTrailManager,
            IRepository<EmailTemplate> emailTemplaterepository)
        {
			_dsApplicationStatusrepository = dsApplicationStatusrepository;
             _auditTrailManager = auditTrailManager;

        }
        
        

        // Get DSApplicationStatus By Id
        public async Task<Data.Entities.DSApplicationStatus.DSApplicationStatus> GetDSApplicationStatusById(int DSApplicationStatusId)
        {
			 Data.Entities.DSApplicationStatus.DSApplicationStatus DSApplicationStatus = new Data.Entities.DSApplicationStatus.DSApplicationStatus();
             DSApplicationStatus = await _dsApplicationStatusrepository.GetAll().FirstOrDefaultAsync(x => x.Id == DSApplicationStatusId);
                   
             return DSApplicationStatus;
        }

		// Get All PTApplications 
        public async Task<List<Data.Entities.DSApplicationStatus.DSApplicationStatus>> GetDSApplicationStatus()
        {
            List<Data.Entities.DSApplicationStatus.DSApplicationStatus> DSApplicationStatus = new List<Data.Entities.DSApplicationStatus.DSApplicationStatus>();
                DSApplicationStatus = await _dsApplicationStatusrepository.GetAllListAsync();
            return DSApplicationStatus;
        }     

        public async Task<Data.Entities.DSApplicationStatus.DSApplicationStatus> UpdateDSApplicationStatus(Data.Entities.DSApplicationStatus.DSApplicationStatus DSApplicationStatus)
        {
            var saveContent = await _dsApplicationStatusrepository.UpdateAsync(DSApplicationStatus);
            await _dsApplicationStatusrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

		// Delete DSApplicationStatus
		public async Task<Data.Entities.DSApplicationStatus.DSApplicationStatus> DeleteDSApplicationStatus(Data.Entities.DSApplicationStatus.DSApplicationStatus DSApplicationStatus)
        {
            var saveContent = await _dsApplicationStatusrepository.UpdateAsync(DSApplicationStatus);
            await _dsApplicationStatusrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

		//Save DSApplicationStatus
		public async Task<Data.Entities.DSApplicationStatus.DSApplicationStatus> SaveDSApplicationStatus(Data.Entities.DSApplicationStatus.DSApplicationStatus DSApplicationStatus)
        {
			var saveContent = await _dsApplicationStatusrepository.InsertAsync(DSApplicationStatus);
                                   await _dsApplicationStatusrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

       // Get DSApplicationStatus By Name
		public async Task<Data.Entities.DSApplicationStatus.DSApplicationStatus> GetDSApplicationStatusByName(string DSApplicationStatusName)
        {
			 Data.Entities.DSApplicationStatus.DSApplicationStatus DSApplicationStatus = new Data.Entities.DSApplicationStatus.DSApplicationStatus();
             DSApplicationStatus = await _dsApplicationStatusrepository.GetAll().FirstOrDefaultAsync(x => x.StatusDescription.ToUpper() == DSApplicationStatusName.ToUpper());
            
             return DSApplicationStatus;
        }


		// Check Existing DSApplicationStatus
		public async Task<Data.Entities.DSApplicationStatus.DSApplicationStatus> CheckExistingDSApplicationStatus(string DSApplicationStatusName)
        {
            Data.Entities.DSApplicationStatus.DSApplicationStatus DSApplicationStatus = new Data.Entities.DSApplicationStatus.DSApplicationStatus();
            DSApplicationStatus = await _dsApplicationStatusrepository.GetAll().FirstOrDefaultAsync(x => x.StatusDescription.ToUpper() == DSApplicationStatusName.ToUpper());
            
            return DSApplicationStatus;
        }
	}
}
