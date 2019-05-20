using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Data;
using IPORevamp.Data.Entities.Event;
using IPORevamp.Data.Entities.Modules;
using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;

namespace IPORevamp.Repository.DSApplicationStatus
{
    public interface IDSApplicationStatusRepository : IAutoDependencyRegister
    {
		#region DSApplicationStatus Respository

		Task<Data.Entities.DSApplicationStatus.DSApplicationStatus> SaveDSApplicationStatus(Data.Entities.DSApplicationStatus.DSApplicationStatus DSApplicationStatus);
        Task<Data.Entities.DSApplicationStatus.DSApplicationStatus> GetDSApplicationStatusById(int DSApplicationStatusId);
		Task<List <Data.Entities.DSApplicationStatus.DSApplicationStatus>> GetDSApplicationStatus();
		Task<Data.Entities.DSApplicationStatus.DSApplicationStatus> GetDSApplicationStatusByName(string DSApplicationStatusName);
        Task<Data.Entities.DSApplicationStatus.DSApplicationStatus> CheckExistingDSApplicationStatus(string DSApplicationStatusName);
        Task<Data.Entities.DSApplicationStatus.DSApplicationStatus> UpdateDSApplicationStatus(Data.Entities.DSApplicationStatus.DSApplicationStatus DSApplicationStatus);
         Task<Data.Entities.DSApplicationStatus.DSApplicationStatus> DeleteDSApplicationStatus(Data.Entities.DSApplicationStatus.DSApplicationStatus DSApplicationStatus);
        #endregion


    }
}
