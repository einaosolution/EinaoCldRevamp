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

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Trademark")]
    [ApiController]
    public class TrademarkController : BaseController
    {
        private readonly InewApplication _newApplicationRepository;

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
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


        }


        [HttpGet("GetAllTest")]
        public async Task<IActionResult> GetAllTest()
        {
            string ip = "";

            return Ok();


         
        }

        [HttpPost("SavePwallet")]
        public async Task<IActionResult> SavePwallet([FromForm] string userId,
      [FromForm] string Applicationtypeid, [FromForm] string transactionid, [FromForm] string logo_descriptionID, [FromForm] string nation_classID, [FromForm] string tm_typeID, [FromForm] string product_title, [FromForm] string nice_class)
        {
            try
            {

                MarkInfo_View markinfoview = new MarkInfo_View();

                markinfoview.tm_typeID = tm_typeID;
                markinfoview.userid = userId;
                markinfoview.product_title = product_title;
                markinfoview.nice_class = nation_classID;
                markinfoview.logo_descriptionID = logo_descriptionID;
               







                if (Request.Form.Files.Count > 0)
                {
                    foreach (var  file in Request.Form.Files)
                    {
                        
                        var files = new List<string>();
                        if (file.Name == "FileUpload")
                        {

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


                // attempt to save
                Pwallet content = new Pwallet();
                content.Applicationtypeid = Convert.ToInt32(Applicationtypeid);
                content.DateCreated = DateTime.Now;
                content.application_status = "Pending";
                content.data_status = "Search";
                content.userid = userId;
              //  content.transactionid = transactionid;
               
                content.IsActive = true;

                content.IsDeleted = false;


                var save = await _newApplicationRepository.Savepwallet(content);
                string pwalletid = save.Id.ToString();
                markinfoview.pwalletid = save.Id;
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

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

                SaveMarkInfo(markinfoview);
               

                // get User Information
                //  user = await _userManager.FindByIdAsync(SectorViewModel.CreatedBy.ToString());


                // Added A New Sector 
              

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, pwalletid);
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
                int id = Convert.ToInt32(pwalletid);

                var vpwallet = (from c in _contex.Pwallet where c.Id == id  select c).FirstOrDefault();

               

                if (vpwallet != null)
                {
                    vpwallet.transactionid = transid;
                    vpwallet.application_status = "Fresh";

                    _contex.SaveChanges();

                    // get User Information




                }

                return Ok();
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select State", "");
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
               

                var vpwallet = (from c in _contex.Pwallet where c.Id == id select c).FirstOrDefault();
                int userid = Convert.ToInt32(vpwallet.userid);
               var vmarkinfo  = (from c in _contex.Mark_Info where c.pwalletid == vpwallet.Id select c).FirstOrDefault();
                var vAppUser = (from c in _contex.ApplicationUsers where c.Id == userid select c).FirstOrDefault();

                TrademarkView tm = new TrademarkView();
                tm.applicationUser = vAppUser;
                tm.markinfo = vmarkinfo;
                tm.pwallet = vpwallet;





                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, tm);

              

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select State", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }

        public  void UpdateMarkInfo(int id )
        {
            var vmark_info = (from c in _contex.Mark_Info where c.Id ==id select c).FirstOrDefault();
            
            if (vmark_info.tm_typeID == "Local")
            {
                vmark_info.reg_number = "NG/TM/O/" + DateTime.Today.Date.ToString("yyyy") + "/" +id;
            }

            else
            {
                vmark_info.reg_number = "F/TM/O/" + DateTime.Today.Date.ToString("yyyy") + "/" + id;
            }

            _contex.SaveChanges();
        }

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
                Mark_Info content = new Mark_Info();
                content.auth_doc = markInfo_View.auth_doc;
              

                content.DateCreated = DateTime.Now;
                content.logo_descriptionID = markInfo_View.logo_descriptionID;
                content.logo_pic = markInfo_View.logo_pic;
                content.nation_classID = markInfo_View.nice_class;
                content.nice_class = markInfo_View.nice_class;
                content.product_title = markInfo_View.product_title;
                content.reg_number = markInfo_View.reg_number;
                content.pwalletid = markInfo_View.pwalletid;
                content.sup_doc1 = markInfo_View.sup_doc1;
                content.sup_doc2 = markInfo_View.sup_doc2;
                content.auth_doc = markInfo_View.auth_doc;
                content.tm_typeID = markInfo_View.tm_typeID;
                content.userid = markInfo_View.userid;
               
             

                content.IsActive = true;

                content.IsDeleted = false;
                string json2 = JsonConvert.SerializeObject(markInfo_View, Newtonsoft.Json.Formatting.Indented);

                _contex.Mark_Info.Add(content);

                _contex.AuditTrails.Add(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new  Mark   successfully",
                    Entity = "MarkAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip,
                    RecordAfter = json2
                }
                );
                _contex.SaveChanges();


               //   var save = await _newApplicationRepository.SaveMarkInfo(content);
            

              this.UpdateMarkInfo(content.Id);









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
