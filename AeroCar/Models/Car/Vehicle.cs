using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Car
{
    public class Vehicle
    {
        [Key]
        public long VehicleId { get; set; }
        
        [ForeignKey("AvioCompany.AvioCompanyId")]
        public long AvioCompanyId { get; set; }

        public string Name { get; set; }

        public CarType CarType { get; set; }

        public int Passangers { get; set; }

        public int Baggage { get; set; }

        public int Doors { get; set; }

        public string Fuel { get; set; }

        public string Transmission { get; set; }

        public string Additional { get; set; }
        
        public double CostPerDay { get; set; }
    }
}
