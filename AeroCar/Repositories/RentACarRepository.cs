using AeroCar.Models;
using AeroCar.Models.Car;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class RentACarRepository
    {
        private readonly ApplicationDbContext _context;

        public RentACarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarCompany>> GetCompanies()
        {
            return await _context.CarCompanies
                .Include(rc => rc.FastReservationCarTickets)
                .Include(rc => rc.Offices).ThenInclude(o => o.Location)
                .Include(rc => rc.Vehicles).ToListAsync();
        }

        public async Task<CarCompany> GetCompany(long id)
        {
            return await _context.CarCompanies
                .Include(rc => rc.FastReservationCarTickets)
                .Include(rc => rc.Offices).ThenInclude(o => o.Location)
                .Include(rc => rc.Vehicles).AsNoTracking().SingleOrDefaultAsync(cc => cc.CarCompanyId == id);
        }

        public async Task<CarCompany> GetCompany(string name)
        {
            var profile = await _context.CarCompanyProfiles.AsNoTracking().SingleOrDefaultAsync(ccp => ccp.Name == name);

            if (profile != null)
            {
                return await _context.CarCompanies
                .Include(rc => rc.FastReservationCarTickets)
                .Include(rc => rc.Offices).ThenInclude(o => o.Location)
                .Include(rc => rc.Vehicles).AsNoTracking().SingleOrDefaultAsync(cc => cc.CarCompanyProfileId == profile.CarCompanyProfileId);
            }
            else
            {
                return null;
            }
        }

        public async Task<CarCompanyProfile> GetCompanyProfile(long profileId)
        {
            return await _context.CarCompanyProfiles.AsNoTracking().SingleOrDefaultAsync(ccp => ccp.CarCompanyProfileId == profileId);
        }

        public async Task CreateCompany(CarCompanyProfile profile)
        {
            var result = await _context.CarCompanyProfiles.AddAsync(profile);
            await _context.SaveChangesAsync();

            long carCompanyProfileId = result.Entity.CarCompanyProfileId;
            await _context.CarCompanies.AddAsync(new CarCompany() { CarCompanyProfileId = carCompanyProfileId });
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveCompany(long id)
        {
            var company = await GetCompany(id);
            if (company != null)
            {
                long profileId = company.CarCompanyProfileId;

                _context.CarCompanies.Remove(await GetCompany(id));
                await _context.SaveChangesAsync();

                if (profileId > 0)
                {
                    _context.CarCompanyProfiles.Remove(await GetCompanyProfile(profileId));
                    await _context.SaveChangesAsync();
                }

                return true;
            }

            return false;
        }

        public async Task UpdateCompanyProfile(CarCompanyProfile profile)
        {
            _context.CarCompanyProfiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCompany(CarCompany carCompany)
        {
            _context.CarCompanies.Update(carCompany);
            await _context.SaveChangesAsync();
        }
    }
}
