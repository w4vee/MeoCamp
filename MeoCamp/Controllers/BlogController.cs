using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services;
using MeoCamp.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ModelBinding;

namespace MeoCamp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IBlogService _blogService;
        private readonly ILogger<FeedbackController> _logger;

        public BlogController(IConfiguration configuration, IBlogService blogService, ILogger<FeedbackController> logger)
        {
            _configuration = configuration;
            _blogService = blogService;
            _logger = logger;
        }

        [HttpGet("getAllBlogs")]
        public async Task<IActionResult> GetAllBlogAsync()
        {
            try
            {
                var blogs = await _blogService.GetAllBlogAsync();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("getBlogbyuserId/{userId}")]
        public async Task<IActionResult> GetBlogbyUserIdAsync(int userId)
        {
            if (ModelState.IsValid)
            {
                var result = await _blogService.GetBlogbyUserIdAsync(userId);
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
        [HttpPost("CreateBlog/{userId}")]
        public async Task<IActionResult> CreateBlog(int userId, BlogModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _blogService.CreateBlog(userId, model);
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
        [HttpPut("UpdateBlog/{userId}")]
        public async Task<IActionResult> UpdateBlog(int userId, BlogModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _blogService.UpdateBlog(userId, model);
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
        [HttpDelete("DeleteBlog/{userId}")]
        public async Task<IActionResult> DeleteBlog(int userId)
        {
            if (ModelState.IsValid)
            {
                var result = await _blogService.DeleteBlog(userId);
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

