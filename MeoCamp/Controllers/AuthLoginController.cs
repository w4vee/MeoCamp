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
            var user = await _userService.Login(model.Username, model.Password);
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
                var result = await _userService.Register(model.Fullname, model.Username, model.Password, model.Email);
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
    }
}
