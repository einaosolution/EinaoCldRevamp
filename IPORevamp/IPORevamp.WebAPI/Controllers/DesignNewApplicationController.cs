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

using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using IPORevamp.Repository.DesignNewpplication;
using IPORevamp.Data.Entity.Interface.Entities.AddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.PatentInformation;
using IPORevamp.Data.Entity.Interface.Entities.PatentAssignment;
using IPORevamp.Data.Entity.Interface.Entities.PatentApplication;
using IPORevamp.Data.Entity.Interface.Entities.DesignAssigment;
using IPORevamp.Data.Entity.Interface.Entities.DesignAddressOfService;
using IPORevamp.Data.Entity.Interface.Entities.DesignInformation;
using IPORevamp.Data.Entity.Interface.Entities.DesignApplication;
using IPORevamp.Data.Entity.Interface.Entities.PatentInvention;
using IPORevamp.Data.Entity.Interface.Entities.PatentPriorityInformation;
using IPORevamp.Data.Entity.Interface.Entities.DesignCoApplicant;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Design")]
    [ApiController]
    public class DesignNewApplicationController : BaseController
    {
        private readonly IDesignNewpplicationRepository _designRepository;
        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly Repository.Email.IEmailTemplateRepository _EmailTemplateRepository;
        private IFileHandler _fileUploadRespository;
        private readonly IPOContext _contex;
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public DesignNewApplicationController(
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration,
    IMapper mapper, ILogger<UserController> logger,
    IPOContext contex,
    IEmailManager<EmailLog, EmailTemplate> emailManager,
      Repository.Email.IEmailTemplateRepository EmailTemplateRepository,
       IDesignNewpplicationRepository designRepository,




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
          //  _httpContextAccessor = httpContextAccessor;
            //_patentTypeRepository = patentTypeRepository;

            _contex = contex;
        _designRepository = designRepository;
            _fileUploadRespository = fileUploadRespository;
            _EmailTemplateRepository = EmailTemplateRepository;
           

        }





        [HttpGet("UpDateDesignTransactionById")]
        public async Task<IActionResult> UpDateDesignTransactionById([FromQuery] string pwalletid, [FromQuery] string transid)
        {
            try
            {

                EmailTemplate emailTemplate = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.PatentFreshApplication && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();
                EmailTemplate emailTemplate2 = (from c in _contex.EmailTemplates where c.EmailName == IPOCONSTANT.CoApplicant && c.IsActive == true && c.IsDeleted == false select c).FirstOrDefault();
                // check for user information before processing the request
                _designRepository.updateDesignTransactionById(pwalletid, transid);

                string mailContent = emailTemplate.EmailBody;
                string mailContent2 = emailTemplate2.EmailBody;


                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);
                mailContent2 = mailContent2.Replace("#path", _configuration["LOGOURL"]);

                var  result = (from c in _contex.DesignInformation where c.DesignApplicationID == Convert.ToInt32(pwalletid)  select c).FirstOrDefault();
                var coapplicant = (from c in _contex.DesignCoApplicant where c.DesignApplicationID == Convert.ToInt32(pwalletid) select c).ToList();

                var roleid = Convert.ToInt32(IPORoles.Search_Officer_Design);

                var user3 = _userManager.Users.Where(x => x.RolesId == roleid).ToList();

                foreach (var users in user3)
                {
                    mailContent = mailContent.Replace("#Name", users.FirstName + " " + users.LastName);

                    await _emailsender.SendEmailAsync(users.Email, emailTemplate.EmailSubject, mailContent);


                }


                foreach (var userdetail in coapplicant)
                {
                    mailContent2 = mailContent2.Replace("#title", result.TitleOfDesign);

                    await _emailsender.SendEmailAsync(userdetail.email, emailTemplate2.EmailSubject, mailContent2);


                }






                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }



        [HttpGet("GetDesignApplicationByUserId")]
        public async Task<IActionResult> GetPatentApplicationByUserId([FromQuery] string userid)
        {
            var result = await _designRepository.GetDesignApplicationByUserId(userid);



            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, result);
        }


        [HttpGet("GetDesignApplicationById")]
        public async Task<IActionResult> GetPatentApplicationById([FromQuery] string Applicationid)
        {
            var result = await _designRepository.GetDesignApplicationById(Convert.ToInt32(Applicationid));



            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, result);
        }


        [HttpGet("CancelDesignApplicationById")]
        public async Task<IActionResult> CancelDesignApplicationById([FromQuery] string userid ,[FromQuery] string Applicationid)
        {
            string ip = "";
            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(userid); ;

            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }

       _designRepository.GetCancelApplicationById(Convert.ToInt32(Applicationid));

            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Create,
                DateCreated = DateTime.Now,
                Description = $"User {user.FirstName + ' ' + user.LastName}  requested to cancel  Application   successfully",
                Entity = "Cancel  Design",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip
            });

            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, null);
        }

        [HttpGet("CancelPatentApplicationById")]
        public async Task<IActionResult> CancelPatentApplicationById([FromQuery] string userid, [FromQuery] string Applicationid)
        {
            string ip = "";
            ip = Request.Headers["ip"];
            var user = await _userManager.FindByIdAsync(userid); ;

            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
            }

            _designRepository.GetCancelApplication2ById(Convert.ToInt32(Applicationid));

            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Create,
                DateCreated = DateTime.Now,
                Description = $"User {user.FirstName + ' ' + user.LastName}  requested to cancel  Application   successfully",
                Entity = "Cancel  Design",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip
            });

            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, null);
        }




        [HttpPost("SaveDesignCoApplicant")]
        public async Task<IActionResult> SaveDesignCoApplicant([FromBody] CoApplicantView[] coapplicantView)
        {
            CoApplicantView[] result = null;
            try
            {
                result = await _designRepository.SaveCoApplicantInformation(coapplicantView, Convert.ToInt32(coapplicantView[0].ApplicationID));


            }

            catch (Exception ee)
            {


                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, result);
            }


            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, result);


        }

        [HttpPost("SaveDesignPriority")]
        public async Task<IActionResult> SavePatentPriority([FromBody] PatentPriorityInformationView[] PatentPriority)
        {
            PatentPriorityInformationView[] result = null;
            try
            {
                result = await _designRepository.SavePriorityInformation(PatentPriority, Convert.ToInt32(PatentPriority[0].PatentApplicationID));


            }

            catch (Exception ee)
            {


                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, result);
            }


            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, result);


        }

        [HttpPost("SaveDesignInvention")]
        public async Task<IActionResult> SaveDesignInvention([FromBody] PatentInventionView[] PatentInvention)
        {
            var result = await _designRepository.SaveDesignInvention(PatentInvention, Convert.ToInt32(PatentInvention[0].PatentApplicationID));



            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, result);
        }

        [HttpPost("SaveApplication")]
        public async Task<IActionResult> SavePatentApplication([FromForm] string userId,
     [FromForm] string patenttype, [FromForm] string titleofinvention, [FromForm] string description, [FromForm] string AssigneeName, [FromForm] string AssigneeAddress, [FromForm] string AssigneeNationality, [FromForm] string AssignorName, [FromForm] string AssignorAddress, [FromForm] string AssignorNationality, [FromForm] string ApplicationId, [FromForm] string AttorneyCode, [FromForm] string AttorneyName, [FromForm] string Address, [FromForm] string PhoneNumber, [FromForm] string Email, [FromForm] string State , [FromForm] string DesignClass)
        {
            Boolean FileUpload = false;
            Boolean FileUpload1 = false;
            Boolean FileUpload2 = false;
            Boolean FileUpload3 = false;
            Boolean FileUpload4 = false;
            Boolean FileUpload5 = false;
            Boolean FileUpload6 = false;
            Boolean FileUpload7 = false;
            Boolean FileUpload8 = false;
            Boolean FileUpload9 = false;

            string ip = "";
            ip = Request.Headers["ip"];
            DesignAddressOfService addressOfService = new DesignAddressOfService();
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

            catch (Exception ee)
            {

            }

            DesignInformation designInformation = new DesignInformation();

            designInformation.DesignDescription = description;
            designInformation.NationClassID = Convert.ToInt32(DesignClass);

            
            designInformation.DesignTypeID = Convert.ToInt32(patenttype);

            designInformation.TitleOfDesign = titleofinvention;

            designInformation.IsDeleted = false;
            designInformation.IsActive = true;


            designInformation.DateCreated = DateTime.Now;

            DesignAssignment designAssignment = new DesignAssignment();

            designAssignment.AssigneeName = AssigneeName;
            designAssignment.AssigneeAddress = AssigneeAddress;
            designAssignment.AssigneeNationalityId = Convert.ToInt32(AssigneeNationality);
            designAssignment.AssignorAddress = AssignorAddress;
            designAssignment.AssignorName = AssignorName;
            designAssignment.AssignorNationalityId = Convert.ToInt32(AssignorNationality);
            designAssignment.DateOfAssignment = DateTime.Now;

            designAssignment.IsDeleted = false;
            designAssignment.IsActive = true;

            designAssignment.DateCreated = DateTime.Now;







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
                            designInformation.NoveltyStatement = msg;
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
                            designInformation.LetterOfAuthorization= msg;
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
                            designInformation.DeedOfAssignment = msg;
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
                            designInformation.PriorityDocument = msg;
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
                            designInformation.RepresentationOfDesign1 = msg;
                            //  markinfoview.sup_doc2 = msg;

                        }

                        catch (Exception ee)
                        {
                            var kk = ee.Message;
                        }





                    }


                    if (file.Name == "FileUpload6")
                    {
                        FileUpload5 = true;
                        var postedFile = Request.Form.Files["FileUpload6"];

                        string msg = "";
                        try
                        {
                            String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                            String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                            int result1 = Convert.ToInt32(oneMegaByte[0]);
                            int result2 = Convert.ToInt32(fileMaxSize[0]);

                            msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload6"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                              result2);
                            designInformation.RepresentationOfDesign2 = msg;
                            //  markinfoview.sup_doc2 = msg;

                        }

                        catch (Exception ee)
                        {
                            var kk = ee.Message;
                        }





                    }


                    if (file.Name == "FileUpload7")
                    {
                        FileUpload6 = true;
                        var postedFile = Request.Form.Files["FileUpload7"];

                        string msg = "";
                        try
                        {
                            String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                            String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                            int result1 = Convert.ToInt32(oneMegaByte[0]);
                            int result2 = Convert.ToInt32(fileMaxSize[0]);

                            msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload7"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                              result2);
                            designInformation.RepresentationOfDesign3 = msg;
                            //  markinfoview.sup_doc2 = msg;

                        }

                        catch (Exception ee)
                        {
                            var kk = ee.Message;
                        }





                    }


                    if (file.Name == "FileUpload8")
                    {
                        FileUpload7 = true;
                        var postedFile = Request.Form.Files["FileUpload8"];

                        string msg = "";
                        try
                        {
                            String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                            String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                            int result1 = Convert.ToInt32(oneMegaByte[0]);
                            int result2 = Convert.ToInt32(fileMaxSize[0]);

                            msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload8"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                              result2);
                            designInformation.RepresentationOfDesign4 = msg;
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


            DesignApplication content = new DesignApplication();

            content.DateCreated = DateTime.Now;
            content.ApplicationStatus = STATUS.Pending;
            content.DataStatus = DATASTATUS.Search;
            content.userid = userId;
            //  content.transactionid = transactionid;

            content.IsActive = true;

            content.IsDeleted = false;

            if (ApplicationId == null)
            {

                var save = await _designRepository.SaveDesignApplication(content);
                ApplicationId2 = save.Id;

                designAssignment.DesignApplicationID = ApplicationId2;

                designInformation.DesignApplicationID= ApplicationId2;

                addressOfService.DesignApplicationID = ApplicationId2;
                if (designInformation.LetterOfAuthorization != null)
                {
                    await _designRepository.SaveAddressOfService(addressOfService);

                }

                var result = await _designRepository.SaveDesignInformation(designInformation);
                await _designRepository.SaveDesignAssignment(designAssignment);

            }

            else
            {
                var designAllinformation = await _designRepository.GetDesignApplication(Convert.ToInt32(ApplicationId));



                designAllinformation.DesignDescription = description;
                designAllinformation.DesignTypeID = Convert.ToInt32(patenttype);

                designAllinformation.NationClassID = Convert.ToInt32(DesignClass);

                designAllinformation.TitleOfDesign = titleofinvention;

                if (FileUpload)
                {
                    designAllinformation.NoveltyStatement = designInformation.PriorityDocument;
                }

                if (FileUpload1)
                {
                    designAllinformation.LetterOfAuthorization = designInformation.LetterOfAuthorization;
                }

                if (FileUpload2)
                {
                    designAllinformation.DeedOfAssignment = designInformation.DeedOfAssignment;
                }

                if (FileUpload3)
                {
                    designAllinformation.PriorityDocument = designInformation.PriorityDocument;
                }

                if (FileUpload4)
                {
                    designAllinformation.RepresentationOfDesign1 = designInformation.RepresentationOfDesign1;
                }

                if (FileUpload5)
                {
                    designAllinformation.RepresentationOfDesign2 = designInformation.RepresentationOfDesign2;
                }

                if (FileUpload6)
                {
                    designAllinformation.RepresentationOfDesign3 = designInformation.RepresentationOfDesign3;
                }

                if (FileUpload7)
                {
                    designAllinformation.RepresentationOfDesign4 = designInformation.RepresentationOfDesign4;
                }
                await _designRepository.UpdatedesignInformation(designAllinformation);

                addressOfService.DesignApplicationID = Convert.ToInt32(ApplicationId);
                if (designInformation.LetterOfAuthorization != null)
                {
                    _designRepository.SaveAddressOfService(addressOfService);

                }

                await _designRepository.UpdateDesignAssignment(Convert.ToInt32(ApplicationId), designAssignment);

            }



            // Added A New Country 
            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Create,
                DateCreated = DateTime.Now,
                Description = $"User {user.FirstName + ' ' + user.LastName}  requested to add New Design Application   successfully",
                Entity = "Add New Design",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip
            });

            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, ApplicationId2);


        }



        [HttpPost("SaveApplicationMigration")]
        public async Task<IActionResult> SaveApplicationMigration([FromForm] string userId,
    [FromForm] string patenttype, [FromForm] string titleofinvention, [FromForm] string description, [FromForm] string AssigneeName, [FromForm] string AssigneeAddress, [FromForm] string AssigneeNationality, [FromForm] string AssignorName, [FromForm] string AssignorAddress, [FromForm] string AssignorNationality, [FromForm] string ApplicationId, [FromForm] string AttorneyCode, [FromForm] string AttorneyName, [FromForm] string Address, [FromForm] string PhoneNumber, [FromForm] string Email, [FromForm] string State, [FromForm] string DesignClass, [FromForm] string registration_number, [FromForm] string filingdate)
        {
            Boolean FileUpload = false;
            Boolean FileUpload1 = false;
            Boolean FileUpload2 = false;
            Boolean FileUpload3 = false;
            Boolean FileUpload4 = false;
            Boolean FileUpload5 = false;
            Boolean FileUpload6 = false;
            Boolean FileUpload7 = false;
            Boolean FileUpload8 = false;
            Boolean FileUpload9 = false;

            string ip = "";
            ip = Request.Headers["ip"];
            DesignAddressOfService addressOfService = new DesignAddressOfService();
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

            catch (Exception ee)
            {

            }

            DesignInformation designInformation = new DesignInformation();

            designInformation.DesignDescription = description;
            designInformation.NationClassID = Convert.ToInt32(DesignClass);
            designInformation.RegistrationNumber = registration_number;


            designInformation.DesignTypeID = Convert.ToInt32(patenttype);

            designInformation.TitleOfDesign = titleofinvention;

            designInformation.IsDeleted = false;
            designInformation.IsActive = true;


            designInformation.DateCreated = DateTime.Now;

            DesignAssignment designAssignment = new DesignAssignment();

            designAssignment.AssigneeName = AssigneeName;
            designAssignment.AssigneeAddress = AssigneeAddress;
            designAssignment.AssigneeNationalityId = Convert.ToInt32(AssigneeNationality);
            designAssignment.AssignorAddress = AssignorAddress;
            designAssignment.AssignorName = AssignorName;
            designAssignment.AssignorNationalityId = Convert.ToInt32(AssignorNationality);
            designAssignment.DateOfAssignment = DateTime.Now;

            designAssignment.IsDeleted = false;
            designAssignment.IsActive = true;

            designAssignment.DateCreated = DateTime.Now;







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
                            designInformation.NoveltyStatement = msg;
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
                            designInformation.LetterOfAuthorization = msg;
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
                            designInformation.DeedOfAssignment = msg;
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
                            designInformation.PriorityDocument = msg;
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
                            designInformation.RepresentationOfDesign1 = msg;
                            //  markinfoview.sup_doc2 = msg;

                        }

                        catch (Exception ee)
                        {
                            var kk = ee.Message;
                        }





                    }


                    if (file.Name == "FileUpload6")
                    {
                        FileUpload5 = true;
                        var postedFile = Request.Form.Files["FileUpload6"];

                        string msg = "";
                        try
                        {
                            String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                            String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                            int result1 = Convert.ToInt32(oneMegaByte[0]);
                            int result2 = Convert.ToInt32(fileMaxSize[0]);

                            msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload6"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                              result2);
                            designInformation.RepresentationOfDesign2 = msg;
                            //  markinfoview.sup_doc2 = msg;

                        }

                        catch (Exception ee)
                        {
                            var kk = ee.Message;
                        }





                    }


                    if (file.Name == "FileUpload7")
                    {
                        FileUpload6 = true;
                        var postedFile = Request.Form.Files["FileUpload7"];

                        string msg = "";
                        try
                        {
                            String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                            String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                            int result1 = Convert.ToInt32(oneMegaByte[0]);
                            int result2 = Convert.ToInt32(fileMaxSize[0]);

                            msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload7"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                              result2);
                            designInformation.RepresentationOfDesign3 = msg;
                            //  markinfoview.sup_doc2 = msg;

                        }

                        catch (Exception ee)
                        {
                            var kk = ee.Message;
                        }





                    }


                    if (file.Name == "FileUpload8")
                    {
                        FileUpload7 = true;
                        var postedFile = Request.Form.Files["FileUpload8"];

                        string msg = "";
                        try
                        {
                            String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                            String[] fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                            int result1 = Convert.ToInt32(oneMegaByte[0]);
                            int result2 = Convert.ToInt32(fileMaxSize[0]);

                            msg = await _fileUploadRespository.UploadFile(Request.Form.Files["FileUpload8"], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                              result2);
                            designInformation.RepresentationOfDesign4 = msg;
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


            DesignApplication content = new DesignApplication();

            content.DateCreated = Convert.ToDateTime(filingdate);
            content.ApplicationStatus = STATUS.Pending;
            content.DataStatus = DATASTATUS.Migration;
            content.userid = userId;
            //  content.transactionid = transactionid;

            content.IsActive = true;

            content.IsDeleted = false;

            if (ApplicationId == null)
            {

                var save = await _designRepository.SaveDesignApplication(content);
                ApplicationId2 = save.Id;
                save.DateCreated = Convert.ToDateTime(filingdate);
                await _designRepository.UpdateDesignApplication(save);

                designAssignment.DesignApplicationID = ApplicationId2;

                designInformation.DesignApplicationID = ApplicationId2;

                addressOfService.DesignApplicationID = ApplicationId2;
                if (designInformation.LetterOfAuthorization != null)
                {
                    await _designRepository.SaveAddressOfService(addressOfService);

                }

                var result = await _designRepository.SaveDesignInformation2(designInformation);
                await _designRepository.SaveDesignAssignment(designAssignment);

            }

            else
            {
                var designapplication = await _designRepository.GetDesignApplicationById(Convert.ToInt32(ApplicationId));
                designapplication.DateCreated = Convert.ToDateTime(filingdate);
                await _designRepository.UpdateDesignApplication(designapplication);
                var designAllinformation = await _designRepository.GetDesignApplication(Convert.ToInt32(ApplicationId));



                designAllinformation.DesignDescription = description;
                designAllinformation.RegistrationNumber = registration_number;
                designAllinformation.DesignTypeID = Convert.ToInt32(patenttype);

                designAllinformation.NationClassID = Convert.ToInt32(DesignClass);

                designAllinformation.TitleOfDesign = titleofinvention;

                if (FileUpload)
                {
                    designAllinformation.NoveltyStatement = designInformation.PriorityDocument;
                }

                if (FileUpload1)
                {
                    designAllinformation.LetterOfAuthorization = designInformation.LetterOfAuthorization;
                }

                if (FileUpload2)
                {
                    designAllinformation.DeedOfAssignment = designInformation.DeedOfAssignment;
                }

                if (FileUpload3)
                {
                    designAllinformation.PriorityDocument = designInformation.PriorityDocument;
                }

                if (FileUpload4)
                {
                    designAllinformation.RepresentationOfDesign1 = designInformation.RepresentationOfDesign1;
                }

                if (FileUpload5)
                {
                    designAllinformation.RepresentationOfDesign2 = designInformation.RepresentationOfDesign2;
                }

                if (FileUpload6)
                {
                    designAllinformation.RepresentationOfDesign3 = designInformation.RepresentationOfDesign3;
                }

                if (FileUpload7)
                {
                    designAllinformation.RepresentationOfDesign4 = designInformation.RepresentationOfDesign4;
                }
                await _designRepository.UpdatedesignInformation(designAllinformation);

                addressOfService.DesignApplicationID = Convert.ToInt32(ApplicationId);
                if (designInformation.LetterOfAuthorization != null)
                {
                    _designRepository.SaveAddressOfService(addressOfService);

                }

                await _designRepository.UpdateDesignAssignment(Convert.ToInt32(ApplicationId), designAssignment);

            }



            // Added A New Country 
            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Create,
                DateCreated = DateTime.Now,
                Description = $"User {user.FirstName + ' ' + user.LastName}  requested to add New Design Application   successfully",
                Entity = "Add New Design",
                UserId = user.Id,
                UserName = user.UserName,
                IpAddress = ip
            });

            return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, ApplicationId2);


        }
    }
}
