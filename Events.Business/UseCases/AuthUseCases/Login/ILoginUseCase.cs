using Events.Domain.DTO.AuthDtos;

namespace Events.Business.UseCases.AuthUseCases.Login;

public interface ILoginUseCase
{
    public Task<AuthResponse?> Login(LoginDto dto);
}