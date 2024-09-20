using Events.Domain.DTO.LiveEventDtos;

namespace Events.Business.UseCases.LiveEventUseCases.GetLiveEvent;

public interface IGetLiveEventUseCase
{
    public Task<LiveEventDto?> GetLiveEventById(Guid id);

    public Task<LiveEventDto?> GetLiveEventByTitle(string title);

    public Task<IEnumerable<LiveEventDto>> GetLiveEventByPredicate(CriteriaDto dto);
}