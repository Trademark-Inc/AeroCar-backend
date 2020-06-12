using AeroCar.Models.Avio;
using AeroCar.Models.Rating;
using AeroCar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class FlightService
    {
        private readonly FlightRepository _repository;
        private readonly AeroplaneRepository _aeroplaneRepository;

        public FlightService(FlightRepository repository, AeroplaneRepository aeroplaneRepository)
        {
            _repository = repository;
            _aeroplaneRepository = aeroplaneRepository;
        }

        public async Task<List<Flight>> GetFlightsByCompanyId(long id)
        {
            return await _repository.GetFlightsByCompanyId(id);
        }

        public async Task<Flight> GetFlight(long id)
        {
            return await _repository.GetFlight(id);
        }

        public async Task<List<Flight>> GetFlightsBySearch(string origin, string destination, DateTime departure, int peopleCount)
        {
            var flights = await _repository.GetAllFlights();

            List<Flight> retVal = new List<Flight>();
            if (flights != null)
            {
                foreach (Flight f in flights)
                {
                    if (f.DepartureLocation.Name.ToLower().Equals(origin.ToLower()) && f.ArrivalLocation.Name.ToLower().Equals(destination.ToLower()) &&
                        f.Departure >= departure)
                    {
                        var aeroplane = await _aeroplaneRepository.GetAeroplane(f.FlightId);

                        if (aeroplane != null && aeroplane.Seats != null && aeroplane.Seats.DeletedSeats != null && (aeroplane.Seats.SeatCount - aeroplane.Seats.DeletedSeats.Count) >= peopleCount)
                        {
                            retVal.Add(f);
                        }
                    }
                }
            }

            return retVal;
        }

        public async Task AddFlight(Flight f)
        {
            await _repository.AddFlight(f);
        }

        public async Task RemoveFlight(Flight f)
        {
            await _repository.RemoveFlight(f);
        }

        public async Task<List<FlightRating>> FlightRating()
        {
            return await _repository.FlightRating();
        }
    }
}
