using Events.Domain.DTO.LiveEventDtos;

namespace Events.Business.UseCases.LiveEventUseCases.GetAllLiveEvents;

public interface IGetAllLiveEventsUseCase
{
    public Task<IEnumerable<LiveEventDto>> GetAllLiveEvents();
}