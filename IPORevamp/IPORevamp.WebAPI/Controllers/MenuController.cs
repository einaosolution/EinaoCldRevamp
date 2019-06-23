
using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IPORevamp.Data.UserManagement.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using EmailEngine.Base.Repository.EmailRepository;
using IPORevamp.Data.Entities.Email;
using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;

using IPORevamp.Data.Entity.Interface;
using Microsoft.AspNetCore.Hosting;
using IPORevamp.Repository.Menu;
using IPORevamp.Core.Utilities;
using IPORevamp.Data.Entity.Interface.Entities.Menus;
using IPORevamp.Data.Entities.Menus;
using IPORevamp.WebAPI.Models;
using Newtonsoft.Json;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/Menu")]
    [ApiController]

    public class MenuController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMenuRepository _menuRepository;







        public MenuController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,

            IMenuRepository MenuRepository,
           
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
            _menuRepository = MenuRepository;


        }



        //[Authorize]
        /// <summary>
        /// This method will get Menu and related States
        /// </summary>
        /// <param name="MenuId"></param>
        /// <param name="IncludeStates"></param>
        /// <param name="RequestById"></param>
        /// <returns></returns>
       
        [HttpPost("GetMenuById/{MenuId}")]
        public async Task<IActionResult> GetSingleMenuById(int MenuId,string RequestById)
        {
            try
            {


                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                var Menu = await _menuRepository.GetMenuById(MenuId);

            if (Menu != null)
            {

                    user = await _userManager.FindByIdAsync(RequestById.ToString()); 

                    if (user == null)
                    {
                        return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                    }

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Menu 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {Menu.Name} name  successfully",
                        Entity = "GetMenuById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Menu Returned Successfully", false, Menu);
               
            }
            else
            {
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }

        }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Menu", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }

     



        [HttpPost("GetMenuByName/{MenuName}")]
        public async Task<IActionResult> GetSingleMenuByName(string  MenuName, bool IncludeStates, string RequestById, bool IncludeLGAs)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(RequestById.ToString()); ;

                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                
                var Menu = await _menuRepository.GetMenuByName(MenuName);

                if (Menu != null)
                {

                    // get User Information
                  user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Menu 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for {Menu.Name} name  successfully",
                        Entity = "GetMenuById",
                        UserId = user.Id,
                        UserName = user.UserName,
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Menu Returned Successfully", false, Menu);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Menu", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }



        [HttpGet("GetAllParentMenus")]
        public async Task<IActionResult> GetAllParentSideMenuByParentid([FromQuery]string RequestById, [FromQuery] String  PatentId)

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


                var Menu = await _menuRepository.GetAllParentChildMenu(Convert.ToInt32(PatentId));

                if (Menu != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Menu 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all parent menu  successfully",
                        Entity = "GetAllParentMenus",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Menu Returned Successfully", false, Menu);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Menu", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpPost("GetAllParentMenus")]
        public async Task<IActionResult> GetAllParentMenu(string RequestById)
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


                var Menu = await _menuRepository.GetAllParentMenu();

                if (Menu != null)
                {

                    // get User Information
                    user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Menu 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all parent menu  successfully",
                        Entity = "GetAllParentMenus",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Menu Returned Successfully", false, Menu);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Menu", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound);
            }
        }


        [HttpGet("GetAllMenus")]
        public async Task<IActionResult> GetAllMenu([FromQuery] string RequestById)
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


                var Menu = await _menuRepository.GetMenus();
               

                if (Menu != null)
                {
                    

                    // get User Information
                     user = await _userManager.FindByIdAsync(RequestById.ToString());


                    // Added A New Menu 
                    await _auditTrailManager.AddAuditTrail(new AuditTrail
                    {
                        ActionTaken = AuditAction.Create,
                        DateCreated = DateTime.Now,
                        Description = $"User {user.FirstName + ' ' + user.LastName}  requested for all menu  successfully",
                        Entity = "GelAllMenu",
                        UserId = user.Id,
                        UserName = user.UserName,
                        IpAddress = ip 
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Menu Returned Successfully", false, Menu);

                }
                else
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Select Menu", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.RecordNotFound );
            }
        }



        // Save Menu

        [HttpPost("SaveMenu")]
        public async Task<IActionResult> SaveMenu(MenuViewModel menuViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(menuViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Menu Exist 
                var checkCount = await _menuRepository.CheckExistingMenu(menuViewModel.Name);

                if (checkCount != null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to save
                MenuManager content = new MenuManager();
                content.Name = menuViewModel.Name;
                content.ParentId = menuViewModel.ParentId;
                content.Url = menuViewModel.Url;
                content.Icon = menuViewModel.Icon;
                content.DateCreated = DateTime.Now;
                content.CreatedBy = menuViewModel.CreatedBy.ToString();
                content.IsActive = true;
                content.IsDeleted = false;


                var save = await _menuRepository.SaveMenu(content);
                string json = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(menuViewModel.CreatedBy.ToString());


                // Added A New Menu 
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} add a new {content.Name}  Menu  successfully",
                    Entity = "MenuAdded",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordAfter = json
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.SaveRequest, false, content);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Save Menu", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }


        [HttpPost("UpdateMenu")]
        public async Task<IActionResult> UpdateMenu(MenuViewModel MenuViewModel)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(MenuViewModel.CreatedBy.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Menu Exist 

                var record = await _menuRepository.GetMenuById(MenuViewModel.MenuId);
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to update the Menu


                record.Name = MenuViewModel.Name;
                record.ParentId = MenuViewModel.ParentId;
                record.Url = MenuViewModel.Url;
                record.Icon = MenuViewModel.Icon;
                record.DateCreated = DateTime.Now;
                record.CreatedBy = MenuViewModel.CreatedBy.ToString();              
                record.LastUpdateDate = DateTime.Now;
                record.UpdatedBy = MenuViewModel.CreatedBy.ToString();
                record.IsActive = true;
                record.Id = MenuViewModel.MenuId;
                
                 var save = await _menuRepository.UpdateMenu(record);
                string json2 = JsonConvert.SerializeObject(save, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(MenuViewModel.CreatedBy.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} update Menu {record}  Menu  successfully",
                    Entity = "MenuUpdate",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip ,
                    RecordBefore = json ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.UpdateRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Update Menu", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailSaveRequest);
            }
        }



        [HttpGet("DeleteMenu")]
        public async Task<IActionResult> DeleteMenu([FromQuery]String  MenuId, [FromQuery] String  UserId)
        {
            try
            {
                string ip = "";

                ip = Request.Headers["ip"];

                var user = await _userManager.FindByIdAsync(UserId.ToString()); ;
                if (user == null)
                {
                    return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.MissingUserInformation, true, null); ;
                }

                // Check if Menu Exist 
                var record = await _menuRepository.GetMenuById(Convert.ToInt32(MenuId));
                string json = JsonConvert.SerializeObject(record, Newtonsoft.Json.Formatting.Indented);

                if (record == null)
                {
                    return PrepareResponse(HttpStatusCode.Conflict, WebApiMessage.RecordNotFound, false, null);
                }

                // attempt to delete the Menu
   
                record.IsDeleted = true;
                record.DeletedBy = UserId.ToString();
                record.LastUpdateDate = DateTime.Now;
                record.Id = Convert.ToInt32(MenuId);


                var delete = await _menuRepository.DeleteMenu(record);
                string json2 = JsonConvert.SerializeObject(delete, Newtonsoft.Json.Formatting.Indented);

                // get User Information
                user = await _userManager.FindByIdAsync(UserId.ToString());


                // log action
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Create,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.FirstName + ' ' + user.LastName} deleted Menu {record}  Menu  successfully",
                    Entity = "MenuDelete",
                    UserId = user.Id,
                    UserName = user.UserName,
                    IpAddress = ip,
                    RecordBefore = json ,
                    RecordAfter = json2
                });

                return PrepareResponse(HttpStatusCode.OK, WebApiMessage.DeleteRequest, false, record);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Delete Menu", "");
                return PrepareResponse(HttpStatusCode.BadRequest, WebApiMessage.FailDeletedRequest);
            }
        }
    }
}