using Events.Domain.DTO.AuthDtos;

namespace Events.Business.UseCases.AuthUseCases.Register;

public interface IRegisterUseCase
{
    public Task Register(RegisterRequestDto dto);
}