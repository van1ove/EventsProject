using System.Linq.Expressions;
using AutoMapper;
using Events.Business.Utility;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.LiveEventDtos;
using Events.Domain.Entities;

namespace Events.Business.UseCases.LiveEventUseCases.GetLiveEvent;

public class GetLiveEventUseCase : IGetLiveEventUseCase
{
    private IMapper _mapper;
    private IUnitOfWork _unitOfWork;

    public GetLiveEventUseCase(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<LiveEventDto?> GetLiveEventById(Guid id)
    {
        LiveEvent? liveEvent = await _unitOfWork.LiveEventRepository.GetAsync(liveEvent => liveEvent.Id == id);
        return CheckLiveEventValue(liveEvent);
    }

    public async Task<LiveEventDto?> GetLiveEventByTitle(string title)
    {
        LiveEvent? liveEvent = await _unitOfWork.LiveEventRepository
            .GetAsync(liveEvent => liveEvent.Title.ToLower() == title.ToLower());
        return CheckLiveEventValue(liveEvent);
    }
    
    public async Task<IEnumerable<LiveEventDto>> GetLiveEventByPredicate(CriteriaDto dto)
    {
        Expression<Func<LiveEvent, bool>> predicate = dto.ToPredicate();

        IEnumerable<LiveEvent> entities =  await _unitOfWork.LiveEventRepository.GetByPredicateAsync(predicate);

        List<LiveEventDto> dtos = [];

        foreach (var entity in entities)
        {
            dtos.Add(_mapper.Map<LiveEventDto>(entity));
        }

        return dtos;
    }
    
    private LiveEventDto? CheckLiveEventValue(LiveEvent? liveEvent)
    {
        if (liveEvent is null)
            throw new NullReferenceException("Such live event was not found");

        LiveEventDto responce = _mapper.Map<LiveEventDto>(liveEvent);
        return responce;
    }
}