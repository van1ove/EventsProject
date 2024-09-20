using Events.Domain.DTO.LiveEventDtos;

namespace Events.Business.UseCases.LiveEventUseCases.UpdateLiveEvent;

public interface IUpdateLiveEventUseCase
{
    public Task<LiveEventDto?> UpdateLiveEventById(Guid id, UpdateLiveEventRequestDto dto);
}