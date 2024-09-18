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

        public async Task<bool> ChangePassword(string username, string password)
        {
            // Kiểm tra xem người dùng đã tồn tại hay chưa
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("Người dùng chưa tồn tại");
            }else if (existingUser.Password == password)//thay đổi phải khác mk cũ
            {
                throw new Exception("Mật khẩu này đang sử dụng");
            }
            // cập nhật mk mới
            existingUser.Password = password;
            await _userRepository.UpdateProfile(existingUser); 
            return true;
        }

        public async Task<bool> ChangeRole(string username, string role)
        {
             // Kiểm tra xem người dùng đã tồn tại hay chưa
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("Người dùng chưa tồn tại");
            }
            // cập nhật role mới
            existingUser.Role = role;
            await _userRepository.UpdateProfile(existingUser); 
            return true;
        }

        public async Task<User> Login(LoginModel model)
        {
            return await _genericRepo.SingleOrDefaultAsync(
                selector: x => x,
                predicate: x => x.Username == model.Username && x.Password == model.Password
                );
        }

        public async Task<bool> Register(RegisterModel model)
        {
            // Kiểm tra xem người dùng đã tồn tại hay chưa
            var existingUser = await _userRepository.GetUserByUsernameAsync(model.Username);
            if (existingUser != null)
            {
                throw new KeyNotFoundException("Người dùng này đã tồn tại");
            }

            var newUser = new User
            {
                Fullname = model.Fullname,
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                CreatedAt = DateTime.Now,
                Status = "Active", // Có thể để trạng thái mặc định là Active
                Role = "User"      // Mặc định là User
            };

            // Thêm người dùng mới vào db
            await _userRepository.Register(newUser);
            

            return true;
        }

        public async Task<bool> UpdateProfile(string username, ProfileModel profile)
        {
            // Kiểm tra xem người dùng đã tồn tại hay chưa
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("Người dùng chưa tồn tại");
            }
            // cập nhật theo ý người nhập
            existingUser.Fullname = profile.Fullname;
            existingUser.PhoneNumber = profile.PhoneNumber;
            existingUser.Address = profile.Address;
            existingUser.Email = profile.Email;
            // cập nhật vào db
            await _userRepository.UpdateProfile(existingUser);
            return true;
        }

        public async Task<List<User>> ViewListUser()
        {
            // Kiểm tra xem có người dùng đã tồn tại hay chưa
            var listuser = await _userRepository.GetAllUserAsync();
            if (listuser == null)
            {
                throw new KeyNotFoundException("Chưa có người dùng tồn tại");
            }
            return listuser;
        }

        public async Task<User> ViewProfilebyUsername(string username)
        {
            // Kiểm tra xem người dùng đã tồn tại hay chưa
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("Người dùng chưa tồn tại");
            }
            // kiểm soát dữ liệu trả về
            var returnUser = new User
            {
                Fullname = existingUser.Fullname,
                PhoneNumber = existingUser.PhoneNumber,
                Address = existingUser.Address,
                Email = existingUser.Email,
                CreatedAt = existingUser.CreatedAt,
            };
            
            return returnUser;
        }
    }
}
