using AutoMapper;
using Events.DataAccess.UnitOfWork;
using Events.Domain.DTO.LiveEventDtos;
using Events.Domain.Entities;

namespace Events.Business.UseCases.LiveEventUseCases.CreateLiveEvent;

public class CreateLiveEventUseCase : ICreateLiveEventUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateLiveEventUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LiveEventDto> CreateLiveEvent(CreateLiveEventRequestDto dto)
    {
        LiveEvent liveEvent = _mapper.Map<LiveEvent>(dto);
        await _unitOfWork.LiveEventRepository.AddAsync(liveEvent);
        await _unitOfWork.SaveAsync();
        LiveEventDto response = _mapper.Map<LiveEventDto>(liveEvent);
        return response;
    }
}