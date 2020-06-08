using AeroCar.Models;
using AeroCar.Models.Avio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class AvioRepository
    {
        private readonly ApplicationDbContext _context;

        public AvioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AvioCompany>> GetCompanies()
        {
            return await _context.AvioCompanies
                .Include(ac => ac.Aeroplanes).ThenInclude(a => a.Seats)
                .Include(ac => ac.Destinations)
                .Include(ac => ac.FastReservationTickets)
                .Include(ac => ac.Flights).ThenInclude(f => f.DepartureLocation)
                .Include(ac => ac.Flights).ThenInclude(f => f.ArrivalLocation)
                .Include(ac => ac.Flights).ThenInclude(f => f.Transit)
                .Include(ac => ac.PriceList).ToListAsync();
        }

        public async Task<AvioCompany> GetCompany(long id)
        {
            return await _context.AvioCompanies.AsNoTracking()
                .Include(ac => ac.Aeroplanes).ThenInclude(a => a.Seats)
                .Include(ac => ac.Destinations)
                .Include(ac => ac.FastReservationTickets)
                .Include(ac => ac.Flights).ThenInclude(f => f.DepartureLocation)
                .Include(ac => ac.Flights).ThenInclude(f => f.ArrivalLocation)
                .Include(ac => ac.Flights).ThenInclude(f => f.Transit)
                .Include(ac => ac.PriceList)
                .SingleOrDefaultAsync(ac => ac.AvioCompanyId == id);
        }

        public async Task<AvioCompany> GetCompany(string name)
        {
            var profile = await _context.AvioCompanyProfiles.AsNoTracking().SingleOrDefaultAsync(acp => acp.Name == name);

            if (profile != null)
            {
                return await _context.AvioCompanies.AsNoTracking()
                .Include(ac => ac.Aeroplanes).ThenInclude(a => a.Seats)
                .Include(ac => ac.Destinations)
                .Include(ac => ac.FastReservationTickets)
                .Include(ac => ac.Flights).ThenInclude(f => f.DepartureLocation)
                .Include(ac => ac.Flights).ThenInclude(f => f.ArrivalLocation)
                .Include(ac => ac.Flights).ThenInclude(f => f.Transit)
                .Include(ac => ac.PriceList)
                .SingleOrDefaultAsync(ac => ac.AvioCompanyProfileId == profile.AvioCompanyProfileId);
            }
            else
            {
                return null;
            }
        }

        public async Task<AvioCompanyProfile> GetCompanyProfile(long profileId)
        {
            return await _context.AvioCompanyProfiles.AsNoTracking().SingleOrDefaultAsync(acp => acp.AvioCompanyProfileId == profileId);
        }

        public async Task CreateCompany(AvioCompany company, AvioCompanyProfile profile)
        {
            var result = await _context.AvioCompanyProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();

            long avioCompanyProfileId = result.Entity.AvioCompanyProfileId;
            company.AvioCompanyProfileId = avioCompanyProfileId;
            await _context.AvioCompanies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveCompany(long id)
        {
            var company = await GetCompany(id);
            if (company != null)
            {
                long profileId = company.AvioCompanyProfileId;

                _context.AvioCompanies.Remove(await GetCompany(id));
                await _context.SaveChangesAsync();

                if (profileId > 0)
                {
                    _context.AvioCompanyProfiles.Remove(await GetCompanyProfile(profileId));
                    await _context.SaveChangesAsync();
                }

                return true;
            }

            return false;
        }

        public async Task UpdateCompanyProfile(AvioCompanyProfile profile)
        {
            _context.AvioCompanyProfiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCompany(AvioCompany avioCompany)
        {
            _context.AvioCompanies.Update(avioCompany);
            await _context.SaveChangesAsync();
        }
    }
}
