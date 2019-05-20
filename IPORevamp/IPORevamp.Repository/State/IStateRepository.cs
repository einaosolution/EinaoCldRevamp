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
using IPORevamp.Data.Entities;

namespace IPORevamp.Repository.state
{
    public interface IStateRepository : IAutoDependencyRegister
    {
        #region State Respository

        Task<IPORevamp.Data.Entities.State> SaveState(IPORevamp.Data.Entities.State state);
        Task<IPORevamp.Data.Entities.State> GetStateById(int StateId, bool IncludeStates);
        Task<IPORevamp.Data.Entities.State> GetStateByName(string StateName, bool IncludeLGA);
        Task<List<IPORevamp.Data.Entities.State>> GetStates();
        Task<IPORevamp.Data.Entities.State> CheckExistingState(string StateName);
        Task<IPORevamp.Data.Entities.State> GetStateById(int StateId);
        Task<IPORevamp.Data.Entities.State> UpdateState(IPORevamp.Data.Entities.State State);
        Task<IPORevamp.Data.Entities.State> DeleteState(IPORevamp.Data.Entities.State State);
        #endregion


    }
}
