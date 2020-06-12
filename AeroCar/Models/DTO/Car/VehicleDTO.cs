using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Car
{
    public class VehicleDTO
    {
        public long VehicleId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public CarType CarType { get; set; }

        [Required]
        public int Passangers { get; set; }

        [Required]
        public int Baggage { get; set; }

        [Required]
        public int Doors { get; set; }

        [Required]
        public string Fuel { get; set; }

        [Required]
        public string Transmission { get; set; }

        [Required]
        public string Additional { get; set; }

        [Required]
        public double CostPerDay { get; set; }

        public int Rating { get; set; }
    }
}
