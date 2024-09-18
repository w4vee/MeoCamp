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
                return BadRequest("Invalid username or password");
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
                    return Ok("User registered successfully.");
                }
                else
                {
                    return BadRequest("Username already exists.");
                }
            }
            return BadRequest("Invalid data.");
        }
        [HttpPost("listuser")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.ViewListUser();
            if (result != null)
            {
                // Login successful
                return Ok(result);
            }
            else
            {
                // Login failed
                return BadRequest("List is empty");
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
                    return Ok("User updated successfully.");
                }
                else
                {
                    return BadRequest("Username not exists.");
                }
            }
            return BadRequest("Invalid data.");
        }
    }
}
