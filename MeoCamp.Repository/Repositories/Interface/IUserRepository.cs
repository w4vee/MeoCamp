using MeoCamp.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories.Interface
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUserAsync();
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task<User?> GetUserByFullnameAsync(string fullname);
        public Task<User> Register(User user);
        public Task<User> UpdateProfile(User user);

    }
}
