using AeroCar.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Avio
{
    public class FlightSearch
    {
        [Required]
        public string Origin { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public FlightType Ticket { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        public DateTime Arrival { get; set; }

        [Required]
        public int Adults { get; set; }

        [Required]
        public int Children { get; set; }

        [Required]
        public int Infants { get; set; }
    }
}
