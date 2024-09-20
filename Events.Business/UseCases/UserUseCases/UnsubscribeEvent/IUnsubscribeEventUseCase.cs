using Events.Domain.DTO.ParticipantDtos;

namespace Events.Business.UseCases.UserUseCases.UnsubscribeEvent
{
    public interface IUnsubscribeEventUseCase
    {
        Task<bool> UnsubscribeEvent(RemoveParticipantDto dto);
    }
}
