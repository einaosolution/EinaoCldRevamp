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

namespace IPORevamp.Repository.Event
{
    public interface ISettingRepository: IAutoDependencyRegister
    {
      

        Task<UserVerificationTemp> SaveUserVerification(UserVerificationTemp userverificationTemp);
        Task<UserVerificationTemp> EmailConfirmation(string Code);
        Task<UserVerificationTemp> ValidateVerificationEmail(string email);

        #region AccountType Respository

        Task<AccountType> AccountType(string AccountTypeCode);

        #endregion

       

  

        //#region LGA Respository
        //Task<lga> SaveLocalGovtArea(lga state);
        //Task<State> GetLgaById(int Id);
        //#endregion

        #region PTApplication Respository
        Task<PTApplicationStatus> SavePatentApplicationStatus(PTApplicationStatus pTApplicationStatus);
        Task<PTApplicationStatus> GetPatentApplicationStatusById(int Id);
        #endregion


        #region TMApplication Respository
        Task<TMApplicationStatus> SaveTradeMarkApplicationStatus(TMApplicationStatus tMApplicationStatus);
        Task<TMApplicationStatus> GetTradeMarkApplicationStatusById(int Id);
        #endregion

        #region DSApplication Respository
        Task<DSApplicationStatus> SaveDesignApplicationStatus(DSApplicationStatus dSApplicationStatus);
        Task<DSApplicationStatus> GetDesignApplicationStatusById(int Id);
        #endregion

    




    }
}
