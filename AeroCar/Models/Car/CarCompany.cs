using AeroCar.Models.Reservation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Car
{
    public class CarCompany
    {
        [Key]
        public long CarCompanyId { get; set; }

        [ForeignKey("CarCompanyProfile.CarCompanyProfileId")]
        public long CarCompanyProfileId { get; set; }

        public List<Vehicle> Vehicles { get; set; }

        public List<Office> Offices { get; set; }

        public List<FastReservationCarTicket> FastReservationCarTickets { get; set; }
    }
}
