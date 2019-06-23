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
using IPORevamp.Data.Entities;

namespace IPORevamp.Repository.LGA
{
    public interface ILGARepository : IAutoDependencyRegister
    {
        #region LGA Respository

        Task<Data.Entities.LGAs.LGA> SaveLGA(Data.Entities.LGAs.LGA LGA);
        Task<Data.Entities.LGAs.LGA> GetLGAById(int LGAId, bool IncludeLGAs);
        Task<Data.Entities.LGAs.LGA> GetLGAByName(string LGAName, bool IncludeLGA);
        Task<List<Data.Entities.LGAs.LGA>> GetLGAs();
        Task<Data.Entities.LGAs.LGA> CheckExistingLGA(string LGAName);
        Task<Data.Entities.LGAs.LGA> GetLGAById(int LGAId);
        Task<Data.Entities.LGAs.LGA> UpdateLGA(Data.Entities.LGAs.LGA LGA);
        Task<Data.Entities.LGAs.LGA> DeleteLGA(Data.Entities.LGAs.LGA LGA);
        Task<List<Data.Entities.LGAs.LGA>> GetLGAByState(int state);
 
        #endregion


    }
}
