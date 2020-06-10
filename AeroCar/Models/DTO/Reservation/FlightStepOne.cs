using AeroCar.Models.Avio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Models.DTO.Reservation
{
    public class FlightStepOne
    {
        public Flight Flight { get; set; }

        public List<SelectedPriceListItems> SelectedPriceListItems { get; set; }
    }

    public class SelectedPriceListItems
    {
        public int Id { get; set; }

        public bool Selected { get; set; }
    }
}
