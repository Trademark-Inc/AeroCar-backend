using AeroCar.Models;
using AeroCar.Models.Avio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class SeatRepository
    {
        private readonly ApplicationDbContext _context;

        public SeatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Seats>> GetAllSeats()
        {
            return await _context.Seats.AsNoTracking().ToListAsync();
        }

        public async Task<Seats> GetSeatsByAeroplaneId(long id)
        {
            return await _context.Seats.AsNoTracking().SingleOrDefaultAsync(s => s.AeroplaneId == id);
        }

        public async Task<Seats> GetSeats(long id)
        {
            return await _context.Seats.AsNoTracking().SingleOrDefaultAsync(s => s.SeatsId == id);
        }

        public async Task AddSeats(Seats s)
        {
            _context.Seats.Add(s);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveSeats(Seats s)
        {
            _context.Seats.Remove(s);
            await _context.SaveChangesAsync();
        }
    }
}
