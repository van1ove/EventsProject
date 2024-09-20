using Events.Domain.DTO.AuthDtos;

namespace Events.Business.UseCases.AuthUseCases.RefreshToken;

public interface IRefreshTokenUseCase
{
    public Task<AuthResponse?> RefreshToken(RefreshTokenDto dto);
}