using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Reservation
{
    public class FlightReservation
    {
        [Key]
        public long FlightReservationId { get; set; }

        [ForeignKey("Flight.FlightId")]
        public long FlightId { get; set; }

        [ForeignKey("RegularUser.Id")]
        public long UserId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int SeatNumber { get; set; }

        public string Passport { get; set; }

        public bool Invitation { get; set; }

        public bool Canceled { get; set; }

        public bool Finished { get; set; }
    }
}
