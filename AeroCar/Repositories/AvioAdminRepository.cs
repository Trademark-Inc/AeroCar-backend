using AeroCar.Models;
using AeroCar.Models.Admin;
using AeroCar.Models.Rating;
using AeroCar.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class AvioAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AvioAdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AvioAdmin>> GetAdmins()
        {
            return await _context.AvioAdmins.ToListAsync();
        }

        public async Task<RegularUser> GetAdminUser(string id)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AvioAdmin> GetAdmin(string username)
        {
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(a => a.UserName == username);
            
            if (user != null)
                return await _context.AvioAdmins.AsNoTracking().SingleOrDefaultAsync(a => a.UserId == user.Id);

            return null;
        }

        public async Task<AvioAdmin> GetAdminById(string id)
        {
            return await _context.AvioAdmins.AsNoTracking().SingleOrDefaultAsync(u => u.UserId.Equals(id));
        }

        public async Task<AvioAdmin> GetAdminByEmail(string email)
        {
            var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(a => a.Email == email);

            if (user != null)
                return await _context.AvioAdmins.AsNoTracking().SingleOrDefaultAsync(u => u.UserId == user.Id);

            return null;
        }

        public async Task CreateAdmin(AvioAdmin admin)
        {
            await _context.AvioAdmins.AddAsync(admin);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAdmin(AvioAdmin admin)
        {
            _context.AvioAdmins.Update(admin);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RemoveAdmin(string username)
        {
            var admin = await GetAdmin(username);
            if (admin != null)
            {
                var userId = admin.UserId;

                _context.AvioAdmins.Remove(admin);
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

                _context.AvioAdmins.Remove(admin);
                _context.Users.Remove(await GetAdminUser(userId));
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<int> Count()
        {
            return await _context.AvioAdmins.AsNoTracking().CountAsync();
        }
    }
}
