using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using IPORevamp.WebAPI.Models;
using IPORevamp.Data.UserManagement.Model;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.Extensions.Logging;
using IPORevamp.WebAPI.Filters;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;

using System.Data;
using IPORevamp.Data.Entities.Menus;
using System.Reflection;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Linq;

namespace IPORevamp.WebAPI.Controllers
{

    [ValidateModel]
    public class BaseController : ControllerBase
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        protected readonly RoleManager<ApplicationRole> _roleManager;
        protected readonly IConfiguration _configuration;
        protected readonly IMapper _mapper;
        protected readonly ILogger _logger;
        protected readonly IAuditTrailManager<AuditTrail> _auditTrailManager;
        protected readonly ApplicationUser _userInfo;
        


        public BaseController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration,
            IMapper mapper,
            ILogger<BaseController> logger,
            IAuditTrailManager<AuditTrail> auditTrailManager
            

            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
            _roleManager = roleManager;
            _auditTrailManager = auditTrailManager;
            _userInfo = User != null && User.Identity.IsAuthenticated ? _userManager.FindByNameAsync(User?.Identity?.Name).Result : null;
            


        }


        //private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb)
        //{
        //    if (menu.Length > 0)
        //    {
        //        foreach (DataRow dr in menu)
        //        {
        //            string url = dr["Url"].ToString();
        //            string menuText = dr["Name"].ToString();
        //            string icon = dr["Icon"].ToString();

        //            if (url != "#")
        //            {
        //                string line = String.Format(@"<li><a href=""{0}""><i class=""{2}""></i> <span>{1}</span></a></li>", url, menuText, icon);
        //                sb.Append(line);
        //            }

        //            string pid = dr["Id"].ToString();
        //            string parentId = dr["ParentId"].ToString();

        //            DataRow[] subMenu = table.Select(String.Format("ParentId = '{0}'", pid));
        //            if (subMenu.Length > 0 && !pid.Equals(parentId))
        //            {
        //                string line = String.Format(@"<li class=""treeview""><a href=""#""><i class=""{0}""></i> <span>{1}</span><span class=""pull-right-container""><i class=""fa fa-angle-left pull-right""></i></span></a><ul class=""treeview-menu"">", icon, menuText);
        //                var subMenuBuilder = new StringBuilder();
        //                sb.AppendLine(line);
        //                sb.Append(GenerateUL(subMenu, table, subMenuBuilder));
        //                sb.Append("</ul></li>");
        //            }
        //        }
        //    }
        //    return sb.ToString();
        //}


        private string GenerateUL(DataRow[] menu, DataTable table, StringBuilder sb)
        {
            if (menu.Length > 0)
            {
                foreach (DataRow dr in menu)
                {
                    string url = dr["Url"].ToString();
                    string menuText = dr["Name"].ToString();
                    string icon = dr["Icon"].ToString();

                    if (url != "#")
                    {
                        //string line = String.Format(@"<li><a href=""{0}""><i class=""{2}""></i> <span>{1}</span></a></li>", url, menuText, icon);
                        //sb.Append(line);
                        string line = String.Format(@"<li class='sidebar-item'><a href='{0}' class='sidebar-link'><i class='icon-Record'></i><span class='hide-menu'> {1}</span></a></li>", url, menuText);
                       
                        sb.Append(line);
                    }

                    string pid = dr["Id"].ToString();
                    string parentId = dr["ParentId"].ToString();

                    DataRow[] subMenu = table.Select(String.Format("ParentId = '{0}'", pid));
                    if (subMenu.Length > 0 && !pid.Equals(parentId))
                    {
                        string line = String.Format(@" <li class='sidebar-item'><a class='sidebar-link has-arrow waves-effect waves-dark' href='#' aria-expanded='false'><i class='icon-User'></i><span class='hide-menu'>{1} </span></a><ul aria-expanded='false' class='collapse  first-level'>", icon, menuText);

                      //  string line = String.Format(@"<li class=""treeview""><a href=""#""><i class=""{0}""></i> <span>{1}</span><span class=""pull-right-container""><i class=""fa fa-angle-left pull-right""></i></span></a><ul class=""treeview-menu"">", icon, menuText);
                        var subMenuBuilder = new StringBuilder();
                        sb.AppendLine(line);
                        sb.Append(GenerateUL(subMenu, table, subMenuBuilder));
                        sb.Append("</ul></li>");
                    }
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

        protected async Task<AuthModel> GenerateJwtToken(string email, ApplicationUser user, List<MenuManager> menuManager)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

          
            var claims = new List<Claim>
            {
                new Claim("RoleId", user.RolesId.ToString()),
                new Claim("UserId", user.Id.ToString()),
                 new Claim("UserName", user.UserName.ToString())

            }.Union(userClaims);

            var loggedinuser = user.FirstName + " " + user.LastName;
            string roles = string.Empty;
            IList<string> role = await _userManager.GetRolesAsync(user);


            int roleId = Convert.ToInt32(user.RolesId);

            DataSet ds = new DataSet();
            ds = ToDataSet(menuManager);
            DataTable table = ds.Tables[0];
            DataRow[] parentMenus = table.Select("ParentId = 0");

            var sb = new StringBuilder();
            var  menuString = GenerateUL(parentMenus, table, sb);


            


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtExpire"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                 claims,
                  null,
                expires: expires,
                signingCredentials: creds
            );
           
            var auth = new AuthModel
            {
                ExpiryTime = token.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.UserName,
                Role = roles,
                RoleId = user.RolesId != null ? Convert.ToInt32(user.RolesId) :0,
                Email = user.Email,
                UserId = user.Id,
                category = Convert.ToString(user.CategoryId),
                registrationcomplete = user.CompleteRegistration,
                changepassword = user.ChangePassword,
                loggeduser = loggedinuser ,
               DynamicMenu = menuManager,
                menuString = menuString,

            };
            return auth;
        }
       
        protected IActionResult PrepareResponse(HttpStatusCode statusCode, string message = "", bool error = true, object content = null)
        {
            return StatusCode((int)statusCode, new ApiResponse(message, statusCode, content, error));

        }
    }
}