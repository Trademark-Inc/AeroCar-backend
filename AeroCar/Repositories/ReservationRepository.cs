using AeroCar.Models;
using AeroCar.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class ReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UpdateFlightReservation(FlightReservation r)
        {
            _context.FlightReservations.Update(r);
            await _context.SaveChangesAsync();
        }
    }
}
