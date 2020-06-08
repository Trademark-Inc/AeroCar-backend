using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO
{
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RedirectUrl { get; set; }
    }
}
