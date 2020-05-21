using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Registration
{
    public class UserService
    {
        [Key]
        public string UserId { get; set; }

        public UserType Type { get; set; }

        [ForeignKey("UserRegistration.Username")]
        public string Username { get; set; }

        public string Password { get; set; }

        public string Token { get; set; }

        public string AuthId { get; set; }
    }
}
