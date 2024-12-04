using FakeStoreApp.Models;
using FakeStoreApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FakeStoreApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthenticationController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            // Validate user credentials (in a real app, this would involve checking a database)
            if (user.Username == "test" && user.Password == "password")
            {
                var token = _tokenService.GenerateToken(user);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] string token)
        {
            var newToken = _tokenService.RefreshToken(token);
            return Ok(new { Token = newToken });
        }
    }
}
