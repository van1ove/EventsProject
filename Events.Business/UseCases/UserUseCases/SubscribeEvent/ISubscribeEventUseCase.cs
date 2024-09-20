using Events.Domain.DTO.ParticipantDtos;

namespace Events.Business.UseCases.UserUseCases.SubscribeEvent
{
    public interface ISubscribeEventUseCase
    {
        Task<bool> SubscribeEvent(AddParticipantDto dto);
    }
}
