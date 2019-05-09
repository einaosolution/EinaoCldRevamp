using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPORevamp.Data.Entity.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailAsync(string email, string subject, string message, string senderTitle);
        Task SendEmailAsync(string email, string subject, string message, bool isHtml, List<string> attachments = null);
        Task SendEmailAsync(string email, string subject, string message, bool isHtml, string senderTitle, List<string> attachments = null);
    }
}