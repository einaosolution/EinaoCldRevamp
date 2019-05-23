using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Data;


using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;

namespace IPORevamp.Repository.TMApplicationStatus
{
    public interface ITMApplicationStatusRepository : IAutoDependencyRegister
    {
		#region TMApplicationStatus Respository

		Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> SaveTMApplicationStatus(Data.Entities.TMApplicationStatus.TMApplicationStatus TMApplicationStatus);
        Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> GetTMApplicationStatusById(int TMApplicationStatusId);
		Task<List <Data.Entities.TMApplicationStatus.TMApplicationStatus>> GetTMApplicationStatus();
		Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> GetTMApplicationStatusByName(string TMApplicationStatusName);
        Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> CheckExistingTMApplicationStatus(string TMApplicationStatusName);
        Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> UpdateTMApplicationStatus(Data.Entities.TMApplicationStatus.TMApplicationStatus TMApplicationStatus);
         Task<Data.Entities.TMApplicationStatus.TMApplicationStatus> DeleteTMApplicationStatus(Data.Entities.TMApplicationStatus.TMApplicationStatus TMApplicationStatus);
		Task GetTMApplicationStatusById(string statusDescription);
		#endregion


	}
}
