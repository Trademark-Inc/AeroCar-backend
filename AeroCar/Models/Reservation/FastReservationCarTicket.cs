using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Reservation
{
    public class FastReservationCarTicket
    {
        [Key]
        public long FRCTId { get; set; }

        [ForeignKey("Vehicle.VehicleId")]
        public long VehicleId { get; set; }

        public double Percent { get; set; }
    }
}
