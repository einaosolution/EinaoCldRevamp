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

using Microsoft.AspNetCore.Authorization;

using EmailEngine.Repository.FileUploadRepository;

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
        private IFileHandler _fileUploadRespository;





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
            _settings = settingrepository;
            _emailsender = emailsender;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _fileUploadRespository = fileUploadRespository;

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

        [HttpGet("GetUserFromEmail")]
        public async Task<IActionResult> GetUserFromEmail([FromQuery] string EmailAddress)
        {
          

            var user = _userManager.Users.FirstOrDefault(x => x.Email == EmailAddress);

            return Ok(user);


        }
       

        // This method is used for verification of account updating aspnetuser table
        [HttpPost("UpdateUserInfo")]
        [Consumes("multipart/form-data")]
      
        public async Task<IActionResult> UpdateUserInfo([FromForm] string FirstName ,
            [FromForm] string LastName, [FromForm] string Email, [FromForm] string Gender, 
            [FromForm] string DateofBirth, [FromForm] string Identification, [FromForm] string MobileNumber,
            [FromForm] string Street, [FromForm] string City, [FromForm] string State, [FromForm] string PostCode,
            [FromForm] string Country, [FromForm] string CompanyRegistration, [FromForm] string companytelephone, [FromForm] string companyemail, [FromForm] string companywebsite)
        {
          

            var user = _userManager.Users.FirstOrDefault(x => x.Email.ToLower() == Email.ToLower());

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
                  result2, _hostingEnvironment);

            }

            catch (Exception ee)
            {
                var kk = ee.Message;
            }


            string [] result = msg.Split("|");

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
                    user.ProfilePicLoc = result[1].ToString();
                    user.CompleteRegistration = true;
                    user.Rcno = CompanyRegistration;
                    user.MobileNumber = companytelephone;
                  
                    user.Website = companywebsite;

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

            }
            
            return PrepareResponse(HttpStatusCode.OK, "Update Successful", false);
        }

        // This method is used for verification of account 
        [HttpPost("UpdateCorporateRecord")]
        public async Task<IActionResult> UpdateCorporateRecord( [FromForm] string CompanyRegistration, [FromForm] string companytelephone, [FromForm] string companyemail, [FromForm] string companywebsite, [FromForm] string FirstName, [FromForm] string LastName, [FromForm] string Email, [FromForm] string Gender, [FromForm] string DateofBirth, [FromForm] string Identification, [FromForm] string MobileNumber, [FromForm] string Street, [FromForm] string City, [FromForm] string State, [FromForm] string PostCode, [FromForm] string Country)
        {
            var context = _httpContextAccessor.HttpContext;

            var user = _userManager.Users.FirstOrDefault(x => x.Email == Email);

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
                user.Rcno = CompanyRegistration;
                user.MobileNumber = companytelephone;
                user.Email = companyemail;
                user.Website = companywebsite;
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
        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel   model)
        {
            //   _userManager.ChangePasswordAsync()

           

            if (ModelState.IsValid)
            {
                var user = _userManager.Users.FirstOrDefault(x => x.UserName == model.Email);
                var username = user.FirstName + ' ' + user.LastName;
                user.ChangePassword = true;
                await _userManager.UpdateAsync(user);
                await  _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
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


        /// <summary>
        ///  Reset Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpGet("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] string code, [FromQuery] string UserId, [FromQuery] string NewPassord, [FromQuery] string ConfirmPassword)
        {
            

            if (code == null  || UserId == null)
            {
                var error = "";
                return PrepareResponse(HttpStatusCode.BadRequest, "This request was terminated", true, error);
            }
            else
            {
                var emailTemplate = await _settings.GetEmailTemplateByCode(IPOCONSTANT.CHANGE_PASSWORD_FIRST_LOGIN_NOTIFICATION);

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
                            });

                            return PrepareResponse(HttpStatusCode.OK, "Password Reset Successful.", true, null);

                        }

                        else
                        {
                            return PrepareResponse(HttpStatusCode.NotFound, "The operation was not successful", true, null);

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
            
            var emailTemplate = await _settings.GetEmailTemplateByCode(IPOCONSTANT.FORGOT_PASSWORD_EMAIL_TEMPLATE);



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
                        var  emailTemplate = await _settings.GetEmailTemplateByCode(IPOCONSTANT.CHANGE_PASSWORD_FIRST_LOGIN_NOTIFICATION);

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