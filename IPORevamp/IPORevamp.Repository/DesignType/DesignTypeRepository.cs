using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.DesignType
{
  public   class DesignTypeRepository : IDesignTypeRepository
    {

        private IRepository<IPORevamp.Data.Entity.Interface.Entities.DesignType.DesignType> _designtyperepository;

        public DesignTypeRepository(IRepository<IPORevamp.Data.Entity.Interface.Entities.DesignType.DesignType> designtyperepository
         )
        {
            _designtyperepository = designtyperepository;


        }


        public async Task<List<IPORevamp.Data.Entity.Interface.Entities.DesignType.DesignType>> GetDesignType()
        {
            List<IPORevamp.Data.Entity.Interface.Entities.DesignType.DesignType> designtype = new List<IPORevamp.Data.Entity.Interface.Entities.DesignType.DesignType>();
            designtype = await _designtyperepository.GetAllListAsync();
            return designtype;
        }
    }
}
