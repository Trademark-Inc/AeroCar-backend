﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Car
{
    public class CarCompanyDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
    }
}
