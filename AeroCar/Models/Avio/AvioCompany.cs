using AeroCar.Models.Reservation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.Avio
{
    public class AvioCompany
    {
        [Key]
        public long AvioCompanyId { get; set; }

        [ForeignKey("AvioCompanyProfile.AvioCompanyProfileId")]
        public long AvioCompanyProfileId { get; set; }

        public List<Destination> Destinations { get; set; }

        public List<Flight> Flights { get; set; }

        public List<FastReservationFlightTicket> FastReservationTickets { get; set; }

        public List<Aeroplane> Aeroplanes { get; set; }

        public List<PriceListItem> PriceList { get; set; }

        public string BaggageDescription { get; set; }
    }
}
