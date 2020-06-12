using AeroCar.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Rate
{
    public class AvioFlightRatingDTO
    {
        public StarRating ratingAvioCompany { get; set; }

        public StarRating ratingFlight { get; set; }
    }
}
