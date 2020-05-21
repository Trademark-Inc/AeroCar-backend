using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Avio
{
    public class Flight
    {
        [Key]
        public long FlightId { get; set; }

        [ForeignKey("AvioCompany.AvioCompanyId")]
        public long AvioCompanyId { get; set; }

        [ForeignKey("Aeroplane.AeroplaneId")]
        public long AeroplaneId { get; set; }

        public DateTime Departure { get; set; }

        public Destination DepartureLocation { get; set; }

        public DateTime Arrival { get; set; }

        public long TravelTime { get; set; }

        public double TravelDistance { get; set; }

        public List<Destination> Transit { get; set; }

        public double Price { get; set; }
    }
}
