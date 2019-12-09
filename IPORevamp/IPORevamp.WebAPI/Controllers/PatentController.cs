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
using IPORevamp.Repository.PatentType;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;
using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Patent")]
    [ApiController]
    public class PatentController : BaseController
    {

        private readonly IPatentTypeRepository _patentTypeRepository;
        private readonly InewApplication _newApplicationRepository;

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly Repository.Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        private readonly IPOContext _contex;



        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;




        public PatentController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
    IPOContext contex,
    IEmailManager<EmailLog, EmailTemplate> emailManager,
      Repository.Email.IEmailTemplateRepository EmailTemplateRepository,
       InewApplication newApplicationRepository,


    IPatentTypeRepository patentTypeRepository,

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
            _patentTypeRepository = patentTypeRepository;

            _contex = contex;
            _newApplicationRepository = newApplicationRepository;
            _fileUploadRespository = fileUploadRespository;
            _EmailTemplateRepository = EmailTemplateRepository;


        }


        [HttpPost("SavePatentInvention")]
        public async Task<IActionResult> SavePatentInvention([FromBody] PatentInventionView[] PatentInvention)
        {
        var result =    await _newApplicationRepository.SavePatentInvention(PatentInvention ,Convert.ToInt32(PatentInvention[0].PatentApplicationID));

        

            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, result);
        }


        [HttpPost("SavePatentPriority")]
        public async Task<IActionResult> SavePatentPriority([FromBody] PatentPriorityInformationView[] PatentPriority)
        {
            PatentPriorityInformationView[] result = null;
            try
            {
                result = await _newApplicationRepository.SavePriorityInformation(PatentPriority, Convert.ToInt32(PatentPriority[0].PatentApplicationID));
               
                
             }

            catch(Exception ee)
            {


                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, result);
            }


                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, result);

          
        }

        [HttpGet("UpDatePatentTransactionById")]
        public async Task<IActionResult> UpDatePatentTransactionById([FromQuery] string pwalletid, [FromQuery] string transid)
        {
            try
            {

                EmailTemplate emailTemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.PatentFreshApplication && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();
                // check for user information before processing the request
                _newApplicationRepository.updatePatentTransactionById(pwalletid, transid);

                string mailContent = emailTemplate.EmailBody;


                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                var roleid = Convert.ToInt32(IPORoles.Search_Officer_Trade_Mark);

                var user3 = _userManager.Users.Where(x => x.RolesId == roleid).ToList();

                foreach (var users in user3)
                {
                    mailContent = mailContent.Replace("#Name", users.FirstName + " " + users.LastName);
                  
                    await _emailsender.SendEmailAsync(users.Email, emailTemplate.EmailSubject, mailContent);


                }






                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpPost("SendAttachmentAcceptance")]
        public async Task<IActionResult> SendAttachmentAcceptance([FromForm] string userid, [FromForm] string message)
        {
            string ip = "";

            ip = Request.Headers["ip"];


            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(userid));
            EmailTemplate emailtemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.PatentAcceptance);

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

        [HttpGet("GetPatentApplicationById")]
        public async Task<IActionResult> GetPatentApplicationById([FromQuery] string Applicationid)
        {
            var result = await _newApplicationRepository.GetPatentApplicationById( Convert.ToInt32(Applicationid));



            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, result);
        }

        [HttpGet("GetPatentApplicationByUserId")]
        public async Task<IActionResult> GetPatentApplicationByUserId([FromQuery] string userid)
        {
            var result = await _newApplicationRepository.GetPatentApplicationByUserId(userid);



            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, result);
        }

        [HttpPost("SaveApplication")]
        public async Task<IActionResult> SavePatentApplication([FromForm] string userId,
      [FromForm] string patenttype, [FromForm] string titleofinvention , [FromForm] string description, [FromForm] string AssigneeName, [FromForm] string AssigneeAddress , [FromForm] string AssigneeNationality, [FromForm] string AssignorName, [FromForm] string AssignorAddress, [FromForm] string AssignorNationality, [FromForm] string ApplicationId, [FromForm] string AttorneyCode, [FromForm] string AttorneyName, [FromForm] string Address, [FromForm] string PhoneNumber, [FromForm] string Email, [FromForm] string State)
        {
            Boolean FileUpload = false;
            Boolean FileUpload1 = false;
            Boolean FileUpload2 = false;
            Boolean FileUpload3 = false;
            Boolean FileUpload4 = false;
            Boolean FileUpload5 = false;

            string ip = "";
            ip = Request.Headers["ip"];
            AddressOfService addressOfService = new AddressOfService();
            addressOfService.Address = Address;
            addressOfService.AttorneyCode = AttorneyCode;
            addressOfService.AttorneyName = AttorneyName;
            addressOfService.DateCreated = DateTime.Now;
            addressOfService.IsActive = true;
            addressOfService.IsDeleted = false;

            addressOfService.PhoneNumber = PhoneNumber;
            addressOfService.Email = Email;
            try
            {
                addressOfService.StateID = Convert.ToInt32(State);

            }

            catch(Exception  ee)
            {

            }

            PatentInformation patentInformation = new PatentInformation();

            patentInformation.InventionDescription = description;
            patentInformation.PatentTypeID =Convert.ToInt32( patenttype);

            patentInformation.TitleOfInvention = titleofinvention;

            patentInformation.IsDeleted = false;
            patentInformation.IsActive = true;

           
            patentInformation.DateCreated = DateTime.Now;

            PatentAssignment patentAssignment = new PatentAssignment();

            patentAssignment.AssigneeName = AssigneeName;
            patentAssignment.AssigneeAddress = AssigneeAddress;
            patentAssignment.AssigneeNationalityId = Convert.ToInt32(AssigneeNationality);
            patentAssignment.AssignorAddress = AssignorAddress;
            patentAssignment.AssignorName = AssignorName;
            patentAssignment.AssignorNationalityId = Convert.ToInt32(AssignorNationality);
            patentAssignment.DateOfAssignment = DateTime.Now;

            patentAssignment.IsDeleted = false;
            patentAssignment.IsActive = true;

            patentAssignment.DateCreated = DateTime.Now;







            if (Request.Form.Files.Count > 0)
            {
                foreach (var file in Request.Form.Files)
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
                            patentInformation.PctDocument = msg;
                            //  markinfoview.logo_pic = msg;

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
                            patentInformation.LetterOfAuthorization = msg;
                            // markinfoview.auth_doc = msg;

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
                            patentInformation.CompleteSpecificationForm = msg;
                            //   markinfoview.sup_doc1 = msg;

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
                            patentInformation.Claims = msg;
                            //  markinfoview.sup_doc2 = msg;

                        }

                        catch (Exception ee)
                        {
                            var kk = ee.Message;
                        }





                    }

                    if (file.Name == "FileUpload5")
                    {
                        FileUpload4 = true;
                        var postedFile = Request.Form.Files["FileUpload5"];

                        string msg = "";
                        try
                        {
                            String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                            String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                            int result1 = Convert.ToInt32(oneMegaByte[0]);
                            int result2 = Convert.ToInt32(fileMaxSize[0]);

                            msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload5"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                              result2);
                            patentInformation.DeedOfAssignment = msg;
                            //  markinfoview.sup_doc2 = msg;

                        }

                        catch (Exception ee)
                        {
                            var kk = ee.Message;
                        }





                    }
                }

            }






            var user = await _userManager.FindByIdAsync(userId); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }

            string json2 = "";
            int  ApplicationId2 = Convert.ToInt32(ApplicationId);


            PatentApplication content = new PatentApplication();
            
            content.DateCreated = DateTime.Now;
            content.ApplicationStatus = STATUS.Pending;
            content.DataStatus =DATASTATUS.Search ;
            content.userid = userId;
            //  content.transactionid = transactionid;

            content.IsActive = true;

            content.IsDeleted = false;

            if (ApplicationId == null)
            {

                var save = await _newApplicationRepository.SavePatentApplication(content);
                ApplicationId2 = save.Id;

                patentAssignment.PatentApplicationID = ApplicationId2;

                patentInformation.PatentApplicationID = ApplicationId2;

                addressOfService.PatentApplicationID = ApplicationId2;
                if (patentInformation.LetterOfAuthorization !=null) { 
              await  _newApplicationRepository.SaveAddressOfService(addressOfService);

                }

                var result = await _newApplicationRepository.SavePatentInformation(patentInformation);
                await _newApplicationRepository.SavePatentAssignment(patentAssignment);
                
            }

            else
            {
                var patentAllinformation = await _newApplicationRepository.GetPatentApplication(Convert.ToInt32(ApplicationId));



                patentAllinformation.InventionDescription = description;
                patentAllinformation.PatentTypeID = Convert.ToInt32(patenttype);

                patentAllinformation.TitleOfInvention = titleofinvention;

                if (FileUpload)
                {
                    patentAllinformation.PctDocument = patentInformation.PctDocument;
                }

                if (FileUpload1)
                {
                    patentAllinformation.LetterOfAuthorization = patentInformation.LetterOfAuthorization;
                }

                if (FileUpload2)
                {
                    patentAllinformation.CompleteSpecificationForm = patentInformation.CompleteSpecificationForm;
                }

                if (FileUpload3)
                {
                    patentAllinformation.Claims = patentInformation.Claims;
                }

                if (FileUpload4)
                {
                    patentAllinformation.DeedOfAssignment = patentInformation.DeedOfAssignment;
                }
                await _newApplicationRepository.UpdatePatentInformation(patentAllinformation);

                addressOfService.PatentApplicationID = Convert.ToInt32(ApplicationId);
                if (patentInformation.LetterOfAuthorization != null)
                {
                    _newApplicationRepository.SaveAddressOfService(addressOfService);

                }

                await _newApplicationRepository.UpdatePatentAssignment(Convert.ToInt32(ApplicationId), patentAssignment);

            }



            // Added A New Country 
            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Create,
                DateCreated = DateTime.Now,
                Description = $"User {user.FirstName + ' ' + user.LastName}  requested to add New Patent Application   successfully",
                Entity = "Add New Patent",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip
            });

            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, ApplicationId2);
           

        }

        [HttpPost("SaveApplicationMigration")]
        public async Task<IActionResult> SaveApplicationMigration([FromForm] string userId,
             [FromForm] string patenttype, [FromForm] string titleofinvention, [FromForm] string description, [FromForm] string AssigneeName, [FromForm] string AssigneeAddress, [FromForm] string AssigneeNationality, [FromForm] string AssignorName, [FromForm] string AssignorAddress, [FromForm] string AssignorNationality, [FromForm] string ApplicationId, [FromForm] string AttorneyCode, [FromForm] string AttorneyName, [FromForm] string Address, [FromForm] string PhoneNumber, [FromForm] string Email, [FromForm] string State, [FromForm] string registration_number, [FromForm] string filingdate)
        {
            Boolean FileUpload = false;
            Boolean FileUpload1 = false;
            Boolean FileUpload2 = false;
            Boolean FileUpload3 = false;
            Boolean FileUpload4 = false;
            Boolean FileUpload5 = false;

            string ip = "";
            ip = Request.Headers["ip"];
            AddressOfService addressOfService = new AddressOfService();
            addressOfService.Address = Address;
            addressOfService.AttorneyCode = AttorneyCode;
            addressOfService.AttorneyName = AttorneyName;
            addressOfService.DateCreated =Convert.ToDateTime(filingdate);
            addressOfService.IsActive = true;
            addressOfService.IsDeleted = false;

            addressOfService.PhoneNumber = PhoneNumber;
            addressOfService.Email = Email;
            try
            {
                addressOfService.StateID = Convert.ToInt32(State);

            }

            catch (Exception ee)
            {

            }

            PatentInformation patentInformation = new PatentInformation();

            patentInformation.InventionDescription = description;
            patentInformation.PatentTypeID = Convert.ToInt32(patenttype);

            patentInformation.TitleOfInvention = titleofinvention;
            patentInformation.RegistrationNumber = registration_number;

            patentInformation.IsDeleted = false;
            patentInformation.IsActive = true;


            patentInformation.DateCreated = Convert.ToDateTime(filingdate);

            PatentAssignment patentAssignment = new PatentAssignment();

            patentAssignment.AssigneeName = AssigneeName;
            patentAssignment.AssigneeAddress = AssigneeAddress;
            patentAssignment.AssigneeNationalityId = Convert.ToInt32(AssigneeNationality);
            patentAssignment.AssignorAddress = AssignorAddress;
            patentAssignment.AssignorName = AssignorName;
            patentAssignment.AssignorNationalityId = Convert.ToInt32(AssignorNationality);
            patentAssignment.DateOfAssignment = Convert.ToDateTime(filingdate);

            patentAssignment.IsDeleted = false;
            patentAssignment.IsActive = true;

            patentAssignment.DateCreated = Convert.ToDateTime(filingdate);







            if (Request.Form.Files.Count > 0)
            {
                foreach (var file in Request.Form.Files)
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
                            patentInformation.PctDocument = msg;
                            //  markinfoview.logo_pic = msg;

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
                            patentInformation.LetterOfAuthorization = msg;
                            // markinfoview.auth_doc = msg;

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
                            patentInformation.CompleteSpecificationForm = msg;
                            //   markinfoview.sup_doc1 = msg;

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
                            patentInformation.Claims = msg;
                            //  markinfoview.sup_doc2 = msg;

                        }

                        catch (Exception ee)
                        {
                            var kk = ee.Message;
                        }





                    }

                    if (file.Name == "FileUpload5")
                    {
                        FileUpload4 = true;
                        var postedFile = Request.Form.Files["FileUpload5"];

                        string msg = "";
                        try
                        {
                            String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                            String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                            int result1 = Convert.ToInt32(oneMegaByte[0]);
                            int result2 = Convert.ToInt32(fileMaxSize[0]);

                            msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload5"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                              result2);
                            patentInformation.DeedOfAssignment = msg;
                            //  markinfoview.sup_doc2 = msg;

                        }

                        catch (Exception ee)
                        {
                            var kk = ee.Message;
                        }





                    }
                }

            }






            var user = await _userManager.FindByIdAsync(userId); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }

            string json2 = "";
            int ApplicationId2 = Convert.ToInt32(ApplicationId);


            PatentApplication content = new PatentApplication();

            content.DateCreated = Convert.ToDateTime(filingdate);
            content.ApplicationStatus = STATUS.Pending;
            content.DataStatus = DATASTATUS.Migration;
            content.userid = userId;
            //  content.transactionid = transactionid;

            content.IsActive = true;

            content.IsDeleted = false;

            if (ApplicationId == null)
            {

                var save = await _newApplicationRepository.SavePatentApplication(content);
                ApplicationId2 = save.Id;

                var patentApplication = await _newApplicationRepository.GetPatentApplicationById(Convert.ToInt32(ApplicationId2));
                patentApplication.DateCreated = Convert.ToDateTime(filingdate);
                await _newApplicationRepository.UpdatePatentApplication(patentApplication);

                patentAssignment.PatentApplicationID = ApplicationId2;

                patentInformation.PatentApplicationID = ApplicationId2;

                addressOfService.PatentApplicationID = ApplicationId2;
                if (patentInformation.LetterOfAuthorization != null)
                {
                    await _newApplicationRepository.SaveAddressOfService(addressOfService);

                }

                var result = await _newApplicationRepository.SavePatentInformation2(patentInformation);
                await _newApplicationRepository.SavePatentAssignment(patentAssignment);

            }

            else
            {
                var patentApplication = await _newApplicationRepository.GetPatentApplicationById(Convert.ToInt32(ApplicationId));
                patentApplication.DateCreated = Convert.ToDateTime(filingdate);
                await _newApplicationRepository.UpdatePatentApplication(patentApplication);

                var patentAllinformation = await _newApplicationRepository.GetPatentApplication(Convert.ToInt32(ApplicationId));

                patentAllinformation.DateCreated = Convert.ToDateTime(filingdate);

                patentAllinformation.InventionDescription = description;
                patentAllinformation.PatentTypeID = Convert.ToInt32(patenttype);

                patentAllinformation.TitleOfInvention = titleofinvention;

                if (FileUpload)
                {
                    patentAllinformation.PctDocument = patentInformation.PctDocument;
                }

                if (FileUpload1)
                {
                    patentAllinformation.LetterOfAuthorization = patentInformation.LetterOfAuthorization;
                }

                if (FileUpload2)
                {
                    patentAllinformation.CompleteSpecificationForm = patentInformation.CompleteSpecificationForm;
                }

                if (FileUpload3)
                {
                    patentAllinformation.Claims = patentInformation.Claims;
                }

                if (FileUpload4)
                {
                    patentAllinformation.DeedOfAssignment = patentInformation.DeedOfAssignment;
                }
                patentAllinformation.RegistrationNumber = registration_number;
                await _newApplicationRepository.UpdatePatentInformation(patentAllinformation);

                addressOfService.PatentApplicationID = Convert.ToInt32(ApplicationId);
                if (patentInformation.LetterOfAuthorization != null)
                {
                    _newApplicationRepository.SaveAddressOfService(addressOfService);

                }

                await _newApplicationRepository.UpdatePatentAssignment(Convert.ToInt32(ApplicationId), patentAssignment);

            }



            // Added A New Country 
          

            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, ApplicationId2);


        }


        [HttpGet("GetAllPatentType")]
        public async Task<IActionResult> GetAllPatentType([FromQuery] string RequestById)
        {
            string ip = "";


            try
            {
                ip = Request.Headers["ip"];
                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }


                var patenttype = await _patentTypeRepository.GetPatentType();

                if (patenttype != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());

                    // Added A New Country 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all patenttype  successfully",
                        Entity = "GetAllPatentType",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "PatenType Returned Successfully", false, patenttype);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select PatentType", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


    }
}
