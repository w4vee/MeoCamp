using MeoCamp.Data.Models;
using MeoCamp.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories.Interface
{
    public interface IRentalRepository
    {
        public Task<List<Rental>> GetAllRentalAsync();
        public Task<Rental> GetRentalbyUserIdAsync(int userId);
        public Task<Rental> CreateRental(Rental rental);
        public Task<Rental> UpdateRental(Rental rental);
        public Task<bool> DeleteRental(Rental rental);
    }
}
