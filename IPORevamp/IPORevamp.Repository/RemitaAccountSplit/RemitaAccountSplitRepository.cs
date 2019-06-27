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
using IPORevamp.Repository.RemitaAccountSplit;


namespace IPORevamp.Repository.RemitaAccountSplit
{
    public class RemitaAccountSplitRepository : IRemitaAccountSplitRepository
    {
        private IRepository<Data.Entities.RemitaAccountSplit> _RemitaAccountSplitrepository;
       
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public RemitaAccountSplitRepository(IRepository<Data.Entities.RemitaAccountSplit> RemitaAccountSplitrepository, IAuditTrailManager<AuditTrail> auditTrailManager
           )
        {
            _RemitaAccountSplitrepository = RemitaAccountSplitrepository;
            _auditTrailManager = auditTrailManager;

        }



        // Get RemitaAccountSplit By Id
        public async Task<Data.Entities.RemitaAccountSplit> GetRemitaAccountSplitById(int RemitaAccountSplitId)
        {

            return  await _RemitaAccountSplitrepository.GetAll().FirstOrDefaultAsync(x => x.Id == RemitaAccountSplitId);
            
        }


        // Get All Remita 
        public async Task<List<Data.Entities.RemitaAccountSplit>> GetRemitaAccountSplits()
        {
            List<Data.Entities.RemitaAccountSplit> RemitaAccountSplit = new List<Data.Entities.RemitaAccountSplit>();
                RemitaAccountSplit = await _RemitaAccountSplitrepository.GetAllListAsync();
            return RemitaAccountSplit;
        }
       

        public async Task<Data.Entities.RemitaAccountSplit> UpdateRemitaAccountSplit(Data.Entities.RemitaAccountSplit RemitaAccountSplit)
        {
            var saveContent = await _RemitaAccountSplitrepository.UpdateAsync(RemitaAccountSplit);
            await _RemitaAccountSplitrepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<Data.Entities.RemitaAccountSplit> DeleteRemitaAccountSplit(Data.Entities.RemitaAccountSplit RemitaAccountSplit)
        {
            var saveContent = await _RemitaAccountSplitrepository.UpdateAsync(RemitaAccountSplit);
            await _RemitaAccountSplitrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<Data.Entities.RemitaAccountSplit> SaveRemitaAccountSplit(Data.Entities.RemitaAccountSplit RemitaAccountSplit)
        {

                 var saveContent = await _RemitaAccountSplitrepository.InsertAsync(RemitaAccountSplit);
                                   await _RemitaAccountSplitrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        // Get RemitaAccountSplit By Name

        public async Task<Data.Entities.RemitaAccountSplit> GetRemitaAccountSplitByName(string Description)
        {

            var RemitaAccountSplit = await _RemitaAccountSplitrepository.GetAll().FirstOrDefaultAsync(x => x.BeneficiaryName.ToUpper() == Description.ToUpper());
            return RemitaAccountSplit;
        }

        public async Task<Data.Entities.RemitaAccountSplit> CheckExistingRemitaAccountSplit(string Description)
        {
            Data.Entities.RemitaAccountSplit RemitaAccountSplit = new Data.Entities.RemitaAccountSplit();
           
                RemitaAccountSplit = await _RemitaAccountSplitrepository.GetAll().FirstOrDefaultAsync(x => x.BeneficiaryName.ToUpper() == Description.ToUpper());
            

            return RemitaAccountSplit;
        }

     

      
    }
}
