using Events.Business.UseCases.AuthUseCases.Login;
using Events.Business.UseCases.AuthUseCases.RefreshToken;
using Events.Business.UseCases.AuthUseCases.Register;
using Events.Domain.DTO.AuthDtos;
using Microsoft.AspNetCore.Mvc;

namespace Events_Web_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginUseCase _loginUseCase;
        private readonly IRegisterUseCase _registerUseCase;
        private readonly IRefreshTokenUseCase _refreshTokenUseCase;
        public AuthController(ILoginUseCase loginUseCase,
            IRegisterUseCase registerUseCase,
            IRefreshTokenUseCase refreshTokenUseCase)
        {
            _loginUseCase = loginUseCase;
            _registerUseCase = registerUseCase;
            _refreshTokenUseCase = refreshTokenUseCase;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            await _registerUseCase.Register(dto);
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginDto dto)
        {
            var result = await _loginUseCase.Login(dto);            
            return Ok(result);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] RefreshTokenDto dto)
        {
            var result = await _refreshTokenUseCase.RefreshToken(dto);
            return Ok(result);
        }
    }
}
