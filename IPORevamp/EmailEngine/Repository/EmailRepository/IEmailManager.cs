using EmailEngine.Base.Entities;
using EmailEngine.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmailEngine.Base.Repository.EmailRepository
{
    public interface IEmailManager<TEMailLog, TEmailTemplate> where TEMailLog : IPOEmailLog where TEmailTemplate : IPOEmailTemplate
    {
        Task<IPOEmailStatus> SendMailAsync(TEMailLog emailLog);
        IPOEmailStatus SendMail(TEMailLog emailLog);
        Task LogEmail(TEMailLog emailLog);
        Task LogBatchEmail(List<TEMailLog> eMailLogs);
        Task<TEmailTemplate> CreateEmailTempalteAsync(TEmailTemplate emailTemplate);
        IQueryable<TEmailTemplate> GetEmailTemplate(IPOEmailTemplateType type);
        Task SendBatchMailAsync();
        IQueryable<TEmailTemplate> GetEmailTemplates();
        IQueryable<TEmailTemplate> GetEmailTemplates(Expression<Func<TEmailTemplate, bool>> predicate);
        IQueryable<TEmailTemplate> GetEmailTemplateByCode(IPOEmailTemplateType type);



    }
}
