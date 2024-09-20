using AutoMapper;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.LiveEventDtos;
using Events.Domain.Entities;

namespace Events.Business.UseCases.LiveEventUseCases.DeleteLiveEvent;

public class DeleteLiveEventUseCase : IDeleteLiveEventUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public DeleteLiveEventUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<LiveEventDto?> DeleteLiveEventById(Guid id)
    {
        LiveEvent? liveEvent = await _unitOfWork.LiveEventRepository.DeleteAsync(liveEvent => liveEvent.Id == id);
        await _unitOfWork.SaveAsync();
        
        if (liveEvent is null)
            throw new NullReferenceException("Such live event was not found");

        LiveEventDto responce = _mapper.Map<LiveEventDto>(liveEvent);
        return responce;
    }
}