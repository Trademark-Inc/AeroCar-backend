using AeroCar.Models;
using AeroCar.Models.Registration;
using AeroCar.Models.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroCar.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RegularUser> GetUserById(string id)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id.Equals(id));
        }

        public async Task<RegularUser> GetUserByEmail(string email)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<RegularUser> GetUserByUsername(string username)
        {
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UserName.Equals(username));
        }

        public async Task UpdateUser(RegularUser user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Friend>> GetUserFriends(string id)
        {
            var friends = _context.Friends.AsNoTracking().Where(r => r.UserId == id);

            if (friends != null)
                return await friends.ToListAsync();
            else 
                return null;
        }

        public async Task<Friend> GetUserFriend(string userId, string friendId)
        {
            return await _context.Friends.AsNoTracking().SingleOrDefaultAsync(f => f.FriendId == friendId && f.UserId == userId);
        }

        public async Task AddFriend(Friend f)
        {
            await _context.Friends.AddAsync(f);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFriend(Friend f)
        {
            _context.Remove(f);
            await _context.SaveChangesAsync();
        }

        public async Task<int> Count()
        {
            return await _context.Users.AsNoTracking().CountAsync();
        }
    }
}
