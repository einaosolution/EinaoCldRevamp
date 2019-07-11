using EmailEngine.Repository.EmailRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Repository.Fee;
using IPORevamp.Data.Entities.Fee;
using System.Linq;

namespace IPORevamp.Repository.Fee
{

    public class feelistrepository : IFeeListRepository
    {
        private readonly IRepository<FeeList> _feelistrepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;
     
        public feelistrepository(IRepository<FeeList> feelistrepository,IAuditTrailManager<AuditTrail> auditTrailManager)
        {
       
            _feelistrepository = feelistrepository;
            _auditTrailManager = auditTrailManager;

        }

        public async Task<List<FeeList>> GetFeeListById(int[] FeeListId)
        {
            var fees = await _feelistrepository.GetAllListAsync(x => FeeListId.Contains(x.Id));
            return fees;
        }



        // Get Fee By Id
        public async Task<FeeList> GetFeeListById(int FeeListId)
        {

            FeeList entity = new FeeList ();
            entity = await _feelistrepository.GetAll().FirstOrDefaultAsync(x => x.Id == FeeListId);
          
            return entity;
        }


        // Get All FeeLists 
        public async Task<List<FeeList>> GetFeeLists()
        {

            var entities = await _feelistrepository.GetAllListAsync();
           

            return entities;

        }


        

        public async Task<List<FeeList>> GetFeeListsByCategory(string category)
        {

            var entities = await _feelistrepository.GetAllListAsync();
            return entities;

        }



        public async Task<FeeList> UpdateFeeList(FeeList FeeList)
        {
            var entity = await _feelistrepository.UpdateAsync(FeeList);
            await _feelistrepository.SaveChangesAsync();

            return entity.Entity;
        }


        public async Task<FeeList> DeleteFeeList(FeeList  entity)
        {
            var saveContent = await _feelistrepository.UpdateAsync(entity);
            await _feelistrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<FeeList > SaveFeeList(FeeList  entity)
        {

                 var saveContent = await _feelistrepository.InsertAsync(entity);
                                   await _feelistrepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        // 

        public async Task<FeeList > GetFeeListByName(string FeeListName)
        {

            FeeList  content = new FeeList ();
           
                content = await _feelistrepository.GetAll().FirstOrDefaultAsync(x => x.ItemName.ToUpper() == FeeListName.ToUpper());
           
            return content;
        }

        public async Task<FeeList> CheckExistingFeeList(string FeeListName)
        {
           // FeeList  content = new FeeList ();
           
                var content = await _feelistrepository.FirstOrDefaultAsync(x => x.ItemName.ToUpper() == FeeListName.ToUpper());
            

            return content;
        }

       
    }
}
