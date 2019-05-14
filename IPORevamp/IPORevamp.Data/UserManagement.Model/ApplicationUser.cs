using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using IPORevamp.Core.ViewModels;
using IPORevamp.Data.Entity.Interface;

namespace IPORevamp.Data.UserManagement.Model
{
    public class ApplicationUser : IdentityUser<int>, IAudit
    {
        //public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        //{
        //    //// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
        //    //var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        //    //// Add custom user claims here
        //    //userIdentity.AddClaim(new Claim("FullName", FirstName + " " + MiddleName + " " + LastName));
        //    //return userIdentity;
        //}
        public ApplicationUser()
        {
            
        }
        public ApplicationUser(string username)
        {
            UserName = username;
        }


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
        public string MobileNumber { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public string CreatedBy { get; set; }
        public string DeletedBy { get; set; }
        public string UpdatedBy { get; set; }

        public int CategoryId { get; set; }
        public bool ChangePasswordFirstLogin { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public bool IsTransient()
        {
            return EqualityComparer<int>.Default.Equals(Id, default(int));
        }
    }

    public enum Gender
    {
        Male,
        Female
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

}
