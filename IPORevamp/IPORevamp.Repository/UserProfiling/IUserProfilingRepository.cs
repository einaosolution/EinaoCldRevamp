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
using IPORevamp.Data.Entity.Interface.Entities.Setting;
using IPORevamp.Data.Entity.Interface.Entities.UserView;

namespace IPORevamp.Repository.UserProfiling
{
    public interface IUserProfilingRepository : IAutoDependencyRegister
    {
        Task<UserVerificationTemp> SavingUserProfile(UserVerificationTemp userverificationTemp);
        Task<UserVerificationTemp> EmailConfirmation(string Code);
        Task<UserVerificationTemp> ValidateVerificationEmail(string email);
        List<UserVerificationTemp> GetAll();
        int GetAllCount(string dept);
        List<UserView> GetUserListings(string month, string year, string departmentid);
        List<MigratedUsers> MigrateAgentUser();
        List<MigratedUsers> MigrateTrademarkUser();
        List<MigratedUsers> MigratePatentUser();
        List<MigratedUsers> MigrateDesignUser();
        List<UserVerificationTemp> GetAll2(string dept);

    }
}
