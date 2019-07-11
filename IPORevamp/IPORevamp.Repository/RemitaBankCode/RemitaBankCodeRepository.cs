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
using IPORevamp.Repository.RemitaBankCode;

namespace IPORevamp.Repository.RemitaBankCode
{
    public class RemitaBankCodeRepository : IRemitaBankCodeRepository
    {
        private IRepository<Data.Entities.RemitaBankCode> _RemitaBankCoderepository;
       
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public RemitaBankCodeRepository(IRepository<Data.Entities.RemitaBankCode> RemitaBankCoderepository, IAuditTrailManager<AuditTrail> auditTrailManager
           )
        {
            _RemitaBankCoderepository = RemitaBankCoderepository;
            _auditTrailManager = auditTrailManager;

        }



        // Get RemitaBankCode By Id
        public async Task<Data.Entities.RemitaBankCode> GetRemitaBankCodeById(int RemitaBankCodeId)
        {

            return  await _RemitaBankCoderepository.GetAll().FirstOrDefaultAsync(x => x.Id == RemitaBankCodeId);
            
        }


        // Get All Remita 
        public async Task<List<Data.Entities.RemitaBankCode>> GetRemitaBankCodes()
        {
            List<Data.Entities.RemitaBankCode> RemitaBankCode = new List<Data.Entities.RemitaBankCode>();
                RemitaBankCode = await _RemitaBankCoderepository.GetAllListAsync();
            return RemitaBankCode;
        }
       

        public async Task<Data.Entities.RemitaBankCode> UpdateRemitaBankCode(Data.Entities.RemitaBankCode RemitaBankCode)
        {
            var saveContent = await _RemitaBankCoderepository.UpdateAsync(RemitaBankCode);
            await _RemitaBankCoderepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<Data.Entities.RemitaBankCode> DeleteRemitaBankCode(Data.Entities.RemitaBankCode RemitaBankCode)
        {
            var saveContent = await _RemitaBankCoderepository.UpdateAsync(RemitaBankCode);
            await _RemitaBankCoderepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<Data.Entities.RemitaBankCode> SaveRemitaBankCode(Data.Entities.RemitaBankCode RemitaBankCode)
        {

                 var saveContent = await _RemitaBankCoderepository.InsertAsync(RemitaBankCode);
                                   await _RemitaBankCoderepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        // Get RemitaBankCode By Name

        public async Task<Data.Entities.RemitaBankCode> GetRemitaBankCodeByName(string Description)
        {

            var RemitaBankCode = await _RemitaBankCoderepository.GetAll().FirstOrDefaultAsync(x => x.BankName.ToUpper() == Description.ToUpper());
            return RemitaBankCode;
        }

        public async Task<Data.Entities.RemitaBankCode> CheckExistingRemitaBankCode(string Description)
        {
            Data.Entities.RemitaBankCode RemitaBankCode = new Data.Entities.RemitaBankCode();
           
                RemitaBankCode = await _RemitaBankCoderepository.GetAll().FirstOrDefaultAsync(x => x.BankName.ToUpper() == Description.ToUpper());
            

            return RemitaBankCode;
        }

     

      
    }
}
