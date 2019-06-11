using EmailEngine.Repository.EmailRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Data.Entities.AuditTrail;


namespace IPORevamp.Repository.LGA
{

    public class LGARepository : ILGARepository
    {
        private readonly IRepository<Data.Entities.LGAs.LGA> _lgarepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;
     
        public LGARepository(IRepository<Data.Entities.LGAs.LGA> lgarepository,IAuditTrailManager<AuditTrail> auditTrailManager)
        {
       
            _lgarepository = lgarepository;
            _auditTrailManager = auditTrailManager;

        }
        
        

        // Get LGA By Id
        public async Task<Data.Entities.LGAs.LGA> GetLGAById(int LGAId, bool IncludeStates)
        {

            Data.Entities.LGAs.LGA entity = new Data.Entities.LGAs.LGA ();
            if (IncludeStates == true)
            {
                entity = await _lgarepository.GetAll().Include(a => a.State).FirstOrDefaultAsync(x => x.Id == LGAId);
            }
            else
            {
                entity = await _lgarepository.GetAll().FirstOrDefaultAsync(x => x.Id == LGAId);
            }
          
            return entity;
        }


        // Get All LGAs 
        public async Task<List<Data.Entities.LGAs.LGA>> GetLGAs()
        {

           // var entities = await _lgarepository.GetAllListAsync();
            var entities = await _lgarepository.GetAll().Include(a => a.State).ToListAsync();

         


            return entities;

        }


        

     
        public async Task<Data.Entities.LGAs.LGA> GetLGAById(int LGAId)
        {
         
            var content = await _lgarepository.GetAll().FirstOrDefaultAsync(x => x.Id == LGAId);

            return content;
        }
        public async Task<Data.Entities.LGAs.LGA> UpdateLGA(Data.Entities.LGAs.LGA LGA)
        {
            var entity = await _lgarepository.UpdateAsync(LGA);
            await _lgarepository.SaveChangesAsync();

            return entity.Entity;
        }


        public async Task<Data.Entities.LGAs.LGA> DeleteLGA(Data.Entities.LGAs.LGA  entity)
        {
            var saveContent = await _lgarepository.UpdateAsync(entity);
            await _lgarepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<Data.Entities.LGAs.LGA > SaveLGA(Data.Entities.LGAs.LGA  entity)
        {

                 var saveContent = await _lgarepository.InsertAsync(entity);
                                   await _lgarepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        // Get Country By Name

        public async Task<Data.Entities.LGAs.LGA > GetLGAByName(string LGAName, bool IncludeLGAs)
        {

            Data.Entities.LGAs.LGA  content = new Data.Entities.LGAs.LGA ();
            if (IncludeLGAs == true)
            {
                content = await _lgarepository.GetAll().Include(a => a.State).FirstOrDefaultAsync(x => x.LGAName.ToUpper() == LGAName.ToUpper());
            }
            else
            {
                content = await _lgarepository.GetAll().FirstOrDefaultAsync(x => x.LGAName.ToUpper() == LGAName.ToUpper());
            }

            return content;
        }

        public async Task<Data.Entities.LGAs.LGA > CheckExistingLGA(string LGAName)
        {
           // Data.Entities.LGAs.LGA  content = new Data.Entities.LGAs.LGA ();
           
                var content = await _lgarepository.FirstOrDefaultAsync(x => x.LGAName.ToUpper() == LGAName.ToUpper());
            

            return content;
        }

       
    }
}
