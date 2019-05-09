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
using IPORevamp.Repository.Event;
using IPORevamp.Data;
using IPORevamp.Data.TempModel;
using IPORevamp.Repository.Interface;
using IPORevamp.Repository.SystemSetup;
using IPORevamp.Data.Entity.Interface;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

        private readonly ISettingRepository  settings;
        private readonly IEmailSender _emailsender;





        public UserController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager, 
            IConfiguration configuration, 
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,
            ISettingRepository  settingrepository,
            IEmailSender emailsender,

            IAuditTrailManager<AuditTrail> auditTrailManager,
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
            settings = settingrepository;
            _emailsender = emailsender;


        }

        // <summary>
        /// Retrieve the employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired Employee</param>
        /// <returns>A string status</returns>
        /// 

      
        [HttpPost("EmailVerification")]
        public async Task<IActionResult> EmailVerification(EmailVerificationView model)
        {
            var emailtemplate = _emailManager.GetEmailTemplate(IPOEmailTemplateType.AccountCreation
               ).FirstOrDefault(x => x.IsActive);
            EmailLog emaillog = new EmailLog();
            emaillog.MailBody = emailtemplate.EmailBody;
            emaillog.Status = IPOEmailStatus.Fresh;
            emaillog.Subject = emailtemplate.EmailSubject;
            emaillog.DateCreated = DateTime.Now;
            emaillog.Receiver = model.Email;
            emaillog.Sender = emailtemplate.EmailSender;
            emaillog.SendImmediately = true;
            





          //  _emailManager.LogEmail(emaillog);

            UserVerificationTemp UserVerification = new UserVerificationTemp();
            UserVerification.First_Name = model.First_Name;
            UserVerification.Last_Name = model.Last_Name;
            UserVerification.Email = model.Email;
            UserVerification.Category = model.Category;
            UserVerification.expired = false;
            UserVerification.DateCreated = DateTime.Now;
            UserVerification.IsActive = true;


            settings.SaveUserVerification(UserVerification);

            _emailsender.SendEmailAsync(model.Email, emailtemplate.EmailSubject, emailtemplate.EmailBody);




            return PrepareResponse(HttpStatusCode.OK, "EmailVerification Created", false);

        }
        [HttpPost("signup")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var emailtemplate = _emailManager.GetEmailTemplate(IPOEmailTemplateType.AccountCreation
                ).FirstOrDefault(x => x.IsActive);
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
                        //BCC = "timileyinayegbayo@gmail.com",
                        //CC= "timileyinayegbayo@gmail.com",
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
            var emailTemplate = _emailManager.GetEmailTemplate(IPOEmailTemplateType.PasswordReset).FirstOrDefault(x=>x.IsActive);
            if (emailTemplate != null)
            {
                var user = await _userManager.FindByNameAsync(forgot.Username);
                if(user != null)
                {                    
                    var passwordResetCode = await _userManager.GeneratePasswordResetTokenAsync(user);                    

                    var verifyURL = _configuration["PASSWORDRESETURL"] + "?code=" + Uri.EscapeDataString(passwordResetCode)+"&userId="+user.Id;
                    var emailBody = emailTemplate.EmailBody.Replace("{PASSWORDRESETURL}", verifyURL);

                    await _emailManager.LogEmail(new EmailLog
                    {
                      
                        CreatedBy = user.UserName,
                        MailBody = emailBody,
                        Receiver = user.Email,
                        Sender = emailTemplate.EmailSender,
                        Subject = emailTemplate.EmailSubject,
                        DateCreated = DateTime.Now,
                        Status = IPOEmailStatus.Fresh,
                        DateToSend = DateTime.Now.Date,
                        SendImmediately = true
                    });

                    return PrepareResponse(HttpStatusCode.OK, "Forgot password link has been sent to your email", false);
                }
                return PrepareResponse(HttpStatusCode.NotFound, "Username does not exist", true, null);
            }
            return PrepareResponse(HttpStatusCode.PreconditionFailed, "Email Templates have not been setup", true, null);
        }                 

        [HttpPost("authenticate")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Authenticate(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.FirstOrDefault(x=>x.UserName == loginModel.Username || x.Email == loginModel.Username);
                if(user != null)
                {
                    if (user.EmailConfirmed)
                    {
                        var signIn = await _signInManager.PasswordSignInAsync(user.UserName, loginModel.Password, loginModel.RememberMe, false);
                        if (signIn.Succeeded)
                        {
                            //Generate Token                       
                            var userProfile = await GenerateJwtToken(user.Email, user);
                            Response.Cookies. Append("access_token", userProfile.Token, new CookieOptions {
                                Path = "/",
                                HttpOnly = false,
                                IsEssential = true,
                                Expires = DateTime.Now.AddHours(3)
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

            return PrepareResponse(HttpStatusCode.NotAcceptable, "Incomplete Parameters", true);
        }

    }
}