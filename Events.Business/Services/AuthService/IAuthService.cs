using Events.Business.EnumUtility;
using Events.Domain.DTO.AuthDtos;

namespace Events.Business.Services.AuthService
{
    public interface IAuthService
    {
        Task<(OperationResult, List<string>)> Register(RegisterRequestDto dto);
        Task<(OperationResult, AuthResponse?)> Login(LoginDto dto);
        Task<(OperationResult, AuthResponse?)> RefreshToken(RefreshTokenDto dto);
    }
}
