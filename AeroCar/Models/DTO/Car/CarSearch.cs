using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Car
{
    public class CarSearch
    {
        [Required]
        public string PickUpLocation { get; set; }

        [Required]
        public DateTime PickUpDate { get; set; }

        [Required]
        public DateTime PickUpTime { get; set; }

        [Required]
        public string ReturnLocation { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        public DateTime ReturnTime { get; set; }

        public CarType CarType { get; set; }

        public int Passangers { get; set; }
    }
}
