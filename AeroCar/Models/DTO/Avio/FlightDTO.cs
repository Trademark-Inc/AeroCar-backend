using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Avio
{
    public class FlightDTO
    {
        [Required]
        public string Aeroplane { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public Destination DepartureLocation { get; set; }

        [Required]
        public DateTime Arrival { get; set; }

        [Required]
        public Destination ArrivalLocation { get; set; }

        [Required]
        public double TravelTime { get; set; }

        [Required]
        public double TravelDistance { get; set; }

        public List<Destination> Transit { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
