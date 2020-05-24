﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Car
{
    public class CarCompanyProfile
    {
        [Key]
        public long CarCompanyProfileId { get; set; }

        [ForeignKey("CarCompany.CarCompanyId")]
        public long CarCompanyId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PromoDescription { get; set; }
    }
}