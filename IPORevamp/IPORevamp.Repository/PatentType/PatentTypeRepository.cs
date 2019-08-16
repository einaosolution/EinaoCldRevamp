using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Data.Entity.Interface.Entities.PatentType;
using IPORevamp.Repository.Interface;

namespace IPORevamp.Repository.PatentType
{
    class PatentTypeRepository : IPatentTypeRepository
    {
        private IRepository<Data.Entity.Interface.Entities.PatentType.PatentType> _patenttyperepository;

        public PatentTypeRepository(IRepository<Data.Entity.Interface.Entities.PatentType.PatentType> patenttyperepository
         )
        {
            _patenttyperepository = patenttyperepository;
          

        }


        public async  Task<List<Data.Entity.Interface.Entities.PatentType.PatentType>> GetPatentType()
        {
            List<Data.Entity.Interface.Entities.PatentType.PatentType> patenttype = new List<Data.Entity.Interface.Entities.PatentType.PatentType>();
            patenttype = await  _patenttyperepository.GetAllListAsync();
            return patenttype;
        }
    }
}
