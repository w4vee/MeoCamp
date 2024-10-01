using Azure.Identity;
using MeoCamp.Controllers;
using MeoCamp.Data.Models;
using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services;
using MeoCamp.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MeoCamp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IFeedbackService _feedbackService;
        private readonly ILogger<FeedbackController> _logger;

        public FeedbackController(IConfiguration configuration, IFeedbackService feedbackService, ILogger<FeedbackController> logger)
        {
            _configuration = configuration;
            _feedbackService = feedbackService;
            _logger = logger;
        }

        [HttpGet("getAllFeedbacks")]
        public async Task<IActionResult> GetAllFeedbackAsync()
        {
            var result = await _feedbackService.GetAllFeedbackAsync();
            if (result != null)
            {
                // lấy list thành công
                return Ok(result);
            }
            else
            {
                // lấy list failed
                return BadRequest("List rỗng.");
            }
        }

        [HttpGet("getFeedbackbyuserId/{userId}")]
        public async Task<IActionResult> GetFeedbackbyUserIdAsync(int userId)
        {
            if (ModelState.IsValid)
            {
                var result = await _feedbackService.GetFeedbackbyUserIdAsync(userId);
                if (result != null)
                {
                    // lấy feedback thành công
                    return Ok(result);
                }
                else
                {
                    // tìm feedback failed
                    return BadRequest("chưa feedback.");
                }
            }
            return BadRequest("Invalid data.");
        }
        [HttpPost("CreateFeedback/{userId}")]
        public async Task<IActionResult> CreateFeedback(int userId, FeedbackModel feedback)
        {
            if (ModelState.IsValid)
            {
                var result = await _feedbackService.CreateFeedback(userId, feedback);
                if (result != null)
                {
                    // tạo feedback thành công
                    return Ok(result);
                }
                else
                {
                    // tạo feedback failed
                    return BadRequest("feedback không thành công.");
                }
            }
            return BadRequest("Invalid data.");
        }
        [HttpPut("UpdateFeedback/{userId}")]
        public async Task<IActionResult> UpdateFeedback(int userId, FeedbackModel feedback)
        {
            if (ModelState.IsValid)
            {
                var result = await _feedbackService.UpdateFeedback(userId, feedback);
                if (result != null)
                {
                    // cập nhật feedback thành công
                    return Ok(result);
                }
                else
                {
                    // cập nhật feedback failed
                    return BadRequest("feedback cập nhật không thành công.");
                }
            }
            return BadRequest("Invalid data.");
        }
        [HttpDelete("DeleteFeedback/{userId}")]
        public async Task<IActionResult> DeleteFeedback(int userId)
        {
            if (ModelState.IsValid)
            {
                var result = await _feedbackService.DeleteFeedback(userId);
                if (result)
                {
                    // xoá feedback thành công
                    return Ok("feedback xoá không thành công.");
                }
                else
                {
                    // xoá feedback failed
                    return BadRequest("feedback xoá không thành công.");
                }
            }
            return BadRequest("Invalid data.");
        }
    }
}
