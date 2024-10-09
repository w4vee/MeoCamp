using MeoCamp.Data.Models;
using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository.Models;
using MeoCamp.Repository;
using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeoCamp.Data.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeoCamp.Service.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFeedbackRepsitory _feedbackRepository;
        private readonly GenericRepository<Feedback> _genericRepo;

        public FeedbackService(IFeedbackRepsitory feedbackRepository, IUserRepository userRepository, GenericRepository<Feedback> genericRepo)
        {
            _feedbackRepository = feedbackRepository;
            _genericRepo = genericRepo;
            _userRepository = userRepository;
        }
        // tao feadback
        public async Task<Feedback> CreateFeedback(int userId, FeedbackModel model)
        {
            try
            {
                var user = await _userRepository.GetUserByUserIdAsync(userId);
                if (user == null)
                {
                    // Không tìm thấy người dùng, trả về null hoặc xử lý theo cách khác
                    return null;
                }

                var feedback = new Feedback
                {
                    CustomerId = userId,
                    Description = model.description,
                    Rate = model.rate
                };

                // Tạo mới feedback
                await _feedbackRepository.CreateFeedback(feedback);

                return feedback;
            }
            catch (Exception ex)
            {
                // Ghi log chi tiết lỗi (nếu cần)
                throw new Exception("An error occurred while creating feedback: " + ex.Message, ex);
            }
        }

        // xoa feedback
        public async Task<bool> DeleteFeedback(int userId)
        {
            var feedback = await _feedbackRepository.GetFeedbackbyUserIdAsync(userId);
            if (feedback != null)
            {
                await _feedbackRepository.DeleteFeedback(feedback);
                return true;
            }
            return false;
        }

        // tra list feedback
        public async Task<List<Feedback>> GetAllFeedbackAsync()
        {
            var List = await _feedbackRepository.GetAllFeedbackAsync();
            return List;
        }

        // tim chu nhan cua feedback
        public async Task<Feedback> GetFeedbackbyUserIdAsync(int userId)
        {
            return await _feedbackRepository.GetFeedbackbyUserIdAsync(userId);
        }

        // cap nhat feedback
        public async Task<Feedback> UpdateFeedback(int userId, FeedbackModel model)
        {
            var feedback = await _feedbackRepository.GetFeedbackbyUserIdAsync(userId);
            if (feedback != null)
            {
                if (model.description != null || model.description != feedback.Description)
                {
                    feedback.Description = model.description;
                }
                
                if (model.rate != null || model.rate != feedback.Rate)
                {
                    feedback.Rate = model.rate;
                }
            }
            await _feedbackRepository.UpdateFeedback(feedback);

            return feedback;
        }
    }
}
