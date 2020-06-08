using AeroCar.Models.Avio;
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

        public FlightService(FlightRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Flight>> GetFlightsByCompanyId(long id)
        {
            return await _repository.GetFlightsByCompanyId(id);
        }

        public async Task<Flight> GetFlight(long id)
        {
            return await _repository.GetFlight(id);
        }

        public async Task AddFlight(Flight f)
        {
            await _repository.AddFlight(f);
        }

        public async Task RemoveFlight(Flight f)
        {
            await _repository.RemoveFlight(f);
        }
    }
}
