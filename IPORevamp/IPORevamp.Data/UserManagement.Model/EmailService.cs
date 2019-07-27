using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using IPORevamp.Data.Entity.Interface;
using Microsoft.Extensions.Configuration;



namespace NACC.Data.UserManagement.Model
{
    public class EmailService : IEmailSender
    {
        private readonly string _smtpPort;
        private readonly string _username;
        private readonly string _password;
        private readonly string _smtpHost;

        public static bool _mailSent;
        private readonly string _emailFrom;// = "info@nacc.org";
        


        // private ILog _logger;
        public EmailService(IConfiguration configuration )
        {
            _smtpPort = configuration.GetSection("EmailSettings:SMTPPort").Value;
            _username= configuration.GetSection("EmailSettings:UserNameEmail").Value;
            _password = configuration.GetSection("EmailSettings:Password").Value;
            _smtpHost = configuration.GetSection("EmailSettings:SMTPHost").Value;
            _emailFrom = configuration.GetSection("EmailSettings:EmailFrom").Value;
        }

       
            public async Task SendEmailAsync(string email, string subject, string message)
        {

            try
            {
                await SendEmailAsync(email, subject, message, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
        }

        public async Task SendEmailAsync(string email, string subject, string message, string senderTitle)
        {

            try
            {
                await SendEmailAsync(email, subject, message, true, senderTitle);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }


        /// <inheritdoc />
        public  async Task SendEmailAsync(string email, string subject, string message, bool isHtml, List<string> attachments = null)
        {
            MailMessage mMessage = new MailMessage();
           
            mMessage.To.Add(email);
            mMessage.Subject = subject;
            mMessage.From = new MailAddress($"IPO Nigeria <{_emailFrom}>");
            mMessage.Body = message;
            mMessage.Priority = MailPriority.High;
            mMessage.IsBodyHtml = true;

            System.Net.Mail.Attachment attachment;

            try
            {
                foreach (var kk in attachments)
                {
                    attachment = new System.Net.Mail.Attachment(kk);
                    mMessage.Attachments.Add(attachment);

                }

            }

            catch(Exception ee)
            {

            }

            using (SmtpClient smtpMail = new SmtpClient())
            {
                smtpMail.Host = _smtpHost;
                smtpMail.Port = Convert.ToInt32(_smtpPort);
                smtpMail.EnableSsl = true;
                smtpMail.Credentials = new NetworkCredential(_username, _password);
                smtpMail.SendCompleted += SmtpMail_SendCompleted;
                
                await smtpMail.SendMailAsync(mMessage);
               
            }

 
        }

        public async Task SendEmailAsync(string email, string subject, string message, bool isHtml, string senderTitle, List<string> attachments = null)
        {
            MailMessage mMessage = new MailMessage();

            mMessage.To.Add(email);
            mMessage.Subject = subject;
            mMessage.From = new MailAddress($"{senderTitle} <{_emailFrom}>");
            mMessage.Body = message;
            mMessage.Priority = MailPriority.High;
            mMessage.IsBodyHtml = true;

            using (SmtpClient smtpMail = new SmtpClient())
            {
                smtpMail.Host = _smtpHost;
                smtpMail.Port = Convert.ToInt32(_smtpPort);
                smtpMail.EnableSsl = true;
                smtpMail.Credentials = new NetworkCredential(_username, _password);
                smtpMail.SendCompleted += SmtpMail_SendCompleted;
                
                await smtpMail.SendMailAsync(mMessage);

            }            
        }

        private void SmtpMail_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var token = e.UserState;
            //todo DB log

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                throw new ApplicationException(e.Error.Message);
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            _mailSent = true;
        }


        
    }

   
}