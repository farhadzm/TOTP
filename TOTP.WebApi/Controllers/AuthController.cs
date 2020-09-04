using Microsoft.AspNetCore.Mvc;
using TOTP.WebApi.Models;
using TOTP.WebApi.Services;

namespace TOTP.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public IActionResult Login(AuthRequestDto requestDto)
        {
            return Ok(_authService.AuthenticateUser(requestDto));
        }
    }
}
