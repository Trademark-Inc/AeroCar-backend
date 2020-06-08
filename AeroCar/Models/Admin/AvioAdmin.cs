using AeroCar.Models.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Admin
{
    public class AvioAdmin
    {
        [Key]
        [ForeignKey("RegularUser.Id")]
        public string UserId { get; set; }

        [ForeignKey("AvioCompany.AvioCompanyId")]
        public long AvioCompanyId { get; set; }

        public bool SetUpPassword { get; set; }
    }
}
