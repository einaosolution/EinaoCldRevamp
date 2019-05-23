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

namespace IPORevamp.Repository.UserProfiling
{
    public interface IUserProfilingRepository : IAutoDependencyRegister
    {
        Task<UserVerificationTemp> SavingUserProfile(UserVerificationTemp userverificationTemp);
        Task<UserVerificationTemp> EmailConfirmation(string Code);
        Task<UserVerificationTemp> ValidateVerificationEmail(string email);

    }
}
