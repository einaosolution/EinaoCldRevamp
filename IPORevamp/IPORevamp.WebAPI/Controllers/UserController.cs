using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IPORevamp.WebAPI.Models;
using IPORevamp.Data.UserManagement.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EmailEngine.Base.Repository.EmailRepository;
using IPORevamp.Data.Entities.Email;
using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;

using IPORevamp.Data;
using IPORevamp.Data.TempModel;
using IPORevamp.Repository.Interface;

using IPORevamp.Data.Entity.Interface;
using IPORevamp.Data.Entities.Setting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Authorization;

using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Repository.Setting;
using IPORevamp.Repository.UserProfiling;
using IPORevamp.Repository.Email;
using IPORevamp.Repository.Menu;
using IPORevamp.Data.Entities.Menus;
using IPORevamp.Data.Entity.Interface.Entities.Role;
using Newtonsoft.Json;
using IPORevamp.Data.Entity.Interface.Entities.Sms;
using IPORevamp.Data.Entity.Interface.Entities.Setting;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/UserManagement")]
    [ApiController]
    public class UserController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

        private readonly ISettingRepository  _settings;
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IHostingEnvironment _hostingEnvironment;
        private IFileHandler _fileUploadRespository;
        private IPOContext _dbcontext;
        private IUserProfilingRepository _userProfilingRepository;
        private readonly IPORevamp.Repository.Department.IDepartmentRepository _departmentRepository;
        private readonly IEmailTemplateRepository _EmailTemplateRepository;
        private readonly IMenuRepository _menuRepository;

        public UserController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,
            ISettingRepository settingrepository,
            IUserProfilingRepository userProfilingRepository,
             IPORevamp.Repository.Department.IDepartmentRepository departmentRepository,
            IEmailSender emailsender,
            IPOContext dbcontext,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment ,
            IEmailTemplateRepository EmailTemplateRepository,
        IFileHandler fileUploadRespository,
            IAuditTrailManager<AuditTrail> auditTrailManager,
             IMenuRepository menuRepository


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
            _settings = settingrepository;
            _emailsender = emailsender;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _fileUploadRespository = fileUploadRespository;
            _userProfilingRepository = userProfilingRepository;
            _EmailTemplateRepository = EmailTemplateRepository;
            _dbcontext = dbcontext;
            _menuRepository = menuRepository;
            _departmentRepository = departmentRepository;
        }

        // <summary>
        /// Retrieve the employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired Employee</param>
        /// <returns>A string status</returns>
        /// 

        [HttpGet("GetUserFromEncryptEmail")]
        public async Task<IActionResult> GetUserFromEncryptEmail([FromQuery] string EmailAddress)
        {
            string convertString = IPORevamp.Core.Utilities.Utilities.Decrypt(EmailAddress);

            var user = _userManager.Users.FirstOrDefault(x => x.Email == convertString);

         
            return Ok(user);


        }

        [HttpGet("TestSms")]
        public async Task<IActionResult> TestSms()
          {
            var Registrar = "Anthony Dolo";
            var firstname = "Fred";
            var lastname = "agbaje";
            var Department = "Trademark";

            var template = $"Hello {Registrar},{Environment.NewLine} A request to create admin user has been initiated.  Find below the login details {Environment.NewLine} Firstname :{firstname} {Environment.NewLine} Lastname :{lastname} {Environment.NewLine} Department :{Department} {Environment.NewLine} Kindly login to the portal to approve or reject this request.";
          var status = IPORevamp.Core.Utilities.Utilities.SMSServicesmsprovider("bolajiworld@gmail.com", "password", template, "IpoNigeria", "07059394683");

   


            return Ok();
        }

        [HttpGet("GetUserFromEmail")]
        public async Task<IActionResult> GetUserFromEmail([FromQuery] string EmailAddress)
        {
          

            var user = _userManager.Users.FirstOrDefault(x => x.Email == EmailAddress);

           

            return Ok(user);


        }


        [HttpGet("GetUserFromDepartment")]
        public async Task<IActionResult> GetUserFromDepartment()
        {


            var user = _userManager.Users.Where(x => x.IsDeleted != true && x.RolesId == Convert.ToInt32(IPORoles.Patent_Examiner)).ToList(); 

   

            return Ok(user);


        }

        [HttpGet("GetUserFromDesignDepartment")]
        public async Task<IActionResult> GetUserFromDesignDepartment()
        {


            var user = _userManager.Users.Where(x => x.IsDeleted != true && x.RolesId == Convert.ToInt32(IPORoles.Design_Examiner)).ToList();



            return Ok(user);


        }


        [HttpGet("GetUserFromDesignAppealDepartment")]
        public async Task<IActionResult> GetUserFromDesignAppealDepartment()
        {


            var user = _userManager.Users.Where(x => x.IsDeleted != true && x.RolesId == Convert.ToInt32(IPORoles.Appeal_Officer_Design)).ToList();



            return Ok(user);


        }


        [HttpGet("GetUserFromId")]
        public async Task<IActionResult> GetUserFromId([FromQuery] string Id)
        {


            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(Id));



            return Ok(user);


        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {


            var user = _userManager.Users.Where(x => x.IsDeleted != true).ToList();

            return Ok(user);


        }


        [HttpGet("GetTrademarkUser")]
        public List<UserView> GetTrademarkUser()
        {
            DateTime now = DateTime.Today;

            var user  = _userProfilingRepository.GetUserListings(now.ToString("MMM"), now.ToString("yyyy"), DEPARTMENT.Trademark); ;
            

            return user;


        }

        [HttpGet("GetPatentUser")]
        public async Task<IActionResult> GetPatentUser()
        {
            DateTime now = DateTime.Today;

            var user = _userProfilingRepository.GetUserListings(now.ToString("MMM"), now.ToString("yyyy"), DEPARTMENT.Patent); ;

            return Ok(user);


        }


        [HttpGet("GetDesignUser")]
        public async Task<IActionResult> GetDesignUser()
        {
            DateTime now = DateTime.Today;

            var user = _userProfilingRepository.GetUserListings(now.ToString("MMM"), now.ToString("yyyy"), DEPARTMENT.Design); ;

            return Ok(user);


        }


        [HttpGet("MigrateAgentUser")]
        public async Task<IActionResult> MigrateAgentUser()
        {
          

            var userlist = _userProfilingRepository.MigrateAgentUser();
           
            foreach(var users in userlist)
            {
                var user = new ApplicationUser
                {
                    UserName = users.Email,
                    Email = users.Email,
                    FirstName = users.Firstname,
                    LastName = users.Surname,
                    MobileNumber = users.Phonenumber,
                    Street = users.Address,
                    RolesId = Convert.ToInt32(IPORoles.CorporateAgent_Trade_Mark),
                    migratedagentcode = users.migratedagentcode,
                    migrateduserid = Convert.ToString(users.migrateduserid)

                };

                var userCreated = await _userManager.CreateAsync(user, "password100");

                List<IdentityError> errors = userCreated.Errors.ToList();

                foreach(var verror in errors)
                {
                    var description = verror.Description;
                }


            }



            return Ok(userlist);


        }


        [HttpGet("MigrateTrademarkUser")]
        public async Task<IActionResult> MigrateTrademarkUser()
        {


            var userlist = _userProfilingRepository.MigrateTrademarkUser();

            

            foreach (var users in userlist)
            {
                int  assignedrole = 0;

 

                switch (users.roles)
                {
                    case "1":
                        assignedrole=Convert.ToInt32(IPORoles.Administrator);
                        break;

                  

                    case "3":

                        assignedrole = Convert.ToInt32(IPORoles.Search_Officer_Trade_Mark);
                        break;

                    case "4":
                        assignedrole = Convert.ToInt32(IPORoles.Search_Officer_Trade_Mark);
                        break;

                    case "5":
                        assignedrole = Convert.ToInt32(IPORoles.Examination_Officer_Trade_Mark);
                        break;

                    case "6":
                        assignedrole = Convert.ToInt32(IPORoles.Certificate_Officer_Trade_Mark);
                        break;

                    case "7":
                        assignedrole = Convert.ToInt32(IPORoles.Publication_Officer_Trade_Mark);
                        break;

                    case "8":
                        assignedrole = Convert.ToInt32(IPORoles.Opposition_Officer_Trade_Mark);
                        break;

                    case "9":
                        assignedrole = Convert.ToInt32(IPORoles.Certificate_Officer_Trade_Mark);
                        break;

                    case "10":

                        assignedrole = Convert.ToInt32(IPORoles.RegistrarTrademark);
                        break;

                  
                    case "12":
                        assignedrole = Convert.ToInt32(IPORoles.Search_Officer_Trade_Mark);
                        break;

                    case "13":
                        assignedrole = Convert.ToInt32(IPORoles.Recordals_Officers_Trade_Mark);
                        break;

                    case "14":
                        assignedrole = Convert.ToInt32(IPORoles.Administrator);
                        break;

                    case "15":
                        assignedrole = Convert.ToInt32(IPORoles.Appeal_Officer_Trade_Mark);
                        break;
                }

                var user = new ApplicationUser
                {
                    UserName = users.Email,
                    Email = users.Email,
                    FirstName = users.Firstname,
                    LastName = users.Surname,
                    MobileNumber = users.Phonenumber,
                    Street = users.Address,
                    RolesId = assignedrole,
                    migratedagentcode = users.migratedagentcode,
                    migrateduserid = Convert.ToString(users.migrateduserid) ,
                    department = DEPARTMENT.Trademark

                };

                var userCreated = await _userManager.CreateAsync(user, "password100");

                List<IdentityError> errors = userCreated.Errors.ToList();

                foreach (var verror in errors)
                {
                    var description = verror.Description;
                }


            }



            return Ok(userlist);


        }


        [HttpGet("MigratePatentUser")]
        public async Task<IActionResult> MigratePatentUser()
        {


            var userlist = _userProfilingRepository.MigratePatentUser();



            foreach (var users in userlist)
            {
                int assignedrole = 0;



                switch (users.roles)
                {
                    case "1":
                        assignedrole = Convert.ToInt32(IPORoles.Administrator);
                        break;

                    case "2":

                        assignedrole = Convert.ToInt32(IPORoles.Search_Officer_Patent);
                        break;

                    case "3":

                        assignedrole = Convert.ToInt32(IPORoles.Search_Officer_Patent);
                        break;



                    case "4":
                        assignedrole = Convert.ToInt32(IPORoles.Patent_Examiner);
                        break;

                    case "8":
                        assignedrole = Convert.ToInt32(IPORoles.Patent_Examiner);
                        break;

                    case "5":
                        assignedrole = Convert.ToInt32(IPORoles.Certificate_Officer_Patent);
                        break;

                    case "6":
                        assignedrole = Convert.ToInt32(IPORoles.RegistrarPatent);
                        break;

                 

                  


                   
                  
                }

                var user = new ApplicationUser
                {
                    UserName = users.Email,
                    Email = users.Email,
                    FirstName = users.Firstname,
                    LastName = users.Surname,
                    MobileNumber = users.Phonenumber,
                    Street = users.Address,
                    RolesId = assignedrole,
                    migratedagentcode = users.migratedagentcode,
                    migrateduserid = Convert.ToString(users.migrateduserid),
                    department = DEPARTMENT.Trademark

                };

                var userCreated = await _userManager.CreateAsync(user, "password100");

                List<IdentityError> errors = userCreated.Errors.ToList();

                foreach (var verror in errors)
                {
                    var description = verror.Description;
                }


            }



            return Ok(userlist);


        }

        [HttpGet("MigrateDesignUser")]
        public async Task<IActionResult> MigrateDesignUser()
        {


            var userlist = _userProfilingRepository.MigrateDesignUser();



            foreach (var users in userlist)
            {
                int assignedrole = 0;



                switch (users.roles)
                {
                    case "1":
                        assignedrole = Convert.ToInt32(IPORoles.Administrator);
                        break;

                    case "2":

                        assignedrole = Convert.ToInt32(IPORoles.Search_Officer_Design);
                        break;

                    case "3":

                        assignedrole = Convert.ToInt32(IPORoles.Search_Officer_Design);
                        break;



                    case "5":
                        assignedrole = Convert.ToInt32(IPORoles.Design_Examiner);
                        break;

                    case "6":
                        assignedrole = Convert.ToInt32(IPORoles.Design_Examiner);
                        break;

                  


                    case "10":
                        assignedrole = Convert.ToInt32(IPORoles.RegistrarDesign);
                        break;








                }

                var user = new ApplicationUser
                {
                    UserName = users.Email,
                    Email = users.Email,
                    FirstName = users.Firstname,
                    LastName = users.Surname,
                    MobileNumber = users.Phonenumber,
                    Street = users.Address,
                    RolesId = assignedrole,
                    migratedagentcode = users.migratedagentcode,
                    migrateduserid = Convert.ToString(users.migrateduserid),
                    department = DEPARTMENT.Trademark

                };

                var userCreated = await _userManager.CreateAsync(user, "password100");

                List<IdentityError> errors = userCreated.Errors.ToList();

                foreach (var verror in errors)
                {
                    var description = verror.Description;
                }


            }



            return Ok(userlist);


        }




        [HttpGet("GetAllTempUser")]
        public async Task<IActionResult> GetAllTempUser()
        {
        var tempuser =    _userProfilingRepository.GetAll();

          

            return Ok(tempuser);


        }



        [HttpGet("GetAllTempCount")]
        public async Task<IActionResult> GetAllTempCount([FromQuery] string Userid)
        { 


            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(Userid));

            var tempuser = _userProfilingRepository.GetAllCount(user.department);



            return Ok(tempuser);


        }

        [HttpGet("GetAllTempUser2")]
        public async Task<IActionResult> GetAllTempUser2([FromQuery] string EmailAddress)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Email == EmailAddress);

            var tempuser = _userProfilingRepository.GetAll2(user.department);



            return Ok(tempuser);


        }



        [HttpPost("AssignUser")]
       
        public async Task<IActionResult> AssignUser([FromForm] string UserId,
            [FromForm] string RoleId , [FromForm] string RequestedBy)
        {
            var user = await _userManager.FindByIdAsync(UserId); ;
            string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);
            string ip = "";

            ip = Request.Headers["ip"];
            var user2 = await _userManager.FindByIdAsync(RequestedBy); ;
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.OK,"User Does not  Exist", true, null); ;
            }

            user.RolesId = Convert.ToInt32(RoleId);
          
            await _userManager.UpdateAsync(user);
            var user3 = await _userManager.FindByIdAsync(UserId); ;

            string json2 = JsonConvert.SerializeObject(user3, Newtonsoft.Json.Formatting.Indented);

            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Create,
                DateCreated = DateTime.Now,
                Description = $"Assign Role   for  {user.FirstName + ' ' + user.LastName} by  { user2.FirstName + " " + user2.LastName} ",
                Entity = "User",
                UserId = 0,
                UserName = user.Email,
                IpAddress=ip,
                RecordBefore=json,
                RecordAfter = json2
            });

            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);

        }


        [HttpPost("UpdateUser")]

        public async Task<IActionResult> UpdateUser([FromForm] string UserId,
      [FromForm] string RoleId, [FromForm] string RequestedBy , [FromForm] string Firstname, [FromForm] string Lastname, [FromForm] string PhoneNumber, [FromForm] string Occupation , [FromForm] string StaffId , [FromForm] string Gender 
            , [FromForm] string  Street , [FromForm] string City, [FromForm] string Postal , [FromForm] string  Country, [FromForm] string State , [FromForm] string Ministry , [FromForm] string Department , [FromForm] string Unit)
        {
            var user = await _userManager.FindByIdAsync(UserId); ;
            string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);
            var user2 = await _userManager.FindByIdAsync(RequestedBy); ;
            string ip = "";

            ip = Request.Headers["ip"];
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.OK, "User Does not  Exist", true, null); ;
            }
           
            user.RolesId = Convert.ToInt32(RoleId);
            if (Convert.ToInt32(RoleId) == Convert.ToInt32(IPORoles.Individual))
            {
                user.CategoryId = Convert.ToInt32(IPOCategory.Individual);
            }

            if (Convert.ToInt32(RoleId) == Convert.ToInt32(IPORoles.CorporateAgent_Trade_Mark))
            {
                user.CategoryId = Convert.ToInt32(IPOCategory.Agent); ;
            }



            user.FirstName = Firstname;
            user.LastName = Lastname;
            user.PhoneNumber = PhoneNumber;
            user.PostalCode = Postal;
            user.Occupation = Occupation;
            user.ministry = Ministry;
            user.department = Department;
            user.unit = Unit;
            user.State = State;
            user.staffid = StaffId;
            user.Street = Street;
            user.City = City;
            user.CountryCode = Country;
            user.Gender = Gender == "0" ? Data.UserManagement.Model.Gender.Male : Data.UserManagement.Model.Gender.Female ;

            await _userManager.UpdateAsync(user);
            var user3 = await _userManager.FindByIdAsync(UserId); ;
            string json2 = JsonConvert.SerializeObject(user3, Newtonsoft.Json.Formatting.Indented);

            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Update,
                DateCreated = DateTime.Now,
                Description = $"Update    for  {user.FirstName + ' ' + user.LastName} by  { user2.FirstName + " " + user2.LastName} ",
                Entity = "User",
                UserId = 0,
                UserName = user.Email,
                IpAddress = ip,
                RecordBefore =json,
                RecordAfter =json2
            });

            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);

        }


        [HttpGet("Logout")]

        public async Task<IActionResult> Logout(  [FromQuery] string RequestedBy)
        {
            string ip = "";

            ip = Request.Headers["ip"];

            var user = await _userManager.FindByIdAsync(RequestedBy); ;


            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Create,
                DateCreated = DateTime.Now,
                Description = $"Logged Out  User   {user.FirstName + ' ' + user.LastName}  ",
                Entity = "User",
                UserId = 0,
                UserName = user.Email,
                IpAddress = ip,
                
            });

            return PrepareResponse(HttpStatusCode.OK, "LogOut  Successful", false);


        }


        [HttpGet("TrademarkUserRoleCount")]
         public   UserRoleCount  TrademarkUserRoleCount( )
        {
            string ip = "";

            ip = Request.Headers["ip"];

          
           

            UserRoleCount userrole = new UserRoleCount();
            userrole.AppealOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Appeal_Officer_Trade_Mark), DEPARTMENT.Trademark);
            userrole.CertificateOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Certificate_Officer_Trade_Mark), DEPARTMENT.Trademark);
            userrole.ExaminerOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Examination_Officer_Trade_Mark), DEPARTMENT.Trademark);
            userrole.PublicationOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Publication_Officer_Trade_Mark), DEPARTMENT.Trademark);
            userrole.SearchOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Search_Officer_Trade_Mark), DEPARTMENT.Trademark);




    

            return userrole;

        }


        [HttpGet("PatentUserRoleCount")]
        public UserRoleCount PatentUserRoleCount()
        {
            string ip = "";

            ip = Request.Headers["ip"];




            UserRoleCount userrole = new UserRoleCount();
            userrole.AppealOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Appeal_Officer_Patent), DEPARTMENT.Patent);
            userrole.CertificateOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Certificate_Officer_Patent), DEPARTMENT.Patent);
            userrole.ExaminerOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Patent_Examiner), DEPARTMENT.Patent);
            userrole.PublicationOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Publication_Officer_Patent), DEPARTMENT.Patent);
            userrole.SearchOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Search_Officer_Patent), DEPARTMENT.Patent);






            return userrole;

        }


        [HttpGet("DesignUserRoleCount")]
        public UserRoleCount DesignUserRoleCount()
        {
            string ip = "";

            ip = Request.Headers["ip"];




            UserRoleCount userrole = new UserRoleCount();
            userrole.AppealOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Appeal_Officer_Design), DEPARTMENT.Design);
            userrole.CertificateOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Certificate_Officer_Design), DEPARTMENT.Design);
            userrole.ExaminerOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Design_Examiner), DEPARTMENT.Design);
            userrole.PublicationOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Publication_Officer_Design), DEPARTMENT.Design);
            userrole.SearchOfficerCount = this.getuserrolecount(Convert.ToInt32(IPORoles.Search_Officer_Design), DEPARTMENT.Design);






            return userrole;

        }

        [HttpGet("DeleteUser")]

        public async Task<IActionResult> DeleteUser([FromQuery] string UserId,
            [FromQuery] string RequestedBy)
        {
            string ip = "";

            ip = Request.Headers["ip"];

            var user = await _userManager.FindByIdAsync(UserId); ;
            string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);
            var user2 = await _userManager.FindByIdAsync(RequestedBy); ;
            var ExistingAccount = await _userProfilingRepository.ValidateVerificationEmail(user.Email);
            if (user == null)
            {
                return PrepareResponse(HttpStatusCode.OK, "User Does not  Exist", true, null); ;
            }

            user.IsDeleted = true;

            await _userManager.UpdateAsync(user);

            var user3 = await _userManager.FindByIdAsync(UserId); ;

            string json2 = JsonConvert.SerializeObject(user3, Newtonsoft.Json.Formatting.Indented);

            if (ExistingAccount != null)
            {
                ExistingAccount.IsDeleted = true;
                ExistingAccount.IsActive = false;
                _userProfilingRepository.SavingUserProfile(ExistingAccount);
            }

            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Create,
                DateCreated = DateTime.Now,
                Description = $"Delete User   {user.FirstName + ' ' + user.LastName} by  { user2.FirstName + " " + user2.LastName} ",
                Entity = "User",
                UserId = 0,
                UserName = user.Email,
                IpAddress= ip,
                RecordBefore = json,
                RecordAfter = json2
            });

            return PrepareResponse(HttpStatusCode.OK, "Delete  Successful", false);

        }


        // This method is used for verification of account updating aspnetuser table
        [HttpPost("UpdateUserInfo")]
        [Consumes("multipart/form-data")]
      
        public async Task<IActionResult> UpdateUserInfo([FromForm] string FirstName ,
            [FromForm] string LastName, [FromForm] string Email, [FromForm] string Gender, 
            [FromForm] string DateofBirth, [FromForm] string Identification, [FromForm] string MobileNumber,
            [FromForm] string Street, [FromForm] string City, [FromForm] string State, [FromForm] string PostCode,
            [FromForm] string Country, [FromForm] string CompanyRegistration, [FromForm] string companytelephone, [FromForm] string companyemail, [FromForm] string companywebsite, [FromForm] string meanofidentification_value, [FromForm] string lgaid)
        {
            string ip = "";

            ip = Request.Headers["ip"];


            var user = _userManager.Users.FirstOrDefault(x => x.Email.ToLower() == Email.ToLower());
            string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);

            if (user == null)
            {

                return PrepareResponse(HttpStatusCode.Found, "Member record don't exist, please try again", false);

            }

            // file upload
            string msg = "";
            try
            {
                String[] oneMegaByte = _configuration["_oneMegaByte"].Split('*');
                String[]  fileMaxSize = _configuration["_fileMaxSize"].Split('*');
                int result1 = Convert.ToInt32(oneMegaByte[0]);
                int result2 = Convert.ToInt32(fileMaxSize[0]);

                msg = await _fileUploadRespository.UploadFile(Request.Form.Files[0], _configuration["MemberPassportFolder"], _configuration["AllExtensionsImage"], result1,
                  result2);

            }

            catch (Exception ee)
            {
                var kk = ee.Message;
            }

            string[] result = new string[] { };
            try
            {
                result = msg.Split("|");

            }

            catch(Exception ee)
            {

            }

            // check status before processing request 

            if (result[0] == "FAIL")
            {
                return PrepareResponse(HttpStatusCode.BadRequest, " Passport Upload Fail", false);
            }

            else
            {
                if (user != null)
                {
                    user.FirstName = FirstName;
                    user.LastName = LastName;
                    user.Gender = Gender == "Male" ? Data.UserManagement.Model.Gender.Male : Data.UserManagement.Model.Gender.Female;
                    user.DateOfBirth = Convert.ToDateTime(DateofBirth);
                    user.Bio = Identification;
                    user.MobileNumber = MobileNumber;
                    user.City = City;
                    user.State = State;
                    user.PostalCode = PostCode;
                    user.CountryCode = Country;
                    user.Street = Street;
                    if (user.CategoryId ==1)
                    {
                        user.RolesId = Convert.ToInt32(IPORoles.Individual);
                    }

                    else
                    {
                        user.RolesId = Convert.ToInt32(IPORoles.CorporateAgent_Trade_Mark); ;
                    }
                  //  user.ProfilePicLoc = result[1].ToString();
                    user.ProfilePicLoc = msg;

                    user.CompleteRegistration = true;
                    user.Rcno = CompanyRegistration;
                    user.PhoneNumber = companytelephone;
                    user.MeansOfIdentification_value = meanofidentification_value;
                    user.Lga_Id = lgaid;


                    user.Website = companywebsite;

                    await _userManager.UpdateAsync(user);

                    var user3 = _userManager.Users.FirstOrDefault(x => x.Email.ToLower() == Email.ToLower());
                    string json2 = JsonConvert.SerializeObject(user3, Newtonsoft.Json.Formatting.Indented);
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Update,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.UserName} has been Updated  successfully",
                        Entity = "User",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip,
                        RecordBefore = json ,
                        RecordAfter = json2
                    });

                }

            }
            
            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);
        }


        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel   model)
        {
            //   _userManager.ChangePasswordAsync()

            string ip = "";

            ip = Request.Headers["ip"];


            if (ModelState.IsValid)
            {
                var user = _userManager.Users.FirstOrDefault(x => x.UserName == model.Email);
              
          var kk =     await  _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
         
                if (kk.Errors.Count() > 0)
                {
                    var code = kk.Errors.ToArray()[0].Code;
                    if (code == "PasswordMismatch")
                    {
                        return PrepareResponse(HttpStatusCode.BadRequest, "The old password supplied is wrong", false);
                    }
                    return PrepareResponse(HttpStatusCode.BadRequest, code, false);
                }
                var username = user.FirstName + ' ' + user.LastName;
                user.ChangePassword = true;
                user.ChangePasswordFirstLogin = true;
                 user.LastPasswordChangDate = DateTime.Now;
                
                await _userManager.UpdateAsync(user);
                try
                {
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Update,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.UserName} has  Changed password   successfully",
                        Entity = "User",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress=ip
                    });

                }
                catch(Exception ee )
                {

                }

             





                return PrepareResponse(HttpStatusCode.OK, "Password Changed", false);
            }

            return PrepareResponse(HttpStatusCode.NotAcceptable, "Incomplete Parameters", true);

        }


        [HttpPost("EmailVerification")]
        public async Task<IActionResult> EmailVerification(EmailVerificationView model)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];



                var ExistingAccount = await _userProfilingRepository.ValidateVerificationEmail(model.Email);

                if (ExistingAccount != null)
                {
                    return PrepareResponse(HttpStatusCode.Found, "Email already exist", false);

                }
                
                EmailTemplate emailTemplate;

                if (model.Category == IPOCONSTANT.Individual_Account)
                {
                    emailTemplate =  await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.Individual_Account_Verification);

                }
                else
                {
                    emailTemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.Corporate_Account_Verification);
                }

                EmailLog emaillog = new EmailLog();
                emaillog.MailBody = emailTemplate.EmailBody;
                emaillog.Status = IPOEmailStatus.Fresh;
                emaillog.Subject = emailTemplate.EmailSubject;
                emaillog.DateCreated = DateTime.Now;
                emaillog.Receiver = model.Email;
                emaillog.Sender = emailTemplate.EmailSender;
                emaillog.SendImmediately = true;


                // Get the number of hours in min for the link to expire 
                var getSettingList = await _settings.GetSettingByCode(IPOCONSTANT.ACTIVATIONCODE);


                // add the mins to the current datetime, which is study in hours
                DateTime expiringDate = DateTime.Now.AddMinutes(Convert.ToInt32(getSettingList.FirstOrDefault().ItemValue));

                // log the user registration detials before verification 
                UserVerificationTemp userVerification = new UserVerificationTemp();
                userVerification.First_Name = model.First_Name;
                userVerification.Last_Name = model.Last_Name;
                userVerification.Email = model.Email;
                userVerification.CategoryId = model.Category;
                userVerification.expired = false;
                userVerification.ExpiringDate = expiringDate;
                userVerification.DateCreated = DateTime.Now;
                userVerification.IsActive = true;
                

                 await _userProfilingRepository.SavingUserProfile(userVerification);

                string json = JsonConvert.SerializeObject(userVerification, Newtonsoft.Json.Formatting.Indented);

                string mailContent = emailTemplate.EmailBody;
                mailContent = mailContent.Replace("#name", model.First_Name + ' ' + model.Last_Name);
                mailContent = mailContent.Replace("#Duration", expiringDate.ToString());
                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                // Encryt the email address or id based on what you need for verification 
                // usi
                mailContent = mailContent.Replace("#Link", _configuration["ACTIVIATIONURL"] + "?code=" +
                                                            IPORevamp.Core.Utilities.Utilities.Encrypt(model.Email));

               

                //Email the verification to the registered email address 
                await _emailsender.SendEmailAsync(model.Email, emailTemplate.EmailSubject, mailContent);

                // Log the activities 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"Activiation Code  for  {model.First_Name + ' ' + model.Last_Name} was sent to {model.Email} ",
                    Entity = "User",
                    UserId = 0,
                    UserName = model.Email,
                    IpAddress = ip,
                    RecordAfter = json
                });

                return PrepareResponse(HttpStatusCode.OK, "Email Verification Created", false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred", null);
                return PrepareResponse(HttpStatusCode.PreconditionFailed, "Email Verification Failure", false);
            }

        }


        //This method will verificate the email account 
        [HttpGet("Confirmation")]
        public async Task<IActionResult> Confirmation(string code)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            var corporatelink = _configuration["CORPORATEREDIRECTURL"] + code;
            var individuallink = _configuration["INDIVIDUALREDIRECTURL"] + code;

            if (code == null)
            {
                var error = "";
                 return PrepareResponse(HttpStatusCode.BadRequest, "not found", true, error);
            }
            else
            {
                // try to decrypt the code 

            string convertString = IPORevamp.Core.Utilities.Utilities.Decrypt(code);

                // confirm if the email address exist and the date as not expired

                UserVerificationTemp model = await _userProfilingRepository.EmailConfirmation(convertString);

                var expiredate = model.ExpiringDate;
                if (DateTime.Now > expiredate)
                {
                    return PrepareResponse(HttpStatusCode.NotFound, "This Link has expired", true, null);
                }


                if (model == null)
                {

                    return PrepareResponse(HttpStatusCode.NotFound, "The confirmation was not successful or record not existing", true, null);

                }
                else
                {

                    var userExisting = _userManager.FindByNameAsync(model.Email);

                    if (userExisting.IsCompletedSuccessfully)
                    {
                        return PrepareResponse(HttpStatusCode.Found, "Account Currently Exist", false);
                    }

                }
                // check if any user exist to avoid duplicate 


                if (model != null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        FirstName = model.First_Name,
                        LastName = model.Last_Name,
                        CategoryId = model.CategoryId,
                        CreatedBy = model.First_Name + " " + model.Last_Name,
                        DateCreated = DateTime.Now,
                        EmailConfirmed = true,
                        IsActive =true,
                        NormalizedEmail = model.Email,
                        NormalizedUserName = model.Email,
                        ChangePasswordFirstLogin = false,
                        ChangePassword = false
                    };

                    // generate random password 
                   string password = IPORevamp.Core.Utilities.Utilities.GenerateRandomPassword();
                   

                    var userCreated = await _userManager.CreateAsync(user, password);

                    if (userCreated.Succeeded)
                    {
                        var emailtemplate = _emailManager.GetEmailTemplate(IPOEmailTemplateType.AccountCreation).FirstOrDefault(x => x.IsActive);
                        string  emailBody = emailtemplate.EmailBody.Replace("#Username", model.Email);
                         emailBody = emailBody.Replace("#Password", password);
                         emailBody = emailBody.Replace("#Name", model.First_Name + ' '  + model.Last_Name);
                         emailBody = emailBody.Replace("#path", _configuration["LOGOURL"]);
                        
                        await _emailManager.LogEmail(new EmailLog
                        {
                            CreatedBy = user.UserName,
                            MailBody = emailBody,
                            Receiver = user.Email,
                            Sender = emailtemplate.EmailSender,
                            Subject = emailtemplate.EmailSubject,
                            DateCreated = DateTime.Now,
                            DateToSend = DateTime.Now,
                            Status = IPOEmailStatus.Fresh,
                            SendImmediately = true
                        });
                        await _emailsender.SendEmailAsync(model.Email, emailtemplate.EmailSubject, emailBody);


                        // Update the user temp table

                        model.IsActive = true;
                        model.ConfirmationDate = DateTime.Now;
                        await _userProfilingRepository.SavingUserProfile(model);
                        
                        await _auditTrailManager.AddAuditTrail(new AuditTrail
                        {
                            ActionTaken = AuditAction.Create,
                            DateCreated = DateTime.Now,
                            Description = $" Email verfication for  {model.Email}  was successful",
                            Entity = "User",
                            UserId = user.Id,
                            UserName = user.UserName,
                            IpAddress= ip
                        });


                      //  await _userManager.AddToRoleAsync(user, "USERS");
                      //  return PrepareResponse(HttpStatusCode.OK, "Account has been created successfully", false);

                        if (model.CategoryId ==1)
                        {
                            return Redirect(individuallink);
                        }

                        else
                        {
                            return Redirect(corporatelink);
                        }
                    }

                    else
                    {
                        return PrepareResponse(HttpStatusCode.NotFound, userCreated.Errors.FirstOrDefault().Description, true, null);

                    }
                }
                else
                {
                    return PrepareResponse(HttpStatusCode.NotFound, "The confirmation was not successful or record not existing 1", true, null);

                }





            }



        }


        /// <summary>
        ///  Reset Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpGet("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] string code, [FromQuery] string UserId, [FromQuery] string NewPassord, [FromQuery] string ConfirmPassword)
        {
            string ip = "";

            ip = Request.Headers["ip"];

            if (code == null  || UserId == null)
            {
                var error = "";
                return PrepareResponse(HttpStatusCode.BadRequest, "This request was terminated", true, error);
            }
            else
            {
                var emailTemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.CHANGE_PASSWORD_FIRST_LOGIN_NOTIFICATION);

                if(emailTemplate == null)
                {
                    return PrepareResponse(HttpStatusCode.PreconditionFailed, "Email Templates have not been setup", true, null);
                }


                // try to decrypt the request which happen to be the userId 

                string Id = IPORevamp.Core.Utilities.Utilities.Decrypt(UserId);

                var model =  await _userManager.FindByIdAsync(Id);

                if (model == null)
                {
                    return PrepareResponse(HttpStatusCode.NotFound, "User information not found", true, null);
                }
                // invalid account or account is deleted
                else if(model.IsActive == false || model.IsDeleted==true)
           
                {
                    return PrepareResponse(HttpStatusCode.NotFound, "User information not found", true, null);
                }
                // check both password if equal 
                else if (NewPassord != ConfirmPassword)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, "New Password and Confirm Pssword didn't match your request", true, null);
                }




                if (model != null)
                {

                    // generate random password 
                    try
                    {
                        var passwordResetCode = await _userManager.GeneratePasswordResetTokenAsync(model);
                        
                      //   var resetPassword = await _userManager.ResetPasswordAsync(model, code, NewPassord);
                        var resetPassword = await _userManager.ResetPasswordAsync(model, passwordResetCode, NewPassord);

                        

                        if (resetPassword.Succeeded)
                        {


                            string emailBody = emailTemplate.EmailBody.Replace("#Username", model.Email);
                            emailBody = emailBody.Replace("#Password", NewPassord);
                            emailBody = emailBody.Replace("#Name", model.FirstName + ' ' + model.LastName);
                            emailBody = emailBody.Replace("#path", _configuration["LOGOURL"]);

                            await _emailManager.LogEmail(new EmailLog
                            {
                                CreatedBy = model.UserName,
                                MailBody = emailBody,
                                Receiver = model.Email,
                                Sender = emailTemplate.EmailSender,
                                Subject = emailTemplate.EmailSubject,
                                DateCreated = DateTime.Now,
                                DateToSend = DateTime.Now,
                                Status = IPOEmailStatus.Fresh,
                                SendImmediately = true
                            });
                            await _emailsender.SendEmailAsync(model.Email, emailTemplate.EmailSubject, emailBody);


                            // Update the user temp table


                            await _auditTrailManager.AddAuditTrail(new AuditTrail
                            {
                                ActionTaken = AuditAction.Create,
                                DateCreated = DateTime.Now,
                                Description = $"Password Change  for  {model.Email}  was successful",
                                Entity = "User/ResetPassword",
                                UserId = model.Id,
                                UserName = model.UserName,
                                IpAddress= ip
                            });

                            return PrepareResponse(HttpStatusCode.OK, "Password Reset Successful.", true, null);

                        }

                        else
                        {
                            return PrepareResponse(HttpStatusCode.NotFound, "The operation was not successful reason " + resetPassword.Errors.FirstOrDefault().Description, true, null);

                        }

                    }
                    catch(Exception ee)
                    {
                        var ex = ee.Message;
                        return PrepareResponse(HttpStatusCode.NotFound, "The operation was not successful" + ex, true, null); ;
                    }
                }
                else
                {
                    return PrepareResponse(HttpStatusCode.NotFound, "The operation was not successful", true, null);

                }


                


            }



        }



        [HttpPost("signup")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var emailtemplate = _emailManager.GetEmailTemplate(IPOEmailTemplateType.AccountCreation).FirstOrDefault(x => x.IsActive);
            if (emailtemplate != null)
            {

                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                };

                var userCreated = await _userManager.CreateAsync(user, model.Password);
                if (userCreated.Succeeded)
                {


                    //Send Confirmation Link
                    if (!await _roleManager.RoleExistsAsync("USERS"))
                    {
                        await _roleManager.CreateAsync(new ApplicationRole
                        {
                            Name = "Users",
                            NormalizedName = "USERS"
                        });
                    }

                    
                    var verificationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var verifyURL = _configuration["VERIFYURL"] + "?code=" + Uri.EscapeDataString(verificationCode) + "&userId="+user.Id;
                    var emailBody = emailtemplate.EmailBody.Replace("{VerificationURL}", verifyURL);

                    await _emailManager.LogEmail(new EmailLog
                    {
                       
                        CreatedBy = user.UserName,
                        MailBody = emailBody,
                        Receiver = user.Email,
                        Sender = emailtemplate.EmailSender,
                        Subject = emailtemplate.EmailSubject,
                        DateCreated = DateTime.Now,
                        DateToSend = DateTime.Now,
                        Status = IPOEmailStatus.Fresh,
                        SendImmediately = true
                    });

                    await _auditTrailManager.AddAuditTrail(new AuditTrail {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.UserName} has been created successfully",
                        Entity = "User",
                        UserId = user.Id,
                        UserName = user.UserName,                        
                    });
                    await _userManager.AddToRoleAsync(user, "USERS");
                    return PrepareResponse(HttpStatusCode.OK, "Account has been created successfully, a mail has been sent to your email address to validate your email address", false);
                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, userCreated.Errors.FirstOrDefault().Description, true, userCreated.Errors);
                }

            }
            return PrepareResponse(HttpStatusCode.PreconditionFailed, "Email Templates have not been setup", true, null);
        }


        [HttpGet("ApproveUser")]
        public async Task<IActionResult> ApproveUser([FromQuery] String Email , [FromQuery] String Roleid, [FromQuery] String RequestedBy)
        {

            var user = _userManager.Users.FirstOrDefault(x => x.Id ==Convert.ToInt32(RequestedBy));
            string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);

            string ip = "";

            ip = Request.Headers["ip"];

            EmailTemplate emailtemplate;
            emailtemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.Account_Creation);


            if (user == null)
            {

                return PrepareResponse(HttpStatusCode.Found, "Member record don't exist, please try again", false);

            }

            var ExistingAccount = await _userProfilingRepository.ValidateVerificationEmail(Email);



            var user2 = new ApplicationUser
            {
                UserName = ExistingAccount.Email,
                Email = ExistingAccount.Email,
                FirstName = ExistingAccount.First_Name,
                LastName = ExistingAccount.Last_Name,
                MobileNumber = ExistingAccount.MobileNumber,
                Gender = ExistingAccount.Gender == "Male" ? Gender.Male : Gender.Female,
                CategoryId = Convert.ToInt32(ExistingAccount.Unit),
                City = ExistingAccount.City,
                State = ExistingAccount.State,
                PostalCode = ExistingAccount.Postal,
                CountryCode = ExistingAccount.Country,
                ministry = ExistingAccount.ministry,
                department = ExistingAccount.department,
                staffid = ExistingAccount.staffid ,
                Street = ExistingAccount.Street ,
                unit = ExistingAccount.Unit ,

                RolesId = Convert.ToInt32(Roleid),
                EmailConfirmed = true,
                IsActive = true,
                IsDeleted =false


            };
            string password = IPORevamp.Core.Utilities.Utilities.GenerateRandomPassword();


            var userCreated = await _userManager.CreateAsync(user2, password);
            List<IPORevamp.Data.Entities.Setting.Setting> Setting = null;
            String Department = "";
            if (userCreated.Succeeded)
            {
               
              

               

                ExistingAccount.Status = "Approve";
                _userProfilingRepository.SavingUserProfile(ExistingAccount);



                string mailContent = emailtemplate.EmailBody;
                string fullname = ExistingAccount.First_Name + " " + ExistingAccount.Last_Name;
                mailContent = mailContent.Replace("#Name", fullname);
                mailContent = mailContent.Replace(" #Username", ExistingAccount.Email);
                mailContent = mailContent.Replace("#Password", password);
               
                //  mailContent = mailContent.Replace("#Password", password);
                mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);



                await _emailsender.SendEmailAsync(ExistingAccount.Email, emailtemplate.EmailSubject, mailContent);

                var user3 = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(RequestedBy));
                string json2 = JsonConvert.SerializeObject(user3, Newtonsoft.Json.Formatting.Indented);

                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"Create Admin User    {ExistingAccount.Email} ",
                    Entity = "User",
                    UserId = 0,
                    UserName = ExistingAccount.Email,
                    IpAddress =ip ,
                    RecordBefore = json ,
                    RecordAfter = json2
                });


                return PrepareResponse(HttpStatusCode.OK, "Account has been created successfully, a mail has been sent to your email address to validate your email address", false);
            }

            else
            {
                return PrepareResponse(HttpStatusCode.PreconditionFailed, userCreated.Errors.FirstOrDefault().Description, false);

            }



        }

        public int getuserrolecount(int  roleid ,string department)
        {

            var usercount = _userManager.Users.Where(x => x.RolesId == roleid && x.department == department).Count();
            return usercount;
        }

        [HttpGet("RejectUser")]
        public async Task<IActionResult> RejectUser([FromQuery] String Email, [FromQuery] String RequestedBy)
        {
            string ip = "";

            ip = Request.Headers["ip"];

            var user = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(RequestedBy));
            string json = JsonConvert.SerializeObject(user, Newtonsoft.Json.Formatting.Indented);

            EmailTemplate emailtemplate;
            emailtemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.Account_Creation);


            if (user == null)
            {

                return PrepareResponse(HttpStatusCode.Found, "Member record don't exist, please try again", false);

            }

            var ExistingAccount = await _userProfilingRepository.ValidateVerificationEmail(Email);

            ExistingAccount.Status = "Reject";
            ExistingAccount.IsActive = false;
            ExistingAccount.IsDeleted = true;

            _userProfilingRepository.SavingUserProfile(ExistingAccount);

            var user3 = _userManager.Users.FirstOrDefault(x => x.Id == Convert.ToInt32(RequestedBy));
            string json2 = JsonConvert.SerializeObject(user3, Newtonsoft.Json.Formatting.Indented);


            await _auditTrailManager.AddAuditTrail(new AuditTrail
            {
                ActionTaken = AuditAction.Create,
                DateCreated = DateTime.Now,
                Description = $"Reject Admin User    {ExistingAccount.Email} ",
                Entity = "User",
                UserId = 0,
                UserName = ExistingAccount.Email,
                IpAddress = ip,
                RecordBefore = json,
                RecordAfter = json2
            });



            return PrepareResponse(HttpStatusCode.OK, "Account has been created successfully, a mail has been sent to your email address to validate your email address", false);
          

        }


        [HttpPost("signup2")]
        public async Task<IActionResult> Register2(RegisterViewModel model)
        {
           
            EmailTemplate  emailtemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.Send_Registra_Mail);
            string ip = "";

            ip = Request.Headers["ip"];

            String Department = "";
            var record = await _departmentRepository.GetDepartmentById(Convert.ToInt32(model.department));
            
            if (emailtemplate != null)
            {
                int roleid = 0; ;
                if (model.department == DEPARTMENT.Trademark)
                {
                     roleid = Convert.ToInt32(IPORoles.RegistrarTrademark);
                }

                else if (model.department == DEPARTMENT.Patent)
                {
                    roleid = Convert.ToInt32(IPORoles.RegistrarPatent);
                }

                else if (model.department == DEPARTMENT.Design)
                {
                    roleid = Convert.ToInt32(IPORoles.RegistrarDesign);
                }


                var user = _userManager.Users.Where(x => x.RolesId == roleid && x.department == model.department).ToList();
           
             if (user.Count() ==0 )
                {
                    return PrepareResponse(HttpStatusCode.PreconditionFailed, "No user with registra role found for " + record.Name, true, null);
                }

                //var user = new ApplicationUser
                //{
                //    UserName = model.Username,
                //    Email = model.Email,
                //    FirstName = model.Firstname,
                //    LastName = model.Lastname ,
                //    MobileNumber = model.MobileNumber  ,
                //    Gender = model.Gender ,
                //   CategoryId= Convert.ToInt32(model.Unit) ,
                //   City = model.City ,
                //   State = model.State,
                //   PostalCode = model.Postal,
                //   CountryCode = model.Country,
                //   IsActive = false


                //};
                //string password = IPORevamp.Core.Utilities.Utilities.GenerateRandomPassword();


                //var userCreated = await _userManager.CreateAsync(user, password);
                //  if (userCreated.Succeeded)
                //  {


                UserVerificationTemp userVerification = new UserVerificationTemp();
                    userVerification.First_Name = model.Firstname;
                    userVerification.Last_Name = model.Lastname;
                    userVerification.Email = model.Email;
                    userVerification.CategoryId = Convert.ToInt32(model.Unit);
                    userVerification.City = model.City;
                    userVerification.Country = model.Country;
                    userVerification.State = model.State;
                    userVerification.Street = model.Street;
                    userVerification.Gender =Convert.ToString(model.Gender);
                    userVerification.MobileNumber = model.MobileNumber;
                    userVerification.Unit = model.Unit;
                userVerification.staffid = model.staffid;
                userVerification.ministry = model.ministry;
                userVerification.department = model.department;
                    userVerification.Status = "Pending";
                    userVerification.expired = false;
                    userVerification.ExpiringDate = null;
                  //  userVerification.auth = password;
                    userVerification.DateCreated = DateTime.Now;
                    userVerification.IsActive = true;


                    await _userProfilingRepository.SavingUserProfile(userVerification);

                string json = JsonConvert.SerializeObject(userVerification, Newtonsoft.Json.Formatting.Indented);
                List<IPORevamp.Data.Entities.Setting.Setting> Setting = null;
                List<IPORevamp.Data.Entities.Setting.Setting> Setting2 = null;

               
                if (record != null)
                {
                    Department = record.Name;
                }
              
             


        


                foreach (var users in user)
                {
                    var vname = users.FirstName + " " + users.LastName;

                    string mailContent = emailtemplate.EmailBody;
                    mailContent = mailContent.Replace("#firstname", model.Firstname);
                    mailContent = mailContent.Replace("#lastname", model.Lastname);
                    mailContent = mailContent.Replace("#Email", model.Username);
                    mailContent = mailContent.Replace("#Department", Department);
                   // mailContent = mailContent.Replace("#Registrar", Setting2[0].ItemValue);
                    mailContent = mailContent.Replace("#Registrar", vname);
                    //  mailContent = mailContent.Replace("#Password", password);
                    mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);



                   // await _emailsender.SendEmailAsync(Setting[0].ItemValue, emailtemplate.EmailSubject, mailContent);
                    await _emailsender.SendEmailAsync(users.Email, emailtemplate.EmailSubject, mailContent);

                    try
                    {
                        var template = $"Hello {vname},{Environment.NewLine} A request to create admin user has been initiated.  Find below the login details {Environment.NewLine} Firstname :{model.Firstname} {Environment.NewLine} Lastname :{model.Lastname} {Environment.NewLine} Department :{Department} {Environment.NewLine} Kindly login to the portal to approve or reject this request.";
                        var status = IPORevamp.Core.Utilities.Utilities.SMSServicesmsprovider("bolajiworld@gmail.com", "password", template, "IpoNigeria", users.MobileNumber);

                        _dbcontext.Add(new SmsLog
                        {
                            username = "bolajiworld@gmail.com",
                            message = template,
                            sender = "IpoNigeria",
                            mobilenumber = users.MobileNumber,
                            useremail = users.Email,
                            status = status,
                            IsDeleted = false,
                            IsActive = true,
                            DateCreated = DateTime.Now
                        });

                        _dbcontext.SaveChanges();

                    }

                    catch(Exception ee)
                    {

                    }


                }

                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"Create Admin User    {model.Username} ",
                        Entity = "User",
                        UserId = 0,
                        UserName = model.Username,
                        IpAddress = ip ,
                        RecordBefore = json
                    });

              

                  //  var ExistingAccount = await _userProfilingRepository.ValidateVerificationEmail(model.Email);

                //  if (ExistingAccount != null)
                //  {
                //    return PrepareResponse(HttpStatusCode.Found, "Existing Email Record Found", false);

                //  }
                // await _userManager.AddToRoleAsync(user, "USERS");
                return PrepareResponse(HttpStatusCode.OK, "Account has been created successfully, a mail has been sent to your email address to validate your email address", false);
        

            }
            return PrepareResponse(HttpStatusCode.PreconditionFailed, "Email Templates have not been setup", true, null);
        }


        [HttpPost("resendConfirmMail")]
        public async Task<IActionResult> ResendCOnfirmationMail(ForgotPassswordRequest forgot)
        {
            var emailtemplate = _emailManager.GetEmailTemplate(IPOEmailTemplateType.ResendCOnfirmationLink).FirstOrDefault(x => x.IsActive);
    
            if (emailtemplate != null)
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Email == forgot.Username || x.UserName == forgot.Username);
                if (user != null)
                {
                    if (user.EmailConfirmed)
                    {
                        return PrepareResponse(HttpStatusCode.Conflict, "User has already been confirmed");
                    }

                    var verificationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var verifyURL = _configuration["VERIFYURL"] + "?code=" + Uri.EscapeDataString(verificationCode) + "&userId=" + user.Id;
                    var emailBody = emailtemplate.EmailBody.Replace("{VerificationURL}", verifyURL);

                    await _emailManager.LogEmail(new EmailLog
                    {
                        
                        CreatedBy = user.UserName,
                        MailBody = emailBody,
                        Receiver = user.Email,
                        Sender = emailtemplate.EmailSender,
                        Subject = emailtemplate.EmailSubject,
                        DateCreated = DateTime.Now,
                        DateToSend = DateTime.Now,
                        Status = IPOEmailStatus.Fresh,
                        SendImmediately = true
                    });

                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.UserName} request for confirmation link",
                        Entity = "User",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });
                    return PrepareResponse(HttpStatusCode.OK, "Confirmation link has been sent to your email address", false);

                }

                return PrepareResponse(HttpStatusCode.NotFound, "Specified user not found.");
            }
            return PrepareResponse(HttpStatusCode.PreconditionFailed, "EMail template has not been setup confirmation link reset.");
        }

        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody]ConfirmEmail confirmEmail)
        {           
            var user = await _userManager.FindByIdAsync(confirmEmail.UserId);
            if(user != null)
            {
                var confirmed = await _userManager.ConfirmEmailAsync(user, confirmEmail.Code);
                if (confirmed.Succeeded)
                {
                    return PrepareResponse(HttpStatusCode.OK, "Email confirmation has been completed successfully", false, null);
                }
                else
                {
                    var error = confirmed.Errors;
                    return PrepareResponse(HttpStatusCode.BadRequest, error.FirstOrDefault().Description, true, error);

                }
            }

            return PrepareResponse(HttpStatusCode.NotFound, "User not found", true, null);

        }


        [HttpPost("resetPassword")]
        public async  Task<IActionResult> ResetPassword(PasswordResetModel passwordReset)
        {
            var user = await _userManager.FindByIdAsync(passwordReset.UserId);
            if(user != null)
            {
                var resetPassword = await _userManager.ResetPasswordAsync(user, passwordReset.Code, passwordReset.Password);
                if (resetPassword.Succeeded)
                {
                    return PrepareResponse(HttpStatusCode.OK, "Password reset has been completed successfully", false, null);
                }
                else
                {
                    var errors = resetPassword.Errors;
                    return PrepareResponse(HttpStatusCode.BadGateway, errors.FirstOrDefault().Description, true);
                }
            }

            return PrepareResponse(HttpStatusCode.NotFound, "User not found");
        }
        
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPassswordRequest forgot)
        {
            
            var emailTemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.FORGOT_PASSWORD_EMAIL_TEMPLATE);
            string ip = "";

            ip = Request.Headers["ip"];


            if (emailTemplate != null)
            {
                var user = await _userManager.FindByNameAsync(forgot.Username);
                if(user != null)
                {                    
                    var passwordResetCode = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var codeBlue = IPORevamp.Core.Utilities.Utilities.Encrypt(user.Id.ToString());

                  //  var verifyURL = _configuration["FORGOTPASSWORDURL"] + "?code=" + Uri.EscapeDataString(passwordResetCode) + "&request=" + codeBlue;
                    var verifyURL = _configuration["FORGOTPASSWORDURL"] + "?code=" + passwordResetCode + "&request=" + codeBlue;

                    EmailLog emaillog = new EmailLog();
                    emaillog.MailBody = emailTemplate.EmailBody;
                    emaillog.Status = IPOEmailStatus.Fresh;
                    emaillog.Subject = emailTemplate.EmailSubject;
                    emaillog.DateCreated = DateTime.Now;
                    emaillog.Receiver = user.Email;
                    emaillog.Sender = emailTemplate.EmailSender;
                    emaillog.SendImmediately = true;

                    string mailContent = emailTemplate.EmailBody;
                    mailContent = mailContent.Replace("#Link", verifyURL);
                    mailContent = mailContent.Replace("#Name", user.FirstName + ' ' + user.LastName);
                    mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                    //Email the verification to the registered email address 
                    await _emailsender.SendEmailAsync(user.Email, emailTemplate.EmailSubject, mailContent);

                    // Log the activities 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"Forget Password Code  for  {user.FirstName + ' ' + user.LastName} was sent to {user.Email} ",
                        Entity = "User",
                        UserId = user.Id,
                        UserName = user.Email,
                        IpAddress = ip
                    });


                    return PrepareResponse(HttpStatusCode.OK, "Forgot password link has been sent to your email", false);
                }
                return PrepareResponse(HttpStatusCode.NotFound, "Username does not exist", true, null);
            }
            return PrepareResponse(HttpStatusCode.PreconditionFailed, "Email Templates have not been setup", true, null);
        }


        /// <summary>
        ///  This method is used for change password 
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost("ChangePasswordFirstLogin")]
        public async Task<IActionResult> ChangePasswordFirstLogin(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {

                // check tha
               
                var user = _userManager.Users.FirstOrDefault(x => x.UserName == changePasswordModel.Email.ToLower() 
                || x.Email == changePasswordModel.Email.ToLower());

                if (user != null)
                {
                    
                    // confirm the old password with the current password
                    bool changePass =  await _userManager.CheckPasswordAsync(user, changePasswordModel.CurrentPassword);

                    if (changePass ==false)
                    {
                        return PrepareResponse(HttpStatusCode.BadRequest, "Your existing password didn't match the current password, please try again", true);
                    }

                    else if (changePasswordModel.ConfirmPassword != changePasswordModel.NewPassword)
                    {
                        return PrepareResponse(HttpStatusCode.BadRequest, "New password and confirm password didn't match.", true);
                    }

                    else if (user.EmailConfirmed == false)
                    {
                        return PrepareResponse(HttpStatusCode.Unauthorized, "Your account is not validated yet.", true);
                    }
                    else if (user.ChangePassword == true && user.ChangePasswordFirstLogin == true)
                    {

                        return PrepareResponse(HttpStatusCode.BadRequest, "Invalid Operation.", true);

                    }

                    // update the columns
                    
                    var  result = await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, 
                                                                      changePasswordModel.NewPassword);
                        if (result.Succeeded)
                        {
                        // update the status 
                        user.ChangePassword = true;
                        user.ChangePasswordFirstLogin = true;

                         await _userManager.UpdateAsync(user);

                        // send an email
                        var  emailTemplate = await _EmailTemplateRepository.GetEmailTemplateByCode(IPOCONSTANT.CHANGE_PASSWORD_FIRST_LOGIN_NOTIFICATION);

                        EmailLog emaillog = new EmailLog();
                        emaillog.MailBody = emailTemplate.EmailBody;
                        emaillog.Status = IPOEmailStatus.Fresh;
                        emaillog.Subject = emailTemplate.EmailSubject;
                        emaillog.DateCreated = DateTime.Now;
                        emaillog.Receiver = user.Email;
                        emaillog.Sender = emailTemplate.EmailSender;
                        emaillog.SendImmediately = true;


                        string mailContent = emailTemplate.EmailBody;
                        mailContent = mailContent.Replace("#Username", user.Email);
                        mailContent = mailContent.Replace("#Password", changePasswordModel.NewPassword);
                        mailContent = mailContent.Replace("#Name", user.FirstName + ' ' + user.LastName);
                        mailContent = mailContent.Replace("#path", _configuration["LOGOURL"]);

                        // Encryt the email address or id based on what you need for verification 
                        // usi
                    

                        //Email the verification to the registered email address 
                        await _emailsender.SendEmailAsync(user.Email, emailTemplate.EmailSubject, mailContent);



                        // Log the activities 
                        await _auditTrailManager.AddAuditTrail(new AuditTrail
                        {
                            ActionTaken = AuditAction.Update,
                            DateCreated = DateTime.Now,
                            Description = $"Change First Login Password for   {user.FirstName + ' ' + user.LastName} was successful",
                            Entity = "User",
                            UserId = user.Id,
                            UserName = user.Email,
                        });


                        return PrepareResponse(HttpStatusCode.OK, "User first logon password was changed successfully", false, user);
                       }

                }
                else
                    {
                    return PrepareResponse(HttpStatusCode.NotAcceptable, "Incomplete Parameters", true);
                }
                }
            else
            {
                
                return PrepareResponse(HttpStatusCode.NotAcceptable, "Incomplete Parameters", true);
            }

                  return PrepareResponse(HttpStatusCode.NotFound, "User not found");
        }



        [AllowAnonymous]
        [HttpPost("Authenticate")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Authenticate(LoginViewModel loginModel)
        {
            string ip = "";

            ip = Request.Headers["ip"];
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userManager.Users.FirstOrDefault(x => x.UserName == loginModel.Username || x.Email == loginModel.Username);
                  if ( user.LastPasswordChangDate != null ) {
                 if ( Convert.ToDateTime(user.LastPasswordChangDate).AddDays(60)  < DateTime.Now )
                        {
                            user.ChangePassword = false;
                            await _userManager.UpdateAsync(user);


                        }
                    }

                    if (user != null)
                    {
                        if (user.IsActive == false || user.IsDeleted == true)
                        {
                            return PrepareResponse(HttpStatusCode.Unauthorized, "Your account is not Active.", true);
                        }

                        if (user.EmailConfirmed == false)
                        {
                            return PrepareResponse(HttpStatusCode.Unauthorized, "Your account is not validated yet.", true);
                        }
                        else if (user.EmailConfirmed == true && user.ChangePasswordFirstLogin == false)
                        {



                        }


                        if (user.EmailConfirmed)
                        {
                            var signIn = await _signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, loginModel.RememberMe, false);
                            if (signIn.Succeeded)
                            {


                                var   menus = _menuRepository.GetLinkRolesMenus().Where(s => s.RolesId == user.RolesId).Select(s => s.Menus).Distinct().ToList();                            //Generate Token                       


                                var userProfile = await GenerateJwtToken(user.Email, user, menus, user.LastPasswordChangDate);
                                Response.Cookies.Append("access_token", userProfile.Token, new CookieOptions
                                {
                                    Path = "/",
                                    HttpOnly = false,
                                    IsEssential = true,
                                    Expires = DateTime.Now.AddHours(3)
                                });

                                await _auditTrailManager.AddAuditTrail(new AuditTrail
                                {
                                    ActionTaken = AuditAction.Update,
                                    DateCreated = DateTime.Now,
                                    Description = $"Login  for   {user.FirstName + ' ' + user.LastName} was successful",
                                    Entity = "User",
                                    UserId = user.Id,
                                    UserName = user.Email,
                                    IpAddress = ip

                                });
                                return PrepareResponse(HttpStatusCode.OK, "Authentication Successful", false, userProfile);
                            }
                            else if (signIn.IsLockedOut)
                            {
                                return PrepareResponse(HttpStatusCode.Unauthorized, "User has been locked out", true);
                            }

               
                        }
                        else
                        {
                            return PrepareResponse(HttpStatusCode.Unauthorized, "Please verify your email");
                        }
                    }

                    return PrepareResponse(HttpStatusCode.Unauthorized, "Invalid username or password", true);
                }
                catch (Exception ex)
                {
                    string f = ex.Message;
                    return PrepareResponse(HttpStatusCode.NotAcceptable, "Incomplete Parameters", true);
                }
            }

            return PrepareResponse(HttpStatusCode.NotAcceptable, "Incomplete Parameters", true);
        }

    }
}