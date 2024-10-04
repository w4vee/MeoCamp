using MeoCamp.Data.Models;
using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.Services.Interface
{
    public interface IRentalService
    {
        public Task<List<Rental>> GetAllRentalAsync();
        public Task<Rental> GetRentalbyUserIdAsync(int userId);
        public Task<Rental> CreateRental(int userId, RentalModel model);
        public Task<Rental> UpdateRental(int userId, RentalModel model);
        public Task<bool> DeleteRental(int userId);
    }
}
