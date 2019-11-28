using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ipowebportal.Controllers
{
    public class ContactController : Controller
    {
        [HandleError]
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(ipowebportal.Models.MessageBody messageBody)
        {
            if (ModelState.IsValid)
            {
                // send the email 
                util.CommonUtil util = new util.CommonUtil();

                // read the email message 
                     string maincontent = "";
                       maincontent = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/emailtemplate/messagetemplate.html"));

                maincontent = maincontent.Replace("#SenderName", messageBody.Name);
                maincontent = maincontent.Replace("#PhoneNumber", messageBody.Phone);
                maincontent = maincontent.Replace("#SenderMessage", messageBody.Message);


                bool testMail =  util.SendEmail(messageBody.Email, messageBody.Subject, maincontent);
                maincontent = "";

                if (testMail)
                {
                    TempData["message"] = "your message was successfully delivered, someone will contact you shortly.";
                }
                else
                {
                    TempData["message"] = "your message was  not delivered, please try again.";
                }
                ipowebportal.Models.MessageBody clear = new Models.MessageBody();
                return RedirectToAction("Index", "Contact");


            }
            else
            {
                TempData["message"] = "your message was  not delivered, please try again.";
                return RedirectToAction("Index", "Contact");
            }

            
        }
    }
}