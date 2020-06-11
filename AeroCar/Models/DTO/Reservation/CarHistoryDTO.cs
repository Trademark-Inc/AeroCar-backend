using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Reservation
{
    public class CarHistoryDTO
    {
        public long ReservationId { get; set; }

        public DateTime PickUpDate { get; set; }

        public string PickUpLocation { get; set; }

        public DateTime ReturnDate { get; set; }

        public string ReturnLocation { get; set; }

        public string VehicleName { get; set; }

        public string CarCompanyName { get; set; }
    }
}
