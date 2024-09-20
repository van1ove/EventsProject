using Events.Domain.DTO.LiveEventDtos;

namespace Events.Business.UseCases.LiveEventUseCases.CreateLiveEvent;

public interface ICreateLiveEventUseCase
{
    public Task<LiveEventDto> CreateLiveEvent(CreateLiveEventRequestDto dto);
}