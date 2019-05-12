using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IPORevamp.Data.UserManagement.Model;

namespace IPORevamp.WebAPI.Models
{
    public class LoginViewModel
    {

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Please provide current password")]
        public string CurrentPassword{get;set;}
        [Required(ErrorMessage = "Please provide you new password")]
        [DataType(DataType.Password,ErrorMessage = "Please provide a valid password")]
        public string NewPassword { get; set; }
        [Compare("NewPassword",ErrorMessage = "New password and confirm password do not match")]
        public string ConfirmPassword { get; set; }
    }

    public class AuthModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public DateTime ExpiryTime { get; set; }
    }

    public class VerifyEmailViewModel
    {
        public string UserId { get; set; }
        public string Code { get; set; }

        public VerifyEmailViewModel(string code, string userId)
        {
            UserId = userId;
            Code = code;
        }
    }

    public class ForgotPassswordRequest
    {
        [Required(ErrorMessage = "Please provide username")]
        public string Username { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username field is required")]
        [Display(Name = "Username")]
        public string Username{ get; set; }

        [Required(ErrorMessage = "Email address field is required")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Please provide a valid Email address")]
        [Display(Name = "Email")]
        public string Email{ get; set; }

        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }

        [Required]
        [Compare("Password",ErrorMessage = "Confirm password not the same as password")]        
        public string ConfirmPassword{ get; set; }

        public bool UpdateIPORevampProfile { get; set; }        
    }

    public class ConfirmEmail
    {
        [Required(ErrorMessage = "Please provide User's Id")]
        [Display(Name = "UserId")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Please supply verification code")]
        [Display(Name = "Code")]
        public string Code { get; set; }
    }


    public class EmailVerificationView
    {
        [Required(ErrorMessage = "Please provide Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please supply First Name")]
        public string First_Name { get; set; }
        [Required(ErrorMessage = "Please supply Last Name")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Please supply Category")]
        public int Category { get; set; }
    }

    public class PasswordResetModel
    {
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Code { get; set; }      
        [Required]
        public string UserId { get; set; }
    }

    public class UserModel
    {
        public int Id { get; set; }

        //[Required(ErrorMessage ="Please provide your firstname")]
        public string FirstName { get; set; }       
        public string MiddleName { get; set; }
        //[Required(ErrorMessage ="Please provide your surname")]
        public string LastName { get; set; }
        //[Required(ErrorMessage ="Please provide your gender")]
        public Gender Gender { get; set; }
        //[Required(ErrorMessage ="Please provide your date of birth")]
        //[DataType(DataType.Date, ErrorMessage = "Please provide a valid date of birth")]
        public DateTime DoB { get; set; }
        //[Required(ErrorMessage ="Please provide a title")]
        public ProfileTitle Title { get; set; }

        //[StringLength(160,ErrorMessage = "Bio can not be greater than 160 characters")]
        public string Bio { get; set; }
        //[Required(ErrorMessage = "Please provide your nationality")]
        public string Nationality { get; set; }        
        
        public string EmployerName { get; set; }
        
        public string Occupation { get; set; }
        //[Required(ErrorMessage = "Please provide your Residential Address")]
        public string ResidentialAddress { get; set; }
        //[Required(ErrorMessage = "Please provide your phone number")]
        //[DataType(DataType.PhoneNumber,ErrorMessage = "Please provide a valid        Phone number")]
        public string PhoneNumber { get; set; }
        //[Required(ErrorMessage = "Please provide the country code for your phone number")]        
        public string CountryCode { get; set; }
        public string ProfilePicLoc { get; set; }
        //[Required(ErrorMessage = "Please provide you event interests")]
        public string Interests { get; set; }
        public string FaceBook { get; set; }
        public string Twitter { get; set; }
        public string GooglePlus { get; set; }
        public string Instagram { get; set; }
        public string OrganizerDescription { get; set; }
        public string Email { get; set; }
    }


    public enum MaritalStatus
    {
        Single,
        Married,
        Divorced,
    }
}
