using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ipowebportal.Models
{


    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public enum ProfileTitle
    {
        Mr = 1,
        Mrs,
        Dr,
        Prof,
        Engr,
        Barr
    }

    public enum Gender
    {
        Male,
        Female
    }

    public class UserDetails
    {
        public ProfileTitle Title { get; set; }
        public string Bio { get; set; }
        public string Nationality { get; set; }
        public string EmployerName { get; set; }
        public string Occupation { get; set; }
        public string ResidentialAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
        public string ProfilePicLoc { get; set; }
        public string Website { get; set; }
        public string PostalCode { get; set; }
        public string Rcno { get; set; }
        public Boolean ChangePassword { get; set; }
        public Boolean CompleteRegistration { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public IdentificationMeans Identification { get; set; }
        public string MobileNumber { get; set; }
        public string IdentificationNo { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; }
        public string DeletedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int CategoryId { get; set; }
        public bool ChangePasswordFirstLogin { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;        
        public byte[] RowVersion { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string Email { get; set; }
    }

    public enum IdentificationMeans
    {
        DriversLicense = 1
    }

    public class SetFirstPasswordViewModel
    {
        public string code { get; set; }

        [Required]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name ="Firstname")]
        [Required(ErrorMessage = "First name is required")]
        public string First_Name { get; set; }
        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Last name is required")]
        public string Last_Name { get; set; }

        [Display(Name ="Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category")]
        public CategoryEnum Category { get; set; }
    }

    public enum CategoryEnum
    {
        Individual = 1,
        Corporate
    }

    public class HashedPasswordDetails
    {
        public string HashPassword { get; set; }
        public string PasswordSalt { get; set; }
    }
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        public int MemberType { get; set; }

        [Required]
        [Display(Name = "Username")]
       // [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }


    public class LoginViewModelNew
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    //public class RegisterViewModel
    //{
    //    [Required]
    //    [EmailAddress]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }

    //    [Required]
    //    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    //    [DataType(DataType.Password)]
    //    [Display(Name = "Password")]
    //    public string Password { get; set; }

    //    [DataType(DataType.Password)]
    //    [Display(Name = "Confirm password")]
    //    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    //    public string ConfirmPassword { get; set; }
    //}

    public class ResetPasswordViewModel
    {
        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
        public string userCode { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
