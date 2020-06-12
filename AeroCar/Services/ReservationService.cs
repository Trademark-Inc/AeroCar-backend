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

        public async Task<List<int>> GetTakenSeats(Flight f, Aeroplane a)
        {
            var reservations = await _repository.GetFlightReservationsByFlightId(f.FlightId);

            List<int> takenSeats = new List<int>();
            if (reservations != null)
            {
                foreach (FlightReservation fr in reservations)
                {
                    takenSeats.Add(fr.SeatNumber);
                }
            }

            return takenSeats;
        }

        public async Task UpdateFlightReservation(FlightReservation r)
        {
            if (r.Name != null && r.Name != "" &&
                r.Surname != null && r.Surname != "" &&
                r.Passport != null && r.Passport != "" &&
                r.SeatNumber != -1)
            {
                r.Finished = true;
            }

            await _repository.UpdateFlightReservation(r);
        }

        public async Task RemoveFlightReservation(FlightReservation r)
        {
            await _repository.RemoveFlightReservation(r);
        }

        public async Task RemoveCarReservation(CarReservation r)
        {
            await _repository.RemoveCarReservation(r);
        }

        public async Task<FlightReservation> GetFlightReservationById(long id)
        {
            return await _repository.GetFlightReservationNameById(id);
        }

        public async Task<CarReservation> GetCarReservationById(long id)
        {
            return await _repository.GetCarReservationNameById(id);
        }
    }
}
