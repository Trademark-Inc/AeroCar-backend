﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Admin
{
    public class SystemAdmin
    {
        [Key]
        public long SystemAdminId { get; set; }
    }
}
