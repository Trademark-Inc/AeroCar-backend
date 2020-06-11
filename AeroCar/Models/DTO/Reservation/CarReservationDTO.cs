using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Reservation
{
    public class CarReservationDTO
    {
        [Required]
        public long VehicleId { get; set; }

        [Required]
        public CarReservationDetails ReservationDetails { get; set; }
    }

    public class CarReservationDetails
    {
        [Required]
        public DateTime PickUpDate { get; set; }

        [Required]
        public string PickUpTime { get; set; }

        [Required]
        public string PickUpLocation { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        public string ReturnTime { get; set; }

        [Required]
        public string ReturnLocation { get; set; }
    }
}
