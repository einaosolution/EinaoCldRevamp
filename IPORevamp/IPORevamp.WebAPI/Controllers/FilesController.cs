using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmailEngine.Base.Repository.EmailRepository;
using EmailEngine.Repository;
using EmailEngine.Repository.EmailRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.Event;


namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : BaseController
    {
        private readonly IFileHandler _fileHandler;
        public FilesController(UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager,
           SignInManager<ApplicationUser> signInManager,
           IConfiguration configuration,
           IMapper mapper,
           ILogger<UserController> logger,

           IEmailManager<EmailLog, EmailTemplate> emailManager,
           IAuditTrailManager<AuditTrail> auditTrailManager,
           IEventRepository eventRepository,
           IFileHandler fileHandler) :base(
               userManager,
               signInManager,
               roleManager,
               configuration,
               mapper,
               logger,
               auditTrailManager, eventRepository)
        {
            _fileHandler = fileHandler;
        }

        [HttpGet("image/{imageName}/{width}/{height}")]
        public ActionResult GetImage(int? width, int? height, string imageName)
        {

            var bitMap = _fileHandler.FetchImage(width, height, imageName);

            var ms = new MemoryStream();
            bitMap.Save(ms, ImageFormat.Png);
            return File(ms.ToArray(), "image/png");
        }
    }
}