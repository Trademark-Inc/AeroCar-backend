using AeroCar.Models;
using AeroCar.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class OfficeRepository
    {
        private readonly ApplicationDbContext _context;

        public OfficeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task RemoveOffice(Office o)
        {
            _context.Offices.Remove(o);
            await _context.SaveChangesAsync();
        }
    }
}
