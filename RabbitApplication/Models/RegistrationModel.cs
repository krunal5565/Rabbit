using System;
using System.ComponentModel.DataAnnotations;

namespace RabbitApplication.Models
{
    public class RegistrationModel 
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }


        public string MobileNumber { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}
