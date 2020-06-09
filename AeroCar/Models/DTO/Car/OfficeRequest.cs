using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Car
{
    public class OfficeRequest
    {
        public long OfficeId { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}
