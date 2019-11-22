using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ipowebportal.Models
{
    public class MessageBody
    {


        [Required(ErrorMessage = "Your Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Your Subject is required")]
        public string Subject { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Your Phone Number is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Your Message required")]

        public string Message { get; set; }

    }
}