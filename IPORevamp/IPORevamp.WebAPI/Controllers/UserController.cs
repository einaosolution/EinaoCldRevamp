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
using IPORevamp.Data.Entities.Setting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http.Headers;

namespace IPORevamp.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : BaseController
    {

        private readonly IEmailManager<EmailLog, EmailTemplate> _emailManager;

        private readonly ISettingRepository  _settings;
        private readonly IEmailSender _emailsender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IHostingEnvironment _hostingEnvironment;




        public UserController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager, 
            IConfiguration configuration, 
            IMapper mapper, ILogger<UserController> logger,
            IEmailManager<EmailLog, EmailTemplate> emailManager,
            ISettingRepository  settingrepository,
            IEmailSender emailsender,
            IHttpContextAccessor httpContextAccessor,
            IHostingEnvironment hostingEnvironment ,

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
            _settings = settingrepository;
            _emailsender = emailsender;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;


        }

        // <summary>
        /// Retrieve the employee by their ID.
        /// </summary>
        /// <param name="id">The ID of the desired Employee</param>
        /// <returns>A string status</returns>
        /// 

        [HttpGet("GetUserFromEncryptEmail")]
        public async Task<IActionResult> GetUserFromEncryptEmail( String property1)
        {
            string convertString = IPORevamp.Core.Utilities.Utilities.Decrypt(property1);

            var user = _userManager.Users.FirstOrDefault(x => x.Email == convertString);

            return Ok(user);


        }

        [HttpGet("GetUserFromEmail")]
        public async Task<IActionResult> GetUserFromEmail(String property1)
        {
          

            var user = _userManager.Users.FirstOrDefault(x => x.Email == property1);

            return Ok(user);


        }

        // This method is used for verification of account updating aspnetuser table
        [HttpPost("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromForm] string FirstName , [FromForm] string LastName, [FromForm] string Email, [FromForm] string Gender, [FromForm] string DateofBirth, [FromForm] string Identification, [FromForm] string MobileNumber, [FromForm] string Street, [FromForm] string City, [FromForm] string State, [FromForm] string PostCode, [FromForm] string Country)
        {
            var context = _httpContextAccessor.HttpContext;

            var user = _userManager.Users.FirstOrDefault(x => x.Email == Email );

            var file = Request.Form.Files[0];
            string folderName = "Upload";
            string filename = "";
            string webRootPath = _hostingEnvironment.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);

            if (file.Length > 0)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filename = "Upload/" + fileName;
                string fullPath = Path.Combine(newPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }






            if (user != null)
            {
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.Gender = Gender == "Male" ?  Data.UserManagement.Model.Gender.Male: Data.UserManagement.Model.Gender.Female;
                user.DateOfBirth = Convert.ToDateTime(DateofBirth);
                user.Bio = Identification;
                user.MobileNumber = MobileNumber;
                user.City = City;
                user.State = State;
                user.PostalCode = PostCode;
                user.CountryCode = Country;
                user.ProfilePicLoc = filename;
                user.CompleteRegistration = true;

               await _userManager.UpdateAsync(user);
                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Update,
                    DateCreated = DateTime.Now,
                    Description = $"User {user.UserName} has been Updated  successfully",
                    Entity = "User",
                    UserId = user.Id,
                    UserName = user.UserName,
                });
               
            }



            //  var kk =    context.Items["firstname"].ToString();

            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);
        }

        // This method is used for verification of account 

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel   model)
        {
            //   _userManager.ChangePasswordAsync()

            if (ModelState.IsValid)
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Email == model.Email);
                user.ChangePassword = true;
                _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                _userManager.UpdateAsync(user);

                await _auditTrailManager.AddAuditTrail(new AuditTrail
                {
                    ActionTaken = AuditAction.Update,
                    DateCreated = DateTime.Now,
                    Description = $"Pasword Change For  {user.FirstName + ' ' + user.LastName}  ",
                    Entity = "User",
                    UserId = 0,
                    UserName = model.Email,
                });


                return PrepareResponse(HttpStatusCode.OK, "Password Changed", false);
            }

            return PrepareResponse(HttpStatusCode.NotAcceptable, "Incomplete Parameters", true);

        }
        [HttpPost("EmailVerification")]
        public async Task<IActionResult> EmailVerification(EmailVerificationView model)
        {
            try
            {
               
                


                var ExistingAccount = await _settings.ValidateVerificationEmail(model.Email);

                if (ExistingAccount != null)
                {
                    return PrepareResponse(HttpStatusCode.Found, "Existing Email Record Found", false);

                }
                
                EmailTemplate emailTemplate;

                if (model.Category == 1)
                {
                    emailTemplate =  await _settings.GetEmailTemplateByCode(IPOCONSTANT.Individual_Account_Verification);

                }
                else
                {
                    emailTemplate = await _settings.GetEmailTemplateByCode(IPOCONSTANT.Corporate_Account_Verification);
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
                var getSettingList = await _settings.GetSettingsByCode(IPOCONSTANT.ACTIVATIONCODE);


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
                

                 await _settings.SaveUserVerification(userVerification);

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

                UserVerificationTemp model = await _settings.EmailConfirmation(convertString);

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
                        CategoryId = model.CategoryId,
                        CreatedBy = model.First_Name + " " + model.Last_Name,
                        DateCreated = DateTime.Now,
                        EmailConfirmed = true,
                        IsActive =true,
                        NormalizedEmail = model.Email,
                        NormalizedUserName = model.Email,
                        ChangePasswordFirstLogin = false
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
                        await _settings.SaveUserVerification(model);
                        
                        await _auditTrailManager.AddAuditTrail(new AuditTrail
                        {
                            ActionTaken = AuditAction.Create,
                            DateCreated = DateTime.Now,
                            Description = $" Email verfication for  {model.Email}  was successful",
                            Entity = "User",
                            UserId = user.Id,
                            UserName = user.UserName,
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
                        return PrepareResponse(HttpStatusCode.NotFound, "The confirmation was not successful or record not existing", true, null);

                    }
                }
                else
                {
                    return PrepareResponse(HttpStatusCode.NotFound, "The confirmation was not successful or record not existing 1", true, null);

                }





            }



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


        /// <summary>
        ///  This method is used for change password 
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost("ChangePasswordFirstLogin")]
        public async Task<IActionResult> ChangePasswordFirstLogin(LoginViewModel loginModel)
        {
            return null;
        
        }


        [HttpPost("Authenticate")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Authenticate(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.FirstOrDefault(x=>x.UserName == loginModel.Username || x.Email == loginModel.Username);
                if(user != null)
                {

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