using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Rate
{
    public class CarVehicleRatingDTO
    {
        public StarRating ratingCarCompany { get; set; }

        public StarRating ratingVehicle { get; set; }
    }
}
