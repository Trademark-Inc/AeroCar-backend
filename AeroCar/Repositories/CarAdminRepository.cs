using AeroCar.Models;
using AeroCar.Models.Admin;
using AeroCar.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class CarAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public CarAdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarAdmin>> GetAdmins()
        {
            return await _context.CarAdmins.ToListAsync();
        }

        public async Task<RegularUser> GetAdminUser(string id)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<CarAdmin> GetAdmin(string username)
        {
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(a => a.UserName == username);

            if (user != null)
                return await _context.CarAdmins.AsNoTracking().SingleOrDefaultAsync(a => a.UserId == user.Id);

            return null;
        }

        public async Task<CarAdmin> GetAdminById(string id)
        {
            return await _context.CarAdmins.AsNoTracking().SingleOrDefaultAsync(u => u.UserId.Equals(id));
        }

        public async Task<CarAdmin> GetAdminByEmail(string email)
        {
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(a => a.Email == email);

            if (user != null)
                return await _context.CarAdmins.AsNoTracking().SingleOrDefaultAsync(u => u.UserId == user.Id);

            return null;
        }

        public async Task CreateAdmin(CarAdmin admin)
        {
            await _context.CarAdmins.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdmin(CarAdmin admin)
        {
            _context.CarAdmins.Update(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveAdmin(string username)
        {
            var admin = await GetAdmin(username);
            if (admin != null)
            {
                var userId = admin.UserId;

                _context.CarAdmins.Remove(admin);
                _context.Users.Remove(await GetAdminUser(userId));
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> RemoveAdminById(string id)
        {
            var admin = await GetAdminById(id);
            if (admin != null)
            {
                var userId = admin.UserId;

                _context.CarAdmins.Remove(admin);
                _context.Users.Remove(await GetAdminUser(userId));
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<int> Count()
        {
            return await _context.CarAdmins.AsNoTracking().CountAsync();
        }
    }
}
