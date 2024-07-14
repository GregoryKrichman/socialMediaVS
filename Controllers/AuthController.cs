using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using socialMedia.Dtos;
using socialMedia.Models;
using socialMedia.Repositories;
using socialMedia.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace socialMedia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IRepository<User> userRepository, IConfiguration configuration, IUserService userService, ILogger<AuthController> logger)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.LoginUserAsync(loginDto);

            if (result.Success)
            {
                return Ok(new { token = result.Token });
            }

            _logger.LogWarning("Login failed for user: {Username}", loginDto.Username);
            return BadRequest(new { errors = result.Errors });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.RegisterUserAsync(registerDto);

            if (result.Success)
            {
                return Ok(new { token = result.Token });
            }

            _logger.LogWarning("Registration failed for user: {Username}", registerDto.Username);
            return BadRequest(new { errors = result.Errors });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _logger.LogInformation("User logged out");
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
