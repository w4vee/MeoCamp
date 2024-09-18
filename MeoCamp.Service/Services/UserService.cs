using MeoCamp.Data.Repositories;
using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository;
using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.Services
{

    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        private readonly GenericRepository<User> _genericRepo;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
            _genericRepo ??= new GenericRepository<User>();
        }


        public async Task<User> Login(string username, string password)
        {
            return await _genericRepo.SingleOrDefaultAsync(
                selector: x => x,
                predicate: x => x.Username == username && x.Password == password
                );
        }

        public async Task<bool> Register(string fullname, string username, string password, string email)
        {
            // Kiểm tra xem người dùng đã tồn tại hay chưa
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                throw new KeyNotFoundException("Người dùng này đã tồn tại");
            }

            var newUser = new User
            {
                Fullname = fullname,
                Username = username,
                Password = password,
                Email = email,
                CreatedAt = DateTime.Now,
                Status = true, 
                Role = "User"      // Mặc định là User
            };

            // Thêm người dùng mới vào db
            await _userRepository.Register(newUser);
            

            return true;
        }

        
    }
}
