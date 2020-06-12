using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Reservation
{
    public class FlightHistoryDTO
    {
        public long ReservationId { get; set; }

        public Destination DepartureLocation { get; set; }

        public Destination ArrivalLocation { get; set; }

        public DateTime Departure { get; set; }

        public string AvioCompanyName { get; set; }
    }
}
