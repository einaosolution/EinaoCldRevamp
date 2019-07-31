using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Repository.Interface;

namespace IPORevamp.Repository.NationalClass
{
   public  class NationalClassRepository: INationalClassRepository
    {
        private IRepository<IPORevamp.Data.Entity.Interface.Entities.National_Class.NationalClass> _nationalclassrepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public NationalClassRepository(IRepository<IPORevamp.Data.Entity.Interface.Entities.National_Class.NationalClass> nationalclassrepository)
        {
            _nationalclassrepository = nationalclassrepository;


        }


        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.National_Class.NationalClass>> GetNationalClass()
        {

            List<IPORevamp.Data.Entity.Interface.Entities.National_Class.NationalClass> nationalclass = new List<IPORevamp.Data.Entity.Interface.Entities.National_Class.NationalClass>();
            nationalclass = await _nationalclassrepository.GetAllListAsync();
            return nationalclass;
        }

    }
}
