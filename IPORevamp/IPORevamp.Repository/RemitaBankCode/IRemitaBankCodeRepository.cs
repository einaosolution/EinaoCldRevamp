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

namespace IPORevamp.Repository.RemitaBankCode
{
    public interface IRemitaBankCodeRepository : IAutoDependencyRegister
    {
        #region RemitaBankCode Respository

        Task<Data.Entities.RemitaBankCode> SaveRemitaBankCode(Data.Entities.RemitaBankCode RemitaBankCode);
        Task<Data.Entities.RemitaBankCode> GetRemitaBankCodeByName(string RemitaBankCodeName);
        Task<List<Data.Entities.RemitaBankCode>> GetRemitaBankCodes();
        Task<Data.Entities.RemitaBankCode> CheckExistingRemitaBankCode(string RemitaBankCodeName);
        Task<Data.Entities.RemitaBankCode> GetRemitaBankCodeById(int RemitaBankCodeId);
        Task<Data.Entities.RemitaBankCode> UpdateRemitaBankCode(Data.Entities.RemitaBankCode RemitaBankCode);
         Task<Data.Entities.RemitaBankCode> DeleteRemitaBankCode(Data.Entities.RemitaBankCode RemitaBankCode);
        #endregion


    }
}
