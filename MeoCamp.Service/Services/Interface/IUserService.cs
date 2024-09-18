using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.Services.Interface
{
    public interface IUserService
    {
        Task<User> Login(LoginModel model);
        Task<bool> Register(RegisterModel model);
        Task<User> ViewProfilebyUsername(string username);
        Task<List<User>> ViewListUser();
        Task<bool> UpdateProfile(string username, ProfileModel profile);
        Task<bool> ChangePassword(string username, string password);
        Task<bool> ChangeRole(string username, string role);
    }
}
