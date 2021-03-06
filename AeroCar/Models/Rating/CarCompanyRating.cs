﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Rating
{
    public class CarCompanyRating
    {
        [Key]
        public long RatingId { get; set; }

        [ForeignKey("CarReservation.CarReservationId")]
        public long CarReservationId { get; set; }

        [ForeignKey("CarCompany.CarCompanyId")]
        public long CarCompanyId { get; set; }

        [ForeignKey("RegularUser.Id")]
        public string UserId { get; set; }

        public string Comment { get; set; }

        public StarRating Rate { get; set; }
    }
}
