using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models
{
    public class Destination
    {
        public Destination() { }
     
        public Destination(Destination item)
        {
            this.Latitude = item.Latitude;
            this.Longitude = item.Longitude;
            this.Name = item.Name;
        }

        [Key]
        public long DestinationId { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
