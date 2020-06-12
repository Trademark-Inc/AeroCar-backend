using AeroCar.Models;
using AeroCar.Models.Avio;
using AeroCar.Models.Rating;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class FlightRepository
    {
        private readonly ApplicationDbContext _context;

        public FlightRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Flight>> GetAllFlights()
        {
            return await _context.Flights.AsNoTracking()
                .Include(f => f.Transit)
                .Include(f => f.ArrivalLocation)
                .Include(f => f.DepartureLocation).ToListAsync();
        }

        public async Task<List<Flight>> GetFlightsByCompanyId(long id)
        {
            return await _context.Flights.AsNoTracking()
                .Include(f => f.Transit)
                .Include(f => f.ArrivalLocation)
                .Include(f => f.DepartureLocation)
                .Where(f => f.AvioCompanyId == id).ToListAsync();
        }

        public async Task<Flight> GetFlight(long id)
        {
            return await _context.Flights.AsNoTracking()
                .Include(f => f.Transit)
                .Include(f => f.ArrivalLocation)
                .Include(f => f.DepartureLocation).SingleOrDefaultAsync(f => f.FlightId == id);
        }

        public async Task AddFlight(Flight f)
        {
            _context.Flights.Add(f);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFlight(Flight f)
        {
            _context.Flights.Remove(f);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FlightRating>> FlightRating()
        {
            return await _context.FlightRatings.AsNoTracking().ToListAsync();
        }
    }
}
