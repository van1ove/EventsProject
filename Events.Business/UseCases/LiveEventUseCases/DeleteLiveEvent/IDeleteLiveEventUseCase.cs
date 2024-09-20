using Events.Domain.DTO.LiveEventDtos;

namespace Events.Business.UseCases.LiveEventUseCases.DeleteLiveEvent;

public interface IDeleteLiveEventUseCase
{
    public Task<LiveEventDto?> DeleteLiveEventById(Guid id);
}