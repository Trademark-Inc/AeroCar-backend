using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Reservation
{
    public class FastReservationFlightTicket
    {
        [Key]
        public long FRFTId { get; set; }

        [ForeignKey("Flight.FlightId")]
        public long FlightId { get; set; }

        public double Percent { get; set; }
    }
}
