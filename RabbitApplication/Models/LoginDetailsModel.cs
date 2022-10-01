using System;
using System.ComponentModel.DataAnnotations;

namespace RabbitApplication.Models
{
    public class LoginDetailsModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
