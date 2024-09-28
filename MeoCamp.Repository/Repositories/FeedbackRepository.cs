using MeoCamp.Data.Models;
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
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly MeoCampDBContext _context;

        public FeedbackRepository(MeoCampDBContext context)
        {
            _context = context;
        }
        public async Task<Feedback> CreateFeedback(Feedback feedback)
        {
            _context.Feedback.Add(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }
        public async Task<Feedback> UpdateFeedback(Feedback feedback)
        {
            var tracker = _context.Attach(feedback);
            tracker.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return feedback;
        }
        
        public async Task<bool> DeleteFeedback(Feedback feedback)
        {
                _context.Feedback.Remove(feedback);
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<List<Feedback>> GetAllFeedbackAsync()
        {
            return await _context.Set<Feedback>().ToListAsync();
        }

        public async Task<Feedback> GetFeedbackbyUserIdAsync(int userId)
        {
            var Feedback = await _context.Feedback.FirstOrDefaultAsync(x => x.CustomerId == userId);
            return Feedback;
        }
    }
}
