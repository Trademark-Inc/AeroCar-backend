﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Admin
{
    public class CarAdmin : IdentityUser
    {
        [ForeignKey("CarCompany.CarCompanyId")]
        public long CarCompanyId { get; set; }

        public bool SetUpPassword { get; set; }
    }
}
