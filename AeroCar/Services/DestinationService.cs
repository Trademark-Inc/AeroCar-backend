using AeroCar.Models;
using AeroCar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class DestinationService
    {
        private readonly DestinationRepository _repository;

        public DestinationService(DestinationRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Destination>> GetAllDestinations()
        {
            return await _repository.GetAllDestinations();
        }

        public async Task<Destination> GetDestination(long id)
        {
            return await _repository.GetDestination(id);
        }

        public async Task<bool> DestinationExists(string name)
        {
            return (await _repository.GetDestinationByName(name)) != null;
        }

        public async Task AddDestination(Destination d)
        {
           await _repository.AddDestination(d);
        }

        public async Task RemoveDestination(Destination d)
        {
            await _repository.RemoveDestination(d);
        }
    }
}
