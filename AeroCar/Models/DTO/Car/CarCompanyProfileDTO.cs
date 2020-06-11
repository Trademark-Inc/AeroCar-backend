using AeroCar.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Car
{
    public class CarCompanyProfileDTO
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }

        public int RatingPicture { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string Offices { get; set; }

        public List<Office> OfficeList { get; set; }

        public List<VehicleDTO> VehicleList { get; set; }
    }
}
