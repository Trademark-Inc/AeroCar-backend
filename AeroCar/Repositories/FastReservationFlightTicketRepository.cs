using AeroCar.Models;
using AeroCar.Models.Reservation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class FastReservationFlightTicketRepository
    {
        private readonly ApplicationDbContext _context;

        public FastReservationFlightTicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FastReservationFlightTicket>> GetAllTickets()
        {
            return await _context.FastReservationFlightTickets.AsNoTracking().ToListAsync();
        }

        public async Task<FastReservationFlightTicket> GetTicketByFlightId(long id)
        {
            return await _context.FastReservationFlightTickets.AsNoTracking().SingleOrDefaultAsync(t => t.FlightId == id);
        }

        public async Task<FastReservationFlightTicket> GetTicket(long id)
        {
            return await _context.FastReservationFlightTickets.AsNoTracking().SingleOrDefaultAsync(s => s.FRFTId == id);
        }

        public async Task AddTicket(FastReservationFlightTicket t)
        {
            _context.FastReservationFlightTickets.Add(t);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveTicket(FastReservationFlightTicket t)
        {
            _context.FastReservationFlightTickets.Remove(t);
            await _context.SaveChangesAsync();
        }
    }
}
