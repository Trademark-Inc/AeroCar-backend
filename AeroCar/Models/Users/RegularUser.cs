using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Users
{
    public class RegularUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Surname { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Phone { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string City { get; set; }

        public UserStatus Status { get; set; }

    }
}
