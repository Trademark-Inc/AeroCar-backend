using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Car
{
    public class OfficeDTO
    {
        public long OfficeId { get; set; }

        public DestinationDTO Location { get; set; }

        public string Address { get; set; }

        public string Additional { get; set; }
    }
}
