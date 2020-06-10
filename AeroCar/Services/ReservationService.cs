using AeroCar.Models.Avio;
using AeroCar.Models.Reservation;
using AeroCar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class ReservationService
    {
        private readonly ReservationRepository _repository;

        public ReservationService(ReservationRepository r)
        {
            _repository = r;
        }

        public async Task UpdateFlightReservation(FlightReservation r)
        {
            await _repository.UpdateFlightReservation(r);
        }
    }
}
