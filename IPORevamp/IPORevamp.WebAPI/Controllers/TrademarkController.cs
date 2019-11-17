using AutoMapper;
using EmailEngine.Base.Entities;
using EmailEngine.Base.Repository.EmailRepository;
using EmailEngine.Repository.EmailRepository;

using IPORevamp.Core.Utilities;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entity.Interface.Entities.MarkInfo;
using IPORevamp.Data.Entity.Interface.Entities.Pwallet;
using IPORevamp.Data.Entity.Interface.PreliminarySearch;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.FileNewApplication;
using IPORevamp.Repository.PreliminarySearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Data;
using System.IO;
using IPORevamp.Data.Entity.Interface.Entities.ApplicationHistory;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplicationHistory;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Trademark")]
    [ApiController]
    public class TrademarkController : BaseController
    {
        private readonly InewApplication _newApplicationRepository;


        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly Repository.Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        private readonly IPOContext _contex;



        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;




        public TrademarkController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
    IPOContext contex,
    IEmailManager<EmailLog, EmailTemplate> emailManager,
      Repository.Email.IEmailTemplateRepository EmailTemplateRepository,


    InewApplication newApplicationRepository,

    IEmailSender emailsender,
    IHttpContextAccessor httpContextAccessor,
    IFileHandler fileUploadRespository,

    IAuditTrailManager<AuditTrail> auditTrailManager




    ) : base(
        userManager,
        signInManager,
        roleManager,
        configuration,
        mapper,
        logger,
        auditTrailManager

        )
        {
            _emailManager = emailManager;

            _emailsender = emailsender;
            _httpContextAccessor = httpContextAccessor;
            _newApplicationRepository = newApplicationRepository;
           
            _contex = contex;
            _fileUploadRespository = fileUploadRespository;
            _EmailTemplateRepository = EmailTemplateRepository;


        }

        
        [HttpPost("SendAttachment")]
        public async Task<IActionResult> SendAttachment( [FromForm] string userid , [FromForm] string message , [FromForm] string amount, [FromForm] string transactiondate, [FromForm] string transactiondescription , [FromForm] string transactionnumber)
        {
            string ip = "";

            ip = Request.Headers["ip"];


            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));
            EmailTemplate emailtemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.Receipt);


            if (user == null)
            {

                return PrepareResponse(HttpStatusCode.Found, "Member record don't exist, please try again", false);

            }

            var username = user.FirstName + " " + user.LastName;

            string mailContent = emailtemplate.EmailBody;

            mailContent = mailContent.Replace("#Name", username);
            mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);
            mailContent = mailContent.Replace("#tran", transactionnumber);

            mailContent = mailContent.Replace("#description", transactiondescription);
            mailContent = mailContent.Replace("#amount", string.Format("{0:0.#}", amount));
            mailContent = mailContent.Replace("#date", DateTime.Now.ToString("MM/dd/yyyy"));
            //  String ss2 = "Dear " + username + " <br/> Find Attached <br/><br/> Regards";


            //  String ss = "<html>  <head> </head> <body style=\"color: grey; font - size:15px; \"> <font face=\"Helvetica, Arial, sans - serif \">  <div style=\"position: absolute; height: 100px;width: 600px; background - color:0d1d36; padding: 30px; \"> " + ss2 + "</div></body> </html>";






            // file upload
            string msg = "";

            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                    String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                    int result1 = Convert.ToInt32(oneMegaByte[0]);
                    int result2 = Convert.ToInt32(fileMaxSize[0]);

                    var postedFile = Request.Form.Files[0];
                    var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);

                    vfile = vfile+ Guid.NewGuid().ToString();

                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["MemberPassportFolder"], vfile+ ".pdf");

                    using (var fileSrteam = new FileStream(fullPath, FileMode.Create))
                    {
                        await postedFile.CopyToAsync(fileSrteam);
                    }
                    // postedFile.sa.SaveAs(fullPath);

                  //  msg = await _fileUploadRespository.UploadFile(Request.Form.Files[0], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                  //    result2);

                   
                    List<string> vlist = new List<string>();
                    vlist.Add(fullPath);

                   

                    await _emailsender.SendEmailAsync(user.Email, message, mailContent, true, vlist);



                }

                catch (Exception ee)
                {
                    var kk = ee.Message;
                }


            }

          

            var user3 = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));

            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Update,
                DateCreated = DateTime.Now,
                Description = $"Application File Sent successfully",
                Entity = "Pwallet",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip,
                RecordBefore = "",
                RecordAfter = ""
            });

            return PrepareResponse(HttpStatusCode.OK, "Mail Sent", false);

        }



        [HttpPost("SendAKnowlegment")]
        public async Task<IActionResult> SendAKnowlegment([FromForm] string userid, [FromForm] string message, [FromForm] string amount, [FromForm] string transactiondate, [FromForm] string transactiondescription, [FromForm] string transactionnumber)
        {
            string ip = "";

            ip = Request.Headers["ip"];


            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));
            var roleid = Convert.ToInt32(IPORoles.RegistrarTrademark);
            var Registrauser = _userManager.Users.Where(x => x.RolesId == roleid && x.department == DEPARTMENT.Patent).ToList();

           

            EmailTemplate emailtemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.TransactionAcknowledgment);


            if (user == null)
            {

                return PrepareResponse(HttpStatusCode.Found, "Member record don't exist, please try again", false);

            }

            var username = user.FirstName + " " + user.LastName;

            string mailContent = emailtemplate.EmailBody;

            mailContent = mailContent.Replace("#Name", username);
            mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);
            mailContent = mailContent.Replace("#tran", transactionnumber);

            mailContent = mailContent.Replace("#description", transactiondescription);
            mailContent = mailContent.Replace("#amount", string.Format("{0:0.#}", amount));
            mailContent = mailContent.Replace("#date", DateTime.Now.ToString("MM/dd/yyyy"));
            //  String ss2 = "Dear " + username + " <br/> Find Attached <br/><br/> Regards";


            //  String ss = "<html>  <head> </head> <body style=\"color: grey; font - size:15px; \"> <font face=\"Helvetica, Arial, sans - serif \">  <div style=\"position: absolute; height: 100px;width: 600px; background - color:0d1d36; padding: 30px; \"> " + ss2 + "</div></body> </html>";






            // file upload
            string msg = "";

            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                    String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                    int result1 = Convert.ToInt32(oneMegaByte[0]);
                    int result2 = Convert.ToInt32(fileMaxSize[0]);

                    var postedFile = Request.Form.Files[0];
                    var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);

                    vfile = vfile + Guid.NewGuid().ToString();

                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["MemberPassportFolder"], vfile + ".pdf");

                    using (var fileSrteam = new FileStream(fullPath, FileMode.Create))
                    {
                        await postedFile.CopyToAsync(fileSrteam);
                    }
                    // postedFile.sa.SaveAs(fullPath);

                    //  msg = await _fileUploadRespository.UploadFile(Request.Form.Files[0], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                    //    result2);


                    List<string> vlist = new List<string>();
                    vlist.Add(fullPath);

                    foreach (var users in Registrauser)
                    {

                        await _emailsender.SendEmailAsync(users.Email, message, mailContent, true, vlist);

                    }

                    await _emailsender.SendEmailAsync(user.Email, message, mailContent, true, vlist);

                  



                    }

                catch (Exception ee)
                {
                    var kk = ee.Message;
                }


            }



            var user3 = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));

            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Update,
                DateCreated = DateTime.Now,
                Description = $"Application File Sent successfully",
                Entity = "Pwallet",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip,
                RecordBefore = "",
                RecordAfter = ""
            });

            return PrepareResponse(HttpStatusCode.OK, "Mail Sent", false);

        }


        [HttpPost("SendAttachmentReceipt")]
        public async Task<IActionResult> SendAttachmentReceipt([FromForm] string userid, [FromForm] string message  , [FromForm] string amount , [FromForm] string transactiondate , [FromForm] string transactiondescription)
        {
            string ip = "";

            ip = Request.Headers["ip"];


            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));
            EmailTemplate emailtemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.Invoice);

            if (user == null)
            {

                return PrepareResponse(HttpStatusCode.Found, "Member record don't exist, please try again", false);

            }

            var username = user.FirstName + " " + user.LastName;

            string mailContent = emailtemplate.EmailBody;

            mailContent = mailContent.Replace("#Name", username);
            mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);
          
            mailContent = mailContent.Replace("#description", transactiondescription);
            mailContent = mailContent.Replace("#amount", string.Format("{0:0.#}", amount));
            mailContent = mailContent.Replace("#date", DateTime.Now.ToString("MM/dd/yyyy"));

          //  String ss2 = "Dear " + username + " <br/> Find Attached <br/><br/> Regards";


          //  String ss = "<html>  <head> </head> <body style=\"color: grey; font - size:15px; \"> <font face=\"Helvetica, Arial, sans - serif \">  <div style=\"position: absolute; height: 100px;width: 600px; background - color:0d1d36; padding: 30px; \"> " + ss2 + "</div></body> </html>";






            // file upload
            string msg = "";

            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                    String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                    int result1 = Convert.ToInt32(oneMegaByte[0]);
                    int result2 = Convert.ToInt32(fileMaxSize[0]);

                    var postedFile = Request.Form.Files[0];
                    var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);

                    vfile = vfile + Guid.NewGuid().ToString();

                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["MemberPassportFolder"], vfile + ".pdf");

                    using (var fileSrteam = new FileStream(fullPath, FileMode.Create))
                    {
                        await postedFile.CopyToAsync(fileSrteam);
                    }
                    // postedFile.sa.SaveAs(fullPath);

                    //  msg = await _fileUploadRespository.UploadFile(Request.Form.Files[0], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                    //    result2);


                    List<string> vlist = new List<string>();
                    vlist.Add(fullPath);



                    await _emailsender.SendEmailAsync(user.Email, message, mailContent, true, vlist);



                }

                catch (Exception ee)
                {
                    var kk = ee.Message;
                }


            }



            var user3 = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));

            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Update,
                DateCreated = DateTime.Now,
                Description = $"Application File Sent successfully",
                Entity = "Pwallet",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip,
                RecordBefore = "",
                RecordAfter = ""
            });

            return PrepareResponse(HttpStatusCode.OK, "Mail Sent", false);

        }


        [HttpPost("SendAttachmentAcceptance")]
        public async Task<IActionResult> SendAttachmentAcceptance([FromForm] string userid, [FromForm] string message)
        {
            string ip = "";

            ip = Request.Headers["ip"];


            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));
            EmailTemplate emailtemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.Acceptance);

            if (user == null)
            {

                return PrepareResponse(HttpStatusCode.Found, "Member record don't exist, please try again", false);

            }

            var username = user.FirstName + " " + user.LastName;

            string mailContent = emailtemplate.EmailBody;

           
            mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);
            mailContent = mailContent.Replace("#Name", username);



            //  String ss2 = "Dear " + username + " <br/> Find Attached <br/><br/> Regards";


            //  String ss = "<html>  <head> </head> <body style=\"color: grey; font - size:15px; \"> <font face=\"Helvetica, Arial, sans - serif \">  <div style=\"position: absolute; height: 100px;width: 600px; background - color:0d1d36; padding: 30px; \"> " + ss2 + "</div></body> </html>";






            // file upload
            string msg = "";

            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                    String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                    int result1 = Convert.ToInt32(oneMegaByte[0]);
                    int result2 = Convert.ToInt32(fileMaxSize[0]);

                    var postedFile = Request.Form.Files[0];
                    var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);

                    vfile = vfile + Guid.NewGuid().ToString();

                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["MemberPassportFolder"], vfile + ".pdf");

                    using (var fileSrteam = new FileStream(fullPath, FileMode.Create))
                    {
                        await postedFile.CopyToAsync(fileSrteam);
                    }
                    // postedFile.sa.SaveAs(fullPath);

                    //  msg = await _fileUploadRespository.UploadFile(Request.Form.Files[0], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                    //    result2);


                    List<string> vlist = new List<string>();
                    vlist.Add(fullPath);



                    await _emailsender.SendEmailAsync(user.Email, message, mailContent, true, vlist);



                }

                catch (Exception ee)
                {
                    var kk = ee.Message;
                }


            }



            var user3 = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));

            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Update,
                DateCreated = DateTime.Now,
                Description = $"Application File Sent successfully",
                Entity = "Pwallet",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip,
                RecordBefore = "",
                RecordAfter = ""
            });

            return PrepareResponse(HttpStatusCode.OK, "Mail Sent", false);

        }

        [HttpPost("SendAttachmentRefusal")]
        public async Task<IActionResult> SendAttachmentRefusal([FromForm] string userid, [FromForm] string message)
        {
            string ip = "";

            ip = Request.Headers["ip"];


            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));
            EmailTemplate emailtemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.Refusal);

            if (user == null)
            {

                return PrepareResponse(HttpStatusCode.Found, "Member record don't exist, please try again", false);

            }

            var username = user.FirstName + " " + user.LastName;

            string mailContent = emailtemplate.EmailBody;


            mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);
            mailContent = mailContent.Replace("#Name", username);



            //  String ss2 = "Dear " + username + " <br/> Find Attached <br/><br/> Regards";


            //  String ss = "<html>  <head> </head> <body style=\"color: grey; font - size:15px; \"> <font face=\"Helvetica, Arial, sans - serif \">  <div style=\"position: absolute; height: 100px;width: 600px; background - color:0d1d36; padding: 30px; \"> " + ss2 + "</div></body> </html>";






            // file upload
            string msg = "";

            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                    String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                    int result1 = Convert.ToInt32(oneMegaByte[0]);
                    int result2 = Convert.ToInt32(fileMaxSize[0]);

                    var postedFile = Request.Form.Files[0];
                    var vfile = postedFile.FileName.Replace("\"", string.Empty).Replace("'", string.Empty);

                    vfile = vfile + Guid.NewGuid().ToString();

                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["MemberPassportFolder"], vfile + ".pdf");

                    using (var fileSrteam = new FileStream(fullPath, FileMode.Create))
                    {
                        await postedFile.CopyToAsync(fileSrteam);
                    }
                    // postedFile.sa.SaveAs(fullPath);

                    //  msg = await _fileUploadRespository.UploadFile(Request.Form.Files[0], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                    //    result2);


                    List<string> vlist = new List<string>();
                    vlist.Add(fullPath);



                    await _emailsender.SendEmailAsync(user.Email, message, mailContent, true, vlist);



                }

                catch (Exception ee)
                {
                    var kk = ee.Message;
                }


            }



            var user3 = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));

            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Update,
                DateCreated = DateTime.Now,
                Description = $"Application File Sent successfully",
                Entity = "Pwallet",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip,
                RecordBefore = "",
                RecordAfter = ""
            });

            return PrepareResponse(HttpStatusCode.OK, "Mail Sent", false);

        }

        [HttpPost("SaveApplication")]
        public async Task<IActionResult> SaveApplication([FromForm] string userId,
      [FromForm] string Applicationtypeid, [FromForm] string transactionid, [FromForm] string logo_descriptionID, [FromForm] string nation_classID, [FromForm] string tm_typeID, [FromForm] string product_title, [FromForm] string nice_class , [FromForm] string pwalletid, [FromForm] string nice_class_description)
        {
            string pwalletid2 = "";

            Boolean FileUpload = false;
            Boolean FileUpload1 = false;
            Boolean FileUpload2 = false;
            Boolean FileUpload3 = false;

            try
            {

                MarkInfo_View markinfoview = new MarkInfo_View();

                markinfoview.tm_typeID = tm_typeID;
                markinfoview.userid = userId;
                markinfoview.product_title = product_title;
                markinfoview.nice_class = nation_classID;
                markinfoview.nation_classID = nation_classID;
                markinfoview.logo_descriptionID = logo_descriptionID;
                markinfoview.nice_class_description = nice_class_description;









                if (Request.Form.Files.Count > 0)
                {
                    foreach (var  file in Request.Form.Files)
                    {
                        
                        var files = new List<string>();
                        if (file.Name == "FileUpload")
                        {
                            FileUpload = true;
                            var postedFile = Request.Form.Files["FileUpload"];

                            string msg = "";
                            try
                            {
                                String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                                String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                                int result1 = Convert.ToInt32(oneMegaByte[0]);
                                int result2 = Convert.ToInt32(fileMaxSize[0]);

                                msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                                  result2);

                                markinfoview.logo_pic = msg;

                            }

                            catch (Exception ee)
                            {
                                var kk = ee.Message;
                            }





                        }

                        if (file.Name == "FileUpload2")
                        {
                            FileUpload1 = true;

                            var postedFile = Request.Form.Files["FileUpload2"];

                            string msg = "";
                            try
                            {
                                String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                                String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                                int result1 = Convert.ToInt32(oneMegaByte[0]);
                                int result2 = Convert.ToInt32(fileMaxSize[0]);

                                msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload2"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                                  result2);

                                markinfoview.auth_doc = msg;

                            }

                            catch (Exception ee)
                            {
                                var kk = ee.Message;
                            }





                        }


                        if (file.Name == "FileUpload3")
                        {
                            FileUpload2 = true;
                            var postedFile = Request.Form.Files["FileUpload3"];

                            string msg = "";
                            try
                            {
                                String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                                String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                                int result1 = Convert.ToInt32(oneMegaByte[0]);
                                int result2 = Convert.ToInt32(fileMaxSize[0]);

                                msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload3"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                                  result2);

                                markinfoview.sup_doc1 = msg;

                            }

                            catch (Exception ee)
                            {
                                var kk = ee.Message;
                            }





                        }


                        if (file.Name == "FileUpload4")
                        {
                            FileUpload3 = true;
                            var postedFile = Request.Form.Files["FileUpload4"];

                            string msg = "";
                            try
                            {
                                String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                                String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                                int result1 = Convert.ToInt32(oneMegaByte[0]);
                                int result2 = Convert.ToInt32(fileMaxSize[0]);

                                msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload4"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                                  result2);

                                markinfoview.sup_doc2 = msg;

                            }

                            catch (Exception ee)
                            {
                                var kk = ee.Message;
                            }





                        }
                    }

                }

                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(userId); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                string json2 = "";
                if (pwalletid == null)
                {
                    // attempt to save
                    Application content = new Application();
                    content.Applicationtypeid = Convert.ToInt32(Applicationtypeid);
                    content.DateCreated = DateTime.Now;
                    content.ApplicationStatus = STATUS.Pending;
                    content.DataStatus = DATASTATUS.Search;
                    content.userid = userId;
                    //  content.transactionid = transactionid;

                    content.IsActive = true;

                    content.IsDeleted = false;


                    var save = await _newApplicationRepository.SaveApplication(content);
                    pwalletid2 = save.Id.ToString();
                    markinfoview.pwalletid = save.Id;
                     json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);
                    SaveMarkInfo(markinfoview);

  

                }

                else
                {
                    var mark  = await _newApplicationRepository.GetMarkInfo(Convert.ToInt32(pwalletid));
                    pwalletid2 =Convert.ToString( mark.applicationid );
                    mark.TradeMarkTypeID = Convert.ToInt32(tm_typeID);
                    mark.userid = userId;
                    mark.ProductTitle = product_title;
                    mark.NiceClass = markinfoview.nice_class;
                   mark.logo_descriptionID = logo_descriptionID;
                    mark.NationClassID = markinfoview.nice_class;
                    mark.NiceClassDescription = markinfoview.nice_class_description;
                    if (FileUpload) {
                    mark.LogoPicture = markinfoview.logo_pic;
                    }
                    if (FileUpload1)
                    {
                        mark.ApprovalDocument = markinfoview.auth_doc;
                    }
                    if (FileUpload2)
                    {
                        mark.SupportDocument1 = markinfoview.sup_doc1;
                    }

                    if (FileUpload3)
                    {
                        mark.SupportDocument2 = markinfoview.sup_doc2;

                    }


                    var markInfo = await _newApplicationRepository.UpdateMarkInfo(mark);

     


                }










                // get User Information
                //  user = await _userManager.FindByIdAsync(SectorViewModel.CreatedBy.ToString());


                // Added A New Sector 

                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new  Fresh Application   successfully",
                    Entity = "PwalletAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip,
                    RecordAfter = json2
                });




                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, pwalletid2);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Fresh Application", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


      

        [HttpGet("UpDatePwalletById")]
        public async Task<IActionResult> UpDatePwalletById([FromQuery] string pwalletid , [FromQuery] string transid)
        {
            try
            {
                // check for user information before processing the request
                _newApplicationRepository.updateTransactionById(pwalletid, transid);

                return Ok();
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetAllApplicationUserCount")]
        public async Task<IActionResult> GetAllApplicationUserCount( [FromQuery] string userid)
        {
            try
            {
                // check for user information before processing the request
                Task<AppCount> result = null;
                try
                {
                  result = _newApplicationRepository.AllApplicationUserCount(userid);
                }
                catch(Exception ee )
                {

                }

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("GetApplicationUserCount")]
        public async Task<IActionResult> GetApplicationUserCount([FromQuery] string applicationid, [FromQuery] string userid)
        {
            try
            {
                // check for user information before processing the request
             var result =   _newApplicationRepository.ApplicationUserCount(Convert.ToInt32(applicationid), Convert.ToInt32(userid));

                return Ok(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("GetAknwoledgment")]
        public async Task<IActionResult> GetAknwoledgment([FromQuery] string pwalletid)
        {
            try
            {
                // check for user information before processing the request
                int id = Convert.ToInt32(pwalletid);


                var vpwallet = (from c in _contex.Application where c.Id == id select c).FirstOrDefault();
                int userid = Convert.ToInt32(vpwallet.userid);
                var vmarkinfo = (from c in _contex.MarkInformation where c.applicationid == vpwallet.Id select c).Include(o => o.trademarktype).FirstOrDefault();
                var vAppUser = (from c in _contex.ApplicationUsers where c.Id == userid select c).FirstOrDefault();

                var Logo = (from c in _contex .TrademarkLogo  select c).ToList();

                TrademarkView tm = new TrademarkView();
                tm.applicationUser = vAppUser;
                tm.markinfo = vmarkinfo;
                tm.application = vpwallet;
                tm.TrademarkLogo = Logo;





                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, tm);



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select State", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        [HttpGet("GetAknwoledgmentByUserid")]
        public async Task<IActionResult> GetAknwoledgmentByUserid([FromQuery] string userid)
        {
            try
            {
                // check for user information before processing the request
                int usersid = Convert.ToInt32(userid);
               

                var vpwallet = (from c in _contex.Application where c.userid == userid && c.ApplicationStatus == "Pending" select c).FirstOrDefault();
               // int userid = Convert.ToInt32(vpwallet.userid);
               var vmarkinfo  = (from c in _contex.MarkInformation where c.applicationid == vpwallet.Id select c).FirstOrDefault();
                var vAppUser = (from c in _contex.ApplicationUsers where c.Id == usersid select c).FirstOrDefault();

                TrademarkView tm = new TrademarkView();
                tm.applicationUser = vAppUser;
                tm.markinfo = vmarkinfo;
                tm.application= vpwallet;





                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, tm);

              

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select State", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }



        [HttpGet("GetTrademarkLogo")]
        public async Task<IActionResult> GetTrademarkLogo()
        {
            try
            {
                // check for user information before processing the request
               


                var TrademarkLogo = (from c in _contex.TrademarkLogo where c.IsDeleted == false && c.IsActive ==true select c).ToList();
               





                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, TrademarkLogo);



            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select State", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }



        [NonAction]
        public async  void UpdateMarkInfo(int id )

        {
           MarkInformation  vmark_info = await _newApplicationRepository.GetMarkInfo(id);
         //  var vmark_info = (from c in _contex.MarkInformation where c.Id ==id select c).FirstOrDefault();
            
            if (vmark_info.TradeMarkTypeID ==1)
            {
                vmark_info.RegistrationNumber = "NG/TM/O/" + DateTime.Today.Date.ToString("yyyy") + "/" +id;
            }

            else
            {
                vmark_info.RegistrationNumber = "F/TM/O/" + DateTime.Today.Date.ToString("yyyy") + "/" + id;
            }

            await  _newApplicationRepository.UpdateMarkInfo(vmark_info);



        }
        [NonAction]
        public async void  SaveMarkInfo(IPORevamp.Data.Entity.Interface.Entities.MarkInfo.MarkInfo_View markInfo_View)
        {
            try
            {

                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(markInfo_View.userid); ;
                if (user == null)
                {
                    return ; ;
                }


                // attempt to save
                MarkInformation content = new MarkInformation();
                content.ApprovalDocument= markInfo_View.auth_doc;
              

                content.DateCreated = DateTime.Now;
                content.logo_descriptionID = markInfo_View.logo_descriptionID;
                content.LogoPicture = markInfo_View.logo_pic;
                content.NationClassID = markInfo_View.nice_class;
                content.NiceClass = markInfo_View.nice_class;
                content.ProductTitle = markInfo_View.product_title;
                content.RegistrationNumber = markInfo_View.reg_number;
                content.applicationid = markInfo_View.pwalletid;
                content.SupportDocument1 = markInfo_View.sup_doc1;
                content.SupportDocument2 = markInfo_View.sup_doc2;
                content.ApprovalDocument= markInfo_View.auth_doc;
                content.NiceClassDescription = markInfo_View.nice_class_description;
                content.TradeMarkTypeID = Convert.ToInt32(markInfo_View.tm_typeID);


                content.userid = markInfo_View.userid;
               
             

                content.IsActive = true;

                content.IsDeleted = false;
               // string json2 = JsonConvert.SerializeObject(markInfo_View, Newtonsoft.Json.Formatting.Indented);

                MarkInformation  save = await  _newApplicationRepository.SaveMarkInfo(content);
                await _newApplicationRepository.UpdateMarkInfo(save.Id);
                //  UpdateMarkInfo(saveContent.Entity.Id);
                //  UpdateMarkInfo(save.applicationid);
                // _contex.MarkInformation.Add(content);




                //   _contex.SaveChanges();


                //   var save = await _newApplicationRepository.SaveMarkInfo(content);












                // get User Information
                //  user = await _userManager.FindByIdAsync(SectorViewModel.CreatedBy.ToString());


                // Added A New Sector 



            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Mark Application", "");
               
            }
        }

    }
}
