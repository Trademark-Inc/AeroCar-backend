using AeroCar.Models;
using AeroCar.Models.Reservation;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<FlightReservation>> GetFlightReservationsByFlightId(long id)
        {
            var reservations = await _context.FlightReservations.Include(fr => fr.PriceListItems).AsNoTracking().Where(fr => fr.FlightId == id).ToListAsync();
            return reservations;
        }

        public async Task UpdateFlightReservation(FlightReservation r)
        {
            _context.FlightReservations.Update(r);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFlightReservation(FlightReservation r)
        {
            _context.FlightReservations.Remove(r);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCarReservation(CarReservation r)
        {
            _context.CarReservations.Remove(r);
            await _context.SaveChangesAsync();
        }

        public async Task<FlightReservation> GetFlightReservationNameById(long id)
        {
            return await _context.FlightReservations.Include(fr => fr.PriceListItems).AsNoTracking().SingleOrDefaultAsync(fr => fr.FlightReservationId == id);
        }

        public async Task<CarReservation> GetCarReservationNameById(long id)
        {
            return await _context.CarReservations.Include(fr => fr.PickUpLocation).Include(fr=> fr.ReturnLocation).AsNoTracking().SingleOrDefaultAsync(fr => fr.CarReservationId == id);
        }
    }
}
