using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ipowebportal.Controllers
{
    [HandleError]
    public class ServicesController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        
    }
}