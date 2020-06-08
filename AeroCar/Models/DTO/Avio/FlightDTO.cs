using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Avio
{
    public class FlightDTO
    {
        public string Aeroplane { get; set; }

        public DateTime Departure { get; set; }

        public Destination DepartureLocation { get; set; }

        public DateTime Arrival { get; set; }

        public Destination ArrivalLocation { get; set; }

        public double TravelTime { get; set; }

        public double TravelDistance { get; set; }

        public List<Destination> Transit { get; set; }

        public double Price { get; set; }
    }
}
