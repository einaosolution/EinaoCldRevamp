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



using IPORevamp.Data.UserManagement.Model;


using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using System.Data.SqlClient;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entities.Country;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.LGAs;
using IPORevamp.Repository.Email;
using IPORevamp.Data.Entity.Interface.Entities.Setting;
using IPORevamp.Data.Entity.Interface.Entities.UserView;

namespace IPORevamp.Repository.UserProfiling
{
    public class UserProfilingRepository : IUserProfilingRepository
    {
        private IRepository<UserVerificationTemp> _userProfilingRepository;

        private IAuditTrailManager<AuditTrail> _auditTrailManager;
        private IPOContext _dbcontext;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;
        public UserProfilingRepository(IRepository<UserVerificationTemp> userProfilingrepository,
           
            IAuditTrailManager<AuditTrail> auditTrailManager,
           
            IRepository<EmailTemplate> emailTemplaterepository, IPOContext dbcontext)
        {
           
            _userProfilingRepository = userProfilingrepository;
            _auditTrailManager = auditTrailManager;
            _dbcontext = dbcontext;

        }
        
        
       

        public async Task<UserVerificationTemp> SavingUserProfile(UserVerificationTemp userverificationTemp)
        {
            var saveContent = await _userProfilingRepository.InsertOrUpdateAsync(userverificationTemp);
                                _userProfilingRepository.SaveChanges();

            return saveContent.Entity;
        }


        public void  UpdateTrademarkStatus()
        {

            var TrademarApplication = (from c in _dbcontext.Application  select c).ToList();

            foreach(var application in TrademarApplication)
            {
                if ((application.ApplicationStatus =="1" && application.DataStatus == "Fresh") || (application.ApplicationStatus == "2" && application.DataStatus == "Valid"))
                {
                    application.ApplicationStatus = STATUS.Fresh;
                    application.DataStatus = DATASTATUS.Search;

                }

                else if ((application.ApplicationStatus == "2" && application.DataStatus == "Re-conduct search") )
                {
                    application.ApplicationStatus = STATUS.ReconductSearch;
                    application.DataStatus = DATASTATUS.ReconductSearch;

                }

               else if ((application.ApplicationStatus == "3" && application.DataStatus == "Search Conducted") || (application.ApplicationStatus == "33" && application.DataStatus == "Search 2 Conducted"))
                {
                    application.ApplicationStatus = STATUS.Fresh;
                    application.DataStatus = DATASTATUS.Examiner;

                }
            }


        }

        public List<MigratedUsers>  MigrateAgentUser()
        {
            var details = _dbcontext.MigratedUsers
             .FromSql($"MigrateAgentUsers  ")
            .ToList();

            return details;
        }

        public List<MigratedUsers> MigrateTrademarkUser()
        {
            var details = _dbcontext.MigratedUsers
             .FromSql($"MigrateTrademarkUsers  ")
            .ToList();

            return details;
        }

        public List<MigratedUsers> MigratePatentUser()
        {
            var details = _dbcontext.MigratedUsers
             .FromSql($"MigratePatentUsers  ")
            .ToList();

            return details;
        }

        public List<MigratedUsers> MigrateDesignUser()
        {
            var details = _dbcontext.MigratedUsers
             .FromSql($"MigrateDesignUsers  ")
            .ToList();

            return details;
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


        public List<UserVerificationTemp> GetAll()
        {
            var model =  _userProfilingRepository.GetAll().Where(a =>a.Status == "Pending").ToList();

            return model;
        }


        public List<UserView> GetUserListings(string month,string year ,string departmentid)
        {
            var details = _dbcontext.UserView
              .FromSql($"BackendUserReport   @p0, @p1, @p2", parameters: new[] { year, month, departmentid })
             .ToList();

            return details;
            // return null;
        }

        public List<UserVerificationTemp> GetAll2(string dept)
        {
            var model = _userProfilingRepository.GetAll().Where(a => a.Status == STATUS.Pending && a.department ==dept).ToList();

            return model;
        }


        public int  GetAllCount(string dept)
        {
            var model = _userProfilingRepository.GetAll().Where(a => a.Status == STATUS.Pending && a.department == dept).Count();

            return model;
        }


    }
}
