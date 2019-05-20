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

namespace IPORevamp.Repository.Country
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private IRepository<EmailTemplate> _emailTemplaterepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public EmailTemplateRepository(IRepository<EmailTemplate> emailTemplaterepository,
           
            IAuditTrailManager<AuditTrail> auditTrailManager
           )
        {
            _emailTemplaterepository = emailTemplaterepository;           
            _auditTrailManager = auditTrailManager;

        }



        // Get Country By Id
        public async Task<EmailTemplate> GetEmailTemplateByCode(string EmailCode)
        {
           
                 var content = await _emailTemplaterepository.GetAll().FirstOrDefaultAsync(x => x.EmailName == EmailCode && x.IsActive == true);
                return content;
        }


        // Get All Countries 
        public async Task<List<EmailTemplate>> GetEmailTemplates()
        {
            List<EmailTemplate> content = new List<EmailTemplate>();
            content = await _emailTemplaterepository.GetAllListAsync();
            return content;
        }



        public async Task<EmailTemplate> UpdateEmailTemplate(EmailTemplate emailTemplate)
        {
            var saveContent = await _emailTemplaterepository.UpdateAsync(emailTemplate);
            await _emailTemplaterepository.SaveChangesAsync();

            return saveContent.Entity;
        }


      


       
        public async Task<EmailTemplate> SaveEmailTemplate(EmailTemplate emailTemplate)
        {

                 var saveContent = await _emailTemplaterepository.InsertAsync(emailTemplate);
                                   await _emailTemplaterepository.SaveChangesAsync();

            return saveContent.Entity;
        }
       
        // Get Country By Name

        
        public async Task<EmailTemplate> CheckExistingEmailTemplate(string EmailName)
        {
           
                var content = await _emailTemplaterepository.GetAll().FirstOrDefaultAsync(x => x.EmailName.ToUpper() == EmailName.ToUpper());
               return content;
        }

       
    }
}
