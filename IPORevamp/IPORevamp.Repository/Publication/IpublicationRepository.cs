using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IPORevamp.Repository.Publication
{
   public  interface IpublicationRepository : IAutoDependencyRegister
    {
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetFreshApplication();
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetRefuseApplicationByUserid(string userid);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetPublicationById(String id);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Search.DataResult>> GetPublicationByRegistrationId(String id);
        Task<List<IPORevamp.Data.Entity.Interface.Entities.Batch.PublicationBatch>> SelectBatches();
        Task<String> UpdateBatch(String[] ss);

        
    }
}
