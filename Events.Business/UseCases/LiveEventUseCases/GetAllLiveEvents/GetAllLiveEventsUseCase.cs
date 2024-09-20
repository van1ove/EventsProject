using AutoMapper;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.LiveEventDtos;
using Events.Domain.Entities;

namespace Events.Business.UseCases.LiveEventUseCases.GetAllLiveEvents;

public class GetAllLiveEventsUseCase : IGetAllLiveEventsUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public GetAllLiveEventsUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<LiveEventDto>> GetAllLiveEvents()
    {
        IEnumerable<LiveEvent> liveEvents = await _unitOfWork.LiveEventRepository.GetAllAsync();

        List<LiveEventDto> response = new List<LiveEventDto>();
        foreach (LiveEvent liveEvent in liveEvents)
        {
            response.Add(_mapper.Map<LiveEventDto>(liveEvent));
        }

        return response;
    }
}