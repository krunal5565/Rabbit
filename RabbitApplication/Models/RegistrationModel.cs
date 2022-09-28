using System;

namespace RabbitApplication.Models
{
    public class RegistrationModel 
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
