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

namespace IPORevamp.Repository.Email
{
    public interface IEmailTemplateRepository : IAutoDependencyRegister
    {
        #region Email Template Respository
        Task<EmailTemplate> SaveEmailTemplate(EmailTemplate emailTemplate);
        Task<EmailTemplate> GetEmailTemplateByCode(string  EmailTemplateCode);
         Task<List<EmailTemplate>> GetEmailTemplates();
        Task<EmailTemplate> CheckExistingEmailTemplate(string ByCode);
       
        Task<EmailTemplate> UpdateEmailTemplate(EmailTemplate emailTemplate);
      

        #endregion


    }
}
