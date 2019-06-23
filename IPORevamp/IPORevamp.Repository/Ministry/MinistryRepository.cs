using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Repository.Interface;

namespace IPORevamp.Repository.Ministry
{
    class MinistryRepository : IministryRepository
    {

        private IRepository<Data.Entity.Interface.Entities.Ministry.Ministry> _ministryrepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public MinistryRepository(IRepository<Data.Entity.Interface.Entities.Ministry.Ministry> ministryrepository)
        {
            _ministryrepository = ministryrepository;


        }





        public async Task<Data.Entity.Interface.Entities.Ministry.Ministry> GetMinistryById(int ProdId)
        {
            Data.Entity.Interface.Entities.Ministry.Ministry ministry = new Data.Entity.Interface.Entities.Ministry.Ministry();
            ministry = await _ministryrepository.FirstOrDefaultAsync(x => x.Id == ProdId);

            return ministry;
        }

        public async Task<Data.Entity.Interface.Entities.Ministry.Ministry> CheckExistingMinistry(string Code)
        {
            Data.Entity.Interface.Entities.Ministry.Ministry ministry = new Data.Entity.Interface.Entities.Ministry.Ministry();

            ministry = await _ministryrepository.FirstOrDefaultAsync(x => x.Description.ToUpper() == Code.ToUpper());


            return ministry;
        }

        public async Task<Data.Entity.Interface.Entities.Ministry.Ministry> SaveUnit(Data.Entity.Interface.Entities.Ministry.Ministry ministry)
        {

            var saveContent = await _ministryrepository.InsertAsync(ministry);
            await _ministryrepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<Data.Entity.Interface.Entities.Ministry.Ministry> UpdateUnit(Data.Entity.Interface.Entities.Ministry.Ministry  ministry)
        {

            var saveContent = await _ministryrepository.UpdateAsync(ministry);
            await _ministryrepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<List<Data.Entity.Interface.Entities.Ministry.Ministry>> GetMinistry()
        {

            List<Data.Entity.Interface.Entities.Ministry.Ministry> unit = new List<Data.Entity.Interface.Entities.Ministry.Ministry>();
            unit = await _ministryrepository.GetAllListAsync();
            return unit;
        }


        public async Task<Data.Entity.Interface.Entities.Ministry.Ministry> DeleteMinistry(Data.Entity.Interface.Entities.Ministry.Ministry ministry)
        {
            var saveContent = await _ministryrepository.UpdateAsync(ministry);
            await _ministryrepository.SaveChangesAsync();

            return saveContent.Entity;
        }
    }
}
