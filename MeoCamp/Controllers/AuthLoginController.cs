using Azure.Identity;
using MeoCamp.Controllers;
using MeoCamp.Repository.Models;
using MeoCamp.Service.BusinessModel;
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
    public class AuthLoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ILogger<AuthLoginController> _logger;

        public AuthLoginController(IConfiguration configuration, IUserService userService, ILogger<AuthLoginController> logger)
        {
            _configuration = configuration;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost(Name = "login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userService.Login(model);
            if (user != null)
            {
                // Login successful
                return Ok(user);
            }
            else
            {
                // Login failed
                return BadRequest("username hoặc password có thể sai");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.Register(model);
                if (result)
                {
                    // đăng kí thành công
                    return Ok("đăng kí thành công.");
                }
                else
                {
                    // đăng kí không thành công
                    return BadRequest("Username đã tồn tại.");
                }
            }
            return BadRequest("Invalid data.");
        }

        [HttpPost("changePassword/{username}")]
        public async Task<IActionResult> ChangePassword(string username, string PW, String confirmPW)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ChangePassword(username, PW, confirmPW);
                if (result)
                {
                    // cập nhật mk thành công
                    return Ok("Cập nhật mật khẩu thành công.");
                }
                else
                {
                    return BadRequest("Username không tồn tại.");
                }
            }
            return BadRequest("Invalid data.");
        }

        [HttpGet("listUser")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.ViewListUser();
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

        [HttpGet("findUser")]
        public async Task<IActionResult> GetUserbyFullname(string fullname)
        {
            var result = await _userService.ViewProfilebyfullname(fullname);
            if (result != null)
            {
                // Find successful
                return Ok(result);
            }
            else
            {
                // Find failed
                return BadRequest("fullname này không tồn tại hoặc sai.");
            }
        }
        [HttpPost("UpdateProfile/{username}")]
        public async Task<IActionResult> UpdateProfile(String username, ProfileModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateProfile(username, model);
                if (result)
                {
                    //cập nhật thành công
                    return Ok("Cập nhật profile thành công.");
                }
                else
                {
                    return BadRequest("Username không tồn tại.");
                }
            }
            return BadRequest("Invalid data.");
        }
    }
}
