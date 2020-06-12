using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Rating
{
    public class AvioCompanyRating
    {
        [Key]
        public long RatingId { get; set; }

        [ForeignKey("FlightReservation.FlightReservationId")]
        public long FlightReservationId { get; set; }

        [ForeignKey("AvioCompany.AvioCompanyId")]
        public long AvioCompanyId { get; set; }

        [ForeignKey("RegularUser.Id")]
        public string UserId { get; set; }
        
        public string Comment { get; set; }

        public StarRating Rate { get; set; }
    }
}
