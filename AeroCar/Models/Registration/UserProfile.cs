using AeroCar.Models.Reservation;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Registration
{
    public class UserProfile
    {
        [Key]
        public long UserProfileId { get; set; }

        [ForeignKey("RegularUser.Id")]
        public string UserId { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }
    }
}
