using System.Collections.Generic;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;

namespace IPORevamp.Repository.PTApplicationStatus
{
	public interface IPTApplicationStatusRepository : IAutoDependencyRegister
    {
		#region PTApplicationStatus Respository

		Task<Data.Entities.PTApplicationStatus.PTApplicationStatus> SavePTApplicationStatus(Data.Entities.PTApplicationStatus.PTApplicationStatus PTApplicationStatus);
        Task<Data.Entities.PTApplicationStatus.PTApplicationStatus> GetPTApplicationStatusById(int PTApplicationStatusId);
		Task<List <Data.Entities.PTApplicationStatus.PTApplicationStatus>> GetPTApplicationStatus();
		Task<Data.Entities.PTApplicationStatus.PTApplicationStatus> GetPTApplicationStatusByName(string PTApplicationStatusName);
        Task<Data.Entities.PTApplicationStatus.PTApplicationStatus> CheckExistingPTApplicationStatus(string PTApplicationStatusName);
        Task<Data.Entities.PTApplicationStatus.PTApplicationStatus> UpdatePTApplicationStatus(Data.Entities.PTApplicationStatus.PTApplicationStatus PTApplicationStatus);
         Task<Data.Entities.PTApplicationStatus.PTApplicationStatus> DeletePTApplicationStatus(Data.Entities.PTApplicationStatus.PTApplicationStatus PTApplicationStatus);
        #endregion


    }
}
