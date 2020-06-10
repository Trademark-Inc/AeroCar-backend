using AeroCar.Models;
using AeroCar.Models.Avio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class AeroplaneRepository
    {
        private readonly ApplicationDbContext _context;

        public AeroplaneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Aeroplane>> GetAllAeroplanes()
        {
            return await _context.Aeroplanes.Include(a => a.Seats).ThenInclude(s => s.DeletedSeats).AsNoTracking().ToListAsync();
        }

        public async Task<List<Aeroplane>> GetAeroplanesByCompanyId(long id)
        {
            var aeroplanes = await GetAllAeroplanes();
            return aeroplanes.Where(l => l.AvioCompanyId == id).ToList();
        }

        public async Task<Aeroplane> GetAeroplane(long id)
        {
            return await _context.Aeroplanes.Include(a => a.Seats).ThenInclude(s => s.DeletedSeats).AsNoTracking().SingleOrDefaultAsync(a => a.AeroplaneId == id);
        }

        public async Task<Aeroplane> GetAeroplaneByName(string name)
        {
            return await _context.Aeroplanes.Include(a => a.Seats).ThenInclude(s => s.DeletedSeats).AsNoTracking().SingleOrDefaultAsync(a => a.Name == name);
        }

        public async Task AddAeroplane(Aeroplane a)
        {
            _context.Aeroplanes.Add(a);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAeroplane(Aeroplane a)
        {
            _context.Aeroplanes.Remove(a);
            await _context.SaveChangesAsync();
        }
    }
}
