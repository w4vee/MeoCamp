using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MeoCampDBContext _context;

        public UserRepository(MeoCampDBContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _context.Set<User>().ToListAsync();
        }
        public async Task<User?> GetUserByUserIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            return user;
        }

        public async Task<User?> GetUserByFullnameAsync(string fullname)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Fullname == fullname);
            return user;
        }

        public async Task<User> Register(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateProfile(User user)
        {
            var tracker = _context.Attach(user);
            tracker.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }
    }
}

