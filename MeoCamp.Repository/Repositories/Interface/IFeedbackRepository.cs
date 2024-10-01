using MeoCamp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories.Interface
{
    public interface IFeedbackRepsitory
    {
        public Task<List<Feedback>> GetAllFeedbackAsync();
        public Task<Feedback> GetFeedbackbyUserIdAsync(int userId);
        public Task<Feedback> CreateFeedback(Feedback feedback);
        public Task<Feedback> UpdateFeedback(Feedback feedback);
        public Task<bool> DeleteFeedback(Feedback feedback);
    }
}
