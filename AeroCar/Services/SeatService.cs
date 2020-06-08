using AeroCar.Models.Avio;
using AeroCar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class SeatService
    {
        private readonly SeatRepository _repository;

        public SeatService(SeatRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Seats>> GetAllSeats()
        {
            return await _repository.GetAllSeats();
        }

        public async Task<Seats> GetSeatsByAeroplaneId(long id)
        {
            return await _repository.GetSeatsByAeroplaneId(id);
        }

        public async Task<Seats> GetSeats(long id)
        {
            return await _repository.GetSeats(id);
        }

        public async Task<bool> SeatsExist(long aeroplaneId)
        {
            var seats = await _repository.GetSeatsByAeroplaneId(aeroplaneId);
            return seats != null;
        }

        public async Task AddSeats(Seats s)
        {
            await _repository.AddSeats(s);
        }

        public async Task RemoveSeats(Seats s)
        {
            await _repository.RemoveSeats(s);
        }
    }
}
