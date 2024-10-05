using MeoCamp.Service.BusinessModel;
using MeoCamp.Service.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MeoCamp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IRentalService _rentalService;
        private readonly ILogger<RentalController> _logger;

        public RentalController(IConfiguration configuration, IRentalService rentalService, ILogger<RentalController> logger)
        {
            _configuration = configuration;
            _rentalService = rentalService;
            _logger = logger;
        }

        [HttpGet("getAllRentals")]
        public async Task<IActionResult> GetAllRentalAsync()
        {
            var result = await _rentalService.GetAllRentalAsync();
            if (result != null && result.Any())
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

        [HttpGet("getRentalbyuserId/{userId}")]
        public async Task<IActionResult> GetRentalbyUserIdAsync(int userId)
        {
            if (ModelState.IsValid)
            {
                var result = await _rentalService.GetRentalbyUserIdAsync(userId);
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
        [HttpPost("CreateRental/{userId}")]
        public async Task<IActionResult> CreateRental(int userId, RentalModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _rentalService.CreateRental(userId, model);
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
        [HttpPut("UpdateRental/{userId}")]
        public async Task<IActionResult> UpdateRental(int userId, RentalModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _rentalService.UpdateRental(userId, model);

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
        [HttpDelete("DeleteRental/{userId}")]
        public async Task<IActionResult> DeleteRental(int userId)
        {
            if (ModelState.IsValid)
            {
                var result = await _rentalService.DeleteRental(userId);
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
