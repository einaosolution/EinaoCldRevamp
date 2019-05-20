using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Core.Utilities;
using IPORevamp.Data;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Event;
using IPORevamp.Data.Entities.Modules;
using IPORevamp.Data.Entities.Payment;
using IPORevamp.Data.UserManagement.Model;

using IPORevamp.Repository.Event;
using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using System.Data.SqlClient;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entities.Country;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.LGAs;
using IPORevamp.Repository.Email;

namespace IPORevamp.Repository.UserProfiling
{
    public class UserProfilingRepository : IUserProfilingRepository
    {
        private IRepository<UserVerificationTemp> _userProfilingRepository;

        private IAuditTrailManager<AuditTrail> _auditTrailManager;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;
        public UserProfilingRepository(IRepository<UserVerificationTemp> userProfilingrepository,
           
            IAuditTrailManager<AuditTrail> auditTrailManager,
           
            IRepository<EmailTemplate> emailTemplaterepository)
        {
           
            _userProfilingRepository = userProfilingrepository;
            _auditTrailManager = auditTrailManager;
            
        }
        
        
       

        public async Task<UserVerificationTemp> SavingUserProfile(UserVerificationTemp userverificationTemp)
        {
            var saveContent = await _userProfilingRepository.InsertOrUpdateAsync(userverificationTemp);
                                _userProfilingRepository.SaveChanges();

            return saveContent.Entity;
        }


        public Task<UserVerificationTemp> ValidateVerificationEmail(string Email)
        {
            var model = _userProfilingRepository.FirstOrDefaultAsync(a => a.Email.ToLower()
                  == Email.ToLower() && a.expired != true);

            return model;
        }




        public Task<UserVerificationTemp> EmailConfirmation(string Code)
        {
            var model = _userProfilingRepository.FirstOrDefaultAsync(a => a.Email.ToLower()
                  == Code.ToLower() && a.ExpiringDate > DateTime.Now && a.ConfirmationDate == null);

            return model;
        }


    }
}
