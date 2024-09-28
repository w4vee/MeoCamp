using MeoCamp.Data.Models;
using MeoCamp.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Service.Services.Interface
{
    public interface IFeedbackService
    {
        public Task<List<Feedback>> GetAllFeedbackAsync();
        public Task<Feedback> GetFeedbackbyUserIdAsync(int userId);
        public Task<Feedback> CreateFeedback(int userId, FeedbackModel model);
        public Task<Feedback> UpdateFeedback(int userId, FeedbackModel model);
        public Task<bool> DeleteFeedback(int userId);
    }
}
