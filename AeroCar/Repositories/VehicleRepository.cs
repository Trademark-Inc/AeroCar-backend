using AeroCar.Models;
using AeroCar.Models.Car;
using AeroCar.Models.Rating;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class VehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleRating>> GetRatingsByVehicleId(long id)
        {
            return await _context.CarRatings.AsNoTracking().Where(cr => cr.VehicleId == id).ToListAsync();
        }

        public async Task RemoveVehicle(Vehicle v)
        {
            _context.Vehicles.Remove(v);
            await _context.SaveChangesAsync();
        }
    }
}
