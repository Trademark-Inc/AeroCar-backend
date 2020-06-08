using AeroCar.Models.Reservation;
using AeroCar.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Services
{
    public class FastReservationFlightTicketService
    {
        private readonly FastReservationFlightTicketRepository _repository;

        public FastReservationFlightTicketService(FastReservationFlightTicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<FastReservationFlightTicket>> GetAllTickets()
        {
            return await _repository.GetAllTickets();
        }

        public async Task<FastReservationFlightTicket> GetTicketByFlightId(long id)
        {
            return await _repository.GetTicketByFlightId(id);
        }

        public async Task<FastReservationFlightTicket> GetTicket(long id)
        {
            return await _repository.GetTicket(id);
        }

        public async Task<bool> TicketExists(long flightId)
        {
            var ticket = await _repository.GetTicketByFlightId(flightId);
            return ticket != null;
        }

        public async Task AddTicket(FastReservationFlightTicket t)
        {
            await _repository.AddTicket(t);
        }

        public async Task RemoveTicket(FastReservationFlightTicket t)
        {
            await _repository.RemoveTicket(t);
        }
    }
}
