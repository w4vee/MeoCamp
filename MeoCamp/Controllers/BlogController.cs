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
        private readonly ILogger<BlogController> _logger;

        public BlogController(IConfiguration configuration, IBlogService blogService, ILogger<BlogController> logger)
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
                    return BadRequest("chưa có Blog.");
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
                    return BadRequest("Blog không thành công.");
                }
            }
            return BadRequest("Invalid data.");
        }
        [HttpPut("UpdateBlog/{Id}")]
        public async Task<IActionResult> UpdateBlog(int Id, BlogModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _blogService.UpdateBlog(Id, model);
                if (result != null)
                {
                    // cập nhật feedback thành công
                    return Ok(result);
                }
                else
                {
                    // cập nhật feedback failed
                    return BadRequest("Blog cập nhật không thành công.");
                }
            }
            return BadRequest("Invalid data.");
        }
        [HttpPut("ApproveBlog/{Id}")]
        public async Task<IActionResult> ApproveBlog(int Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _blogService.ApproveBlog(Id);
                if (result)
                {
                    // cập nhật feedback thành công
                    return Ok("Blog được phê duyệt");
                }
                else
                {
                    // cập nhật feedback failed
                    return BadRequest("Blog không được phê duyệt");
                }
            }
            return BadRequest("Invalid data.");
        }
        [HttpDelete("DeleteBlog/{Id}")]
        public async Task<IActionResult> DeleteBlog(int Id)
        {
            if (ModelState.IsValid)
            {
                var result = await _blogService.DeleteBlog(Id);
                if (result)
                {
                    // xoá feedback thành công
                    return Ok("Blog xoá không thành công.");
                }
                else
                {
                    // xoá feedback failed
                    return BadRequest("Blog xoá không thành công.");
                }
            }
            return BadRequest("Invalid data.");
        }
    }
}

