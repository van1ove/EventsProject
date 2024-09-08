using Events.Business.EnumUtility;
using Events.Business.Services.AuthService;
using Events.Business.Utility;
using Events.Domain.DTO.AuthDtos;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var result = await _authService.Register(dto);

            if (result.Item1 == OperationResult.Fail)
                return BadRequest();

            return Ok();    
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.Login(dto);

            if (result.Item1 == OperationResult.Fail)
                return Unauthorized();
            
            return Ok(result.Item2);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            var result = await _authService.RefreshToken(dto);

            if (result.Item1 == OperationResult.Fail)
                return Unauthorized();

            return Ok(result.Item2);
        }
    }
}
