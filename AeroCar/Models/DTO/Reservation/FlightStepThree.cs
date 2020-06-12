using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Reservation
{
    public class FlightStepThree
    {
        public int ReservationId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Passport { get; set; }
    }
}
