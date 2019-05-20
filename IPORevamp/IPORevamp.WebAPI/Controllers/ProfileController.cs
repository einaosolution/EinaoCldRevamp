using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EmailEngine.Base.Entities;
using EmailEngine.Base.Repository.EmailRepository;
using EmailEngine.Repository;
using EmailEngine.Repository.EmailRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using IPORevamp.WebAPI.Models;
using IPORevamp.Data.Entities.AuditTrail;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.UserManagement.Model;
using IPORevamp.Repository.Event;
using EmailEngine.Repository.FileUploadRepository;
using IPORevamp.Repository.UserProfiling;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class ProfileController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly IFileHandler _fileHandler;
        private readonly IUserProfilingRepository _userProfilingRepository;

        public ProfileController(
           UserManager<ApplicationUser> userManager,
           RoleManager<ApplicationRole> roleManager,
           SignInManager<ApplicationUser> signInManager,
           IConfiguration configuration,
           IMapper mapper,
           ILogger<UserController> logger,
           IUserProfilingRepository userProfilingRepository,

           IEmailManager<EmailLog, EmailTemplate> emailManager,
           IAuditTrailManager<AuditTrail> auditTrailManager,
           IFileHandler fileHandler
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
            _fileHandler = fileHandler;
            _userProfilingRepository = userProfilingRepository;
        }

        [AllowAnonymous]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProfileDetails(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var returnedUser = _mapper.Map<UserModel>(user);
                return PrepareResponse(HttpStatusCode.OK, "User found", false, returnedUser);

            }

            return PrepareResponse(HttpStatusCode.NotFound, "User not found", true, null);
        }

        [HttpPut("{userId}/changePassword")]
        public async Task<IActionResult> ChangePassword(int userId, ChangePasswordModel model)
        {
            var username = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);

            if (user != null)
            {
                if (user.Id != userId)
                {
                    return PrepareResponse(HttpStatusCode.Unauthorized, "You are not authorized to complete this process", true, null);
                }
                var passwordChanged = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (passwordChanged.Succeeded)
                {
                    await _auditTrailManager.AddAuditTrail(new AuditTrail {
                        ActionTaken = AuditAction.Update,
                        Description = $"User {user.UserName} updated his password",
                        DateCreated = DateTime.Now,
                        UserId = user.Id,
                        UserName = user.UserName
                    });

                    var userProfile = await GenerateJwtToken(user.Email, user);
                    return PrepareResponse(HttpStatusCode.OK, "Password has been updated successfully", false, userProfile);
                }
                else
                {
                    return PrepareResponse(HttpStatusCode.ExpectationFailed, "Password update could not be completed", true, null);
                }
            }

            return PrepareResponse(HttpStatusCode.NotFound, "User not found", true, null);
        }



    }}