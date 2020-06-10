using AeroCar.Models;
using AeroCar.Models.Avio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class PriceListItemRepository
    {
        private readonly ApplicationDbContext _context;

        public PriceListItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PriceListItem>> GetAllPriceListItems()
        {
            return await _context.PriceListItems.AsNoTracking().ToListAsync();
        }

        public async Task<PriceListItem> GetPriceListItem(long id)
        {
            return await _context.PriceListItems.AsNoTracking().SingleOrDefaultAsync(s => s.PriceListIdemId == id);
        }

        public async Task AddPriceListItem(PriceListItem p)
        {
            _context.PriceListItems.Add(p);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePriceListItem(PriceListItem p)
        {
            _context.PriceListItems.Remove(p);
            await _context.SaveChangesAsync();
        }
    }
}
