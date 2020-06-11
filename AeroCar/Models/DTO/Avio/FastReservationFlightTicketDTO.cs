using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Avio
{
    public class FastReservationFlightTicketDTO
    {
        [Required]
        public long FlightId { get; set; }

        [Required]
        public double Percent { get; set; }
    }
}
