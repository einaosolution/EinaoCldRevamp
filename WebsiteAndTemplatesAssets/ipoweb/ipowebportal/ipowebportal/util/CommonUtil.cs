using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace ipowebportal.util
{
    public class CommonUtil
    {
        public bool SendEmail(string Sender, string Subject, string MessageBody)
        {
            string _emailSetting_EmailFrom = ConfigurationManager.AppSettings["EmailFrom"].ToString();
            string _emailSetting_Host = ConfigurationManager.AppSettings["Host"].ToString();
            int _emailSetting_Port = int.Parse(ConfigurationManager.AppSettings["Port"].ToString());
            bool _emailSetting_EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSsl"].ToString());
            string _emailSetting_Username = ConfigurationManager.AppSettings["Username"].ToString();
            string _emailSetting_Password = ConfigurationManager.AppSettings["Password"].ToString();
            string _emailSetting_EmailTo = ConfigurationManager.AppSettings["To"].ToString();



            try
            {
                
                MailMessage message = new MailMessage();
                message.From = new MailAddress(Sender);
                message.To.Add(new MailAddress(_emailSetting_EmailTo));
                message.Subject = Subject;
                message.Priority = MailPriority.High;
                message.IsBodyHtml = true;
                message.Body = HttpUtility.HtmlDecode(MessageBody);

                var smtpClient = new SmtpClient
                {
                    Host = _emailSetting_Host,
                    Port = _emailSetting_Port,
                    EnableSsl = _emailSetting_EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                   TargetName = "STARTTLS/smtp.gmail.com",
                Credentials = new System.Net.NetworkCredential(_emailSetting_Username, _emailSetting_Password)
                };


                string serverPath = AppDomain.CurrentDomain.BaseDirectory;
                smtpClient.Send(message);


            }
            catch (Exception)
            {
                return false;
                //log mail if this is not email scheduler email
            }

            return true;
        }
    }
}

 
     
