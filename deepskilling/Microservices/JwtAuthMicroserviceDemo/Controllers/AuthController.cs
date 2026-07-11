using JwtAuthMicroserviceDemo.Models;
using JwtAuthMicroserviceDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthMicroserviceDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserStore _userStore;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(IUserStore userStore, IJwtTokenService jwtTokenService)
        {
            _userStore = userStore;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _userStore.Validate(model.Username, model.Password);

            if (user is null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            var token = _jwtTokenService.GenerateToken(user.Username, user.Role);
            return Ok(new { Token = token });
        }
    }
}
