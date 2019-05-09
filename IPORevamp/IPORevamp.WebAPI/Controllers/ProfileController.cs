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


namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/profile")]
    [ApiController]
    [Authorize]
    public class ProfileController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;
        private readonly IFileHandler _fileHandler;

        public ProfileController(
           UserManager<ApplicationUser> userManager,
           RoleManager<ApplicationRole> roleManager,
           SignInManager<ApplicationUser> signInManager,
           IConfiguration configuration,
           IMapper mapper,
           ILogger<UserController> logger,
         
           IEmailManager<EmailLog, EmailTemplate> emailManager,
           IAuditTrailManager<AuditTrail> auditTrailManager,
           IFileHandler fileHandler,
           IEventRepository eventRepository
           ) : base(
               userManager,
               signInManager,
               roleManager,
               configuration,
               mapper,
               logger,
               auditTrailManager,
               eventRepository
               )
        {
            _emailManager = emailManager;
            _fileHandler = fileHandler;
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
                if(user.Id != userId)
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

        [Consumes("multipart/form-data")]
        [HttpPut("{userId}/upload-image")]
        public async Task<IActionResult> UploadProfileImage(int userId, IFormFile image)
        {
            var loggedUser = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(loggedUser);
            if(user.Id != userId)
            {
                return PrepareResponse(HttpStatusCode.Forbidden, "You are forbidden to perform this task");
            }

            var uploadedImage = await _fileHandler.UploadFile(image, FileType.PICTURE); 
            if (!string.IsNullOrEmpty(uploadedImage))
            {
                user.ProfilePicLoc = uploadedImage;
                await _userManager.UpdateAsync(user);

                return PrepareResponse(HttpStatusCode.OK, "Profile Image has been uploaded successfully", false);
            }

            return PrepareResponse(HttpStatusCode.InternalServerError, "An error occured while uploading profile image");
        }

        
            


        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateProfileDetails(int userId, UserModel model)
        {
            var username = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(username);
            if(user != null)
            {
                if (user.Id == userId)
                {
                    user.Bio = model.Bio;
                    user.CountryCode = model.CountryCode;
                    user.DoB = model.DoB;
                    user.EmployerName = model.EmployerName;
                    user.FaceBook = model.FaceBook;
                    user.FirstName = model.FirstName;
                    user.Gender = model.Gender;
                    user.GooglePlus = model.GooglePlus;
                    user.Instagram = model.Instagram;
                    user.Interests = model.Interests;
                    user.LastName = model.LastName;
                    user.MiddleName = model.MiddleName;
                    user.MobileNumber = model.PhoneNumber;
                    user.PhoneNumber = model.PhoneNumber;
                    user.ResidentialAddress = model.ResidentialAddress;
                    user.Title = model.Title;
                    user.Twitter = model.Twitter;
                    user.Nationality = model.Nationality;

                    var updated = await _userManager.UpdateAsync(user);
                    if (updated.Succeeded)
                    {
                        return PrepareResponse(HttpStatusCode.OK, "Profile update has been successfully completed", false, user);
                    }

                    return PrepareResponse(HttpStatusCode.ExpectationFailed, updated.Errors.FirstOrDefault().Description, true, null);
                }            

                return PrepareResponse(HttpStatusCode.NotFound, "Specified user not found", true, null);
            }

            return PrepareResponse(HttpStatusCode.Unauthorized, "You are not authorized to make this required", true, null);
        }
    }
}