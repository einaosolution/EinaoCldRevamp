using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using IPORevamp.WebAPI.Models;
using IPORevamp.Data.UserManagement.Model;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Filters;
using IPORevamp.WebAPI.Filters;
using EmailEngine.Repository.EmailRepository;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.Event;
using IPORevamp.Repository.Event;

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
        protected readonly IEventRepository _eventRepository;


        public BaseController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IConfiguration configuration,
            IMapper mapper,
            ILogger<BaseController> logger,
            IAuditTrailManager<AuditTrail> auditTrailManager,
            IEventRepository eventRepository

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
            _eventRepository = eventRepository;


        }

        protected async Task<AttendeeViewModel> GenerateAttendeeToken(string email, Attendee attendee)
        {
            var userClaims = await _userManager.GetClaimsAsync(attendee.User);
                     
            var claims = new List<Claim>
            {
                new Claim("EVENTID", attendee.EventId.ToString()),
                new Claim("INVCODE", attendee.InvitationCode),
                new Claim("ATTENDEEID", attendee.Id.ToString()),
                
            }.Union(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtExpire"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            var attendeeVM = _mapper.Map<AttendeeViewModel>(attendee);
            attendeeVM.AccessCode = new JwtSecurityTokenHandler().WriteToken(token);
            return attendeeVM;
        }                

        protected async Task<AuthModel> GenerateJwtToken(string email, ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var loggedinuser = user.FirstName + " " + user.LastName;
            string roles = string.Empty;
            IList<string> role = await _userManager.GetRolesAsync(user);
            //   var organizedEvents = await _eventRepository.FetchOrganizedEvents(user.Id);

            //  if (role.Any())
            // {
            // roles = role.Join();
            // }
          
            //    var claims = new List<Claim>
            //{
            //    new Claim(JwtRegisteredClaimNames.Sub, email),
            //    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
            //    new Claim(ClaimTypes.NameIdentifier, user.UserName),
            //    new Claim(ClaimTypes.Email, user.Email),
            //   new Claim("Category", Convert.ToString(user.CategoryId)),
            //    new Claim(ClaimTypes.Name, user.UserName),
              
            //}.Union(userClaims);

           

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtExpire"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
              //  claims,
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
                Email = user.Email,
                UserId = user.Id,
                category = Convert.ToString(user.CategoryId),
                registrationcomplete = user.CompleteRegistration,
                changepassword = user.ChangePassword,
                loggeduser = loggedinuser 
                
            };
            return auth;
        }
       
        protected IActionResult PrepareResponse(HttpStatusCode statusCode, string message = "", bool error = true, object content = null)
        {
            return StatusCode((int)statusCode, new ApiResponse(message, statusCode, content, error));

        }
    }
}