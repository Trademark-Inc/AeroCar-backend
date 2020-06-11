using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AeroCar.Models.Reservation
{
    public class CarReservation
    {
        [Key]
        public long CarReservationId { get; set; }

        [ForeignKey("Vehicle.VehicleId")]
        public long VehicleId { get; set; }

        public DateTime PickUpDate { get; set; }

        public Destination PickUpLocation { get; set; }

        public DateTime ReturnDate { get; set; }

        public Destination ReturnLocation { get; set; }

        public bool Canceled { get; set; }

        public bool Finished { get; set; }
    }
}
