using Events.Domain.DTO.LiveEventDtos;

namespace Events.Business.Services.LiveEventService
{
    public interface ILiveEventService
    {
        Task<LiveEventDto> CreateLiveEvent(CreateLiveEventRequestDto dto);

        Task<IEnumerable<LiveEventDto>> GetAllLiveEvents();

        Task<LiveEventDto?> GetLiveEventById(Guid id);

        Task<LiveEventDto?> GetLiveEventByTitle(string title);

        Task<LiveEventDto?> UpdateLiveEventById(Guid id, UpdateLiveEventRequestDto dto);

        Task<LiveEventDto?> DeleteLiveEventById(Guid id);

        Task<IEnumerable<LiveEventDto>> GetLiveEventByPredicate(CriteriaDto dto);
    }
}
