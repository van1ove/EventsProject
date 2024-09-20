using AutoMapper;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.LiveEventDtos;
using Events.Domain.Entities;

namespace Events.Business.UseCases.LiveEventUseCases.UpdateLiveEvent;

public class UpdateLiveEventUseCase : IUpdateLiveEventUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public UpdateLiveEventUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<LiveEventDto?> UpdateLiveEventById(Guid id, UpdateLiveEventRequestDto dto)
    {
        LiveEvent? existingLiveEvent = await _unitOfWork.LiveEventRepository.GetAsync(x => x.Id == id);

        if (existingLiveEvent == null)
            throw new NullReferenceException("Live event was not found");

        _mapper.Map(dto, existingLiveEvent);
            
        await _unitOfWork.LiveEventRepository.UpdateAsync(existingLiveEvent, x => x.Id == existingLiveEvent.Id);
        await _unitOfWork.SaveAsync();
        
        if (existingLiveEvent is null)
            throw new NullReferenceException("Such live event was not found");

        LiveEventDto responce = _mapper.Map<LiveEventDto>(existingLiveEvent);
        return responce;
    }
}