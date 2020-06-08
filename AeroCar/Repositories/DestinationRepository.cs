using AeroCar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class DestinationRepository
    {
        private readonly ApplicationDbContext _context;

        public DestinationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Destination>> GetAllDestinations()
        {
            return await _context.Destinations.AsNoTracking().ToListAsync();
        }

        public async Task<Destination> GetDestination(long id)
        {
            return await _context.Destinations.AsNoTracking().SingleOrDefaultAsync(d => d.DestinationId == id);
        }

        public async Task<Destination> GetDestinationByName(string name)
        {
            return await _context.Destinations.AsNoTracking().SingleOrDefaultAsync(d => d.Name == name);
        }

        public async Task AddDestination(Destination d)
        {
            _context.Destinations.Add(d);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveDestination(Destination d)
        {
            _context.Destinations.Remove(d);
            await _context.SaveChangesAsync();
        }
    }
}
