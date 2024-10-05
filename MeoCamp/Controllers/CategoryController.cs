using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ModelBinding;

namespace MeoCamp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(IConfiguration configuration, ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _configuration = configuration;
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategory();
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

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.CreateCategory(model);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Category đã tồn tại.");
                }
            }
            return BadRequest("Invalid data.");
        }

        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateCategory(id, model);
                if (result != null)
                {
                    return Ok("Cập nhật thành công.");
                }
                else
                {
                    return BadRequest("Category chưa tồn tại.");
                }
            }
            return BadRequest("Invalid data.");
        }
    }
}
