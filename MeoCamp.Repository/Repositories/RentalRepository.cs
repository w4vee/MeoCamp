using MeoCamp.Data.Models;
using MeoCamp.Data.Repositories;
using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly MeoCampDBContext _context;

        public  RentalRepository(MeoCampDBContext context)
        {
            _context = context;
        }
        public async Task<Rental> CreateRental(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return rental;
        }

        public async Task<bool> DeleteRental(Rental rental)
        {
            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Rental>> GetAllRentalAsync()
        {
            return await _context.Rentals.ToListAsync();
        }

        public async Task<Rental> GetRentalbyUserIdAsync(int userId)
        {
            var category = await _context.Rentals.FirstOrDefaultAsync(x => x.CustomerId == userId);
            return category;
        }

        public async Task<Rental> UpdateRental(Rental rental)
        {
            var tracker = _context.Attach(rental);
            tracker.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return rental;
        }
    }
}