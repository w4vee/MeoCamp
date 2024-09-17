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
        Task<User> Login(string username, string password);

        Task<bool> Register(string fullname, string username, string password, string email);
    }
}
