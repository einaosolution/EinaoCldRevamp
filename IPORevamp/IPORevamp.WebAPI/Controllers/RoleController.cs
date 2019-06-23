
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
using IPORevamp.Data.SetupViewModel;
using IPORevamp.Data.Entities.Country;
using IPORevamp.Repository.Country;
using IPORevamp.Core.Utilities;
using IPORevamp.Repository.Role;
using IPORevamp.Data.Entities;
using IPORevamp.Repository.Role;
using IPORevamp.Data.Entity.Interface.Entities.Role;
using System.Data;
using System.Reflection;
using System.Text;
using IPORevamp.Repository.Menu;
using Newtonsoft.Json;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Role")]
    [ApiController]
    public class RoleController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

      
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleRepository _roleRepository;
        private readonly IMenuRepository _menuRepository;





        public RoleController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,
            IMenuRepository menuRepository,
            IRoleRepository roleRepository,
            IEmailSender emailsender,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment,
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
            _roleRepository = roleRepository;
            _menuRepository = menuRepository;


        }



        //[Authorize]
        /// <summary>
        /// This method will get Role and related Roles
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="IncludeRoles"></param>
        /// <param name="RequestById"></param>
        /// <returns></returns>
       
        [HttpGet("GetRoleById")]
        public async Task<IActionResult> GetSingleRoleById([FromQuery]String  RoleId, [FromQuery]String RequestById)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var Role = await _roleRepository.GetRoleManagerById(Convert.ToInt32(RoleId));

            if (Role != null)
            {
                    int vroleid = Convert.ToInt32(RoleId);

                    DataSet ds = new DataSet();
                  //  List<string> menus_id = _menuRepository.GetLinkRolesMenus().Where(s => s.RolesId == vroleid).Select(s => s.MenusId.ToString()).ToList();
                    var menus = _menuRepository.GetLinkRolesMenus().Where(s => s.RolesId == vroleid).Select(s => s.Menus).ToList();
                   // ds = ToDataSet(_menuRepository.GetMenuManagers());
                   // DataTable table = ds.Tables[0];
                   // DataRow[] parentMenus = table.Select("ParentId = 0");

                    var sb = new StringBuilder();
                  //  string unorderedList = GenerateUL(parentMenus, table, sb, menus_id);
                  //  ViewBag.menu = unorderedList;
                   // MenuResult menuResult = new MenuResult();
                   // menuResult.Role = Role;
                   // menuResult.menu = unorderedList;
                   // menuResult.menuid = menus_id;


                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Role 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {Role.Title} name  successfully",
                        Entity = "GetRoleById",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip 
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Role Returned Successfully", false, menus);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Role", "");
                return PrepareResponse(HttpStatusCode.BadRequest,  WebApiMessage.RecordNotFound);
            }
        }



        private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb, List<string> menus_id)
        {
            if (menu.Length > 0)
            {
                foreach (DataRow dr in menu)
                {
                    string id = dr["id"].ToString();
                    string handler = dr["url"].ToString();
                    string menuText = dr["name"].ToString();
                    string icon = dr["icon"].ToString();

                    string pid = dr["id"].ToString();
                    string parentId = dr["ParentId"].ToString();

                    string status = (menus_id.Contains(id)) ? "Checked" : "";

                    DataRow[] subMenu = table.Select(String.Format("ParentId = '{0}'", pid));
                    if (subMenu.Length > 0 && !pid.Equals(parentId))
                    {
                        string line = String.Format(@"<li class=""has""><input type=""checkbox"" name=""subdomain[]"" id=""{5}"" value=""{1}"" {4}><label>> {1}</label>", handler, menuText, icon, "target", status, id);
                        sb.Append(line);

                        var subMenuBuilder = new StringBuilder();
                        sb.AppendLine(String.Format(@"<ul>"));
                        sb.Append(GenerateUL(subMenu, table, subMenuBuilder, menus_id));
                        sb.Append("</ul>");
                    }
                    else
                    {
                        string line = String.Format(@"<li class=""""><input type=""checkbox"" name=""subdomain[]"" id=""{5}"" value=""{1}"" {4}><label>{1}</label>", handler, menuText, icon, "target", status, id);
                        sb.Append(line);
                    }
                    sb.Append("</li>");
                }
            }
            return sb.ToString();
        }


        public DataSet ToDataSet<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            DataSet ds = new DataSet();
            ds.Tables.Add(dataTable);
            return ds;
        }



        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles([FromQuery] string RequestById)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var Role = await _roleRepository.GetAllRoles();
                List<RoleViewModel> RoleViewModel = new List<RoleViewModel>();
                foreach (var role in Role)
                {
                    RoleViewModel roleview = new RoleViewModel();

                    roleview.RoleId = role.Id;
                    roleview.Title = role.Title;
                    roleview.Description = role.Description;
                    RoleViewModel.Add(roleview);




                }

                if (Role != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Role 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Read,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all roles  successfully",
                        Entity = "GetAllRoles",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip 
                    });

                    //   return PrepareResponse(HttpStatusCode.OK, "Role Returned Successfully", false, Role);
                    return PrepareResponse(HttpStatusCode.OK, "Role Returned Successfully", false, RoleViewModel);


                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Role", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }



        // Save Role

        [HttpPost("SaveRole")]
        public async Task<IActionResult> SaveRole(RoleViewModel roleViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(roleViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Role Exist 

                var checkCount = await _roleRepository.CheckExistingRole(roleViewModel.Title);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to save
                RoleManager content = new RoleManager();
                
                content.Title = roleViewModel.Title;          
                content.DateCreated = DateTime.Now;
                content.CreatedBy = roleViewModel.CreatedBy.ToString();
                content.Description = roleViewModel.Description;
                content.IsActive = true;       
                content.IsDeleted = false;

                var save = await _roleRepository.SaveRole(content);
                string json = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(roleViewModel.CreatedBy.ToString());


                // Added A New Role 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Read,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.Title}  Role  successfully",
                    Entity = "RoleAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip,
                    RecordAfter = json
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content.Title);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Role", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateRolePermission")]
        public async Task<IActionResult> UpdateRolePermission([FromBody]  ReceiveRoleArray ReceiveRoles)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(ReceiveRoles.UserId.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Role Exist 

                var record = await _roleRepository.GetRoleManagerById(ReceiveRoles.CurrentRole);
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                //  var msg  = await  _roleRepository.UpdateRole(record.Id, AssignedRoles, CurrentRole);
                var msg = await _roleRepository.UpdateRole(record.Id, ReceiveRoles.AssignedRoles, ReceiveRoles.CurrentRole);

                var record2 = await _roleRepository.GetRoleManagerById(ReceiveRoles.CurrentRole);
                string json2 = JsonConvert.SerializeObject(record2, Newtonsoft.Json.Formatting.Indented);
                // get User Information
                user = await _userManager.FindByIdAsync(ReceiveRoles.UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Update,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update Role {record}  Role  successfully",
                    Entity = "RoleUpdate",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordBefore = json ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.UpdateRequest, false, msg);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Role", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpPost("EditRole")]
        public async Task<IActionResult> EditRole(RoleViewModel roleViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(roleViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Role Exist 

                var record = await _roleRepository.GetRoleManagerById(roleViewModel.RoleId);
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the Role
                record.DateCreated = DateTime.Now;
                record.UpdatedBy = roleViewModel.CreatedBy.ToString();
                record.IsActive = true;
                record.Title = roleViewModel.Title;
                record.Description = roleViewModel.Description;
                record.Id = roleViewModel.RoleId;
               

                var save = await _roleRepository.EditRole(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(roleViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Update,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update Role {record}  Role  successfully",
                    Entity = "RoleUpdate",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip,
                    RecordBefore = json ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.UpdateRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Role", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpGet("DeleteRole")]
        public async Task<IActionResult> DeleteRole([FromQuery] String  RoleId, [FromQuery] String UserId)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                // Check if Role Exist 
                var record = await _roleRepository.GetRoleManagerById(Convert.ToInt32(RoleId));
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the Role
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(RoleId);


                var delete = await _roleRepository.DeleteRole(record);
                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                var user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Delete,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted Role {record}  Role  successfully",
                    Entity = "RoleDelete",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordBefore = json ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.DeleteRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete Role", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}