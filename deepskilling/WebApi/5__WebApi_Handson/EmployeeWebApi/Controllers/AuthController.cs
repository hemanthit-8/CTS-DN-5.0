using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeWebApi.Controllers
{
    /// <summary>
    /// Hands-On 5: issues JWTs that EmployeeController's [Authorize] attribute
    /// then validates. AllowAnonymous because you obviously can't already have
    /// a token to get a token.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        // GET: api/Auth/token?userId=1&userRole=Admin
        // Hands-On 5, Task 1: invoke GenerateJSONWebToken sending a user id and
        // a role (e.g. "Admin") to set claims used by [Authorize(Roles = ...)].
        [HttpGet("token")]
        public ActionResult<string> GetToken([FromQuery] int userId = 1, [FromQuery] string userRole = "Admin")
        {
            var token = GenerateJSONWebToken(userId, userRole);
            return Ok(new { token });
        }

        private string GenerateJSONWebToken(int userId, string userRole)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userRole),
                new Claim("UserId", userId.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                // Hands-On 5, Task 3: shortened to 2 minutes to demonstrate expiry.
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
